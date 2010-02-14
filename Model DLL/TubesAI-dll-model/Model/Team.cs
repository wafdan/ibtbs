using System;
using System.Collections.Generic;
using System.Text;

namespace TubesAI.Model
{
    public class Team
    {
        public int index { get; private set; }
        private int n_lifePotion;
        private int n_potion;
        public List<Unit> listUnit;

        /// <summary>
        /// Memberikan Unit pada Team team dengan index ke-index
        /// </summary>
        /// <param name="team">team Unit berada</param>
        /// <param name="index">index unit dari 0-10</param>
        /// <returns></returns>
        public Unit FindUnit(int index)
        {
            foreach (Unit un in listUnit)
            {
                if (listUnit.FindIndex(re => re == un) == index) return un;
            }
            return null;
        }

        public Team(Team copyTeam)
        {
            index = copyTeam.index;
            listUnit = new List<Unit>();

            n_lifePotion = 10;
            n_potion = 10;

            foreach (var unit in copyTeam.listUnit)
            {
                listUnit.Add(unit);
            }
        }

        public Team(int n_archer, int n_rider, int n_spearman, int n_medic, int n_swordsman, int _index)
        {
            index = _index;
            int iter = 0;
            listUnit=new List<Unit>();

            n_lifePotion=10;
            n_potion=10;

            for (int i = 0; i < n_archer; i++)
            {
                listUnit.Add(new Archer(iter++));
            }

            for (int i = 0; i < n_swordsman; i++)
            {
                listUnit.Add(new Swordsman(iter++));
            }

            for (int i = 0; i < n_spearman; i++)
            {
                listUnit.Add(new Spearman(iter++));
            }

            for (int i = 0; i < n_medic; i++)
            {
                listUnit.Add(new Medic(iter++));
            }
            
            for (int i = 0; i < n_rider; i++)
            {
                listUnit.Add(new Rider(iter++));
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
