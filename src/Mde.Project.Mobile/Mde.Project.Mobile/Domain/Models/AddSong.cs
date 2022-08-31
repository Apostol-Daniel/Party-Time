using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mde.Project.Mobile.Domain.Models
{
    public class AddSong
    {
        public ObservableCollection<Song> songList { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
