﻿#pragma checksum "..\..\..\RibbonTabItems\RTIEmployeesView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8C8763C7E34A3FF69C40B1EE1C12A5A2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Fluent;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace DarkStorm.Desktop.Modules.TimeCard.UI.RibbonTabItems {
    
    
    /// <summary>
    /// RTIEmployeesView
    /// </summary>
    public partial class RTIEmployeesView : Fluent.RibbonTabItem, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\RibbonTabItems\RTIEmployeesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.Button NewButton;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\RibbonTabItems\RTIEmployeesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.Button EditButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\RibbonTabItems\RTIEmployeesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Fluent.Button DeleteButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DarkStorm.Desktop.Modules.TimeCard.UI;component/ribbontabitems/rtiemployeesview." +
                    "xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\RibbonTabItems\RTIEmployeesView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NewButton = ((Fluent.Button)(target));
            return;
            case 2:
            this.EditButton = ((Fluent.Button)(target));
            return;
            case 3:
            this.DeleteButton = ((Fluent.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

