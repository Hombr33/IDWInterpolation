﻿#pragma checksum "..\..\HyperParams.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "21C83456BE02BAC18433FD44124AE1FD6BF43B34"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using IDWInterpolation;
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


namespace IDWInterpolation {
    
    
    /// <summary>
    /// HyperParams
    /// </summary>
    public partial class HyperParams : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\HyperParams.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox uiDensity;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\HyperParams.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox uiDivisions;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\HyperParams.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox uiEquidistance;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\HyperParams.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox uiRadius;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\HyperParams.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button uiStart;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\HyperParams.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button uiExport;
        
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
            System.Uri resourceLocater = new System.Uri("/IDWInterpolation;component/hyperparams.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\HyperParams.xaml"
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
            this.uiDensity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.uiDivisions = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.uiEquidistance = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.uiRadius = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.uiStart = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\HyperParams.xaml"
            this.uiStart.Click += new System.Windows.RoutedEventHandler(this.uiStart_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.uiExport = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\HyperParams.xaml"
            this.uiExport.Click += new System.Windows.RoutedEventHandler(this.uiExport_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

