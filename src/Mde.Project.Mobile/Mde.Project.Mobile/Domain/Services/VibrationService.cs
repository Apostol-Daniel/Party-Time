using MediaManager;
using MediaManager.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mde.Project.Mobile.Domain.Services
{
    public class VibrationService : IVibrationService
    {
        public void TurnOnVibration()
        {
            try
            {                              
                var duration = TimeSpan.FromSeconds(.1);
                Vibration.Vibrate(duration);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to turn on vibtrator:{ex.Message}");
            }
        }

        public void TurnOffVibration()
        {
            try
            {
                Vibration.Cancel();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to turn off vibtrator:{ex.Message}");
            }
        }

        public async void DisharmoniousVibrate() 
        {
            var IsSong = CrossMediaManager.Current.Queue.Current;
           
            do
            {
                if (IsSong != null)
                {
                    TurnOnVibration();
                    await Task.Delay(900);
                    TurnOffVibration();
                }

                if (IsSong == null) 
                {
                    break; 
                }

                IsSong = CrossMediaManager.Current.Queue.Current;
            }
            while (IsSong != null);           
            
        }       
    }
}
