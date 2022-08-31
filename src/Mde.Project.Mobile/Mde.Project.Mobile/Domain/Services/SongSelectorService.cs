using Mde.Project.Mobile.Domain.Constants;
using Mde.Project.Mobile.Domain.Models;
using MediaManager;
using MediaManager.Library;
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

namespace Mde.Project.Mobile.Domain.Services
{
    public class SongSelectorService : ISongSelectorService
    {
        public void InitMediaPlayer()
        {
            if (!CrossMediaManager.Current.IsPrepared())
            {
                //was used to load songs at app startup for Android
                //Android media player is slower than on windows
                //Not currently in use
                // critical for Android 
                CrossMediaManager.Current.Init();
                SongList songMedia = new SongList();

                foreach (var songString in songMedia)
                {
                    IMediaItem song = new MediaItem(songString);
                    CrossMediaManager.Current.Queue.Add(song);

                }               
            }

        }

        public async Task <ObservableCollection<Song>> InitPlayAsync(ObservableCollection<Song> songList)
        {
            if (!CrossMediaManager.Current.IsPrepared()) 
            {
                CrossMediaManager.Current.Init();          
                SongList songMedia = new SongList();
            
                await CrossMediaManager.Current.Play(songMedia.SongMedia);               
                SetupPlaylist(false,songList);          
                return songList;
            }
            
           return SetupPlaylist(false, songList);
        }
 
        public ObservableCollection<Song> SetupPlaylist(bool isNew, ObservableCollection<Song> songList)
        {
            List<IMediaItem> queueList = CrossMediaManager.Current.Queue.MediaItems.ToList();

            if(songList == null) 
            {
                songList = new ObservableCollection<Song>();
            }
           
            for (int i = 0; i < queueList.Count; i++)
            {
                IMediaItem song = queueList[i];

                string title;
                string artist;

                #region Validation
                if (!string.IsNullOrEmpty(song.DisplayTitle))
                {
                    title = $"{song.DisplayTitle}";
                }
                else
                {
                    title = "No title found in song data.";
                }

                if (!string.IsNullOrEmpty(song.Artist))
                {
                    artist = $"{song.Artist}";
                }
                else
                {
                    artist = "No artist found in song data.";
                }
                #endregion 

                songList.Add(new Song()
                {
                    Title = title,
                    Artist = artist,
                    Number = i + 1
                });
            }

            return songList;
        }             

        public object SelectSong(SelectedSong currentSong)
        {
            string songName;
            string songArtist;
            currentSong.CurrentSong = CrossMediaManager.Current.Queue.Current;

            #region Validation
            if (!string.IsNullOrEmpty(currentSong.CurrentSong.DisplayTitle))
            {
                songName = currentSong.CurrentSong.DisplayTitle;
            }
            else
            {
                songName = "Unknown title";
            }

            if (!string.IsNullOrEmpty(currentSong.CurrentSong.Artist))
            {
                songArtist = currentSong.CurrentSong.Artist;
            }
            else
            {
                songArtist = "Unknown artist";
            }
            currentSong.SongTitleArtist = $"{songName} by {songArtist}";
            #endregion      
            
            return currentSong;

        }

        public async Task<AddSong> AddMoreSongs(AddSong addSong)
        {
            string[] songExtension = { ".mp3" };                     

            ObservableCollection<Song> songList = new ObservableCollection<Song>();
            var pickedFile = await CrossFilePicker.Current.PickFile(songExtension);
            if (pickedFile != null)
            {
                var cachedFilePathName = Path.Combine(FileSystem.CacheDirectory, pickedFile.FileName);

                if (!File.Exists(cachedFilePathName))
                    File.WriteAllBytes(cachedFilePathName, pickedFile.DataArray);
                try 
                {
                    if (File.Exists(cachedFilePathName))
                    {
                        var generatedMediaItem =
                            await CrossMediaManager.Current.Extractor.CreateMediaItem(cachedFilePathName);
                        CrossMediaManager.Current.Queue.Add(generatedMediaItem);

                        SetupPlaylist(true, songList);                       
                        addSong.songList = songList;
                        addSong.ExceptionMessage = null;
                        return addSong;
                    }

                }
                catch (Exception ex) 
                {
                    Debug.WriteLine($"Error adding song: {ex.Message}");                   
                    SetupPlaylist(false,songList);
                    addSong.ExceptionMessage = ex.Message;
                    addSong.songList = songList;
                    return addSong;
                }
               
            }
            else
            {
                SetupPlaylist(false,songList);
                addSong.ExceptionMessage = FeedbackMessages.NoSongAdded;
                addSong.songList = songList;
                return addSong;
            }

            SetupPlaylist(false, songList);
            addSong.songList = songList;
            return addSong;
        }

        public async void Forward()
        {
            await CrossMediaManager.Current.StepForward();
        }

        public async void Rewind()
        {
            await CrossMediaManager.Current.StepBackward();
        }


        public async void Previous()
        {
            await CrossMediaManager.Current.PlayPrevious();
        }


        public async void Next()
        {
            await CrossMediaManager.Current.PlayNext();
        }

        public async void Stop()
        {
            await CrossMediaManager.Current.Stop();
        }
    }
}
