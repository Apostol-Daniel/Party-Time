using Mde.Project.Mobile.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain
{
    public class SongEnumerator
    {
        //not currently in use
        //needed to foreach a song from SongList
        int nIndex;
        readonly SongList collection;
        public SongEnumerator(SongList songCollection)
        {
            collection = songCollection;
            nIndex = -1;
        }

        public bool MoveNext()
        {
            nIndex++;
            return nIndex < collection.SongMedia.Length;
        }

        public string Current => collection.SongMedia[nIndex];
    }
}
