using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public GameController(String dllpath1, String dllpath2, bool flipcoin, int[] komposisi1, int[] komposisi2)
        {
            //Inisialisasi TeamController
            TC = new TeamController(flipcoin);

            //Inisialisasi agen 1 dan 2

        }

        /*
         * Output: true jika sebuah aksi valid, false jika tidak.
         */
        private bool isActionValid(AksiAgent act)
        {
            return true;
        }

        /*
         * Output: Memutuskan untuk menyuruh team control menghitung atau tidak.
         */
        public void GameLoop()
        {
            TC.AturGiliran();
        }

    }
}
