using System;
using System.Collections.Generic;
using System.Text;

namespace Knucklebones
{
    public class Board
    {
        private Dice[,] columns = new Dice[3, 3];

        public Dice[,] Columns { get => columns; }

        // Konstruktor

        // Methoden
        public static void ShowBoards(Board b1, Board b2, Player p1, Player p2)
        {
            Console.Clear();
            if (p1.Initiative) Console.WriteLine("{0} *", p1.PlayerName);
            else Console.WriteLine(p1.PlayerName);

            Console.WriteLine(" {0} | {1} | {2}", b1.GetFieldString(0, 2), b1.GetFieldString(1, 2), b1.GetFieldString(2, 2));
            Console.WriteLine(" {0} | {1} | {2}", b1.GetFieldString(0, 1), b1.GetFieldString(1, 1), b1.GetFieldString(2, 1));
            Console.WriteLine(" {0} | {1} | {2}", b1.GetFieldString(0, 0), b1.GetFieldString(1, 0), b1.GetFieldString(2, 0));
            Console.WriteLine("------------");
            Console.WriteLine(" {0} | {1} | {2} | Score: {3}", b1.GetColumnScore(0), b1.GetColumnScore(1), b1.GetColumnScore(2), b1.GetScore());
            Console.WriteLine("------------\n");

            if (p2.Initiative) Console.WriteLine("{0} *", p2.PlayerName);
            else Console.WriteLine(p2.PlayerName);

            Console.WriteLine(" {0} | {1} | {2}", b2.GetFieldString(0, 0), b2.GetFieldString(1, 0), b2.GetFieldString(2, 0));
            Console.WriteLine(" {0} | {1} | {2}", b2.GetFieldString(0, 1), b2.GetFieldString(1, 1), b2.GetFieldString(2, 1));
            Console.WriteLine(" {0} | {1} | {2}", b2.GetFieldString(0, 2), b2.GetFieldString(1, 2), b2.GetFieldString(2, 2));
            Console.WriteLine("------------");
            Console.WriteLine(" {0} | {1} | {2} | Score: {3}", b2.GetColumnScore(0), b2.GetColumnScore(1), b2.GetColumnScore(2), b2.GetScore());
            Console.WriteLine("------------\n");
        }

        public void ResetBoard()
        {
            // sinnvoll?
            columns = new Dice[3, 3];
        }
        public int GetColumnScore(int column)
        {
            Dice row0 = Columns[column, 0];
            Dice row1 = Columns[column, 1];
            Dice row2 = Columns[column, 2];

            // no dices in column
            if (row0 == null) return 0;
            // one dice in column
            else if (row1 == null) return row0.Number;
            // two dices in column
            else if (row2 == null)
            {
                // both same
                if (row0.Number.Equals(row1.Number)) return 2 * (row0.Number + row1.Number);
                // not same
                else return row0.Number + row1.Number;
            }
            else
            {
                // all 3 same
                if (row0.Number.Equals(row1.Number) && row0.Number.Equals(row2.Number)) return 9 * row0.Number;
                // first and second are same
                else if (row0.Number.Equals(row1.Number)) return 2 * (row0.Number + row1.Number) + row2.Number;
                // second and third are same
                else if (row1.Number.Equals(row2.Number)) return row0.Number + 2 * (row1.Number * row2.Number);
                // first and third are same
                else if (row0.Number.Equals(row2.Number)) return 2 * (row0.Number + row2.Number) + row1.Number;
                // all diffrent
                else return row0.Number + row1.Number + row2.Number;
            }
        }

        public int GetScore()
        {
            return GetColumnScore(0) + GetColumnScore(1) + GetColumnScore(2);
        }

        string GetFieldString(int i, int j)
        {
            if (columns[i, j] == null) return " ";
            else return Convert.ToString(columns[i, j].Number);
        }

        /*public void Anzeigen()
        {
            Console.WriteLine(" {0} | {1} | {2}", GetFieldString(0,2), GetFieldString(1, 2), GetFieldString(2, 2));
            Console.WriteLine(" {0} | {1} | {2}", GetFieldString(0,1), GetFieldString(1, 1), GetFieldString(2, 1));
            Console.WriteLine(" {0} | {1} | {2}", GetFieldString(0, 0), GetFieldString(1, 0), GetFieldString(2, 0));
            Console.WriteLine("------------");
            Console.WriteLine(" {0} | {1} | {2} | {3}", GetColumnScore(0), GetColumnScore(1), GetColumnScore(2), GetScore());
            Console.WriteLine("------------\n");
        }*/

        public void DicesFallDown(int column)
        {
            for (int i = 1; i < 3; i++)
            {
                if (!(Columns[column, i] == null) && Columns[column, i-1] == null)
                {
                    Columns[column, i - 1] = Columns[column, i];
                    Columns[column, i] = null;
                }
            }
        }
    }
}
