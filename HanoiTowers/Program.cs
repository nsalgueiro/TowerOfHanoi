using System;
using HanoiTowers.Model;

namespace HanoiTowers
{
    class Program
    {
        private const int  Disks = 5;
        private static Tower tower1 = new Tower("Tower 1");
        private static Tower tower2 = new Tower("Tower 2");
        private static Tower tower3 = new Tower("Tower 3");

        //private static Tower[] towers;
        //private static (Tower t1, Tower t2) lastMove;

        static void Main(string[] args)
        {
            InitializeTowers();
            /*
            MoveDisk((tower1, tower2));

            Console.WriteLine("Tower1 size is {0}", tower1.Count);
            Console.WriteLine("Tower2 size is {0}", tower2.Count);
            Console.WriteLine("Tower3 size is {0}", tower3.Count);
            
            MoveDisk(tower1, tower2);

            Console.WriteLine("Tower1 size is {0}", tower1.Count);
            Console.WriteLine("Tower2 size is {0}", tower2.Count);
            Console.WriteLine("Tower3 size is {0}", tower3.Count);

            MoveDisk(tower1, tower3);

            Console.WriteLine("Tower1 size is {0}", tower1.Count);
            Console.WriteLine("Tower2 size is {0}", tower2.Count);
            Console.WriteLine("Tower3 size is {0}", tower3.Count);
            */
            
            var moves = 0;

            while(tower3.Elements.Count<Disks && moves<10000)
            {
                (Tower,Tower) move = DetermineNextMove(++moves);
                Console.WriteLine("{0} - Moving disk {1} from {2} to {3}", moves, move.Item1.Elements.Peek(), move.Item1.Name, move.Item2.Name);
                MoveDisk(move);
                Console.Write("{0} {1} {2}", tower1.ToString(), tower2, tower3);
                Console.WriteLine();
                Console.WriteLine();
            }
            
            //towerOfHanoi(3, 'A', 'C', 'B');
        }

        static void TowerOfHanoi(int n, char fromTower, char toTower, char auxTower)
        {
            if (n == 1)
            {
                Console.WriteLine("Move disk 1 from rod " +
                                  fromTower + " to rod " + toTower);
                return;
            }
            TowerOfHanoi(n - 1, fromTower, auxTower, toTower);
            Console.WriteLine("Move disk " + n + " from tower " +
                              fromTower + " to tower " + toTower);
            TowerOfHanoi(n - 1, auxTower, toTower, fromTower);
        }

        private static void InitializeTowers()
        {
            for (int i = Disks; i > 0; i--)
            {
                tower1.Elements.Push(i);
            }

            //towers = new Tower[3] { tower1, tower2, tower3 };
        }

        /*
        private static (Tower, Tower) DetermineNextMove()
        {
            //Don't allow reverse of previous move
            var lastMoveReverse = GetMoveString(lastMove.t2, lastMove.t1);

            List<(Tower, Tower)> moves = new List<(Tower, Tower)>();
            var validMoves = 0;

            foreach(Tower from in towers)
            {
                foreach(Tower to in towers)
                {
                    if(from!=to && !GetMoveString(from, to).Equals(lastMoveReverse) && IsValidMove(from, to))
                    {
                        moves.Add((from, to));
                    }
                }
            }

            if (moves.Count > 0)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("Available Moves: ");
                for (int i = 0; i < moves.Count; i++)
                {
                    {
                        Console.WriteLine("{0} {1}=>{2} ", moves[i].Item1.Elements.Peek(), moves[i].Item1.Name, moves[i].Item2.Name);
                    }
                }
                Console.WriteLine("-------------------");
            }
            return validMoves>1 ? DecideBestMove(moves) : moves[0];
        }
        */


        /**
         * The most effective solution for the problem follows a repeating pattern, every 3 moves the move will always be between the same towers
         * with a small note that if the number of towers is even or odd the move will be different.
         * We just don't know the direction but that will be determined in another method
         */ 
        private static (Tower, Tower) DetermineNextMove(int move)
        {

            if ((move %3  == 1  && Disks%2 == 0) || (move % 3 == 2 && Disks % 2 > 0))
            {
                return DetermineDirection(tower1, tower2);
            }
            else if ((move %3 == 2 && Disks %2 == 0) || (move %3 == 1 && Disks %2 >0))
            {
                return DetermineDirection(tower1, tower3);
            }
            else
            {
                return DetermineDirection(tower2, tower3);
            }
        }

        /**
         *  This method will determine the direction of the move between the two towers ir receives.
         *  We will check the top values of both towers and return the correct movement.
         *  Note - to avoid adding additional "ifs" to check if a column is empty the getTopElement value
         *  returns the max value of int when the tower is empty so the lesser than comparision is enough.
         */ 
        private static (Tower,Tower) DetermineDirection( Tower a, Tower b )
        {
            if (a.getTopElementValue() < b.getTopElementValue())
            {
                return (a, b);
            }
            else
            {
                return (b, a);
            }
        }

        private static bool MoveDisk((Tower from, Tower to) move)
        {
            /*
            if (!IsValidMove(move.from, move.to))
            {
                Console.WriteLine("Invalid move, from element is bigger than top element of destination");
                return false;
            }
            */
            var disk = move.from.Elements.Pop();
            //Console.WriteLine("Moving disk {0} from {1} to {2}", disk, move.from.Name, move.to.Name);
            move.to.Elements.Push(disk);

            //lastMove = move;
            return true;
        }

        private static bool IsValidMove(Tower from, Tower to)
        {
            int topElementFrom;

            if (from.Elements.Count==0)
            {
                return false;
            } else
            {
                topElementFrom = (int)from.Elements.Peek();
            }

            if (to.Elements.Count == 0 || (int)to.Elements.Peek() > topElementFrom)
            {
                return true;
            }

            return false;
        }

        private static string GetMoveString(Tower from, Tower to)
        {
            return (from != null && to!=null) ? from.Name + "=>" + to.Name : "n/a";
        }
    }

    
}
