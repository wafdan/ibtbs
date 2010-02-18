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

        public Swordsman(int _index, int curHP)
        {
            index = _index;
            this.maxHP = 2000;
            this.currentHP = curHP;
            this.urutan = 2;
            isBertahan = false;
        }

        public Swordsman(Swordsman _swordsman)
        {
            this.index = _swordsman.index;
            this.maxHP = _swordsman.maxHP;
            this.currentHP = _swordsman.currentHP;
            this.urutan = _swordsman.urutan;
            this.isBertahan = false;
        }

    }
}
