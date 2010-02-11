using System;
using System.Collections.Generic;
using System.Text;

namespace TubesAI.Model
{
    public class Swordsman : Unit
    {

        public Swordsman()
        {
           //assignment atribut yang dasar di kelas ini.
            this.maxHP = 2000;
            this.currentHP = this.maxHP;
            this.urutan = 2;
            isBertahan = false;
        }
    }
}
