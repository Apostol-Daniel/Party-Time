using Mde.Project.Mobile.Domain.Models;
using MediaManager.Library;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Mde.Project.Mobile.Domain.Services
{
    public interface ISongSelectorService
    {
        void InitMediaPlayer();
        Task <ObservableCollection<Song>> InitPlayAsync(ObservableCollection<Song> songList);
        Task<AddSong> AddMoreSongs(AddSong addSong);
        void Forward();
        void Next();
        void Previous();
        void Stop();
        void Rewind();
        object SelectSong(SelectedSong selectSong);       
        ObservableCollection<Song> SetupPlaylist(bool isNew, ObservableCollection<Song> songMediaList);
    }
}