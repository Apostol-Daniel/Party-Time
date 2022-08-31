using MediaManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mde.Project.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartyActionPage : ContentPage
    {
        public PartyActionPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        //public void InitPlay() 
        //{
        //    if (!CrossMediaManager.Current.IsPlaying()) 
        //    {
        //        do
        //        {
        //            Task.Delay(500);
        //        }
        //        while(!CrossMediaManager.Current.IsPlaying());
        //    }
        //}
    }
}