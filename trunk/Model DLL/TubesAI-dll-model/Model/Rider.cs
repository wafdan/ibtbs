using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TubesAI.Model
{
    public class Rider : Unit
    {
        public static Texture2D textureL;
        public static Texture2D textureR;

        public Rider(int _index)
        {
            //assignment atribut yang dasar di kelas ini.
            index = _index;
            this.maxHP = 3000;
            this.currentHP = this.maxHP;
            this.urutan = 5;
            isBertahan = false;
        }

        public Rider(Rider _rider)
        {
            this.index = _rider.index;
            this.maxHP = _rider.maxHP;
            this.currentHP = _rider.currentHP;
            this.urutan = _rider.urutan;
            this.isBertahan = false;
        }
    }
}
