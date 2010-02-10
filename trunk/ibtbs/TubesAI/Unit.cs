using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class Unit
    {
        /// <summary>
        /// HP
        /// </summary>
        protected int currentHP;
        /// <summary>
        /// jumlah frame untuk gerakan penyerangan
        /// </summary>
        protected int jumlahFrameSerang;
        /// <summary>
        /// jumlah frame yang gerakan lagi diam
        /// </summary>
        protected int jumlahFrameDiam;
        /// <summary>
        /// jumlah frame buat pose bertahan
        /// </summary>
        protected int jumlahFrameBertahan;
        /// <summary>
        /// path ke gambar frame diam
        /// </summary>
        protected string pathFrameDiam;
        /// <summary>
        /// path ke gambar frame serang
        /// </summary>
        protected string pathFrameSerang;
        /// <summary>
        /// path ke gambar frame bertahan
        /// </summary>
        protected string pathFrameBertahan;
        /// <summary>
        /// HP maksimal yang bisa dimiki oleh suatu unit
        /// </summary>
        protected int maxHP;
        /// <summary>
        /// Urutan penyerangan masing-masing unit
        /// </summary>
        protected int urutan;

        public void setHP(int newHP)
        {
            this.currentHP = newHP;
        }

        public int getCurrentHP()
        {
            return this.currentHP;
        }

        public int getJumlahFrameBertahan()
        {
            return this.jumlahFrameBertahan;
        }

        public int getJumlahFrameSerang()
        {
            return this.jumlahFrameSerang;
        }

        public int getJumlahFrameDiam()
        {
            return this.jumlahFrameDiam;
        }

        public int getPathBertahan()
        {
            return this.pathFrameBertahan;
        }

        public string getPathDiam()
        {
            return this.pathFrameDiam;
        }

        public string getPathSerang()
        {
            return pathFrameSerang;
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
            return this.currentHP < 0;
        }
    }
}
