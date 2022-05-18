﻿#pragma checksum "..\..\..\..\Views\RedWireView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B716A140113B1A348B4122A6D22733906612C5A1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Projet_File_Rouge.ViewModel;
using Projet_File_Rouge.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Projet_File_Rouge.Views {
    
    
    /// <summary>
    /// RedWireView
    /// </summary>
    public partial class RedWireView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\Views\RedWireView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TopGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Projet File Rouge;component/views/redwireview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\RedWireView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TopGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 45 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.TextBox)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.InsertTextEnterKey);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 46 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.InsertText);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 86 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenPriseEnChargePopUp);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 108 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PriseEnChargeNoButton);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 119 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PriseEnChargeYesButton);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 120 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PriseEnChargeNoButton);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 140 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenProblemeQuestionPopUp);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 162 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ProblemeQuestionNoButton);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 172 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ProblemeQuestionYesButton);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 173 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ProblemeQuestionNoButton);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 179 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenReponseClientPopUp);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 202 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ReponseClientNoButton);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 214 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ReponseClientYesButton);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 215 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ReponseClientNoButton);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 221 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenTransfertTechPopUp);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 244 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.TransfertTechNoButton);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 256 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.TransfertTechYesButton);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 257 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.TransfertTechNoButton);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 263 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenPriseEnChargeTransfertPopUp);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 285 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PriseEnChargeTransfertNoButton);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 296 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PriseEnChargeTransfertYesButton);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 297 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PriseEnChargeTransfertNoButton);
            
            #line default
            #line hidden
            return;
            case 24:
            
            #line 303 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenFinPopUp);
            
            #line default
            #line hidden
            return;
            case 25:
            
            #line 326 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FinNoButton);
            
            #line default
            #line hidden
            return;
            case 26:
            
            #line 347 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FinYesButton);
            
            #line default
            #line hidden
            return;
            case 27:
            
            #line 348 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FinNoButton);
            
            #line default
            #line hidden
            return;
            case 28:
            
            #line 349 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FinSkipButton);
            
            #line default
            #line hidden
            return;
            case 29:
            
            #line 355 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenNonReparablePopUp);
            
            #line default
            #line hidden
            return;
            case 30:
            
            #line 378 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NonReparableNoButton);
            
            #line default
            #line hidden
            return;
            case 31:
            
            #line 398 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NonReparableYesButton);
            
            #line default
            #line hidden
            return;
            case 32:
            
            #line 399 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NonReparableNoButton);
            
            #line default
            #line hidden
            return;
            case 33:
            
            #line 405 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenPECDevisPopUp);
            
            #line default
            #line hidden
            return;
            case 34:
            
            #line 428 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PECDevisNoButton);
            
            #line default
            #line hidden
            return;
            case 35:
            
            #line 449 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PECDevisYesButton);
            
            #line default
            #line hidden
            return;
            case 36:
            
            #line 450 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PECDevisNoButton);
            
            #line default
            #line hidden
            return;
            case 37:
            
            #line 451 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PECDevisSkipButton);
            
            #line default
            #line hidden
            return;
            case 38:
            
            #line 457 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenPECClientResponsePopUp);
            
            #line default
            #line hidden
            return;
            case 39:
            
            #line 479 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PECClientResponseNoButton);
            
            #line default
            #line hidden
            return;
            case 40:
            
            #line 488 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PECClientResponseYesButton);
            
            #line default
            #line hidden
            return;
            case 41:
            
            #line 489 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PECClientResponseNoButton);
            
            #line default
            #line hidden
            return;
            case 42:
            
            #line 495 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenFinAppelPopUp);
            
            #line default
            #line hidden
            return;
            case 43:
            
            #line 518 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FinAppelNoButton);
            
            #line default
            #line hidden
            return;
            case 44:
            
            #line 540 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FinAppelYesPhoneButton);
            
            #line default
            #line hidden
            return;
            case 45:
            
            #line 541 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FinAppelYesMailButton);
            
            #line default
            #line hidden
            return;
            case 46:
            
            #line 542 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FinAppelSkipButton);
            
            #line default
            #line hidden
            return;
            case 47:
            
            #line 543 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FinAppelNoButton);
            
            #line default
            #line hidden
            return;
            case 48:
            
            #line 549 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenFinPayementPopUp);
            
            #line default
            #line hidden
            return;
            case 49:
            
            #line 571 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FinPayementNoButton);
            
            #line default
            #line hidden
            return;
            case 50:
            
            #line 581 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FinPayementYesButton);
            
            #line default
            #line hidden
            return;
            case 51:
            
            #line 582 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FinPayementNoButton);
            
            #line default
            #line hidden
            return;
            case 52:
            
            #line 588 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenNonReparableAppelPopUp);
            
            #line default
            #line hidden
            return;
            case 53:
            
            #line 611 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NonReparableAppelNoButton);
            
            #line default
            #line hidden
            return;
            case 54:
            
            #line 633 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NonReparableAppelYesPhoneButton);
            
            #line default
            #line hidden
            return;
            case 55:
            
            #line 634 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NonReparableAppelYesMailButton);
            
            #line default
            #line hidden
            return;
            case 56:
            
            #line 635 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NonReparableAppelSkipButton);
            
            #line default
            #line hidden
            return;
            case 57:
            
            #line 636 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NonReparableAppelNoButton);
            
            #line default
            #line hidden
            return;
            case 58:
            
            #line 642 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenNonReparableRenduPopUp);
            
            #line default
            #line hidden
            return;
            case 59:
            
            #line 664 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NonReparableRenduNoButton);
            
            #line default
            #line hidden
            return;
            case 60:
            
            #line 674 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NonReparableRenduYesButton);
            
            #line default
            #line hidden
            return;
            case 61:
            
            #line 675 "..\..\..\..\Views\RedWireView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NonReparableRenduNoButton);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

