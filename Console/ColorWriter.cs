/*
 *
 * File Name: ColorWriter - BimmrUtilities
 * 
 * Description: Utilities class to make adding color to a console easier
 * 
 * Revisions:
 *          11/12/15 - Randy Bimm - Created Class
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace BimmCore.Console
{
    public class ColorWriter
    {

        private Dictionary<int, String> texts = new Dictionary<int, string>();
        private Dictionary<int, ConsoleColor> textsColor = new Dictionary<int, ConsoleColor>();

        private int place = 0;
        private Boolean save;

        public ColorWriter() { }

        public ColorWriter(String text)
        {
            texts.Add(place, text);
        }

        public ColorWriter(ConsoleColor color)
        {
            textsColor.Add(place, color);
        }

        public ColorWriter(ConsoleColor color, String text)
        {
            textsColor.Add(place, color);
            Next(text);
        }

        public ColorWriter Next(ConsoleColor color)
        {
            textsColor.Add(place, color);
            return this;
        }
        public ColorWriter Save()
        {
            this.save = true;
            return this;
        }

        public ColorWriter Next(ConsoleColor color, String text)
        {
            textsColor.Add(place, color);
            Next(text);
            return this;
        }

        public ColorWriter Next(String text)
        {
            texts.Add(place, text);
            place++;
            return this;
        }

        public void write()
        {
            ConsoleColor original = System.Console.ForegroundColor;
            for (int i = 0; i < place; i++)
            {
                if (textsColor.ContainsKey(i))
                    System.Console.ForegroundColor = textsColor[i];
                System.Console.Write(texts[i]);
            }
            System.Console.ForegroundColor = original;

            if (!save)
                delete();
        }

        public void writeLine()
        {
            write();
            System.Console.WriteLine();
        }

        private void delete()
        {
            texts = null;
            textsColor = null;
        }

        public static ConsoleColor getRandomColor()
        {
            return (ConsoleColor)new Random().Next(Enum.GetNames(typeof(ConsoleColor)).Length);
        }
        public static ConsoleColor getRandomColor(ConsoleColor[] colors)
        {
            ConsoleColor color;
            do
            {
                color = (ConsoleColor)new Random().Next(Enum.GetNames(typeof(ConsoleColor)).Length);
            }
            while (!colors.Contains(color));

            return color;
        }

    }
}


