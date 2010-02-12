using System;
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
            return new String('a', 10);
        }

        /*
         * Output: Hasil parsing dari file komposisi P1 (Perlu divalidasi?)
         */
        public static int[] kload()
        {
            return new int[10];
        }

    }
}
