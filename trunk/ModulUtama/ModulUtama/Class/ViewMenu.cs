using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModulUtama.Class
{
    public static class ViewMenu
    {
        /*
         * Output: Path absolut hasil browse file DLL P1 (Perlu divalidasi?)
         */
        public static String dload1()
        {
            return new String('a', 10);
        }

        /*
         * Output: Path absolut hasil browse file DLL P2 (Perlu divalidasi?)
         */
        public static String dload2()
        {
            return new String('b', 10);
        }

        /*
         * Output: Hasil parsing dari file komposisi P1 (Perlu divalidasi?)
         */
        public static int[] kload1()
        {
            return new int[10];
        }

        /*
         * Output: Hasil parsing dari file komposisi P2 (Perlu divalidasi?)
         */
        public static int[] kload2()
        {
            return new int[10];
        }

    }
}
