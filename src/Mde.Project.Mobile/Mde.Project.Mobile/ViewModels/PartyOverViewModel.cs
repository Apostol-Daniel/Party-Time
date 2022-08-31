using FreshMvvm;
using Mde.Project.Mobile.Domain.Services;
using MediaManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mde.Project.Mobile.ViewModels
{
    public class PartyOverViewModel : FreshBasePageModel
    {       
        public ICommand PartyReset { get; set; }
        public ICommand OpenCatApiPage { get; set; }
        public ICommand OpenHomeScreen { get; set; }
        public PartyOverViewModel()
        {           
            PartyReset = new Command(
                async () =>
                {
                    await CoreMethods.PushPageModel<PartyConfigurationViewModel>();
                });

            OpenCatApiPage = new Command(
            async () =>
            {
                await CoreMethods.PushPageModel<CatApiViewModel>();
            });

            OpenHomeScreen = new Command(
            async () =>
            {
                await CoreMethods.PopToRoot(true);
            });
        }

        public override void Init(object initData)
        {
            base.Init(initData);

        }
    }
}
