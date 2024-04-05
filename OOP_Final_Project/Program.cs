//using System;

///* public class ConectFourGame
//{ */

//public interface IConnect4
//{ 
//     void makeMove(); // method to be implemented by other class

//}


//public class HumanPlayer: IConnect4 // this class implements IConnect4
//{ 
//    public void makeMove()
//    {
//        // implementation
//    }

//}
//class Program
//{
//    static void Main(string[] args)
//    {
//        string restart;
//        string column;
//        int numRows = 6;
//        int numCols = 7;
//        int[,] arrayGame = new int[numRows, numCols];
//        string player1 = "X";
//        string player2 = "O";
//        Console.WriteLine("Connect 4 Game Development Project");

//        Console.WriteLine("1  2   3   4   5   6   7");
//        Console.WriteLine("Player X, choose a column (1-7):");
//        column = Console.ReadLine();



//        // print result
//      //  Console.WriteLine("It's a Connect 4 Game....  ");
//        do
//        {
//            Console.WriteLine("Restart? Yes(1) No(0): ");
//            restart = Console.ReadLine();
//        } while (restart != "1" && restart != "0");
//    }
//}

using System;
using System.ComponentModel;

namespace ConnectFourGame
{
    interface IPlayer
    {
        int MakeMove();
    }

    class HumanPlayer : IPlayer
    {
        public int MakeMove()
        {
            Console.Write("\nChoose a column (1-7): ");
            int col = int.Parse(Console.ReadLine()) - 1;
            return col;
        }
    }

    //class ComputerPlayer : IPlayer
    //{
    //    private readonly Random random;

    //    public ComputerPlayer()
    //    {
    //        random = new Random();
    //    }

    //    public int MakeMove()
    //    {
    //        // Simulate computer AI (random move)
    //        return random.Next(7);
    //    }
    //}

    class ConnectFourGame
    {
        private char[,] board;
        private char currentPlayer;
        private IPlayer player1;
        private IPlayer player2;

        public ConnectFourGame(IPlayer player1, IPlayer player2)
        {
           //todo
           //porperties initializer constructor
        }

        private void InitializeBoard()
        {
            //todo
            //implemet the InitializeBoard method
        }

        public void PrintBoard()
        {
            //todo
            //implemet the PrintBoard method
        }

        public bool IsValidMove(int col)
        {
            //todo
            //implemet the IsValidMove method
        }

        public void MakeMove(int col)
        {
            //todo
            //implemet the MakeMove method
        }

        public bool CheckWinner()
        {
            // Implement the check for a winner (rows, columns, diagonals)
            // Return true if a player wins, otherwise return false.
            return false;
        }

        public void Play()
        {
            //todo
            //implemet the Play method

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IPlayer humanPlayer1 = new HumanPlayer();
            IPlayer humanPlayer2 = new HumanPlayer();

            //IPlayer computerPlayer = new ComputerPlayer();

            ConnectFourGame game = new ConnectFourGame(humanPlayer1, humanPlayer2);
            game.Play();
        }
    }
}
