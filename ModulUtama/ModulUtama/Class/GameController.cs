using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TubesAI.Model;

namespace ModulUtama.Class
{
    class GameController
    {
        public TeamController TC { get; private set; }
        private AgentInterface Agent1;
        private AgentInterface Agent2;
        private List<ElemenAksi> Agent1Action;
        private List<ElemenAksi> Agent2Action;
        private int count;

        /*
         * Output: Pembantukkan agen 1, agen 2, dan pembentukkan teamcontroller.
         */
        public GameController(String dllpath1, String dllpath2, bool flipcoin, 
                                int[] komposisi1, int[] komposisi2, String typeAgent1, String typeAgent2)
        {
            //Inisialisasi TeamController
            TC = new TeamController(flipcoin);

            //Inisialisasi agen 1 dan 2
            Assembly asm1 = Assembly.LoadFile(dllpath1);
            Agent1 = (AgentInterface)asm1.CreateInstance("Algoritma." + typeAgent1);
            Assembly asm2 = Assembly.LoadFile(dllpath2);
            Agent2 = (AgentInterface)asm1.CreateInstance("Algoritma." + typeAgent2);
            
        }

        /*
         * Output: true jika sebuah aksi valid, false jika tidak.
         */
        private bool isActionValid(Unit unit, ElemenAksi act)
        {
            if (unit is Archer)
            {
                if (act.aksi == Aksi.heal)
                    return false;
            }
            else if (unit is Swordsman)
            {
                if (act.aksi == Aksi.heal)
                    return false;
            }
            else if (unit is Spearman)
            {
                if (act.aksi == Aksi.heal)
                    return false;
            }
            else if (unit is Rider)
            {
                if (act.aksi == Aksi.heal || act.aksi == Aksi.use_item)
                {
                    Console.WriteLine("Rider Heal");
                    return false;
                }
            }
            else if (unit is Medic)
            {
                if (act.aksi == Aksi.menyerang)
                    return false;
            }
            return true;
        }

        /*
         * Output: true jika sebuah aksi valid, false jika tidak.
         */
        private bool isListActionValid(Team team, List<ElemenAksi> act)
        {
            bool[] isMove = new bool[11];
            for (int i = 0; i < 11; i++)
                isMove[i] = false;

            if (act.Count() > 11)
                return false;
            else
            {
                foreach (var iter_act in act)
                {
                    if (isMove[iter_act.index_pelaku] == true) return false;
                    if (!isActionValid(TeamController.FindUnit(team, iter_act.index_pelaku), iter_act)) return false;
                    else
                        isMove[iter_act.index_pelaku] = true;
                }
            }

            return true;
        }

        /*
         * Output: Memutuskan untuk menyuruh team control menghitung atau tidak.
         */
        public void GameLoop()
        {
            if (!TC.isEndGame())
            {
                count = 0;
                do
                {
                    Agent1Action = Agent1.Execute(TC.Team1, TC.Team2);
                    count++;
                } while (!isListActionValid(TC.Team1, Agent1Action) && count < 3);
                
                count = 0;
                do
                {
                    Agent2Action = Agent2.Execute(TC.Team2, TC.Team1);
                    count++;
                } while (!isListActionValid(TC.Team2, Agent2Action) && count < 3);
                
                Console.WriteLine(Agent1Action.First().tim_sasaran);
                TC.AturGiliran();
            }
        }

    }
}
