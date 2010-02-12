using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ModulUtama.Class
{
    public static class ViewGame
    {
        public static void draw(SpriteBatch spritebatch, Texture2D texture)
        {
            spritebatch.Begin();
            spritebatch.Draw(texture, Vector2.Zero, Color.White);
            spritebatch.End();
        }

    }
}
