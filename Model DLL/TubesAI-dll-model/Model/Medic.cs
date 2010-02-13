using System;
using System.Collections.Generic;
using System.Text;

namespace TubesAI.Model
{
    public class Medic : Unit
    {
        /// <summary>
        /// Angka maksinal si medic masih bisa melakukan healing
        /// </summary>
        private int avalaibleCuring;

        public Medic(int _index)
        {
            //assignment atribut yang dasar di kelas ini.
            index = _index;
            this.maxHP = 2000;
            this.currentHP = this.maxHP;
            this.urutan = 4;
            this.avalaibleCuring = 10;
            isBertahan = false;
        }

        public void decreaseAvalCuring()
        {
            this.avalaibleCuring--;
        }

        public bool isTidakBisaCuring()
        {
            return this.avalaibleCuring == 0;
        }
    }
}
