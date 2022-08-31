
using Mde.Project.Mobile.Domain.Constants;
using Mde.Project.Mobile.ViewModels;
using MediaManager;
using MediaManager.Library;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mde.Project.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartyConfigurationPage : ContentPage
    {
        public IMediaItem selectedSong;
        public PartyConfigurationPage()
        {
            InitializeComponent();
            
            FirstColor.Source = ImageSource.FromFile("placeholder.png");
            FirstColor.BackgroundColor = Color.Transparent;
            SecondColor.BackgroundColor = Color.Transparent;
            ThirdColor.BackgroundColor = Color.Transparent;
        }

        // Not in use, on standby
        //private async void Color_Clicked(object sender, EventArgs e)
        //{
        //    var modalPage = new ColorSelectorPage();
        //    await Navigation.PushModalAsync(modalPage, true);

        //}

        protected override void OnAppearing() 
        {           

            if (Device.RuntimePlatform == Device.UWP) 
            {
                chkVibrations.IsEnabled = false;
                chkFlash.IsEnabled = false;
                lblFlash.Text = "Flash not available on this device.";
                lblVibrations.Text = "Vibrations not available  on this device.";
            }

            //lblColorFeedback.Text = "Click on the squares to select your colors.";
            //lblColorFeedback.TextColor = Color.White;
            //lblSelectedSong.TextColor = Color.White;

            if (FirstColor.BackgroundColor != Color.Transparent && SecondColor.BackgroundColor != Color.Transparent && ThirdColor.BackgroundColor != Color.Transparent)
            {
                lblColorFeedback.Text = "Colors ready for party!";
                lblColorFeedback.TextColor = Color.ForestGreen;
            }           
        }              
    }
}