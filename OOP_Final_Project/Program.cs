using System;

/* public class ConectFourGame
{ */

public interface IConnect4
{ 
     void makeMove(); // method to be implemented by other class

}


public class HumanPlayer: IConnect4 // this class implements IConnect4
{ 
    public void makeMove()
    {
        // implementation
    }

}
class Program
{
    static void Main(string[] args)
    {
        string restart;
        string column;
        int numRows = 6;
        int numCols = 7;
        int[,] arrayGame = new int[numRows, numCols];
        string player1 = "X";
        string player2 = "O";
        Console.WriteLine("Connect 4 Game Development Project");

        Console.WriteLine("1  2   3   4   5   6   7");
        Console.WriteLine("Player X, choose a column (1-7):");
        column = Console.ReadLine();



        // print result
      //  Console.WriteLine("It's a Connect 4 Game....  ");
        do
        {
            Console.WriteLine("Restart? Yes(1) No(0): ");
            restart = Console.ReadLine();
        } while (restart != "1" && restart != "0");
    }
}


