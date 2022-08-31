using FreshMvvm;
using Mde.Project.Mobile.Domain.Constants;
using Mde.Project.Mobile.Domain.Models;
using Mde.Project.Mobile.Domain.Services;
using MediaManager;
using MediaManager.Library;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mde.Project.Mobile.ViewModels
{
    public class PartySongSelectorViewModel : FreshBasePageModel
    {
        #region Commands and vars
        public PartyConfiguration PartyConfiguration { get; set; }
        private readonly SongSelectorService _songSelectorService;
        public ICommand CloseModal { get; set; }
        public ICommand Forward { get; set; }
        public ICommand Rewind { get; set; }
        public ICommand Previous { get; set; }
        public ICommand Next { get; set; }
        public ICommand SelectSong { get; set; }
        public ICommand AddSong { get; set; }
        private IMediaItem selectedSong;
        public IMediaItem SelectedSong
        {
            get { return selectedSong; }
            set
            {
                selectedSong = value;
                RaisePropertyChanged();
            }
        }

        private string selectedSongTitleArtist;
        public string SelectedSongTitleArtist
        {
            get { return selectedSongTitleArtist; }
            set
            {
                selectedSongTitleArtist = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Song> songList;
        public ObservableCollection<Song> SongList
        {
            get { return songList; }
            set
            {
                songList = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public PartySongSelectorViewModel(SongSelectorService songSelectorService)
        {
            #region Declare vars, bind values

            SelectedSongTitleArtist = "Select Song";
            _songSelectorService = songSelectorService;
            var selectedSong = new SelectedSong();   
            var addSong = new AddSong();
            ObservableCollection<Song> SongMediaList = new ObservableCollection<Song>();
    
            _ = _songSelectorService.InitPlayAsync(SongMediaList);
            SongList = SongMediaList;

            #endregion

            #region Commands
            SelectSong = new Command(
            async () =>
            {
                _songSelectorService.SelectSong(selectedSong);
                PartyConfiguration.SelectedSong = selectedSong.CurrentSong;
                PartyConfiguration.SongTitleArtist = selectedSong.SongTitleArtist;              
                SelectedSongTitleArtist = selectedSong.SongTitleArtist;
                await CoreMethods.DisplayAlert(null, $"{selectedSong.SongTitleArtist} has been selected", "Ok");
            });

            AddSong = new Command(
            async () =>
            {
                SongList.Clear();
                await _songSelectorService.AddMoreSongs(addSong);
                SongList = addSong.songList;
                if(addSong.ExceptionMessage != null && addSong.ExceptionMessage != FeedbackMessages.NoSongAdded) 
                {
                    await CoreMethods.DisplayAlert(null, "Song could not be added", "Ok");
                }
                else if(addSong.ExceptionMessage == FeedbackMessages.NoSongAdded) 
                {
                    //add DisplayToast no song added
                }
                else 
                {
                    await CoreMethods.DisplayAlert(null, "Song has been added", "Ok");          
                }                           
            });

            Forward = new Command(
            () =>
            {
                _songSelectorService.Forward();
            });

            Rewind = new Command(
            () =>
            {
                _songSelectorService.Rewind();
            });

            Previous = new Command(
            () =>
            {
                _songSelectorService.Previous();
            });

            Next = new Command(
            () =>
            {
                _songSelectorService.Next();
            });

            CloseModal = new Command(
            async () =>
            {
                _songSelectorService.Stop();
                await CoreMethods.PopPageModel(PartyConfiguration, modal: true);
            });
            #endregion

        }

        public override void Init(object initData)
        {
            base.Init(initData);

            PartyConfiguration = initData as PartyConfiguration;
        }

    }
}
