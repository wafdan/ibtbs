using System;
using System.Collections.Generic;
using System.Text;

namespace TubesAI.Model
{
    public class Spearman : Unit
    {

        public Spearman()
        {
            //assignment atribut yang dasar di kelas ini.
            this.maxHP = 2000;
            this.currentHP = this.maxHP;
            this.urutan = 3;
            isBertahan = false;
        }
    }
}
