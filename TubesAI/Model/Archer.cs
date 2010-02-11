using System;
using System.Collections.Generic;
using System.Text;

namespace TubesAI.Model
{
    public class Archer : Unit
    {

        public Archer()
        {
            //assignment atribut yang dasar di kelas ini.
            this.maxHP = 1500;
            this.currentHP = this.maxHP;
            this.urutan = 1;
            isBertahan = false;
        }
    }
}
