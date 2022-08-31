using Mde.Project.Mobile.Domain.Interfaces;
using MediaManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mde.Project.Mobile.Domain.Services
{
    public class FlashlightService : IFlashLightService
    {
        public async void TurnOnFlashLight() 
        {
            try 
            {
                await Flashlight.TurnOnAsync();
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"Unable to turn on flashlight:{ex.Message}");
            }
        }

        public async void TurnOffFlashLight()
        {
            try
            {
                await Flashlight.TurnOffAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to turn off flashlight:{ex.Message}");
            }
        }

        public async void DisharmoniousFlash() 
        {

            var IsSong = CrossMediaManager.Current.Queue.Current;
            do
            {
                if(IsSong != null) 
                {
                    TurnOnFlashLight();
                    await Task.Delay(100);
                    TurnOffFlashLight();
                }

                if (IsSong == null) 
                { 
                    TurnOffFlashLight();
                    break; 
                }

               IsSong = CrossMediaManager.Current.Queue.Current;
            }
            while (IsSong != null);
        }
    }
}
