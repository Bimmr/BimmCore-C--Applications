//TODO: Add comments
using System;
using System.Collections.Generic;
using System.Linq;

namespace BimmCore.Console
{
    public class ColorWriter
    {

        private Dictionary<int, string> texts = new Dictionary<int, string>();
        private Dictionary<int, ConsoleColor> textsColor = new Dictionary<int, ConsoleColor>();

        private int place;
        private bool save;

        public ColorWriter(string text)
        {
            texts.Add(place, text);
        }

        public ColorWriter(ConsoleColor color)
        {
            textsColor.Add(place, color);
        }

        public ColorWriter(ConsoleColor color, string text)
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
            save = true;
            return this;
        }

        public ColorWriter Next(ConsoleColor color, string text)
        {
            textsColor.Add(place, color);
            Next(text);
            return this;
        }

        public ColorWriter Next(string text)
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


