using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Classic_Snake
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string src = "https://iamanujsain.github.io/Snake-html5";

        public MainPage()
        {
            this.InitializeComponent();
            bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();
            if (isInternetConnected)
            {
                myWebView.Navigate(new System.Uri(src));
            }
            else
            {
                CancelCommandButton_Click(this);
            }
        }

        private async void CancelCommandButton_Click(object sender)
        {
            // Create the message dialog and set its content
            var messageDialog = new MessageDialog("No internet connection has been found.");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Try again",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand(
                "Close",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            // Display message showing the label of the command that was invoked
            //MainPage.NotifyUser("The '" + command.Label + "' command has been selected.",
            //    NotifyType.StatusMessage);
            if (command.Label == "Close")
            {
                CloseApp();
            }
            else if (command.Label == "Try again")
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    myWebView.Navigate(new System.Uri(src));
                } else
                {
                    CancelCommandButton_Click(this);
                }
            }
        }

        public void CloseApp()
        {
            Application.Current.Exit();
        }
    }
}
