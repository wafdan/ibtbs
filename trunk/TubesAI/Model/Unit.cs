using System;
using System.Collections.Generic;
using System.Text;

namespace TubesAI.Model
{
    public abstract class Unit
    {
        
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
        public bool isBertahan;

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
            return this.currentHP < 0;
        }
    }
}
