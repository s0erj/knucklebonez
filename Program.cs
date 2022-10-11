using System;

namespace Knucklebones
{
    class Program
    {
        static void Main(string[] args)
        {
            Board bP1 = new Board();
            Board bP2 = new Board();

            // demo
            while (true)
            {
                // Player choose names for board and highscore
                (Player p1, Player p2) = Player.PlayerCreation();

                bool play = true;
                while (play)
                {
                    if (p1.Initiative)
                    {
                        Board.ShowBoards(bP1, bP2, p1, p2);
                        play = p1.Turn(bP1, bP2, p2);
                        // Games ends if all playes are in use
                        if (!play) break;

                        Board.ShowBoards(bP1, bP2, p1, p2);
                        play = p2.Turn(bP2, bP1, p1);
                    }
                    else
                    {
                        Board.ShowBoards(bP1, bP2, p1, p2);
                        play = p2.Turn(bP1, bP2, p1);
                        if (!play) break;

                        Board.ShowBoards(bP1, bP2, p1 , p2);
                        play = p1.Turn(bP1, bP2, p2);
                    }
                }
                bP1.ResetBoard();
                bP2.ResetBoard();
            }
        }
    }
}
