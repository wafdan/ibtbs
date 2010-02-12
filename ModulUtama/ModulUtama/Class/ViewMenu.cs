using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ModulUtama.Class
{
    public static class ViewMenu
    {
        /*
         * Output: Path absolut hasil browse file DLL P1 (Perlu divalidasi?)
         */
        public static String dload()
        {
            System.Windows.Forms.OpenFileDialog file = new System.Windows.Forms.OpenFileDialog();
            try
            {
                file.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                file.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return file.FileName;
        }

        /*
         * Output: Hasil parsing dari file komposisi P1 (Perlu divalidasi?)
         */
        public static int[] kload()
        {
            System.Windows.Forms.OpenFileDialog file = new System.Windows.Forms.OpenFileDialog();
            int[] komp = new int[11];

            try
            {
                file.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                file.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(file.FileName))
                {
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    int i = 0;
                    while ((komp[i]=(sr.Read() - 48)) != null)
                    {
                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                // Console.WriteLine("The file could not be read:");
                // Console.WriteLine(e.Message);
            }

            return komp;
        }

    }
}
