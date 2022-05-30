using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Views
{
    public partial class RedWireView : UserControl
    {
        public RedWireView()
        {
            InitializeComponent();
        }

        private void InsertTextEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (DataContext as RedWireViewModel).InsertText();
            }
        }

        private void InsertText(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).InsertText(); }

        private void OpenPriseEnChargePopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenPriseEnChargePopUp(); }
        private void PriseEnChargeYesButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).PriseEnChargeYesButton(); }
        private void PriseEnChargeNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).PriseEnChargeNoButton(); }

        private void OpenPECDevisPopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenPECDevisPopUp(); }
        private void PECDevisYesButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).PECDevisYesButton(); }
        private void PECDevisNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).PECDevisNoButton(); }
        private void PECDevisSkipButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).PECDevisSkipButton(); }

        private void OpenPECClientResponsePopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenPECClientResponsePopUp(); }
        private void PECClientResponseYesButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).PECClientResponseYesButton(); }
        private void PECClientResponseNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).PECClientResponseNoButton(); }

        private void OpenProblemeQuestionPopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenProblemeQuestionPopUp(); }
        private void ProblemeQuestionYesButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).ProblemeQuestionYesButton(); }
        private void ProblemeQuestionNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).ProblemeQuestionNoButton(); }

        private void OpenReponseClientPopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenReponseClientPopUp(); }
        private void ReponseClientYesButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).ReponseClientYesButton(); }
        private void ReponseClientNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).ReponseClientNoButton(); }

        private void OpenTransfertTechPopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenTransfertTechPopUp(); }
        private void TransfertTechYesButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).TransfertTechYesButton(); }
        private void TransfertTechNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).TransfertTechNoButton(); }

        private void OpenPriseEnChargeTransfertPopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenPriseEnChargeTransfertPopUp(); }
        private void PriseEnChargeTransfertYesButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).PriseEnChargeTransfertYesButton(); }
        private void PriseEnChargeTransfertNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).PriseEnChargeTransfertNoButton(); }

        private void OpenFinPopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenFinPopUp(); }
        private void FinYesButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).FinYesButton(); }
        private void FinNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).FinNoButton(); }
        private void FinSkipButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).FinSkipButton(); }

        private void OpenFinAppelPopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenFinAppelPopUp(); }
        private void FinAppelYesPhoneButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).FinAppelYesPhoneButton(); }
        private void FinAppelYesMailButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).FinAppelYesMailButton(); }
        private void FinAppelNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).FinAppelNoButton(); }
        private void FinAppelSkipButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).FinAppelSkipButton(); }

        private void OpenFinPayementPopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenFinPayementPopUp(); }
        private void FinPayementYesButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).FinPayementYesButton(); }
        private void FinPayementNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).FinPayementNoButton(); }

        private void OpenNonReparablePopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenNonReparablePopUp(); }
        private void NonReparableYesButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).NonReparableYesButton(); }
        private void NonReparableNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).NonReparableNoButton(); }

        private void OpenNonReparableAppelPopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenNonReparableAppelPopUp(); }
        private void NonReparableAppelYesPhoneButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).NonReparableAppelYesPhoneButton(); }
        private void NonReparableAppelYesMailButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).NonReparableAppelYesMailButton(); }
        private void NonReparableAppelNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).NonReparableAppelNoButton(); }
        private void NonReparableAppelSkipButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).NonReparableAppelSkipButton(); }

        private void OpenNonReparableRenduPopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenNonReparableRenduPopUp(); }
        private void NonReparableRenduYesButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).NonReparableRenduYesButton(); }
        private void NonReparableRenduNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).NonReparableRenduNoButton(); }

        private void OpenCommandePiecePopUp(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).OpenCommandePiecePopUp(); }
        private void CommandePieceYesButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).CommandePieceYesButton(); }
        private void CommandePieceNoButton(object sender, RoutedEventArgs e) { (DataContext as RedWireViewModel).CommandePieceNoButton(); }
    }
}
