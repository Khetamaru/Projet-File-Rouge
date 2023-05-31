using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    /// <summary>
    /// Notification Page View
    /// </summary>
    public class NotificationPageViewModel : ViewModelBase
    {
        private readonly List<string> stackPanelContent;
        private readonly string warningText;

        private User user;

        public NotificationPageViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            // set up command
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);

            // set up BDD objects
            user = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));

            // set up view objects
            stackPanelContent = new List<string>();
            warningText = string.Empty;

            if (user.UserLevel == User.AccessLevel.Admin)
            {
                RedWireNotifEditAdmin(user);
                CommandListNotifEdit();
                PurgeNotifAdmin();
                SaveDataAdmin();
            }
            else
            {
                RedWireNotifEdit(user);

                if ((int)user.UserLevel >= (int)User.AccessLevel.SuperUser)
                {
                    CommandListNotifEdit();
                }
            }
            MissingCallNotifEdit(user.Id);
            warningText = WarningTextEdit(user);
        }

        private void SaveDataAdmin()
        {
            DbSave dbSave = RequestCenter.GetDbSaveLast();
            if (dbSave == null || dbSave.date <= DateTime.Now.AddDays(-7))
            {
                if (dbSave != null) stackPanelContent.Add("\n" +
                                          "Date de la dernière sauvegarde : " + dbSave.date + "\n\n" +
                                          "Une sauvegarde de la base de données doit être effectuée." +
                                          "\n");

                else stackPanelContent.Add("\n" +
                                          "Aucune sauvegarde effectuée\n\n" +
                                          "Une sauvegarde de la base de données doit être effectuée." +
                                          "\n");
            }
        }

        /// <summary>
        /// Format red wires notifications
        /// </summary>
        /// <param name="user"></param>
        private void RedWireNotifEdit(User user)
        {
            List<RedWire> redWireList = RequestCenter.GetRedWireNotif(user.Id);
            foreach (RedWire redWire in redWireList)
            {
                if (redWire.ActualState == RedWire.State.transit)
                {
                    stackPanelContent.Add("\n" +
                                          "Utilisateur actuel : " + redWire.ActualRepairMan.Name + "\n" +
                                          "Utilisateur ciblé : " + redWire.TransfertTarget.Name + "\n" +
                                          "Nom du client : " + redWire.ClientName + "\n" +
                                          "Dernière action sur le dossier : " + redWire.LastUpdateFormated + "\n" +
                                          "Etat actuel : " + redWire.ActualState.ToString() + "\n\n" +
                                          "Un transit entre deux techniciens est en cours sur ce dossier." +
                                          "\n");
                }
                else if (redWire.RepairStartDate <= DateTime.Now.AddMonths(-6))
                {
                    stackPanelContent.Add("\n" +
                                          "Utilisateur : " + redWire.ActualRepairMan.Name + "\n" +
                                          "Nom du client : " + redWire.ClientName + "\n" +
                                          "Dernière action sur le dossier : " + redWire.LastUpdateFormated + "\n" +
                                          "Nombre de jours depuis prise en charge : " + Math.Round((DateTime.Now - redWire.RepairStartDate).TotalDays, 1) + "\n" +
                                          "Etat actuel : " + redWire.ActualState.ToString() + "\n\n" +
                                          "Le dossier est présent depuis 6 mois. Voir si il doit aller en destruction." +
                                          "\n");
                }
                else if (redWire.ActualState == RedWire.State.attente_fournisseur)
                {
                    stackPanelContent.Add("\n" +
                                          "Utilisateur : " + redWire.ActualRepairMan.Name + "\n" +
                                          "Nom du client : " + redWire.ClientName + "\n" +
                                          "Dernière action sur le dossier : " + redWire.LastUpdateFormated + "\n" +
                                          "Nombre de jours depuis la dernière action : " + Math.Round((DateTime.Now - redWire.LastUpdate).TotalDays, 1) + "\n" +
                                          "Etat actuel : " + redWire.ActualState.ToString() + "\n\n" +
                                          "Le fournisseur a été contacté depuis plus de 2 jours sans réaction du réparateur." +
                                          "\n");
                }
                else
                {
                    stackPanelContent.Add("\n" +
                                          "Utilisateur : " + redWire.ActualRepairMan.Name + "\n" +
                                          "Nom du client : " + redWire.ClientName + "\n" +
                                          "Dernière action sur le dossier : " + redWire.LastUpdateFormated + "\n" +
                                          "Nombre de jours sans action : " + Math.Round((DateTime.Now - redWire.LastUpdate).TotalDays, 1) + "\n" +
                                          "Etat actuel : " + redWire.ActualState.ToString() + "\n\n" +
                                          "Le dossier doit être mis à jour ou cloturé par le réparateur." +
                                          "\n");
                }
            }

            List<RedWire> redWirePrividerWaitingList = RequestCenter.GetPrividerWaitingNotif(user.Id);
            foreach (RedWire redWire in redWirePrividerWaitingList)
            {
                stackPanelContent.Add("\n" +
                                          "Utilisateur : " + redWire.ActualRepairMan.Name + "\n" +
                                          "Nom du client : " + redWire.ClientName + "\n" +
                                          "Dernière action sur le dossier : " + redWire.LastUpdateFormated + "\n" +
                                          "Nombre de jours sans action : " + Math.Round((DateTime.Now - redWire.LastUpdate).TotalDays, 1) + "\n" +
                                          "Etat actuel : " + redWire.ActualState.ToString() + "\n\n" +
                                          "Le fournisseur n'a toujours pas répondu à la demande tech.\n" +
                                          "Il doit être relancé ou le dossier doit être mis à jour." +
                                          "\n");
            }
        }

        /// <summary>
        /// Format commands notifications
        /// </summary>
        public void CommandListNotifEdit()
        {
            List<CommandList> commandList = RequestCenter.GetCommandNotif();
            foreach (CommandList command in commandList)
            {
                if (command.State == CommandList.CommandStatusEnum.livraison_en_cours)
                {
                    stackPanelContent.Add("\n" +
                                          "Commande : " + command.Name + "\n" +
                                          "URL de commande : " + command.Url + "\n" +
                                          "Date de livraison prévue : " + command.DeliveryDate + "\n" +
                                          "Utilisateur : " + command.RedWire.ActualRepairMan.Name + "\n\n" +
                                          "Date de livraison dépassée. Veuillez mettre à jour." +
                                          "\n");
                }
                else if (command.State == CommandList.CommandStatusEnum.commande_en_attente)
                {
                    stackPanelContent.Add("\n" +
                                          "Commande : " + command.Name + "\n" +
                                          "URL de commande : " + command.Url + "\n" +
                                          "Utilisateur : " + command.RedWire.ActualRepairMan.Name + "\n\n" +
                                          "Commande à faire." +
                                          "\n");
                }
            }
        }

        /// <summary>
        /// Format commands notifications
        /// </summary>
        public void MissingCallNotifEdit(int userId)
        {
            List<MissingCall> missingCalls = RequestCenter.GetMissingCallUnread(userId);
            foreach (MissingCall missingCall in missingCalls)
            {
                stackPanelContent.Add("\n" +
                                          "Main courante en attente de lecture. \n" +
                                          "Date : " + missingCall.Date + "\n" +
                                          "Technicien témoin : " + missingCall.Author.Name + "\n" +
                                          "Auteur du message : " + missingCall.Caller + "\n" +
                                          "\n");
            }
        }

        /// <summary>
        /// Format admin notifications
        /// </summary>
        private void RedWireNotifEditAdmin(User user)
        {
            List<RedWire> redWireList = RequestCenter.GetRedWireNotifAdmin();
            foreach (RedWire redWire in redWireList)
            {
                if (redWire.ActualState == RedWire.State.transit)
                {
                    stackPanelContent.Add("\n" +
                                          "Utilisateur actuel : " + redWire.ActualRepairMan.Name + "\n" +
                                          "Utilisateur ciblé : " + redWire.TransfertTarget.Name + "\n" +
                                          "Nom du client : " + redWire.ClientName + "\n" +
                                          "Dernière action sur le dossier : " + redWire.LastUpdateFormated + "\n" +
                                          "Etat actuel : " + redWire.ActualState.ToString() + "\n\n" +
                                          "Un transit entre deux techniciens est en cours sur ce dossier." +
                                          "\n");
                }
                else if (redWire.RepairStartDate <= DateTime.Now.AddMonths(-6))
                {
                    stackPanelContent.Add("\n" +
                                          "Utilisateur : " + redWire.ActualRepairMan.Name + "\n" +
                                          "Nom du client : " + redWire.ClientName + "\n" +
                                          "Dernière action sur le dossier : " + redWire.LastUpdateFormated + "\n" +
                                          "Nombre de jours depuis prise en charge : " + Math.Round((DateTime.Now - redWire.RepairStartDate).TotalDays, 1) + "\n" +
                                          "Etat actuel : " + redWire.ActualState.ToString() + "\n\n" +
                                          "Le dossier est présent depuis 6 mois. Voir si il doit aller en destruction." +
                                          "\n");
                }
                else if (redWire.ActualState == RedWire.State.attente_fournisseur)
                {
                    if (redWire.ActualRepairMan.Id == user.Id)
                    {
                        stackPanelContent.Add("\n" +
                                          "Utilisateur : " + redWire.ActualRepairMan.Name + "\n" +
                                          "Nom du client : " + redWire.ClientName + "\n" +
                                          "Dernière action sur le dossier : " + redWire.LastUpdateFormated + "\n" +
                                          "Nombre de jours depuis la dernière action : " + Math.Round((DateTime.Now - redWire.LastUpdate).TotalDays, 1) + "\n" +
                                          "Etat actuel : " + redWire.ActualState.ToString() + "\n\n" +
                                          "Le fournisseur a été contacté depuis plus de 2 jours sans réaction du réparateur." +
                                          "\n");
                    }
                    else
                    {
                        stackPanelContent.Add("\n" +
                                              "Utilisateur : " + redWire.ActualRepairMan.Name + "\n" +
                                              "Nom du client : " + redWire.ClientName + "\n" +
                                              "Dernière action sur le dossier : " + redWire.LastUpdateFormated + "\n" +
                                              "Nombre de jours depuis la dernière action : " + Math.Round((DateTime.Now - redWire.LastUpdate).TotalDays, 1) + "\n" +
                                              "Etat actuel : " + redWire.ActualState.ToString() + "\n\n" +
                                              "Le fournisseur a été contacté depuis plus de 5 jours sans réponse." +
                                              "\n");
                    }
                }
                else if (redWire.ActualState == RedWire.State.payement_différé)
                {
                    stackPanelContent.Add("\n" +
                                          "Utilisateur : " + redWire.ActualRepairMan.Name + "\n" +
                                          "Nom du client : " + redWire.ClientName + "\n" +
                                          "Dernière action sur le dossier : " + redWire.LastUpdateFormated + "\n" +
                                          "Nombre de jours sans action : " + Math.Round((DateTime.Now - redWire.LastUpdate).TotalDays, 1) + "\n" +
                                          "Etat actuel : " + redWire.ActualState.ToString() + "\n\n" +
                                          "Payement différé en attente de règlement." +
                                          "\n");
                }
                else
                {
                    stackPanelContent.Add("\n" +
                                          "Utilisateur : " + redWire.ActualRepairMan.Name + "\n" +
                                          "Nom du client : " + redWire.ClientName + "\n" +
                                          "Dernière action sur le dossier : " + redWire.LastUpdateFormated + "\n" +
                                          "Nombre de jours sans action : " + Math.Round((DateTime.Now - redWire.LastUpdate).TotalDays, 1) + "\n" +
                                          "Etat actuel : " + redWire.ActualState.ToString() + "\n\n" +
                                          "Le dossier doit être mis à jour ou cloturé." +
                                          "\n");
                }
            }

            List<RedWire> redWirePrividerWaitingList = RequestCenter.GetPrividerWaitingNotifAdmin();
            foreach (RedWire redWire in redWirePrividerWaitingList)
            {
                stackPanelContent.Add("\n" +
                                          "Utilisateur : " + redWire.ActualRepairMan.Name + "\n" +
                                          "Nom du client : " + redWire.ClientName + "\n" +
                                          "Dernière action sur le dossier : " + redWire.LastUpdateFormated + "\n" +
                                          "Nombre de jours sans action : " + Math.Round((DateTime.Now - redWire.LastUpdate).TotalDays, 1) + "\n" +
                                          "Etat actuel : " + redWire.ActualState.ToString() + "\n\n" +
                                          "Le fournisseur n'a toujours pas répondu à la demande tech. Il doit être relancé ou le dossier doit être mis à jour." +
                                          "\n");
            }
        }

        /// <summary>
        /// Format purge notifications
        /// </summary>
        public void PurgeNotifAdmin()
        {
            int count = RequestCenter.GetRedWirePurgeNumber();
            if (count > 0)
            {
                stackPanelContent.Add("\n" +
                                      "Purge Dossier à faire dès que possible.\n" +
                                      "Nombre de dossiers à purger : " + count + "\n" +
                                      "\n");
            }
        }

        /// <summary>
        /// set up Vincent to LMAO
        /// </summary>
        /// <param name="user"></param>
        private string WarningTextEdit(User user)
        {
            switch (StackPanelContent.Count)
            {
                case <= 1:
                    return "Tout va bien.";
                case <= 2:
                    return "Bon aller, faut s'y mettre " + user.Name + ".";
                case < 5:
                    return "Erreur 500 : l'utilisateur met du temps à se mettre au boulot.";
                case < 10:
                    return "Erreur 404 : méthodologie de travail non trouvée.";
                case >= 10:
                    return "Nouveau record atteint ! Si tu es un utilisateur tu es stupide, si tu es un administrateur il est temps de sortir le lance-flamme !";
            }
        }

        public List<string> StackPanelContent => stackPanelContent;
        public string WarningText => warningText;

        /// <summary>
        /// Commands
        /// </summary>
        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
    }
}