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
        private Rectangle[] Animate = new Rectangle[3];

        public void Attack(ElemenAksi EA,int poin,bool miss)
        {
            Unit from = TeamController.FindUnit(EA.index_pelaku);
            Unit to = TeamController.FindUnit(EA.index_sasaran);
            
            //set gambar unit 1 menyerang
            if (from is Archer)
            {
            }
            else if (from is Rider)
            {
            }
            else if (from is Spearman)
            {
            }
            else
            {
            } 
            
            if (miss == 1)
            {   
                //set tulisan MISS untuk muncul
            }
            else
            {
                //set gambar unit 2 menerima serangan
                if (to is Archer)
                {
                }
                else if (to is Rider)
                {
                }
                else if (to is Spearman)
                {
                }
                else if (to is Medic)
                {
                }
                else
                {
                }
                //set tulisan besar damage
                UpdateHealth(EA.index_sasaran, poin * (-1));
            }
        }

        public void Kill(ElemenAksi EA, int poin)
        {
            Unit from = TeamController.FindUnit(EA.index_pelaku);
            Unit to = TeamController.FindUnit(EA.index_sasaran);
            
            //set gambar unit 1 menyerang
            if (from is Archer)
            {
            }
            else if (from is Rider)
            {
            }
            else if (from is Spearman)
            {
            }
            else
            {
            }

            //set gambar unit 2 terkena serangan
            if (to is Archer)
            {
            }
            else if (to is Rider)
            {
            }
            else if (to is Spearman)
            {
            }
            else if (to is Medic)
            {
            }
            else
            {
            }

            //set tulisan besar damage
            //set gambar unit 2 mati
            UpdateHealth(EA.index_sasaran, poin * (-1));
        }

        public void Defend(ElemenAksi EA)
        {
            Unit pelaku = TeamController.FindUnit(EA.index_pelaku);
            //set gambar unit bertahan
            if (pelaku is Archer)
            {
            }
            else if (pelaku is Spearman)
            {
            }
            else if (pelaku is Medic)
            {
            }
            else if (pelaku is Rider)
            {
            }
            else
            {
            }
        }

        public void Heal(ElemenAksi EA, int poin, int miss)
        {
            Unit from = TeamController.FindUnit(EA.index_pelaku);
            Unit to = TeamController.FindUnit(EA.index_sasaran);

            //set gambar unit 1 heal
            if (miss == 1)
            {
                //set tulisan MISS untuk muncul
            }
            else
            {
                //set gambar unit 2 terkena heal
                if (to is Archer)
                {
                }
                else if (to is Rider)
                {
                }
                else if (to is Spearman)
                {
                }
                else if (to is Medic)
                {
                }
                else
                {
                }
                //set tulisan besar heal point
                UpdateHealth(EA.index_sasaran, poin);
            }
        }

        public void UseItem(ElemenAksi EA, int poin, int miss)
        {
            Unit from = TeamController.FindUnit(EA.index_pelaku);
            Unit to = TeamController.FindUnit(EA.index_sasaran);

            //set gambar unit 1 mengeluarkan item
            if (from is Archer)
            {
            }
            else if (from is Spearman)
            {
            }
            else if (from is Medic)
            {
            }
            else
            {
            }

            if (EA.Item == Item.life_potion)
            {
                if (miss == 1)
                {
                    //set gambar life potion
                    //set tulisan MISS untuk muncul
                }
                else
                {
                    //set gambar life potion
                    //set gambar unit 2 hidup kembali
                    if (to is Archer)
                    {
                    }
                    else if (to is Rider)
                    {
                    }
                    else if (to is Spearman)
                    {
                    }
                    else if (to is Medic)
                    {
                    }
                    else
                    {
                    }

                    UpdateHealth(EA.index_sasaran, poin);
                }
            }
            else if (EA.Item == Item.potion)
            {
                if (miss == 1)
                {
                    //set gambar potion
                    //set tulisan MISS untuk muncul
                }
                else
                {
                    //set gambar potion
                    //set gambar unit 2 terkena heal
                    if (to is Archer)
                    {
                    }
                    else if (to is Rider)
                    {
                    }
                    else if (to is Spearman)
                    {
                    }
                    else if (to is Medic)
                    {
                    }
                    else
                    {
                    }

                    UpdateHealth(EA.index_sasaran, poin);
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
