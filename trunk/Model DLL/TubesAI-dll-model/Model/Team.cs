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
        private const int jumlahPotion = 10;
        private const int jumlahLifePotion = 5;
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

            n_lifePotion = copyTeam.n_lifePotion;
            n_potion = copyTeam.n_potion;

            foreach (var unit in copyTeam.listUnit)
            {
                if (unit is Archer)
                {
                    listUnit.Add(new Archer(unit.index,unit.getCurrentHP()));
                }
                else if (unit is Swordsman)
                {
                    listUnit.Add(new Swordsman(unit.index, unit.getCurrentHP()));
                }
                else if (unit is Spearman)
                {
                    listUnit.Add(new Spearman(unit.index, unit.getCurrentHP()));
                }
                else if (unit is Medic)
                {
                    listUnit.Add(new Medic(unit.index, unit.getCurrentHP()));
                }
                else if (unit is Rider)
                {
                    listUnit.Add(new Rider(unit.index, unit.getCurrentHP()));
                }
            }
        }

        public Team(int n_archer, int n_rider, int n_spearman, int n_medic, int n_swordsman, int _index)
        {
            index = _index;
            int iter = 0;
            listUnit=new List<Unit>();

            n_lifePotion = jumlahLifePotion;
            n_potion = jumlahPotion;

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
           return n_lifePotion <= 0;
        }

        public bool isPotionRunOut()
        {
            return n_potion <= 0;
        }

        public void ResetPotion()
        {
            n_potion = jumlahPotion;
        }

        public void ResetLifePotion()
        {
            n_lifePotion = jumlahLifePotion;
        }
    }
}
