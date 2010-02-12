using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ModulUtama.Class
{
    class AnimationController
    {
        public void Attack(String ImagePath)
        {
            ViewGame.draw(Game.spriteBatch, TubesAI.Model.Archer.texture);
        }

        public void Kill()
        {
        }

        public void Defend()
        {
        }

        public void Heal()
        {
        }

        public void UseItem()
        {
        }

        public void Win()
        {
        }

    }
}
