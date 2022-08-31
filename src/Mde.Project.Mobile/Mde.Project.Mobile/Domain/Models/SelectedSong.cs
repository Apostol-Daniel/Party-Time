using MediaManager.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Models
{
    public class SelectedSong
    {
        public string SongTitleArtist { get; set; }
        public IMediaItem CurrentSong { get; set; }
    }
}
