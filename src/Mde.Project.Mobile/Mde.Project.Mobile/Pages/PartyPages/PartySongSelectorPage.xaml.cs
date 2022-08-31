using Mde.Project.Mobile.Domain.Services;
using Mde.Project.Mobile.ViewModels;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using MediaManager.Playback;
using MediaManager.Player;
using MediaManager.Queue;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mde.Project.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartySongSelectorPage : ContentPage
    {      
        public IMediaItem SelectedSong { get; set; }
        public bool IsMediaLoaded { get; set; }

        public PartySongSelectorPage()
        {
            InitializeComponent();
            #region MediaPlayer Button Images
            ButtonAddMoreSongs.Source = ImageSource.FromFile("addsong.png");
            //DynamicButtonPlayPause.Source = ImageSource.FromFile("play.png");
            RewindButton.Source = ImageSource.FromFile("rewind.png");
            ForwardButton.Source = ImageSource.FromFile("fastforward.png");
            PreviusButton.Source = ImageSource.FromFile("backwards.png");
            PlayPauseButton.Source = ImageSource.FromFile("playpause.png");
            NextButton.Source = ImageSource.FromFile("forwards.png");
            #endregion
           
            NavigationPage.SetHasBackButton(this, false);
            //CrossMediaManager.Current.Init();
            CrossMediaManager.Current.StateChanged += Current_OnStateChanged;
            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            CrossMediaManager.Current.MediaItemChanged += Current_MediaItemChanged;          
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(CollectionView.ItemsSource == null) 
            {
                IsMediaLoaded = false;
                LabelCurrentTrackIndex.Text = "Loading media";
                LabelCurrentTrackTitle.Text = "Please wait";              
            }

            if (!CrossMediaManager.Current.IsPrepared())
            {

                InitializePlayAsync();
                // Set up Player Preferences
                CrossMediaManager.Current.ShuffleMode = ShuffleMode.Off;
                CrossMediaManager.Current.PlayNextOnFailed = true;
                CrossMediaManager.Current.RepeatMode = RepeatMode.All;
                CrossMediaManager.Current.AutoPlay = true;
            }
            else
            {
                SetupCurrentMediaDetails(CrossMediaManager.Current.Queue.Current);
                SetupCurrentMediaPositionData(CrossMediaManager.Current.Position);
                SetupCurrentMediaPlayerState(CrossMediaManager.Current.State);              
            }
        }

        private void InitializePlayAsync()
        {                      
            var currentMediaItem = CrossMediaManager.Current.Queue.Current;
            SetupCurrentMediaDetails(currentMediaItem);
            var isPlaying =  CrossMediaManager.Current.IsPlaying();
            DynamicButtonPlayPause.Source = isPlaying ? ImageSource.FromFile("play.png") : ImageSource.FromFile("pause.png");
        }

        #region MediaManagement
        private void Current_MediaItemChanged(object sender, MediaItemEventArgs e)
        {
            SetupCurrentMediaDetails(e.MediaItem);
        }

        private void Current_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SetupCurrentMediaPositionData(e.Position);
            });
        }

        private void Current_OnStateChanged(object sender, StateChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SetupCurrentMediaPlayerState(e.State);
            });
        }
        
        private void ToggleUI(bool IsEnabled) 
        {
            if (IsEnabled == false) 
            {
                ButtonAddMoreSongs.IsEnabled = false;
                ButtonClose.IsEnabled = false;
                ButtonSelect.IsEnabled = false;
                DynamicButtonPlayPause.IsEnabled = false;
                PlayPauseButton.IsEnabled = false;
                RewindButton.IsEnabled = false;
                ForwardButton.IsEnabled = false;
                NextButton.IsEnabled = false;
                PreviusButton.IsEnabled = false;
            }

            if (IsEnabled == true)
            {
                ButtonAddMoreSongs.IsEnabled = true;
                ButtonClose.IsEnabled = true;
                ButtonSelect.IsEnabled = true;
                DynamicButtonPlayPause.IsEnabled = true;
                PlayPauseButton.IsEnabled = true;
                RewindButton.IsEnabled = true;
                ForwardButton.IsEnabled = true;
                NextButton.IsEnabled = true;
                PreviusButton.IsEnabled = true;
            }
        }
       
        private async void SetupCurrentMediaDetails(IMediaItem playingSong)
        {
            //For Android devices;MediaPlayer instance starts slower than on UWP
            //
            if(Device.RuntimePlatform == Device.Android && IsMediaLoaded == false) 
            {
                do
                {
                    LabelCurrentTrackIndex.Text = "Loading media";
                    LabelCurrentTrackTitle.Text = "Please wait";
                    ToggleUI(false);
                    await Task.Delay(10000);
                    if (CrossMediaManager.Current.IsPlaying() == true)
                    {
                        playingSong = CrossMediaManager.Current.Queue.Current;
                    };

                    if (playingSong != null) 
                    {
                        IsMediaLoaded = true;
                        ToggleUI(true); break;
                    } 
                }
                while (playingSong == null);

            }

            string displayDetails;

            #region Validation
            if (!string.IsNullOrEmpty(playingSong.DisplayTitle))
            {
                displayDetails = playingSong.DisplayTitle;
            }
            else
            {
                displayDetails = "Unknown title";
            }


            if (!string.IsNullOrEmpty(playingSong.Artist))
            {
                displayDetails = $"{displayDetails} - {playingSong.Artist}";
            }
            else
            {
                displayDetails = $"{displayDetails} - Unknown artist";
            }
            #endregion Validation

            LabelCurrentTrackTitle.Text = displayDetails;

            LabelCurrentTrackIndex.Text = $"CURRENT TRACK: {CrossMediaManager.Current.Queue.CurrentIndex + 1}/{CrossMediaManager.Current.Queue.Count}";
        }

        private void SetupCurrentMediaPositionData(TimeSpan currentPlayTime)
        {
            var formattingPattern = @"hh\:mm\:ss";
            if (CrossMediaManager.Current.Duration.Hours <= 0)
                formattingPattern = @"mm\:ss";

            var fullLengthString = CrossMediaManager.Current.Duration.ToString(formattingPattern);
            LabelMediaDetails.Text = $"{currentPlayTime.ToString(formattingPattern)}/{fullLengthString}";

            SliderSongPlayDisplay.Value = currentPlayTime.Ticks;
        }

        private void SetupCurrentMediaPlayerState(MediaPlayerState currentPlayerState)
        {
            LabelCurrentPlayerStatus.Text = $"{currentPlayerState.ToString().ToUpper()}";

            if (currentPlayerState == MediaPlayerState.Loading)
            {
                SliderSongPlayDisplay.Value = 0;
            }
            else if (currentPlayerState == MediaPlayerState.Playing
                    && CrossMediaManager.Current.Duration.Ticks > 0)
            {
                SliderSongPlayDisplay.Maximum = CrossMediaManager.Current.Duration.Ticks;
            }
        }
        #endregion

        #region MediaPlayerButtons      
        private async void DynamicButtonPlayPause_Clicked(object sender, EventArgs e)
        {
            var playImg = ImageSource.FromFile("play.png");
            var pauseImg = ImageSource.FromFile("pause.png");
            if (CrossMediaManager.Current.IsPlaying())
            {
                await CrossMediaManager.Current.Pause();
                DynamicButtonPlayPause.Source = playImg;
            }
            else
            {
                await CrossMediaManager.Current.Play();
                DynamicButtonPlayPause.Source = pauseImg;
            }
        }

        private async void ButtonPlaySong_Clicked(object sender, EventArgs e)
        {
            //if (!(((ImageButton)sender).BindingContext is Song selectedSong))

            var selectedSong = ((ImageButton)sender).BindingContext as Song;

            if (selectedSong == null)
                return;

            _ = await CrossMediaManager.Current.PlayQueueItem(selectedSong.Number - 1);

            if (!CrossMediaManager.Current.IsPlaying())
                await CrossMediaManager.Current.Play(selectedSong.Number - 1);



            DynamicButtonPlayPause.Source = ImageSource.FromFile("pause.png");
        }

        private async void PlayPauseButton_Clicked(object sender, EventArgs e)
        {

            var playImg = ImageSource.FromFile("play.png");
            var pauseImg = ImageSource.FromFile("pause.png");
            if (CrossMediaManager.Current.IsPlaying())
            {
                await CrossMediaManager.Current.Pause();
                DynamicButtonPlayPause.Source = playImg;
            }
            else
            {
                await CrossMediaManager.Current.Play();
                DynamicButtonPlayPause.Source = pauseImg;
            }
        }              
        #endregion  
    }
}