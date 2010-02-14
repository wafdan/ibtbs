using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TubesAI.Model
{
    public class Swordsman : Unit
    {
        public static Texture2D textureL;
        public static Texture2D textureR;

        public Swordsman(int _index)
        {
            //assignment atribut yang dasar di kelas ini.
            index = _index;
            this.maxHP = 2000;
            this.currentHP = this.maxHP;
            this.urutan = 2;
            isBertahan = false;
        }
    }
}
