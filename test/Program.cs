// a class defining a game of tic tac toe
TicTacToe.PlayRound();

public class TicTacToe
{
    // a method for a round of tic tac toe
    public static void PlayRound()
    {
        // declare variables
        int[,] board = new int[3, 3];
        int player = 1;
        int row, column;
        bool valid;

        // loop until the game is over
        do
        {
            // display the board
            DisplayBoard(board);

            // get the player's move
            do
            {
                // prompt the player
                Console.Write("Player {0}, enter a row and column: ", player);

                // get the row and column
                row = int.Parse(Console.ReadLine());
                column = int.Parse(Console.ReadLine());

                // determine if the move is valid
                valid = IsValidMove(board, row, column);
                if (!valid)
                    Console.WriteLine("Invalid move. Try again.");
            } while (!valid);

            // update the board
            board[row, column] = player;

            // determine if the game is over
            if (IsWinner(board, player))
                Console.WriteLine("Player {0} wins!", player);
            else if (IsTie(board))
                Console.WriteLine("The game ends in a tie.");
            else
                player = (player % 2) + 1;
        } while (!IsWinner(board, player) && !IsTie(board));
    }

    // a method to display the board
    public static void DisplayBoard(int[,] board)
    {
        // display the column headings
        Console.Write("   ");
        for (int column = 0; column < 3; column++)
            Console.Write(" {0} ", column);
        Console.WriteLine();

        // display the board
        for (int row = 0; row < 3; row++)
        {
            Console.Write("{0}  ", row);
            for (int column = 0; column < 3; column++)
                Console.Write("{0} ", board[row, column]);
            Console.WriteLine();
        }
    }

    //IsWinner method
    public static bool IsWinner(int[,] board, int player)
    {
        // check for three-in-a-row
        for (int row = 0; row < 3; row++)
            if (board[row, 0] == player && board[row, 1] == player && board[row, 2] == player)
                return true;
        for (int column = 0; column < 3; column++)
            if (board[0, column] == player && board[1, column] == player && board[2, column] == player)
                return true;
        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
            return true;
        if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
            return true;
        return false;
    }

    //IsValidMove method
    public static bool IsValidMove(int[,] board, int row, int column)
    {
        // determine if the move is valid
        if (row < 0 || row > 2 || column < 0 || column > 2)
            return false;
        if (board[row, column] != 0)
            return false;
        return true;
    }

    //IsTie method
    public static bool IsTie(int[,] board)
    {
        // declare variables
        bool tie = true;
        int row, column;

        // loop through the board
        for (row = 0; row < 3; row++)
        {
            for (column = 0; column < 3; column++)
            {
                // if there is an empty square, the game is not over
                if (board[row, column] == 0)
                    tie = false;
            }
        }

        // return the result
        return tie;
    }
}