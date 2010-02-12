using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ModulUtama.Class
{
    class MenuController
    {
        public GameController GC{ get; private set; }

        /*
         * Input: dllpath1(jelas), dllpath2(jelas), komposisi1(jelas), komposisi2(jelas)
         * Output: konstruksi kontroler beruntun
         */
        public MenuController(String dllPath1, String dllPath2, int[] komposisi1, int[] komposisi2, String typeAgent1, String typeAgent2)
        {
            GC = new GameController(dllPath1, dllPath2, flipcoin(), komposisi1, komposisi2, typeAgent1, typeAgent2);
        }

        /*
         * Output: Random, jika true = P1 duluan, false = P2 duluan.
         */
        public bool flipcoin()
        {
            Random rd = new Random();
            int x = rd.Next() % 2;
            if (x == 1)
            {
                return true;
            }
            else //x==0
            {
                return false;
            }
        }

    }
}
