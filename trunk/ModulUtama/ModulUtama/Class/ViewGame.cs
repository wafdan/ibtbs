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

        public static void draw(SpriteBatch spritebatch, Texture2D texture, Rectangle source, int index)
        {
            int x = 0; //nanti x dan y nya diset tergantung posisi si karakter ke-index.. mudah-mudahan :p
            int y = 0;
            Rectangle destination = new Rectangle(x, y, 50, 80);
            spritebatch.Begin();
            spritebatch.Draw(texture, destination, source, Color.White);
            spritebatch.End();
        }

    }
}
