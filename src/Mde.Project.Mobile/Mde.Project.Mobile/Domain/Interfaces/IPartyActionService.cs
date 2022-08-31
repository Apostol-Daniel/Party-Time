using Mde.Project.Mobile.Domain.Models;
using MediaManager.Library;
using System.Threading.Tasks;

namespace Mde.Project.Mobile.Domain.Services
{
    public interface IPartyActionService
    {
        Task InitParty(IMediaItem currentSong);
        void Stop();
    }
}