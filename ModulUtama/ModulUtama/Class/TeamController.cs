using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TubesAI.Model;

namespace ModulUtama.Class
{
    /*
       Jenis2 Set image:
        AC.Attack(ElemenAksi A,int poin,bool miss);
        AC.Kill(ElemenAksi A,int poin);
        AC.Defend(ElemenAksi A);
        AC.Heal(ElemenAksi A,int poin,int miss);
        AC.UseItem(ElemenAksi A,int poin,int miss);
        AC.Win(int Team);
    */

    class TeamController
    {
        #region properties
        private AnimationController AC;
        private Team Team1;
        private Team Team2;
        private bool FirstMove;
        #endregion

        #region constructors
        public TeamController(bool firstMove)
        {
            //Inisialisasi AC

            //Inisiaslisai Team

            //Inisialisasi atribut lainnya
            FirstMove = firstMove;
        }
        #endregion

        #region methods
        /*
         * Output: semua status team penuh kembali.
         */
        public void ResetTeam()
        {
            // Fullkan HP seluruh unit pada Team 1 dan Team 2
            // Bekali 10 potion dan 10 life potion
        }
        
        /*
         * Output: Mengidentifikasi jika permainan sudah berakhir atau belum.
         */
        public bool isEndGame()
        {
            // Cek apakah ada team yang menang 
            // Jika ada,
            //		Set image untuk kemenangan
            //		return true
            // Jika tidak ada,
            //		return false
            return true;
        }

        /*
         * Output: Memberikan Unit pada Team team dengan index ke-index. 
         */
        public Unit FindUnit(Team team, int index)
        {
            foreach (Unit un in team.listUnit)
            {
                if (team.listUnit.FindIndex(re => re == un) == index) return un;
            }
            return null;
        }

        /*
         * Output: kalkulasi damage unit1 terhadap unit2.
         */
        public void CalculationDamage(int idx1, int idx2)
        {
            Unit satu = FindUnit(Team1, idx1);
            Unit dua = FindUnit(Team2, idx2);
            int damage = 200;

            if (satu is Archer)
            {
                if (dua is Rider)//kelemahan
                    damage /= 2;
                else if ((dua is Swordsman) || (dua is Medic)) //kuat
                    damage = (int)(damage * 1.5);
            }
            else if (satu is Swordsman)
            {
                if (dua is Archer)//kelemahan
                    damage /= 2;
                else if ((dua is Spearman) || (dua is Medic)) //kuat
                    damage = (int)(damage * 1.5);
            }
            else if (satu is Spearman)
            {
                if (dua is Rider)//kelemahan
                    damage /= 2;
                else if ((dua is Rider) || (dua is Medic)) //kuat
                    damage = (int)(damage * 1.5);
            }
            else if (satu is Rider)
            {
                if (dua is Spearman)//kelemahan
                    damage /= 2;
                else if ((dua is Archer) || (dua is Medic)) //kuat
                    damage = (int)(damage * 1.5);
            }
            if (dua.isBertahan) damage /= 2;
            dua.setHP(dua.getCurrentHP() - damage);
        }
        
        /*
         * Output: kalkulasi heal unit1 terhadap unit2.
         */
        public void CalculationHeal(int idx1, int idx2)
        {
            Unit satu = FindUnit(Team1, idx1);
            Unit dua = FindUnit(Team2, idx2);

            dua.setHP(dua.getCurrentHP() + 500);
            if (dua.getCurrentHP() > dua.getMaxHP()) dua.setHP(dua.getMaxHP());
        }
        
        /*
         * Output: kalkulasi life potion yang diberikan pada unit2.
         */
        public void CalculationLife(int idx1, int idx2)
        {
            Unit satu = FindUnit(Team1, idx1);
            Unit dua = FindUnit(Team2, idx2);

            dua.setHP((int)(0.5 * dua.getMaxHP()));
        }
        
        /*
         * Output: mengatur urutan jalan.
         */
        public void AturGiliran(/*List<AksiAgent> actsTeam1, List<AksiAgent> actsTeam2*/)
        {
            // Terima List<ElemenAksi> dari GameController
            // Hitung semua unit yang masih hidup dari Team 1 dan Team 2, dapet TotalUnit
            // Loop dari 1 hingga TotalUnit
            // Pilih Unit yang akan dijalankan
            //      Pilih seluruh unit yang bertahan untuk jalan dahulu
            //      Setelah tidak ada unit yang bertahan yang dapat dipilih, mulai pilih dari yang tercepat hingga terlambat
            //      Setiap pemilihan unit, cek apakah unit masih hidup
            // Jalankan unit yang dipilih:
            //  Jika unit attack,
            //      Jika unit yang diattack belum mati
            //          Panggil Calculation
            //          Set image untuk kasus ini
            //      Jika unit yang diattack sudah mati
            //          Set image untuk kasus ini
            //  Jika unit bertahan,
            //		Set image untuk kasus ini
            //  Jika unit heal,
            //      Jika unit yang diheal belum mati
            //          Tambahkan HP pada unit yang diheal
            //          Set image untuk kasus ini
            //      Jika unit yang diheal sudah mati
            //          Set image untuk kasus ini
            //  Jika unit pake potion,
            //		Jika potion masih ada, 
            //      	Jika unit yang dikasih potion belum mati
            //          	Tambahkan HP pada unit yang dikasih potion
            //          	Kurangi jumlah potion pada tim
            //          	Set image untuk kasus ini
            //      	Jika unit yang dikasih potion sudah mati
            //          	Kurangi jumlah potion pada tim ?? Kesepakatan ??
            //          	Set image untuk kasus ini
            //		Jika potion tidak ada,
            //			Set image untuk kasus ini
            // Jika unit pake life potion,
            //		Jika life potion masih ada,
            //      	Jika unit yang dikasih life potion belum mati
            //          	Kurangi jumlah life potion pada tim ?? Kesepakatan ??
            //          	Set image untuk kasus ini
            //      	Jika unit yang dikasih life potion sudah mati
            //          	Tambahkan 50% dari max HP pada unit yang dikasih life potion 
            //          	Kurangi jumlah life potion pada tim
            //          	Set image untuk kasus ini
            //		Jika life potion tidak ada,
            //			Set image untuk kasus ini
            // Jika do nothing,
            //		Set image untuk kasus ini
        }

        #endregion

    }
}
