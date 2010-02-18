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
        public bool hasilFlipCoin;

        /*
         * Input: dllpath1(jelas), dllpath2(jelas), komposisi1(jelas), komposisi2(jelas)
         * Output: konstruksi kontroler beruntun
         */
        public MenuController(String dllPath1, String dllPath2, int[] komposisi1, int[] komposisi2, String typeAgent1, String typeAgent2)
        {
            hasilFlipCoin = flipcoin();
            GC = new GameController(dllPath1, dllPath2, hasilFlipCoin, countTeam(komposisi1), countTeam(komposisi2), typeAgent1, typeAgent2);
        }

        /*
         * Input: Array team dalam format: 012340123401
         * Output: Array team dalam ukuran 5 dalam format: <0-jmlArcher><1-jmlSwordsMan><2-jmlSpearman><3-jmlRider><4-jmlMedic>
         */
        private int[] countTeam(int[] komposisi)
        {
            int[] bufferTeam = new int[5];
            foreach (var unit in komposisi)
            {
                switch (unit)
                {
                    case 0:
                        {
                            bufferTeam[0]++;
                            break;
                        }
                    case 1:
                        {
                            bufferTeam[1]++;
                            break;
                        }
                    case 2:
                        {
                            bufferTeam[2]++;
                            break;
                        }
                    case 3:
                        {
                            bufferTeam[3]++;
                            break;
                        }
                    case 4:
                        {
                            bufferTeam[4]++;
                            break;
                        }
                }
            }
            return bufferTeam;
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
