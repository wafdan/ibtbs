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
        private bool isActionValid(ElemenAksi act)
        {
            return true;
        }

        /*
         * Output: Memutuskan untuk menyuruh team control menghitung atau tidak.
         */
        public void GameLoop()
        {
            if (!TC.isEndGame())
            {
                Agent1.Execute(TC.Team1, TC.Team2);
                Agent2.Execute(TC.Team2, TC.Team1);
                TC.AturGiliran();
            }
            else
            {
            }
        }

    }
}
