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
        private List<ElemenAksi> Agent1Action = new List<ElemenAksi>();
        private List<ElemenAksi> Agent2Action = new List<ElemenAksi>();
        private int count;

        /*
         * Output: Pembantukkan agen 1, agen 2, dan pembentukkan teamcontroller.
         */
        public GameController(String dllpath1, String dllpath2, bool flipcoin, 
                                int[] komposisi1, int[] komposisi2, String typeAgent1, String typeAgent2)
        {
            //Inisialisasi TeamController
            Team buffTeam1 = new Team(komposisi1[0], komposisi1[3], komposisi1[2], komposisi1[1], komposisi1[4], 0);
            Team buffTeam2 = new Team(komposisi2[0], komposisi2[3], komposisi2[2], komposisi2[1], komposisi2[4], 1);
            TC = new TeamController(buffTeam1, buffTeam2, flipcoin);

            //Inisialisasi agen 1 dan 2
            Assembly asm1 = Assembly.LoadFile(dllpath1);
            Agent1 = (AgentInterface)asm1.CreateInstance("Algoritma." + typeAgent1);
            Assembly asm2 = Assembly.LoadFile(dllpath2);
            Agent2 = (AgentInterface)asm2.CreateInstance("Algoritma." + typeAgent2);
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
                    return false;
                }
            }
            else if (unit is Medic)
            {
                if (act.aksi == Aksi.menyerang)
                {
                    return false;
                }
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
                    if (iter_act.index_pelaku < 0 || iter_act.index_pelaku > 10 || 
                        iter_act.index_sasaran < 0 || iter_act.index_sasaran > 10 ||
                        iter_act.tim_sasaran < 0 || iter_act.tim_sasaran > 1) 
                        return false;
                    if (isMove[iter_act.index_pelaku] == true) return false;
                    if (!isActionValid(team.FindUnit(iter_act.index_pelaku), iter_act)) return false;
                    else
                        isMove[iter_act.index_pelaku] = true;
                }
            }

            return true;
        }

        /*
         * Fungsi untuk membuang aksi yang tidak valid
         */
        private void buangAksi(Team team, List<ElemenAksi> act)
        {
            bool[] isMove = new bool[11];
            for (int i = 0; i < 11; i++)
                isMove[i] = false;

            if (act.Count() < 12)
            {
                foreach (var iter_act in act)
                {
                    if (!isActionValid(team.FindUnit(iter_act.index_pelaku), iter_act) ||
                        (isMove[iter_act.index_pelaku] == true) ||
                        iter_act.index_pelaku < 0 || iter_act.index_pelaku > 10 ||
                        iter_act.index_sasaran < 0 || iter_act.index_sasaran > 10 ||
                        iter_act.tim_sasaran < 0 || iter_act.tim_sasaran > 1)
                    {
                        iter_act.aksi = Aksi.nothing;
                    }
                    else
                    {
                        isMove[iter_act.index_pelaku] = true;
                    }
                }
            }
            else
            {
                foreach (var iter_act in act)
                {
                    iter_act.aksi = Aksi.nothing;
                }
            }
        }

        /*
         * Output: Memutuskan untuk menyuruh team control menghitung atau tidak.
         */
        public void GameLoop()
        {
            if (!TC.isEndGame())
            {
                // Untuk Agent 1
                Team bufTeam1_1 = new Team(TC.Team1);
                Team bufTeam1_2 = new Team(TC.Team2);
                // Untuk Agent 2
                Team bufTeam2_1 = new Team(TC.Team1);
                Team bufTeam2_2 = new Team(TC.Team2);
                count = 0;
                do
                {
                    Agent1Action = Agent1.Execute(bufTeam1_1, bufTeam1_2);
                    count++;
                } while (!isListActionValid(TC.Team1, Agent1Action) && count < 3);
                buangAksi(TC.Team1, Agent1Action);

                count = 0;
                do
                {
                    Agent2Action = Agent2.Execute(bufTeam2_1, bufTeam2_2);
                    count++;
                } while (!isListActionValid(TC.Team2, Agent2Action) && count < 3);
                buangAksi(TC.Team2, Agent2Action);

                TC.AturGiliran(Agent1Action, Agent2Action);
            }
        }

    }
}
