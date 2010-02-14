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
        public static Vector2[] places = new Vector2[22];
        private static Queue<toDraw> DList = new Queue<toDraw>();
        public static int waitFrame;

        private class toDraw
        {
            public Texture2D textDraw;
            public Rectangle sourceDraw;
            public int indexDraw;

            public toDraw(Texture2D _textDraw, Rectangle _sourceDraw, int _indexDraw)
            {
                textDraw = _textDraw;
                sourceDraw = _sourceDraw;
                indexDraw = _indexDraw;
            }
        }

        /// <summary>
        /// looping draw for game
        /// </summary>
        public static void draw(SpriteBatch spritebatch)
        {
            if (DList.Count > 0)
            {
                toDraw FList;
                spritebatch.Begin();
                while (DList.Count > 0 && (DList.First().sourceDraw.Y == 0 //Wait
                        || DList.First().sourceDraw.Y == 6 * 80 //Dead
                        || DList.First().sourceDraw.Y == 3 * 80) //Attacked
                        )
                {
                    FList = DList.Dequeue();
                    spritebatch.Draw(FList.textDraw, places[FList.indexDraw], FList.sourceDraw, Color.White);
                }
                if (DList.Count > 0)
                {
                    FList = DList.Dequeue();
                    spritebatch.Draw(FList.textDraw, places[FList.indexDraw], FList.sourceDraw, Color.White);
                    Console.WriteLine(FList.indexDraw);
                }
                spritebatch.End();
                
            }
        }

        /// <summary>
        /// add a draw command to list
        /// </summary>
        public static void QDList(Texture2D texture, Rectangle source, int index)
        {
            DList.Enqueue(new toDraw(texture, source, index));
        }

        public static void DrawPoint(int poin)
        {
            //buat prosedur untuk ngegambar point di koordinat yang tetap
            //kalo pointnya minus berarti miss
        }

    }
}
