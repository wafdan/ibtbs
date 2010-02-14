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
    /// <summary>
    /// Controller yang menentukan gambar-gambar yang diperlukan
    /// untuk menampilkan animasi aksi unit dalam battle
    /// </summary>
    class AnimationController
    {
        #region properties
        /// <summary>
        /// Team 1
        /// </summary>
        private Team Team1;
        /// <summary>
        /// Team 2
        /// </summary>
        private Team Team2;
        /// <summary>
        /// Mengatur posisi frame animasi saat ini (untuk environment)
        /// </summary>
        public static int CurrentFrame = 0;
        /// <summary>
        /// Menyimpan gambar semua unit
        /// </summary>
        private Texture2D[] Textures = new Texture2D[22];
        /// <summary>
        /// Array posisi gambar untuk keperluan animasi karakter 1
        /// </summary>
        private Rectangle[] Char1 = new Rectangle[5];
        /// <summary>
        /// Array posisi gambar untuk keperluan animasi karakter 2
        /// </summary>
        private Rectangle[] Char2 = new Rectangle[5];
        /// <summary>
        /// Array posisi gambar untuk keperluan animasi karakter yang tidak beraksi
        /// </summary>
        private Rectangle[] CharEnvi = new Rectangle[5];
        #endregion

        #region konstruktor
        public AnimationController(Team A, Team B)
        {
            Team1 = A;
            Team2 = B;
            setTexture();
        }
        #endregion

        public Team FindTeam(int index)
        {
            if (index < 11)
                return Team1;
            else
                return Team2;
        }

        private void setTexture()
        {
            #region menentukan subjek

            foreach (var unit in Team1.listUnit)
            {
                if (unit is Archer)
                {
                    Textures[unit.index] = Archer.texture;
                }
                else if (unit is Swordsman)
                {
                    Textures[unit.index] = Swordsman.texture;
                }
                else if (unit is Spearman)
                {
                    Textures[unit.index] = Spearman.texture;
                }
                else if (unit is Rider)
                {
                    Textures[unit.index] = Rider.texture;
                }
                else if (unit is Medic)
                {
                    Textures[unit.index] = Medic.texture;
                }
            }
            foreach (var unit in Team2.listUnit)
            {
                if (unit is Archer)
                {
                    Textures[unit.index + 11] = Archer.texture;
                }
                else if (unit is Swordsman)
                {
                    Textures[unit.index + 11] = Swordsman.texture;
                }
                else if (unit is Spearman)
                {
                    Textures[unit.index + 11] = Spearman.texture;
                }
                else if (unit is Rider)
                {
                    Textures[unit.index + 11] = Rider.texture;
                }
                else if (unit is Medic)
                {
                    Textures[unit.index + 11] = Medic.texture;
                }
            }
            #endregion
        }

        private void enviView(int _subject)
        {
            foreach (var unit in Team1.listUnit)
            {
                if (unit.index != _subject)
                    if (!unit.isDead())
                        doNothing(unit.index);
                    else
                        Dead(unit.index);
            }
            foreach (var unit in Team2.listUnit)
            {
                if (unit.index != _subject - 11)
                    if (!unit.isDead())
                        doNothing(unit.index+11);
                    else
                        Dead(unit.index+11);
            }
        }

        private void enviView(int _subject, int _object)
        {
            foreach (var unit in Team1.listUnit)
            {
                if (unit.index != _subject && unit.index != _object)
                    if (!unit.isDead())
                        doNothing(unit.index);
                    else
                        Dead(unit.index);
            }

            foreach (var unit in Team2.listUnit)
            {
                if (unit.index != _subject - 11 && unit.index != _object - 11)
                    if (!unit.isDead())
                        doNothing(unit.index+11);
                    else
                        Dead(unit.index+11);
            }
        }

        public void doNothing(int _subject)
        {
            for (int i = 0; i <= 3; i++)
                CharEnvi[i] = new Rectangle(CurrentFrame * 50, 0, 50, 80);
            ViewGame.QDList(Textures[_subject], CharEnvi[CurrentFrame], _subject);
            if (CurrentFrame == 3)
                CurrentFrame = 0;
        }

        public void Dead(int _subject)
        {
            for (int i = 0; i <= 3; i++)
                CharEnvi[i] = new Rectangle(CurrentFrame * 50, 480, 50, 80);
            ViewGame.QDList(Textures[_subject], CharEnvi[CurrentFrame], _subject);
            if (CurrentFrame == 3)
                CurrentFrame = 0;
        }

        public void Attack(int _subject, int _object, int poin, bool miss)
        {
            Char1[0] = new Rectangle(0, 160, 50, 80);
            Char2[0] = new Rectangle(0, 0, 50, 80);
            for (int i = 1; i < 5; i++)
            {
                if (i < 4)
                    Char1[i] = new Rectangle(i * 50, 160, 50, 80);
                else
                    Char1[i] = new Rectangle(0, 0, 50, 80);
                if (miss)
                    Char2[i] = new Rectangle((i % 4) * 50, 0, 50, 80);
                else
                    Char2[i] = new Rectangle((i - 1) * 50, 240, 50, 80);
            }

            //set gambar unit 1 menyerang
            for (int i = 0; i < 5; i++)
            {
                enviView(_subject, _object);
                ViewGame.QDList(Textures[_object], Char2[i], _object);
                ViewGame.QDList(Textures[_subject], Char1[i], _subject);
            }
            
            if (miss)
            {
                if (CurrentFrame > 1)
                    ViewGame.DrawPoint(-1);
            }
            else
            {
                if (CurrentFrame > 1)
                    ViewGame.DrawPoint(poin);
                if (CurrentFrame == 4)
                {   
                    UpdateHealth(_object, poin * (-1));
                    CurrentFrame = 0;
                }
            }
        }

        public void Kill(int _subject, int _object, int poin)
        {
            Char1[0] = new Rectangle(0, 160, 50, 80);
            Char2[0] = new Rectangle(0, 0, 50, 80);
            for (int i = 1; i < 5; i++)
            {
                if (i < 4)
                    Char1[i] = new Rectangle(i * 50, 160, 50, 80);
                else
                    Char1[i] = new Rectangle(0, 0, 50, 80);
                Char2[i] = new Rectangle((i-1) * 50, 480, 50, 80);
            }

            enviView(_subject, _object);
            ViewGame.QDList(Textures[_subject], Char1[CurrentFrame], _subject);
            ViewGame.QDList(Textures[_object], Char2[CurrentFrame], _object);

            if (CurrentFrame > 3)
                ViewGame.DrawPoint(poin);
            if (CurrentFrame == 7)
            {
                UpdateHealth(_object, poin * (-1));
                CurrentFrame = 0;
            }
        }

        public void Defend(int _subject)
        {
            for (int i = 0; i <= 3; i++)
                Char1[i] = new Rectangle(CurrentFrame * 50, 80, 50, 80);

            enviView(_subject);
                ViewGame.QDList(Textures[_subject], Char1[CurrentFrame], _subject);
            
            if (CurrentFrame == 3)
                CurrentFrame = 0;
        }

        public void Heal(int _subject, int _object, int poin, bool miss)
        {
            Char1[0] = new Rectangle(0, 160, 50, 80);
            Char2[0] = new Rectangle(0, 0, 50, 80);
            for (int i = 1; i < 5; i++)
            {
                if (i < 4)
                    Char1[i] = new Rectangle(i * 50, 160, 50, 80);
                else
                    Char1[i] = new Rectangle(0, 0, 50, 80);
                if (miss)
                    Char2[i] = new Rectangle((i % 4) * 50, 0, 50, 80);
                else
                    Char2[i] = new Rectangle((i - 1) * 50, 320, 50, 80);
            }

            enviView(_subject, _object);
            ViewGame.QDList(Textures[_subject], Char1[CurrentFrame], _subject);
            ViewGame.QDList(Textures[_object], Char2[CurrentFrame], _object);

            if (miss)
            {
                if (CurrentFrame > 1)
                    ViewGame.DrawPoint(-1);
            }
            else
            {
                if (CurrentFrame > 1)
                    ViewGame.DrawPoint(poin);
                if (CurrentFrame == 4)
                {
                    UpdateHealth(_object, poin);
                    CurrentFrame = 0;
                }
            }
        }

        public void UseItem(int _subject, int _object, Item _item, int poin, bool miss)
        {
            if (_item == Item.potion)
                Char1[0] = new Rectangle(0, 560, 50, 80);
            else
                Char1[0] = new Rectangle(0, 640, 50, 80);
            Char2[0] = new Rectangle(0, 0, 50, 80);
            for (int i = 1; i < 5; i++)
            {
                if (_item == Item.potion)
                    Char1[i] = new Rectangle(i * 50, 560, 50, 80);
                else
                    Char1[i] = new Rectangle(i * 50, 640, 50, 80);
                if (miss)
                    Char2[i] = new Rectangle((i % 4) * 50, 0, 50, 80);
                else
                    if (_item == Item.potion)
                        Char2[i] = new Rectangle((i - 1) * 50, 320, 50, 80);
                    else
                        Char2[i] = new Rectangle((i - 1) * 50, 400, 50, 80);
            }

            enviView(_subject, _object);
            ViewGame.QDList(Textures[_subject], Char1[CurrentFrame], _subject);
            ViewGame.QDList(Textures[_object], Char2[CurrentFrame], _object);

            if (miss)
            {
                if (CurrentFrame > 1)
                    ViewGame.DrawPoint(-1);
            }
            else
            {
                if ((CurrentFrame > 1) && (_item==Item.potion))
                    ViewGame.DrawPoint(poin);
                if (CurrentFrame == 4)
                {
                    UpdateHealth(_object, poin);
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
