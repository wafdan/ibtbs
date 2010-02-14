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

    /// <summary>
    /// Controller yang mengatur giliran unit untuk melakukan aksi,
    /// Kalkulasi setiap aksi, dan menyuruh Animation Controller untuk menggambar tampilan game
    /// </summary>
    class TeamController
    {
        #region properties
        /// <summary>
        /// Animation Controller
        /// </summary>
        private AnimationController AC;
        /// <summary>
        /// Team 1
        /// </summary>
        public Team Team1 {get; private set;}
        /// <summary>
        /// Team 2
        /// </summary>
        public Team Team2 { get; private set; }
        /// <summary>
        /// Jika bernilai true maka Team 1 menyerang dahulu, 
        /// jika bernilai false maka Team 2 menyerang dahulu.
        /// </summary>
        private bool FirstMove;
        /// <summary>
        /// Damage normal = 200 poin
        /// </summary>
        private const int Damage = 200;
        /// <summary>
        /// Heal normal = 500 poin
        /// </summary>
        private const int Heal = 500;
        /// <summary>
        /// Jumlah item normal = 10 item
        /// </summary>
        private const int numitem = 10;
        #endregion

        #region constructors

        /// <param name="te1">Team 1</param>
        /// <param name="te2">Team 2</param>
        /// <param name="firstMove">
        /// boolean yang menentukan Team mana yang jalan duluan
        /// jika bernilai true, Team 1 menyerang dahulu,
        /// jika bernilai false, Team 2 menyerang dahulu.
        /// </param>
        public TeamController(Team te1,Team te2,bool firstMove)
        {
            //Inisiaslisasi Team
            Team1 = te1;
            Team2 = te2;
            
            // Inisialisasi Animation Controller
            AC = new AnimationController(Team1, Team2);

            //Inisialisasi atribut lainnya
            FirstMove = firstMove;
        }
        #endregion

        #region methods

        /// <summary>
        /// Memberikan Unit pada Team team dengan index ke-index
        /// </summary>
        /// <param name="team">team Unit berada</param>
        /// <param name="index">index unit dari 0-10</param>
        /// <returns></returns>
        public static Unit FindUnit(Team team, int index)
        {
            foreach (Unit un in team.listUnit)
            {
                if (team.listUnit.FindIndex(re => re == un) == index) return un;
            }
            return null;
        }

        /// <summary>
        /// Memberikan ElemenAksi pada listelemenaksi pada index ke-index
        /// </summary>
        /// <param name="listelemenaksi">list of ElemenAksi</param>
        /// <param name="index">integer index</param>
        /// <returns></returns>
        private static ElemenAksi FindElemenAksi(List<ElemenAksi> listelemenaksi,int index)
        {
            foreach (ElemenAksi el in listelemenaksi)
            {
                if (listelemenaksi.FindIndex(re => re == el) == index) return el;
            }
            return null;
        }


        /// <summary>
        /// Me-restore semua Unit pada masing-masing Team.
        /// Membekali masing-masing Team dengan 10 potion dan 10 life Potion.
        /// </summary>
        public void ResetTeam()
        {
            // Fullkan HP seluruh unit pada Team 1 dan Team 2
            foreach (Unit un in Team1.listUnit)
            {
                un.setHP(un.getMaxHP());
            }
            foreach (Unit un in Team2.listUnit)
            {
                un.setHP(un.getMaxHP());
            }
            // beri 10 potion dan 10 life potion
            Team1.giveLifePotion(numitem);
            Team1.givePotion(numitem);
            Team2.giveLifePotion(numitem);
            Team2.givePotion(numitem);
        }
        
        /// <summary>
        /// Mengembalikan true jika ada team yang menang.
        /// </summary>
        /// <returns>boolean</returns>
        public bool isEndGame()
        {
            // Cek apakah ada team yang menang 
            int count = 0;
            foreach (Unit un in Team1.listUnit)
            {
                if (!un.isDead())
                {
                }
            }

            foreach (Unit un in Team2.listUnit)
            {
            }
            return true;
        }


        /// <summary>
        /// Kalkulasi damage yang dihasilkan dari Unit satu kepada Unit dua
        /// </summary>
        /// <param name="satu">Unit yang menyerang</param>
        /// <param name="dua">Unit yang diserang</param>
        public void CalculationDamage(Unit satu,Unit dua)
        {
            int DamageTaken = Damage;

            if (satu is Archer)
            {
                if (dua is Rider)//kelemahan
                    DamageTaken /= 2;
                else if ((dua is Swordsman) || (dua is Medic)) //kuat
                    DamageTaken = (int)(DamageTaken * 1.5);
            }
            else if (satu is Swordsman)
            {
                if (dua is Archer)//kelemahan
                    DamageTaken /= 2;
                else if ((dua is Spearman) || (dua is Medic)) //kuat
                    DamageTaken = (int)(DamageTaken * 1.5);
            }
            else if (satu is Spearman)
            {
                if (dua is Swordsman)//kelemahan
                    DamageTaken /= 2;
                else if ((dua is Rider) || (dua is Medic)) //kuat
                    DamageTaken = (int)(DamageTaken * 1.5);
            }
            else if (satu is Rider)
            {
                if (dua is Spearman)//kelemahan
                    DamageTaken /= 2;
                else if ((dua is Archer) || (dua is Medic)) //kuat
                    DamageTaken = (int)(DamageTaken * 1.5);
            }
            if (dua.isBertahan) DamageTaken /= 2;
            dua.setHP(dua.getCurrentHP() - DamageTaken);
        }
        
        /// <summary>
        /// Kalkulasi Heal dari Unit satu kepada Unit dua
        /// </summary>
        /// <param name="satu">Unit yang heal</param>
        /// <param name="dua">Unit yang diheal</param>
        public void CalculationHeal(Unit satu,Unit dua)
        {
            dua.setHP(dua.getCurrentHP() + Heal);
            if (dua.getCurrentHP() > dua.getMaxHP()) dua.setHP(dua.getMaxHP());
        }
        
        /// <summary>
        /// Kalkulasi Life potion yang diberikan dari Unit satu kepada Unit dua
        /// </summary>
        /// <param name="satu">Unit yang memberi life potion</param>
        /// <param name="dua">Unit yang diberi life potion</param>
        public void CalculationLife(Unit satu,Unit dua)
        {
            dua.setHP((int)(0.5 * dua.getMaxHP()));
        }
        
        /// <summary>
        /// Mengatur giliran Unit mana dulu yang berjalan dahulu
        /// </summary>
        /// <param name="actsTeam1">List aksi Team 1</param>
        /// <param name="actsTeam2">List aksi Team 2</param>
        public void AturGiliran(List<ElemenAksi> actsTeam1, List<ElemenAksi> actsTeam2)
        {
            /*************************************************/
            // Inisialisasi boolean unit yang melakukan aksi
            bool[] team1 = new bool[11];
            bool[] team2 = new bool[12];
            Unit unitaktif,unitpasif;
            ElemenAksi aksi;
            int i = 0;
            int count = 0;
            for (i = 0; i < 11; i++)
            {
                unitaktif = FindUnit(Team1, i);
                aksi = FindElemenAksi(actsTeam1,i);
                if (unitaktif.isDead())
                    team1[i] = true;
                else
                {
                     team1[i] = false;
                     count++;
                }

                unitaktif = FindUnit(Team2, i);
                if (unitaktif.isDead())
                    team2[i] = true;
                else
                {
                    team2[i] = false;
                    count++;
                }
            }
            /*************************************************/

            // Pilih Unit yang akan Pilih
            for (i = 0; i <= count; i++)
            {
                
                // Setelah tidak ada unit yang bertahan yang dapat dipilih, mulai pilih dari yang tercepat hingga terlambat
                // Setiap pemilihan unit, cek apakah unit masih hidup
                /*************************************************/
            }
            
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

            int iter = 0;
            foreach (var unit in actsTeam1)
            {
                iter %= 11;
                AC.Attack(unit.index_pelaku, iter + 11, 0, false);
                iter++;
            }
        }

        #endregion

    }
}
