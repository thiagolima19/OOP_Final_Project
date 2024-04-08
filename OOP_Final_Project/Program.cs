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

    class HumanPlayer : IPlayer // Constructor Method
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

        public ConnectFourGame(IPlayer player1, IPlayer player2) // Patricia 04/07/24
        {
            //todo
            //properties initializer constructor
            board = new char[6, 7];
            currentPlayer = 'X';
            InitializeBoard();
            this.player1 = player1;
            this.player2 = player2;
        }

        private void InitializeBoard() // Patricia 04/07/24
        { // this method is used to create a new game of 6 rows and 7 columns
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        public void PrintBoard() // Patricia 04/07/24
        { // this method displays values over the game
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    Console.Write(board[row, col] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("1 2 3 4 5 6 7");
        }

        public bool IsValidMove(int col)
        {
            //todo
            //implement the IsValidMove method
        }

        public void MakeMove(int col)
        {
            //todo
            //implement the MakeMove method
        }

        public bool CheckWinner()
        {
            // Implement the check for a winner (rows, columns, diagonals)
            // Return true if a player wins, otherwise return false.
            return false;
        }

        public void Play() // not finished
        {
            //todo
            //implement the Play method
            PrintBoard(); //Patricia 04/07/24
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IPlayer humanPlayer1 = new HumanPlayer();
            IPlayer humanPlayer2 = new HumanPlayer();

            //IPlayer computerPlayer = new ComputerPlayer(); // in case of play with AI

            ConnectFourGame game = new ConnectFourGame(humanPlayer1, humanPlayer2);
            game.Play();
        }
    }
}
