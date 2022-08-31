using FreshMvvm;
using Mde.Project.Mobile.Domain.Interfaces;
using Mde.Project.Mobile.Domain.Services;
using MediaManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mde.Project.Mobile.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {        
        public ICommand OpenPartyConfigurationPage { get; set; }
        public ICommand OpenCatApiPage { get; set; } 

        public MainViewModel()
        {           

            OpenPartyConfigurationPage = new Command(
            async () =>
            {
                //if (Device.RuntimePlatform == Device.Android)
                //{
                //    _songSelectorService.InitMediaPlayer();
                //}
                await CoreMethods.PushPageModel<PartyConfigurationViewModel>();
            });

            OpenCatApiPage = new Command(
            async () =>
            {
                await CoreMethods.PushPageModel<CatApiViewModel>();
            });

        }     
    }
}
