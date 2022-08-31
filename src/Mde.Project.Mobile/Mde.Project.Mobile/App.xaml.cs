using FreshMvvm;
using Mde.Project.Mobile.Domain.Interfaces;
using Mde.Project.Mobile.Domain.Services;
using Mde.Project.Mobile.ViewModels;
using System;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mde.Project.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            FreshIOC.Container.Register<ISongSelectorService>(new SongSelectorService());
            FreshIOC.Container.Register<IPartyActionService>(new PartyActionService());
            FreshIOC.Container.Register<IFlashLightService>(new FlashlightService());
            FreshIOC.Container.Register<IVibrationService>(new VibrationService());
            //FreshIOC.Container.Register<IStatusBarPlatformSpecific>(new StatusbarService());
            

            MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainViewModel>());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
