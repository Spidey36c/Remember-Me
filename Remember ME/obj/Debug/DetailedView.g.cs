﻿#pragma checksum "..\..\DetailedView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D32BAEF21056C9C52712049AAFCA034EE79D418936E4E98B372A74E2963D2906"
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
    /// DetailedView
    /// </summary>
    public partial class DetailedView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\DetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image EntryImg;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\DetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image TempImg;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\DetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EntryName;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\DetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EntryGroup;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\DetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EntryDesc;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\DetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement Video;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\DetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PauseVideo;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\DetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PlayVideo;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\DetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditButton;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\DetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Exit;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\DetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Export;
        
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
            System.Uri resourceLocater = new System.Uri("/Remember Me;component/detailedview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DetailedView.xaml"
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
            this.EntryImg = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.TempImg = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.EntryName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.EntryGroup = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.EntryDesc = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.Video = ((System.Windows.Controls.MediaElement)(target));
            return;
            case 7:
            this.PauseVideo = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\DetailedView.xaml"
            this.PauseVideo.Click += new System.Windows.RoutedEventHandler(this.PauseVideo_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.PlayVideo = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\DetailedView.xaml"
            this.PlayVideo.Click += new System.Windows.RoutedEventHandler(this.PlayVideo_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.EditButton = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\DetailedView.xaml"
            this.EditButton.Click += new System.Windows.RoutedEventHandler(this.Edit_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.Exit = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\DetailedView.xaml"
            this.Exit.Click += new System.Windows.RoutedEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.Export = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\DetailedView.xaml"
            this.Export.Click += new System.Windows.RoutedEventHandler(this.Export_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

