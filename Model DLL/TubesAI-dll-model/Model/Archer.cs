using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TubesAI.Model
{
    public class Archer : Unit
    {
        public static Texture2D textureL;
        public static Texture2D textureR;

        public Archer(int _index)
        {
            //assignment atribut yang dasar di kelas ini.
            index = _index;
            this.maxHP = 1500;
            this.currentHP = this.maxHP;
            this.urutan = 1;
            isBertahan = false;
        }
    }
}
