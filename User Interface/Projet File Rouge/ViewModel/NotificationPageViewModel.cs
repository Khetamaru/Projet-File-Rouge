using System;
using System.Collections.Generic;
using Projet_File_Rouge.Commands;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.Stores;
using Projet_File_Rouge.Tools;

namespace Projet_File_Rouge.ViewModel
{
    public class NotificationPageViewModel : ViewModelBase
    {
        private readonly List<string> stackPanelContent;
        private readonly string warningText;

        public NotificationPageViewModel(NavigationStore navigationStore, CacheStore cacheStore)
        {
            NavigateMainMenuCommand = new NavigateMainMenuCommand(navigationStore, cacheStore);

            stackPanelContent = new List<string>();
            warningText = string.Empty;

            User user = RequestCenter.GetUser(cacheStore.GetObjectCache(ObjectCacheStoreEnum.ActualUser));

            List<RedWire> redWireList = RequestCenter.GetRedWireNotif(user.Id);
            foreach(RedWire redWire in redWireList)
            {
                if (redWire.RepairStartDate <= DateTime.Now.AddMonths(-6))
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

            if ((int)user.UserLevel >= (int)User.AccessLevel.SuperUser)
            {
                List<CommandList> commandList = RequestCenter.GetCommandNotif();
                foreach(CommandList command in commandList)
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

            switch (StackPanelContent.Count)
            {
                case <= 1:
                    warningText = "Tout va bien.";
                    break;
                case <= 2:
                    warningText = "Bon aller, faut s'y mettre " + user.Name + ".";
                    break;
                case < 5:
                    warningText = "Erreur 500 : l'utilisateur met du temps à se mettre au boulot.";
                    break;
                case < 10:
                    warningText = "Erreur 404 : méthodologie de travail non trouvée.";
                    break;
                case >= 10:
                    warningText = "Nouveau record atteint ! Si tu es un utilisateur tu es stupide, si tu es un administrateur il est temps de sortir le lance-flamme !";
                    break;
            }
        }

        public List<string> StackPanelContent => stackPanelContent;
        public string WarningText => warningText;

        public NavigateMainMenuCommand NavigateMainMenuCommand { get; }
    }
}