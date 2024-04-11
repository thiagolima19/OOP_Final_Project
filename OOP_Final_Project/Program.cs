using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace ConnectFourGame
{
    interface IPlayer //IPlayer is an interface that will work as base for other class that will implement its fields and methods if any
    {
        int MakeMove(); //this method is incomplete and will be completed inside of the concrete class
    }

    class HumanPlayer : IPlayer //class to create a humamz player object to allow two humans playing the game
    {
        /*public int MakeMove() //Constructor Method that will implement the MakeMove() from the interface IPLayer
        {
            Console.Write("\nChoose a column (1-7): ");
            int col = int.Parse(Console.ReadLine()) - 1;
            return col;
        } */
        public int MakeMove() // Patricia 04/11/24 - EXCEPTION HANDLING ERRORS and option to leave game
        {
            int col = -1;
            bool validInput = false;
            while (!validInput)
            {
                try
                {
                    Console.Write($"\n{Name}, choose a column (1-7): ");
                    string input = Console.ReadLine();

                    // verify if user wants to finish the game
                    if (input.ToLower() == "exit")
                    {
                        Environment.Exit(0);  // Leave game
                    }

                    col = int.Parse(input) - 1;

                    if (col < 0 || col > 6)
                    {
                        throw new ArgumentOutOfRangeException(); // out of array boundary
                    }

                    validInput = true; // This command ensures the program advances after valid input is provided
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: This entry is not a number\n");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Error: Typed number is out of boundary between 1 and 7\n");
                }
                catch (Exception)
                {
                    Console.WriteLine($"Unexpected error: Please, try again\n");
                }
            }

            return col;
        }
    }

    //class ComputerPlayer : IPlayer //class to create a computer player object to allow a human and a computer (AI) playing the game
    //{
    //    private readonly Random random; //property to generate random numbers inside the range

    //    public ComputerPlayer() //constructor method to initialize a ComputerPlayer creating a new random object
    //    {
    //        random = new Random();
    //    }

    //    public int MakeMove() //Constructor Method that will implement the MakeMove() from the interface IPLayer
    //    {
    //        return random.Next(7); // Simulate computer AI (random move)
    //    }
    //}

    class ConnectFourGame //this class will implement the ConnectFourGame itself and its game logic
    {
        private char[,] board; //array to enforce the boundaries of the board 6x7
        private char currentPlayer; //X or O it is used to holds the current player
        private IPlayer player1; //player 1 and 2 is an Iplayer interface object
        private IPlayer player2; // may be a human or the computer

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
        { // this method is used to initialize a new game of 6 rows and 7 columns
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        public void PrintBoard() // Patricia 04/07/24
        { // this method will print the empty string to hold the place for a piece and displays values over the game 
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {                   
                    Console.Write($" | {board[row, col]}");
                }
                Console.WriteLine(" |");
            }
                 Console.WriteLine(" -----------------------------");
                 Console.WriteLine("   1   2   3   4   5   6   7  ");               
        }

        public bool IsValidMove(int col) //this boolean method will check if there is a empty string on the column select, and if it is a valid column inside the board
        {
            return board[0, col] == ' ';
        }

        public void MakeMove(int col) // this method will drop the piece as if gravity was pulling it to the floor. So, it will start placing the pieces from the botton to the top on the column.
        {
            for (int row = 5; row >= 0; row--)
            {
                if (board[row, col] == ' ')
                {
                    board[row, col] = currentPlayer;
                    break;
                }
            }
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
