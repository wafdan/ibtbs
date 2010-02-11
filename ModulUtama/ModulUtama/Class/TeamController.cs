using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TubesAI.Model;

namespace ModulUtama.Class
{
    class TeamController
    {
        private AnimationController AC;
        private Team Team1;
        private Team Team2;
        private bool FirstMove;

        public TeamController(bool firstMove)
        {
            //Inisialisasi AC

            //Inisiaslisai Team

            //Inisialisasi atribut lainnya
            FirstMove = firstMove;
        }
        
        /*
         * Output: semua status team penuh kembali.
         */
        public void ResetTeam()
        {
        }
        
        /*
         * Output: Mengidentifikasi jika permainan sudah berakhir atau belum.
         */
        public bool isEndGame()
        {
            return true;
        }
        
        /*
         * Output: kalkulasi damage unit1 terhadap unit2.
         */
        public void CalculationDamage(Unit unit1, Unit unit2)
        {
        }
        
        /*
         * Output: kalkulasi heal unit1 terhadap unit2.
         */
        public void CalculationHeal(Unit unit1, Unit unit2)
        {
        }
        
        /*
         * Output: kalkulasi ???
         */
        public void CalculationLeft(Unit unit1, Unit unit2)
        {
        }
        
        /*
         * Output: mengatur urutan jalan.
         */
        public void AturGiliran(/*List<AksiAgent> acts*/)
        {
        }

    }
}
