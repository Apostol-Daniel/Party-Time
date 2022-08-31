using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Constants
{
    public class SongList
    {
        public  string[] SongMedia = new string[]
        {
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/music/blocSonic/Flex_Vector/Born_Ready/Flex_Vector_-_Born_Ready.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/music/Creative_Commons/Ketsa/Raising_Frequency/Ketsa_-_08_-_Multiverse.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/music/Oddio_Overplay/Carl_Phaser/End_Of_The_Dark/Carl_Phaser_-_02_-_Porcelain.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/music/WFMU/Voodoo_Suite/blissbloodcom/Voodoo_Suite_-_03_-_Little_Grass_Shack.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/tracks/NHz9XwFJBZOBv05JTybpvzhYPQyoyfiwAT9DL6ub.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/tracks/Y9c0R5LRuH4K50mDOxhik1y2p0P3YJHo2PgMXAO5.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/tracks/aicztSzEYAOCBN2iS4ISioIrMSb7ESbLHDeObmkf.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/tracks/w7p73R2sUQf201iw3DTsIImAAOMs3gIMgqmNpmWf.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/tracks/mhO6e4AoF2aaAoP7eAoFGRnFNt2RTKGNOoM1Jz6C.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/tracks/fe9hTvzmyU153wswTUmDIvnh2PFlDdwwZxnbT5T2.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/tracks/ud2pCRhDQazlDWIjPkwTktopQelMUy2QengvvXym.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/tracks/rE6br0xbc2r41WsXwzx43NX52Govnzt3cv3Cfeju.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/music/no_curator/Meavy_Boy/EP_71_to_20/Meavy_Boy_-_02_-_Everyone_is_Happy_Here.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/music/Music_for_Video/Soft_and_Furious/You_know_where_to_find_me/Soft_and_Furious_-_07_-_Powerful_Stasis.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/music/Music_for_Video/Soft_and_Furious/You_know_where_to_find_me/Soft_and_Furious_-_08_-_Murky_sweet_sweet_style.mp3",
                "https://files.freemusicarchive.org/storage-freemusicarchive-org/music/no_curator/Anonymous420/Anonymous420_-_Singles/Anonymous420_-_02_-__startup_nation.mp3"
        };      

        public SongEnumerator GetEnumerator() 
        {
            return new SongEnumerator(this);
        }
    }
}
