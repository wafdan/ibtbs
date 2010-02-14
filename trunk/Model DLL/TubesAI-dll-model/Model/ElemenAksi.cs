using System;
using System.Collections.Generic;
using System.Text;

namespace TubesAI.Model
{
    /// <summary>
    /// Kelas yang akan dilempar 
    /// </summary>
    public class ElemenAksi
    {
        public int index_pelaku { get; private set; }
        public int index_sasaran { get; private set; }
        public int tim_sasaran { get; private set; }
        public Aksi aksi { get; set; }
        public Item item { get; private set; }

        public ElemenAksi (int i_pelaku, Aksi act, int i_sasaran, int sasaran)
        {
            aksi = act;
            index_pelaku = i_pelaku;
            index_sasaran = i_sasaran;
            tim_sasaran = sasaran;
        }

        public ElemenAksi(int i_pelaku, Aksi act)
        {
            aksi = act;
            index_pelaku = i_pelaku;
        }

        public ElemenAksi(int i_pelaku, Aksi act, int i_sasaran, int sasaran, Item _item)
        {
            aksi = act;
            index_pelaku = i_pelaku;
            index_sasaran = i_sasaran;
            tim_sasaran = sasaran;
            item = _item;
        }

    }
}
