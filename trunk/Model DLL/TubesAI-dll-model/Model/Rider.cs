using System;
using System.Collections.Generic;
using System.Text;

namespace TubesAI.Model
{
    public class Rider : Unit
    {

        public Rider()
        {
            //assignment atribut yang dasar di kelas ini.
            this.maxHP = 3000;
            this.currentHP = this.maxHP;
            this.urutan = 5;
            isBertahan = false;
        }
    }
}
