﻿#pragma checksum "D:\Daniel Shvartz\Final Project\Space_Invaders\Final Project\Pages\GamePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F06400906400D34B2C8B361612E0F4BF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Final_Project.Pages
{
    partial class GamePage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // Pages\GamePage.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                }
                break;
            case 2: // Pages\GamePage.xaml line 13
                {
                    this.canvas = (global::Windows.UI.Xaml.Controls.Canvas)(target);
                }
                break;
            case 3: // Pages\GamePage.xaml line 14
                {
                    this.shield_1 = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 4: // Pages\GamePage.xaml line 15
                {
                    this.shield_2 = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 5: // Pages\GamePage.xaml line 16
                {
                    this.shield_3 = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 6: // Pages\GamePage.xaml line 17
                {
                    this.shield_hp_3 = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 7: // Pages\GamePage.xaml line 18
                {
                    this.shield_hp_1 = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 8: // Pages\GamePage.xaml line 19
                {
                    this.shield_hp_2 = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

