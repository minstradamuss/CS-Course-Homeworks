public class SudokuValidator
{
    private static bool IsValidGroup(char[] group)
    {
        HashSet<char> set = new HashSet<char>();
        foreach (char num in group)
        {
            if (num != '.')
            {
                if (set.Contains(num))
                {
                    return false;
                }
                set.Add(num);
            }
        }
        return true;
    }

    public static bool IsValidSudoku(char[][] board)
    {
        bool[] rowsValid = new bool[9];
        bool[] colsValid = new bool[9];
        bool[] blocksValid = new bool[9];

        Parallel.For(0, 9, i =>
        {
            char[] row = new char[9];
            for (int j = 0; j < 9; j++)
            {
                row[j] = board[i][j];
            }
            rowsValid[i] = IsValidGroup(row);
        });

        Parallel.For(0, 9, j =>
        {
            char[] col = new char[9];
            for (int i = 0; i < 9; i++)
            {
                col[i] = board[i][j];
            }
            colsValid[j] = IsValidGroup(col);
        });

        Parallel.For(0, 9, blockIndex =>
        {
            char[] block = new char[9];
            int rowOffset = (blockIndex / 3) * 3;
            int colOffset = (blockIndex % 3) * 3;
            int index = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    block[index++] = board[rowOffset + i][colOffset + j];
                }
            }
            blocksValid[blockIndex] = IsValidGroup(block);
        });

        for (int i = 0; i < 9; i++)
        {
            if (!rowsValid[i] || !colsValid[i] || !blocksValid[i])
            {
                return false;
            }
        }

        return true;
    }

    public static void Main(string[] args)
    {
        char[][] board1 = {
            new char[] {'5', '3', '.', '.', '7', '.', '.', '.', '.'},
            new char[] {'6', '.', '.', '1', '9', '5', '.', '.', '.'},
            new char[] {'.', '9', '8', '.', '.', '.', '.', '6', '.'},
            new char[] {'8', '.', '.', '.', '6', '.', '.', '.', '3'},
            new char[] {'4', '.', '.', '8', '.', '3', '.', '.', '1'},
            new char[] {'7', '.', '.', '.', '2', '.', '.', '.', '6'},
            new char[] {'.', '6', '.', '.', '.', '.', '2', '8', '.'},
            new char[] {'.', '.', '.', '4', '1', '9', '.', '.', '5'},
            new char[] {'.', '.', '.', '.', '8', '.', '.', '7', '9'}
        };

        char[][] board2 = {
            new char[] {'8', '3', '.', '.', '7', '.', '.', '.', '.'},
            new char[] {'6', '.', '.', '1', '9', '5', '.', '.', '.'},
            new char[] {'.', '9', '8', '.', '.', '.', '.', '6', '.'},
            new char[] {'8', '.', '.', '.', '6', '.', '.', '.', '3'},
            new char[] {'4', '.', '.', '8', '.', '3', '.', '.', '1'},
            new char[] {'7', '.', '.', '.', '2', '.', '.', '.', '6'},
            new char[] {'.', '6', '.', '.', '.', '.', '2', '8', '.'},
            new char[] {'.', '.', '.', '4', '1', '9', '.', '.', '5'},
            new char[] {'.', '.', '.', '.', '8', '.', '.', '7', '9'}
        };

        Console.WriteLine(IsValidSudoku(board1));

        Console.WriteLine(IsValidSudoku(board2));
    }
}
