using System;
using System.Collections.Generic;
using System.Text;

namespace TubesAI.Model
{
    /// <summary>
    /// Kelas yang akan dilempar 
    /// </summary>
    public class AksiAgent
    {
        public struct ElemenAksi
        {
            public int index_sasaran;
            public Aksi aksi;
            public string kenapa;
        }

        

        public List<ElemenAksi> listAksi;

        public AksiAgent()
        {
            listAksi = new List<ElemenAksi>();
        }
    }
}
