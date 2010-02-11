using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KelompokGw
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
		
    public class TeamController
    {
		#region properties
		//  Team Team1
		//  Team Team2
		//  bool firstmove
		//  AnimationController AC
		#endregion
		
		#region constructors
		public TeamController(bool first)
		{
			// Create Team1
			// Create Team2
			// firstmove = first
			// AC.Init(Team1,Team2);
		}
		#endregion
		
		#region methods
        private void ResetTeam()
        {
            // Fullkan HP seluruh unit pada Team 1 dan Team 2
            // Bekali 10 potion dan 10 life potion
        }

        private bool IsEndGame()
        {
			// Cek apakah ada team yang menang 
			// Jika ada,
			//		Set image untuk kemenangan
			//		return true
			// Jika tidak ada,
			//		return false
        }

        private void AturGiliran() // List<ElemenAksi> 
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
        }

		public void CalculationDamage() // Unit1, Unit2
		{
		}
		
		public void CalculationHeal() // Unit1, Unit2
		{
		}
		
		public void CalculationLife() // Unit1, Unit2
		{
		}
		
		#endregion
		
    }
}
