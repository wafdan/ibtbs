using System;
using System.Collections.Generic;
using System.Text;

namespace TubesAI.Model
{
    public class Team
    {
        private int n_lifePotion;
        private int n_potion;
        public List<Unit> listUnit;

        public Team(int n_archer, int n_rider, int n_spearman, int n_medic, int n_swordsman)
        {
            listUnit=new List<Unit>();

            n_lifePotion=10;
            n_potion=10;

            for (int i = 0; i < n_archer; i++)
            {
                listUnit.Add(new Archer());
            }

            for (int i = 0; i < n_swordsman; i++)
            {
                listUnit.Add(new Swordsman());
            }

            for (int i = 0; i < n_spearman; i++)
            {
                listUnit.Add(new Spearman());
            }

            for (int i = 0; i < n_medic; i++)
            {
                listUnit.Add(new Medic());
            }
            
            for (int i = 0; i < n_rider; i++)
            {
                listUnit.Add(new Rider());
            }

        }

        public void useLifePotion()
        {
            n_lifePotion--;
        }

        public void usePotion()
        {
            n_potion--;
        }

        public bool isLifePotionRunOut()
        {
           return n_lifePotion > 0;
        }

        public bool isPotionRunOut()
        {
            return n_potion > 0;
        }

        public void givePotion(int num)
        {
            n_potion = num;
        }

        public void giveLifePotion(int num)
        {
            n_lifePotion = num;
        }
    }
}
