using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimmCore.Console
{
    public class Menu
    {
        private List<MenuItem> items = new List<MenuItem>();
        private int selected = 0;
        private String header;
        private bool listen;
        private Action<ConsoleKey> keyClick;
        private String rewriteAfter;

        public Menu(String header)
        {
            this.header = header;

        }
        public Menu AddExtaListener(Action<ConsoleKey> keyClick)
        {
            this.keyClick = keyClick;
            return this;
        }
        public Menu AddOption(MenuItem menuItem)
        {
            items.Add(menuItem);
            return this;
        }
        public Menu AddOption(String text, Action onSelect)
        {
            return AddOption(new MenuItem(text, onSelect));
        }
        public void StopListen() { this.listen = false; }

        public void Listen()
        {
            listen = true;
            while (listen)
            {
                var ch = System.Console.ReadKey(false).Key;
                keyClick?.Invoke(ch);

                if (ch != ConsoleKey.UpArrow && ch != ConsoleKey.DownArrow)
                {
                    ClearRewriteAfter();
                    Draw();
                }

                if (listen)
                    switch (ch)
                    {
                        case ConsoleKey.UpArrow:
                            Up();
                            Draw();
                            break;

                        case ConsoleKey.DownArrow:
                            Down();
                            Draw();
                            break;

                        case ConsoleKey.Enter:
                            Select();
                            break;
                        default:
                            Draw();
                            break;
                    }
            }

        }
        public Menu Start()
        {
            Draw();
            Listen();
            return this;
        }
        public void ClearRewriteAfter() { this.rewriteAfter = null; }

        public void Select()
        {
            ClearRewriteAfter();
            items[selected].Select();
            StopListen();
        }
        public void SetRewriteAfter(String rewriteAfter) { this.rewriteAfter = rewriteAfter; }
        public void Draw()
        {
            System.Console.Clear();
            System.Console.WriteLine("-------------------------------");
            System.Console.WriteLine(this.header);
            System.Console.WriteLine("-------------------------------");
            for (var i = 0; i < items.Count; i++)
            {
                bool current = i == selected;
                if (current)
                    System.Console.Write("[");
                else
                    System.Console.Write(" ");

                System.Console.Write(items[i].GetText());

                if (current)
                    System.Console.Write("]");

                System.Console.WriteLine();
            }
            System.Console.WriteLine("-------------------------------");
            System.Console.WriteLine();

            if (this.rewriteAfter != null)
                System.Console.WriteLine(this.rewriteAfter);
        }

        private void Down()
        {

            this.selected++;

            if (this.selected >= items.Count)
                this.selected = 0;
        }

        private void Up()
        {

            this.selected--;

            if (this.selected < 0)
                this.selected = items.Count - 1;
        }

    }
    public class MenuItem
    {
        private String text;
        private Action onSelect;

        public MenuItem(String text, Action onSelect)
        {
            this.text = text;
            this.onSelect = onSelect;
        }

        public void Select()
        {
            onSelect?.Invoke();
        }
        public String GetText() { return this.text; }

    }
}
