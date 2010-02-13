using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TubesAI.Model;

namespace ModulUtama.Class
{
    class AnimationController
    {
        private Team Team1;
        private Team Team2;
        int CurrentFrame = 0;
        private Texture2D Sender;
        private Texture2D Receiver;
        private Rectangle[] Char1 = new Rectangle[7];
        private Rectangle[] Char2 = new Rectangle[7];

        public AnimationController(Team A, Team B)
        {
        }

        public Team FindTeam(int index)
        {
            if (index < 11)
                return Team1;
            else
                return Team2;
        }

        public void SetTexture (Unit U, Texture2D CharImg)
        {
            //gimana caranya nge set texture2d, bisa langsung di = kah? :p
            if (U is Archer)
            {
                CharImg = Archer.texture;
            }
            else if (to is Rider)
            {
                CharImg = Rider.texture;
            }
            else if (to is Spearman)
            {
                CharImg = Spearman.texture;
            }
            else if (to is Medic)
            {
                CharImg = Medic.texture;
            }
            else
            {
                CharImg = Swordsman.texture;
            }
        }

        public void Attack(ElemenAksi EA, int poin, bool miss)
        {
            Unit from = TeamController.FindUnit(FindTeam(EA.index_pelaku), EA.index_pelaku);
            Unit to = TeamController.FindUnit(FindTeam(EA.index_sasaran), EA.index_sasaran);

            SetTexture(from, Sender);
            SetTexture(to, Receiver);

            for (int i = 0; i <= 3; i++)
            {
                Char1[i] = new Rectangle(CurrentFrame * 50, 160, 50, 80);
                Char2[i] = new Rectangle(CurrentFrame * 50, 0, 50, 80);
            }
            for (int i = 4; i <= 7; i++)
            {
                Char1[i] = new Rectangle((CurrentFrame - 3) * 50, 0, 50, 80);
                if (miss == 1)
                    Char2[i] = new Rectangle((CurrentFrame - 3) * 50, 0, 50, 80);
                else
                    Char2[i] = new Rectangle((CurrentFrame - 3) * 50, 240, 50, 80);
            }

            //set gambar unit 1 menyerang
            ViewGame.draw(Game.spriteBatch, Sender, Char1[CurrentFrame], EA.index_pelaku);
            ViewGame.draw(Game.spriteBatch, Receiver, Char2[CurrentFrame], EA.index_sasaran);

            
            if (miss == 1)
            {
                if (CurrentFrame > 3)
                    ViewGame.DrawPoint(-1);
            }
            else
            {
                if (CurrentFrame > 3)
                    ViewGame.DrawPoint(poin);
                if (CurrentFrame == 7)
                {   
                    UpdateHealth(EA.index_sasaran, poin * (-1));
                    CurrentFrame = 0;
                }
            }
        }

        public void Kill(ElemenAksi EA, int poin)
        {
            Unit from = TeamController.FindUnit(FindTeam(EA.index_pelaku), EA.index_pelaku);
            Unit to = TeamController.FindUnit(FindTeam(EA.index_sasaran), EA.index_sasaran);

            SetTexture(from, Sender);
            SetTexture(to, Receiver);

            for (int i = 0; i <= 3; i++)
            {
                Char1[i] = new Rectangle(CurrentFrame * 50, 160, 50, 80);
                Char2[i] = new Rectangle(CurrentFrame * 50, 0, 50, 80);
            }
            for (int i = 4; i <= 7; i++)
            {
                Char1[i] = new Rectangle((CurrentFrame - 3) * 50, 0, 50, 80);
                Char2[i] = new Rectangle((CurrentFrame - 3) * 50, 480, 50, 80);
            }

            ViewGame.draw(Game.spriteBatch, Sender, Char1[CurrentFrame], EA.index_pelaku);
            ViewGame.draw(Game.spriteBatch, Receiver, Char2[CurrentFrame], EA.index_sasaran);

            if (CurrentFrame > 3)
                ViewGame.DrawPoint(poin);
            if (CurrentFrame == 7)
            {
                UpdateHealth(EA.index_sasaran, poin * (-1));
                CurrentFrame = 0;
            }
        }

        public void Defend(ElemenAksi EA)
        {
            Unit pelaku = TeamController.FindUnit(FindTeam(EA.index_pelaku), EA.index_pelaku);
            SetTexture(pelaku, Sender);
            for (int i = 0; i <= 3; i++)
                Char1[i] = new Rectangle(CurrentFrame *50, 80, 50, 80);
            ViewGame.draw(Game.spriteBatch, Receiver, Char1[CurrentFrame], EA.index_pelaku);
            if (CurrentFrame == 3)
                CurrentFrame = 0;
        }

        public void Heal(ElemenAksi EA, int poin, int miss)
        {
            Unit from = TeamController.FindUnit(FindTeam(EA.index_pelaku), EA.index_pelaku);
            Unit to = TeamController.FindUnit(FindTeam(EA.index_sasaran), EA.index_sasaran);

            SetTexture(from, Sender);
            SetTexture(to, Receiver);

            for (int i = 0; i <= 3; i++)
            {
                Char1[i] = new Rectangle(CurrentFrame * 50, 160, 50, 80);
                Char2[i] = new Rectangle(CurrentFrame * 50, 0, 50, 80);
            }
            for (int i = 4; i <= 7; i++)
            {
                Char1[i] = new Rectangle((CurrentFrame-3) * 50, 0, 50, 80);
                if (miss == 1)
                    Char2[i] = new Rectangle((CurrentFrame - 3) * 50, 0, 50, 80);
                else
                    Char2[i] = new Rectangle((CurrentFrame - 3) * 50, 320, 50, 80);
            }

            ViewGame.draw(Game.spriteBatch, Sender, Char1[CurrentFrame], EA.index_pelaku);
            ViewGame.draw(Game.spriteBatch, Receiver, Char2[CurrentFrame], EA.index_sasaran);

            if (miss == 1)
            {
                if (CurrentFrame > 3)
                    ViewGame.DrawPoint(-1);
            }
            else
            {
                if (CurrentFrame > 3)
                    ViewGame.DrawPoint(poin);
                if (CurrentFrame == 7)
                {
                    UpdateHealth(EA.index_sasaran, poin);
                    CurrentFrame = 0;
                }
            }
        }

        public void UseItem(ElemenAksi EA, int poin, int miss)
        {
            Unit from = TeamController.FindUnit(FindTeam(EA.index_pelaku), EA.index_pelaku);
            Unit to = TeamController.FindUnit(FindTeam(EA.index_sasaran), EA.index_sasaran);

            SetTexture(from, Sender);
            SetTexture(to, Receiver);

            for (int i = 0; i <= 3; i++)
            {
                if (EA.Item == Item.potion)
                    Char1[i] = new Rectangle(CurrentFrame * 50, 560, 50, 80);
                else
                    Char1[i] = new Rectangle(CurrentFrame * 50, 640, 50, 80);
                Char2[i] = new Rectangle(CurrentFrame * 50, 0, 50, 80);
            }
            for (int i = 4; i <= 7; i++)
            {
                Char1[i] = new Rectangle((CurrentFrame - 3) * 50, 0, 50, 80);
                if (miss == 1)
                    Char2[i] = new Rectangle((CurrentFrame - 3) * 50, 0, 50, 80);
                else
                    if (EA.Item == Item.potion)
                        Char2[i] = new Rectangle((CurrentFrame - 3) * 50, 320, 50, 80);
                    else
                        Char2[i] = new Rectangle((CurrentFrame - 3) * 50, 400, 50, 80);
            }

            ViewGame.draw(Game.spriteBatch, Sender, Char1[CurrentFrame], EA.index_pelaku);
            ViewGame.draw(Game.spriteBatch, Receiver, Char2[CurrentFrame], EA.index_sasaran);

            if (miss == 1)
            {
                if (CurrentFrame > 3)
                    ViewGame.DrawPoint(-1);
            }
            else
            {
                if ((CurrentFrame > 3) && (EA.item==Item.potion))
                    ViewGame.DrawPoint(poin);
                if (CurrentFrame == 7)
                {
                    UpdateHealth(EA.index_sasaran, poin);
                    CurrentFrame = 0;
                }
            }
        }

        public void UpdateHealth(int index, int poin)
        {
            //tambahkan panjang healthbar sesuai jumlah poin
        }

        public void Win(int Team)
        {
            for (int i = 0; i < 11; i++)
            {
                if (Team == 0)
                {
                    //set gambar unit ke-i jadi pose menang
                    //set gambar unit ke-(i+11) jadi pose kalah 
                }
                else
                {

                    //set gambar unit ke-(i+11) jadi pose menang
                    //set gambar unit ke-i jadi pose kalah
                }
            }
        }
    }
}
