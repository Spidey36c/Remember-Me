﻿#pragma checksum "..\..\Settigns.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C4DFD68AC9F6C2E09E311F0EDFACE7DB4F9A2F9C8E254F9655A44D654D9649C1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Remember_Me;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace Remember_Me {
    
    
    /// <summary>
    /// Settigns
    /// </summary>
    public partial class Settigns : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\Settigns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ImportFolder;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Settigns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ImportBrowse;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Settigns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ExportFolder;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Settigns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExportBrowse;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Settigns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Close;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Settigns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Test;
        
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
            System.Uri resourceLocater = new System.Uri("/Remember Me;component/settigns.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Settigns.xaml"
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
            this.ImportFolder = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.ImportBrowse = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\Settigns.xaml"
            this.ImportBrowse.Click += new System.Windows.RoutedEventHandler(this.ImportBrowse_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ExportFolder = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ExportBrowse = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\Settigns.xaml"
            this.ExportBrowse.Click += new System.Windows.RoutedEventHandler(this.ExportBrowse_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Close = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\Settigns.xaml"
            this.Close.Click += new System.Windows.RoutedEventHandler(this.Close_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Test = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\Settigns.xaml"
            this.Test.Click += new System.Windows.RoutedEventHandler(this.Test_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

