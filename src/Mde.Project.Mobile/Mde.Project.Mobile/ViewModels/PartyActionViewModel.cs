using FreshMvvm;
using Mde.Project.Mobile.Domain.Models;
using Mde.Project.Mobile.Domain.Services;
using MediaManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mde.Project.Mobile.ViewModels
{
    public class PartyActionViewModel : FreshBasePageModel
    {
        public PartyConfiguration PartyConfiguration { get; set; }
        private readonly PartyActionService _partyActionService;
        private readonly FlashlightService _flashLightService;
        private readonly VibrationService _vibrationService;
        public ICommand CloseModal { get; set; }
        private Color? pageBackgroundColor;
        public Color? PageBackgroundColor 
        {
            get { return pageBackgroundColor; }
            set {
                  pageBackgroundColor = value; 
                  RaisePropertyChanged();
            }
        }
        public PartyActionViewModel(PartyActionService partyActionService,
                                    VibrationService vibrationService,
                                    FlashlightService flashlightService)
        {          
            _flashLightService = flashlightService;
            _vibrationService = vibrationService;
            _partyActionService = partyActionService;
         

            CloseModal = new Command(
            async () =>
            {
                _partyActionService.Stop();
                if(Device.RuntimePlatform == Device.Android) 
                {
                    if (!CrossMediaManager.Current.IsStopped())
                    {
                        do
                        {
                            await Task.Delay(500);
                        }
                        while (!CrossMediaManager.Current.IsStopped());
                    }
                }
                
               //_vibrationService.TurnOffVibration();
                
               //_vibrationService.DisharmoniousVibrate();
               
                await CoreMethods.PushPageModel<PartyOverViewModel>(null, modal: false);
            });
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
           
            var IsPlaying = CrossMediaManager.Current.IsPlaying();
            PartyConfiguration = initData as PartyConfiguration;
            _ = _partyActionService.InitParty(PartyConfiguration.SelectedSong);          
            SwitchColors();
            if (Device.RuntimePlatform == Device.Android) 
            {
                if (!CrossMediaManager.Current.IsPlaying())
                {
                    do
                    {
                        await Task.Delay(500);
                    }
                    while (!CrossMediaManager.Current.IsPlaying());
                }

                if (PartyConfiguration.IsFlash == true) 
                {
                    _flashLightService.DisharmoniousFlash();
                }

                if (PartyConfiguration.IsVibrate == true)
                {
                    _vibrationService.DisharmoniousVibrate();
                }                          
            }
        }

        public async void SwitchColors()
        {
            do
            {
                PageBackgroundColor = PartyConfiguration.FirstColor;
                await Task.Delay(250);
                PageBackgroundColor = PartyConfiguration.SecondColor;
                await Task.Delay(250);
                PageBackgroundColor = PartyConfiguration.ThirdColor;
                await Task.Delay(250);
            }
            while (true);
        }
    }
}
