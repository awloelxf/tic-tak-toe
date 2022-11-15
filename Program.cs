string exit = "n";
int[] move = new int[2];
bool move_ok = false;
do
{
    Game game = new Game();
    Board board = new Board();
    do
    {
        do 
        {
            move = game.Move(); //--move input
            move_ok = board.Move(move[0], move[1]); //--send move to board
        }
        while (!move_ok);
        board.DrawBoard();
        game.PlayerTurn = game.PlayerTurn switch { 0 => 1, 1 => 0 };
    }
    while (!game.EndGame(board));
    //---game END--
    Console.WriteLine(game.EndGameText);
    Console.Write("Type y for new game: ");
    exit = Console.ReadLine() ?? "y";
}
while (exit == "y");
class Game
{
    public int PlayerTurn { get; set; } = 0;
    public string EndGameText { get; private set; } = "Accident";
    public int[] Move()
    {
        string _move;
        int[] res = new int[2];
        do
        {
            Console.Write($"{PlayerTurn switch { 0 => "Player 1", 1 => "Player 2" }} turn: ");
            _move = Console.ReadLine();
        }
        while (!(_move == "1" || _move == "2" || _move == "3" || _move == "4" || _move == "5" || _move == "6" || _move == "7" || _move == "8" || _move == "9"));
        res[0] = Convert.ToInt32(_move);
        res[1] = PlayerTurn;
        return res;
    }
    public bool EndGame(Board _board)
    {
        return (PlayerWin(_board, 0) || PlayerWin(_board, 1) || boardEnd(_board));
    }
    public bool PlayerWin(Board _board, int player)
    {
        bool end;
        bool[] colunmEnd = new bool[3];
        bool[] rawEnd = new bool[3];
        bool diagonalEnd;
        for (int i = 0; i <= 2; i++) colunmEnd[i] = true;
        for (int i = 0; i <= 2; i++) rawEnd[i] = true;
        //--//---Column check
        for (int i = 0; i <= 2; i++)
            for (int j = 0; j <= 2; j++)
            {
                if (_board.board[i, j] == player) colunmEnd[i] = colunmEnd[i] && true;
                else colunmEnd[i] = false;
            }
        end = colunmEnd[0] || colunmEnd[1] || colunmEnd[2];
        //--//---Raw check
        if (!end)
        {
            for (int i = 0; i <= 2; i++)
                for (int j = 0; j <= 2; j++)
                {
                    if (_board.board[j, i] == player) rawEnd[i] = rawEnd[i] && true;
                    else rawEnd[i] = false;
                }
            end = rawEnd[0] || rawEnd[1] || rawEnd[2];
        }
        //--//---Diagonal 1 check
        if (!end)
        {
            diagonalEnd = true;
            for (int i = 0; i <= 2; i++)
            {
                if (_board.board[i, i] == player) diagonalEnd = diagonalEnd && true;
                else diagonalEnd = false;
            }
            end = diagonalEnd;
        }
        //--//---Diagonal 2 check
        if (!end)
        {
            diagonalEnd = true;
            for (int i = 0; i <= 2; i++)
            {
                if (_board.board[2 - i, i] == player) diagonalEnd = diagonalEnd && true;
                else diagonalEnd = false;
            }
            end = diagonalEnd;
        }
        if (end) EndGameText = "Player " + (player + 1) + " WIN!";
        return end;
    }
    //--/Player win--
    //---board full--
    public bool boardEnd(Board _board)
    { 
        bool end = true;
        foreach (int sign in _board.board)
        {
            if (sign == 2) end = false;
            else end = end && true;
        }
        if (end) EndGameText = "No moves left. Draw.";
        //--/board full--
        return end;
    }
    public Game()
    {
        Console.Clear();
        Console.WriteLine("  \nNew \"Tic tac toe\" game. Square numbers are:" +
            "\n 7 | 8 | 9 \n---+---+---\n 4 | 5 | 6 \n---+---+---\n 1 | 2 | 3");
        Console.WriteLine(
            "\n   |   |   \n---+---+---\n   |   |   \n---+---+---\n   |   |  ");
    }
}
class Board
{
    public int[,] board { get; } = new int[3 ,3] { { 2, 2, 2 }, { 2,2,2}, { 2, 2, 2 } };
    public Board()
    { }
    public bool Move(int square, int sign)
    {
        int _square1 = square switch
        {
            7 => 0, 4 => 0, 1 => 0,
            8 => 1, 5 => 1, 2 => 1,
            9 => 2, 6 => 2, 3 => 2,
            
        };
        int _square2 = square switch
        {
            7 => 0, 8 => 0, 9 => 0,
            4 => 1, 5 => 1, 6 => 1,
            1 => 2, 2 => 2, 3 => 2,
        };
        if (board[_square1, _square2] == 2)
        {
            board[_square1, _square2] = sign;
            return true;
        }
        else return false;
    }
    public void DrawBoard()
    {
        string[,] _board = new string[3,3];
        for (int i = 0; i <=2; i++)
            for (int j = 0; j <=2; j++)
                _board[i, j] = board[i, j] switch
                {
                    0 => "X",
                    1 => "O",
                    2 => " "
                };
        Console.Clear();
        Console.WriteLine("  \nSquare numbers are:" +
            "\n 7 | 8 | 9 \n---+---+---\n 4 | 5 | 6 \n---+---+---\n 1 | 2 | 3");
        Console.WriteLine(
            "\n " + _board[0,0] + " | " + _board[1,0] + " | " + _board[2,0] + 
            "\n---+---+---" +
            "\n " + _board[0,1] + " | " + _board[1,1] + " | " + _board[2,1] + 
            "\n---+---+---" +
            "\n " + _board[0,2] + " | " + _board[1,2] + " | " + _board[2,2]);
    }
}