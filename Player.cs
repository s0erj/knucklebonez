using System;

namespace Knucklebones
{
    public class Player
    {
        string playerName;
        bool initiative = false;

        public string PlayerName { get => playerName; set => playerName = value; }
        public bool Initiative { get => initiative; set => initiative = value; }

        // Konstruktor
        public Player() { }
        public Player(string name)
        {
            playerName = name;
        }

        // public Methoden
        public static (Player, Player) PlayerCreation()
        {
            Console.Clear();
            Console.WriteLine("Player 1 name:");
            Player p1 = new Player(Console.ReadLine());
            Random rnd = new Random();
            if (Convert.ToBoolean(rnd.Next(0, 2))) p1.initiative = true;
            Console.WriteLine("Player 2 name:");
            Player p2 = new Player(Console.ReadLine());
            if (!p1.initiative) p2.initiative = true;
            return (p1, p2);
        }
        public bool Turn(Board b, Board bEnemy, Player enemy)
        {
            Dice d = ThrowNewDice();
            Insert(d, b, bEnemy);
            if (CheckIfGameOver(b, bEnemy, this, enemy))
            {
                return false;
            }
            EndInitiative(enemy);
            return true;
        }

        // private

        void EndInitiative(Player enemy)
        {
            initiative = false;
            enemy.initiative = true;
        }
        Dice ThrowNewDice()
        {
            Dice d = new Dice();
            Console.WriteLine(playerName + " rolled a " + d.Number + "!");
            return d;
        }
        int MakeChoice()
        {
            //Console.WriteLine("Make a Choice[a,s,d]:");
            while (true)
            {
                string column = Console.ReadLine();
                if (column == "a") return 0;
                if (column == "s") return 1;
                if (column == "d") return 2;
            }
        }   

        void DestroyEnemyDices(Dice d, Board bEnemy, int column)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!(bEnemy.Columns[column, i] is null) && bEnemy.Columns[column, i].Number == d.Number)
                    bEnemy.Columns[column, i] = null;
            }
        }
        void Insert(Dice d, Board b, Board bEnemy)
        {
            bool inserting = true;
            while (inserting)
            {
                int column = MakeChoice();
                // hier
                if (b.Columns[column, 2] is null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (b.Columns[column, i] is null)
                        {
                            b.Columns[column, i] = d;
                            break;
                        }
                    }
                    DestroyEnemyDices(d, bEnemy, column);
                    bEnemy.DicesFallDown(column);
                    inserting = false;


                }
                Console.WriteLine("This column is already full!\nPlease choose another one.");
            }
        }
        bool CheckIfGameOver(Board b, Board b2, Player p, Player p2)
        {
            if (!(b.Columns[0, 2] == null) && !(b.Columns[1, 2] == null) && !(b.Columns[2, 2] == null))
            {
                Console.Clear();
                Console.WriteLine("Game Over!");
                if (b.GetScore() > b2.GetScore()) Console.WriteLine("Player {0} won the game! Gratulations!", p.playerName);
                else if (b.GetScore() == b2.GetScore()) Console.WriteLine("No one won! Sorry.");
                else Console.WriteLine("Player {0} won the game! Gratulations!", p2.playerName);

                Console.Read();
                return true;
            }
            return false;
        }

    }
}
