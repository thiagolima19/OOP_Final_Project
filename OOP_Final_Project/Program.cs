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
        public string Name { get; } //property Name to hold a player name inputed on the console

        public HumanPlayer(string name) //constructor to initialize Name as the name passed by the user
        {
            Name = name;
        }

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

    class ComputerPlayer : IPlayer //class to create a computer player object to allow a human and a computer (AI) playing the game
    {
        private readonly Random random; //property to generate random numbers inside the range

        public string Name { get; } //property to get the name of the computer player whem a game is played by a human vs a computer player
        public ComputerPlayer() //constructor method to initialize a ComputerPlayer creating a new random object
        {
            random = new Random();
            Name = "Computer";
        }

        public int MakeMove() //Constructor Method that will implement the MakeMove() from the interface IPLayer
        {
            return random.Next(7); // Simulate computer AI (random move)
        }
    }

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

        public string CheckWinner() //this method will check a winner using the methods CheckHorizontal, CheckVertical, CheckDiagonal and CheckAntiDiagonal pieces position.
        {
            char playerSymbol = currentPlayer == 'X' ? 'X' : 'O'; //this line will check for the current player and will change it to the another one

            // Check horizontally, vertically, diagonally, and anti-diagonally and returns a winner if any
            if (CheckHorizontal(playerSymbol) || CheckVertical(playerSymbol) || CheckDiagonal(playerSymbol) || CheckAntiDiagonal(playerSymbol))
            {
                if (currentPlayer == 'X')
                    return player1.Name;
                else
                    return player2.Name;
            }

            return null;
        }

        private bool CheckHorizontal(char playerSymbol)
        {
            // Check horizontally
            for (int row = 0; row < 6; row++) //outer loop goes from index 0 to 6 in the rows
            {
                for (int col = 0; col < 4; col++) //inner loop will check evrey 4 places up the max number of columns 
                {   //it will check if on each place in the board will have the same symblo/char from the same player than will return a winner if any
                    if (board[row, col] == playerSymbol && board[row, col + 1] == playerSymbol && board[row, col + 2] == playerSymbol && board[row, col + 3] == playerSymbol)
                        return true;
                }
            }
            return false;
        }

        private bool CheckVertical(char playerSymbol)
        {
            // Check vertically
            for (int col = 0; col < 7; col++)//outer loop goes from index 0 to 6 in the columns 
            {
                for (int row = 0; row < 3; row++)  //inner loop will check evrey 4 places up the max number of rows
                {   //it will check if on each place in the board will have the same symblo/char from the same player than will return a winner if any
                    if (board[row, col] == playerSymbol && board[row + 1, col] == playerSymbol && board[row + 2, col] == playerSymbol && board[row + 3, col] == playerSymbol)
                        return true;
                }
            }
            return false;
        }

        private bool CheckDiagonal(char playerSymbol)
        {
            // Check diagonally
            for (int row = 0; row < 3; row++)//outer loop goes from index 0 to 2 in the rows, because the row only contains true diagonals from the index 0 to 2 that fits 4 pieces
            {
                for (int col = 0; col < 4; col++) //inner loop goes from index 0 to 3 in the columns, because the columns only contains true diagonals from the index 0 to 3 that fits 4 pieces
                {   //it will check if on each place in the board will have the same symblo/char from the same player than will return a winner if any
                    if (board[row, col] == playerSymbol && board[row + 1, col + 1] == playerSymbol && board[row + 2, col + 2] == playerSymbol && board[row + 3, col + 3] == playerSymbol)
                        return true;
                }
            }
            return false;
        }

        private bool CheckAntiDiagonal(char playerSymbol)
        {
            // Check anti-diagonally
            for (int row = 0; row < 3; row++) //outer loop goes from index 0 to 2 in the rows, because the row only contains true anti-diagonals from the index 0 to 2 that fits 4 pieces
            {
                for (int col = 3; col < 7; col++)//inner loop goes from index 3 to 6 in the columns, because the columns only contains true anti-diagonals from the index 3 to 6 that fits 4 pieces
                {   //it will check if on each place in the board will have the same symblo/char from the same player than will return a winner if any
                    if (board[row, col] == playerSymbol && board[row + 1, col - 1] == playerSymbol && board[row + 2, col - 2] == playerSymbol && board[row + 3, col - 3] == playerSymbol)
                        return true;
                }
            }
            return false;
        }

        public bool CheckDraw() //this method will check for a draw
        {
            for (int col = 0; col < 7; col++) //this for loop will go through the board checking if the move is valid or not
            {
                if (IsValidMove(col))
                    return false;
            }
            return true;
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
