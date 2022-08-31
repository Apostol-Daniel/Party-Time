using Mde.Project.Mobile.Domain.Models;
using MediaManager;
using MediaManager.Library;
using MediaManager.Player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mde.Project.Mobile.Domain.Services
{
    public class PartyActionService : IPartyActionService
    {
        public async Task InitParty(IMediaItem currentSong)
        {
            CrossMediaManager.Current.Queue.Clear();

            await CrossMediaManager.Current.Play(currentSong);

        }

        public async void Stop()
        {          
            await CrossMediaManager.Current.Stop();           
            CrossMediaManager.Current.Queue.Clear();          
        }
       
    }
}
