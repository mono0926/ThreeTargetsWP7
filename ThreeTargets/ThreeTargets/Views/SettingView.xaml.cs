using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;

namespace ThreeTargets.Views
{
    /// <summary>
    /// Description for SettingView.
    /// </summary>
    public partial class SettingView : PhoneApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the SettingView class.
        /// </summary>
        public SettingView()
        {
            InitializeComponent();

            Messenger.Default.Register<string>(this, Contract.SettingToHome,
                s =>
                {
                    //NavigationService.GoBack();
                });

            Messenger.Default.Register<string>(this, Contract.SettingToHome,
                s =>
                {
                    ConfirmPopup.IsOpen = false;
                });
        }

        private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            Messenger.Default.Send("", Contract.SaveSetting);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ConfirmPopup.IsOpen = false;
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            ConfirmPopup.IsOpen = true;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (ConfirmPopup.IsOpen)
            {
                e.Cancel = true;
                ConfirmPopup.IsOpen = false;
            }
            else
            {
                base.OnBackKeyPress(e);
            }
        }
    }
}