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
        private static Queue<toPoint> PList = new Queue<toPoint>();
        private static Queue<toDraw> DList = new Queue<toDraw>();
        public static int drawcount;
        public static int[] health = new int[22];
        public static int[] maxhealth = new int[22]; 

        public class toDraw
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

        private class toPoint
        {
            public int point;
            public int indexDraw;

            public toPoint(int _point, int _indexDraw)
            {
                point= _point;
                indexDraw = _indexDraw;
            }
        }

        /// <summary>
        /// looping draw for game
        /// </summary>
        public static void draw(SpriteBatch spritebatch, SpriteFont spritefont, Game game)
        {
            drawcount = DList.Count;
            if (DList.Count > 0)
            {
                toDraw FList;
                toPoint CurrPoint;
                while (DList.Count > 0 && (DList.First().sourceDraw.Y == 0 //Wait
                        || DList.First().sourceDraw.Y == 6 * 80 //Dead
                        || DList.First().sourceDraw.Y == 3 * 80 //Attacked
                        || DList.First().sourceDraw.Y == 4 * 80 //Healed
                        || DList.First().sourceDraw.Y == 5 * 80 //RES
                        || DList.First().sourceDraw.Y == 1 * 80) //Defend
                        )
                {
                    FList = DList.Dequeue();
                    spritebatch.Draw(FList.textDraw, places[FList.indexDraw], FList.sourceDraw, Color.White);
                }
                if (DList.Count > 0)
                {
                    FList = DList.Dequeue();
                    CurrPoint = PList.Dequeue();
                    spritebatch.Draw(FList.textDraw, places[FList.indexDraw], FList.sourceDraw, Color.White);
                    if (CurrPoint.point != 0)
                    {
                        if(CurrPoint.point > 0)
                            spritebatch.DrawString(spritefont, CurrPoint.point.ToString(), places[CurrPoint.indexDraw], Color.Green);
                        else
                            spritebatch.DrawString(spritefont, CurrPoint.point.ToString(), places[CurrPoint.indexDraw], Color.Red);
                        if (DList.Count % 4 == 1)
                            Game.sound.SFX_ok(game);
                        var buffHealth = health[CurrPoint.indexDraw] + CurrPoint.point / 4;
                        if (buffHealth > maxhealth[CurrPoint.indexDraw])
                            health[CurrPoint.indexDraw] = maxhealth[CurrPoint.indexDraw];
                        else
                            health[CurrPoint.indexDraw] = buffHealth;
                    }
                    else
                    {
                        spritebatch.DrawString(spritefont, "MISS!", places[CurrPoint.indexDraw], Color.Red);
                        if (DList.Count % 4 == 1)
                            Game.sound.SFX_notfound(game);
                    }
                    //Console.WriteLine("ATTACKER:" + FList.indexDraw.ToString() +  "/ DEFENDER:" + CurrPoint.indexDraw.ToString());
                }
            }
        }

        public static void drawBar(SpriteBatch spriteBatch, Texture2D texture)
        {
            for (int i = 0; i < 22; i++)
            {
                spriteBatch.Draw(texture, ViewGame.places[i], new Rectangle(0, 0, health[i] * 50 / maxhealth[i], 10), Color.White);
            }
        }

        /// <summary>
        /// add a draw command to list
        /// </summary>
        public static void QDList(Texture2D texture, Rectangle source, int index)
        {
            DList.Enqueue(new toDraw(texture, source, index));
        }

        public static void DrawPoint(int poin, int index)
        {
            PList.Enqueue(new toPoint (poin, index));
        }

    }
}
