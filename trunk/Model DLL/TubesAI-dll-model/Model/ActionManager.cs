using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TubesAI.Model
{
    public static class ActionManager
    {
        /// <summary>
        /// Melakukan penyerangan
        /// </summary>
        /// <param name="i_pelaku">Index dari unit penyerang (0 - 10). </param>
        /// <param name="i_sasaran">Index dari unit yang ingin diserang (0 - 10). </param>
        /// <param name="sasaran">Index dari tim yang akan diserang (0 = tim sendiri; 1 = tim lain). </param>
        public static ElemenAksi Attack(int i_pelaku, int i_sasaran, int sasaran)
        {
            return new ElemenAksi(i_pelaku, Aksi.menyerang, i_sasaran, sasaran);
        }

        /// <summary>
        /// Melakukan pertahanan
        /// </summary>
        /// <param name="i_pelaku">Index dari unit yang ingin bertahan (0 - 10). </param>
        public static ElemenAksi Defend(int i_pelaku)
        {
            return new ElemenAksi(i_pelaku, Aksi.bertahan);
        }

        /// <summary>
        /// Melakukan penyembuhan
        /// </summary>
        /// <param name="i_pelaku">Index dari unit penyembuh (0 - 10). </param>
        /// <param name="i_sasaran">Index dari unit yang ingin disembuhkan (0 - 10). </param>
        /// <param name="sasaran">Index dari tim yang akan disembuhkan (0 = tim sendiri; 1 = tim lain). </param>
        public static ElemenAksi Heal(int i_pelaku, int i_sasaran, int sasaran)
        {
            return new ElemenAksi(i_pelaku, Aksi.heal, i_sasaran, sasaran);
        }

        /// <summary>
        /// Tidak melakukan apapun
        /// </summary>
        /// <param name="i_pelaku">Index dari unit yang tidak melakukan apapun (0 - 10). </param>
        public static ElemenAksi doNothing(int i_pelaku)
        {
            return new ElemenAksi(i_pelaku, Aksi.nothing);
        }

        /// <summary>
        /// Menggunakan benda
        /// </summary>
        /// <param name="i_pelaku">Index dari unit yang akan menggunakan (0 - 10). </param>
        /// <param name="i_sasaran">Index dari unit yang akan diberikan benda (0 - 10). </param>
        /// <param name="sasaran">Index dari tim yang akan diberikan benda (0 = tim sendiri; 1 = tim lain). </param>
        /// <param name="_item">Benda yang akan digunakan</param>
        public static ElemenAksi useItem(int i_pelaku, int i_sasaran, int sasaran, Item _item)
        {
            return new ElemenAksi(i_pelaku, Aksi.use_item, i_sasaran, sasaran, _item);
        }

    }
}
