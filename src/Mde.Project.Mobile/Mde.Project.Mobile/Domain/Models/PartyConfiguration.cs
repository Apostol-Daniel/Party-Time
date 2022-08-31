using MediaManager.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mde.Project.Mobile.Domain.Models
{
    public class PartyConfiguration
    {
        public Color? FirstColor { get; set; }
        public Color? SecondColor { get; set; }
        public Color? ThirdColor { get; set; }
        public int? ColorNumber { get; set; }
        public IMediaItem SelectedSong { get; set; }
        public string SongTitleArtist { get; set; }
        public bool IsFlash { get; set; }
        public bool IsVibrate { get; set; }


    }
}
