using ConnectFourGame;
using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace ConnectFourGame
{
    interface IPlayer //IPlayer is an interface that will work as base for other class that will implement its fields and methods if any
    {

        string Name { get; }// Patricia 04/16/24
        int MakeMove(); //this method is implemented inside of the concrete classes
    }

    class HumanPlayer : IPlayer //class to create a human player object to allow two humans playing the game
    {
        public string Name { get; } //property Name to hold a player name input on the console

        public HumanPlayer(string name) //constructor to initialize Name as the name passed by the user
        {
            Name = name;
        }

        public int MakeMove()  // method came from Interface
        {
            int col = -1;
            bool validInput = false;
            while (!validInput)
            {
                // Patricia 04/11/24 - EXCEPTION HANDLING ERRORS and option to leave game
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

        public string Name { get; } //property to get the name of the computer player when a game is played by a human vs a computer player
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
        private IPlayer player1; //player 1 and 2 is an IPlayer interface object
        private IPlayer player2; // may be a human or the computer

        public ConnectFourGame(IPlayer player1, IPlayer player2) // Patricia 04/07/24
        {
        //properties initializer constructor
            board = new char[6, 7];
            currentPlayer = 'X'; //X or O it is used to holds the current player, but starts using this value
            InitializeBoard();
            this.player1 = player1; //player 1 and 2 is an Iplayer interface object
            this.player2 = player2; // may be a human or the computer player
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

        // this method will print the empty string to hold the place for a piece and displays values over the game 
        public void PrintBoard() // Patricia 04/07/24
        { 
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

        //this boolean method will check if there is an empty string on the column select, and if it is a valid column inside the board
        public bool IsValidMove(int col)
        { 
            return board[0, col] == ' ';
        }

        // this method will drop a piece as if gravity was pulling it to the floor. So, it will start placing the pieces from the bottom to the top on the column
        public void DropPiece(int col) 
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

        //this method will check a winner using the methods CheckHorizontal, CheckVertical, CheckDiagonal and CheckAntiDiagonal pieces position.
        public string CheckWinner() 
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
                for (int col = 0; col < 4; col++) //inner loop will check every 4 places up the max number of columns 
                {   //it will check if on each place in the board will have the same symbol/char from the same player than will return a winner if any
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
                for (int row = 0; row < 3; row++)  //inner loop will check every 4 places up the max number of rows
                {   //it will check if on each place in the board will have the same symbol/char from the same player than will return a winner if any
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
                {   //it will check if on each place in the board will have the same symbol/char from the same player than will return a winner if any
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
                {   //it will check if on each place in the board will have the same symbol/char from the same player than will return a winner if any
                    if (board[row, col] == playerSymbol && board[row + 1, col - 1] == playerSymbol && board[row + 2, col - 2] == playerSymbol && board[row + 3, col - 3] == playerSymbol)
                        return true;
                }
            }
            return false;
        }

        //this method will check if the game gets a draw
        public bool CheckDraw()  
        {
            for (int col = 0; col < 7; col++) //this for loop will go through the board checking if the move is valid or not
            {
                if (IsValidMove(col))
                    return false;
            }
            return true;
        }

        // this method allows the player to choose to play again or quit
        private bool PromptPlayAgain() // Patricia 04/16/24
        {
            Console.Write("\nDo you want to play again? (yes/no): ");
            string input = Console.ReadLine().Trim().ToLower();
            return input == "yes" || input == "y";
        }


        public void Play() //Patricia 04/16/24
        {

            while (true)
            {
                PrintBoard();

                int col;
                if (currentPlayer == 'X') // This variable keeps track of whose turn it is ('X' or 'O')
                    col = player1.MakeMove(); //This method is called based on the current player's turn to get its move (column selection)
                else
                    col = player2.MakeMove();

                if (col >= 0 && col < 7 && IsValidMove(col)) // verifies if the selected column is inside the boundary of array and the cell itself is empty
                {
                    DropPiece(col);
                    string winner = CheckWinner();
                    if (!string.IsNullOrEmpty(winner))
                    {
                        PrintBoard();
                        Console.WriteLine($"Congratulations!!!\nPlayer {winner} wins!");

                        if (PromptPlayAgain())
                        {
                            InitializeBoard(); // restart the game
                            currentPlayer = 'X';
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (CheckDraw()) // check if the result is draw (there is no winner)
                    {
                        PrintBoard();
                        Console.WriteLine("Sorry!!!\nIt's a draw!");

                        if (PromptPlayAgain())
                        {
                            InitializeBoard();
                            currentPlayer = 'X';
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }                
            }
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
    

