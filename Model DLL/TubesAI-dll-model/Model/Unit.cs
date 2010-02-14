using System;
using System.Collections.Generic;
using System.Text;

namespace TubesAI.Model
{
    public abstract class Unit
    {
        public int index { get; protected set; }
        /// <summary>
        /// HP
        /// </summary>
        protected int currentHP;
        /// <summary>
        /// HP maksimal yang bisa dimiki oleh suatu unit
        /// </summary>
        protected int maxHP;
        /// <summary>
        /// Urutan penyerangan masing-masing unit
        /// </summary>
        protected int urutan;
        public bool isBertahan { get; set; }

        public void setHP(int newHP)
        {
            this.currentHP = newHP;
        }

        public int getCurrentHP()
        {
            return this.currentHP;
        }

        public int getUrutan()
        {
            return this.urutan;
        }

        public int getMaxHP()
        {
            return this.maxHP;
        }

        public Boolean isDead()
        {
            return this.currentHP <= 0;
        }

        /// <summary>
        /// Melakukan penyerangan
        /// </summary>
        /// <param name="i_pelaku">Index dari unit penyerang (0 - 10). </param>
        /// <param name="i_sasaran">Index dari unit yang ingin diserang (0 - 10). </param>
        /// <param name="sasaran">Index dari tim yang akan diserang (0 = tim sendiri; 1 = tim lain). </param>
        public ElemenAksi Attack(int i_sasaran, Team tim_sasaran)
        {
            return new ElemenAksi(index, Aksi.menyerang, i_sasaran, tim_sasaran.index);
        }

        /// <summary>
        /// Melakukan pertahanan
        /// </summary>
        /// <param name="i_pelaku">Index dari unit yang ingin bertahan (0 - 10). </param>
        public ElemenAksi Defend()
        {
            return new ElemenAksi(index, Aksi.bertahan);
        }

        /// <summary>
        /// Melakukan penyembuhan
        /// </summary>
        /// <param name="i_pelaku">Index dari unit penyembuh (0 - 10). </param>
        /// <param name="i_sasaran">Index dari unit yang ingin disembuhkan (0 - 10). </param>
        /// <param name="sasaran">Index dari tim yang akan disembuhkan (0 = tim sendiri; 1 = tim lain). </param>
        public ElemenAksi Heal(int i_sasaran, Team tim_sasaran)
        {
            return new ElemenAksi(index, Aksi.heal, i_sasaran, tim_sasaran.index);
        }

        /// <summary>
        /// Tidak melakukan apapun
        /// </summary>
        /// <param name="i_pelaku">Index dari unit yang tidak melakukan apapun (0 - 10). </param>
        public ElemenAksi doNothing()
        {
            return new ElemenAksi(index, Aksi.nothing);
        }

        /// <summary>
        /// Menggunakan benda
        /// </summary>
        /// <param name="i_pelaku">Index dari unit yang akan menggunakan (0 - 10). </param>
        /// <param name="i_sasaran">Index dari unit yang akan diberikan benda (0 - 10). </param>
        /// <param name="sasaran">Index dari tim yang akan diberikan benda (0 = tim sendiri; 1 = tim lain). </param>
        /// <param name="_item">Benda yang akan digunakan</param>
        public ElemenAksi useItem(int i_sasaran, Team tim_sasaran, Item _item)
        {
            return new ElemenAksi(index, Aksi.use_item, i_sasaran, tim_sasaran.index, _item);
        }

    }
}
