using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TubesAI.Model
{
    public class Medic : Unit
    {
        public static Texture2D textureL;
        public static Texture2D textureR;

        /// <summary>
        /// Angka maksinal si medic masih bisa melakukan healing
        /// </summary>
        private int avalaibleCuring;
        private const int available = 10;

        public Medic(int _index)
        {
            //assignment atribut yang dasar di kelas ini.
            index = _index;
            this.maxHP = 2000;
            this.currentHP = this.maxHP;
            this.urutan = 4;
            this.avalaibleCuring = available;
            isBertahan = false;
        }

        public Medic(Medic _medic)
        {
            this.index = _medic.index;
            this.maxHP = _medic.maxHP;
            this.currentHP = _medic.currentHP;
            this.urutan = _medic.urutan;
            this.avalaibleCuring = available;
            this.isBertahan = false;
        }

        public void decreaseAvalCuring()
        {
            this.avalaibleCuring--;
        }

        public void resetAvalCuring()
        {
            this.avalaibleCuring = available;
        }

        public bool isTidakBisaCuring()
        {
            return this.avalaibleCuring == 0;
        }

    }
}
