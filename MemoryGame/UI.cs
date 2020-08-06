using System;
using System.Text;
using System.Threading;

// $G$ CSS-999 (-3) The class must have an access modifier.
class UI
{
    public static void Start()
    {
        bool o_Exit = false;
        string i_NamePlayer2 = null;
        int o_NumOfPlayers;
        Player[] i_Player = new Player[2];
        i_Player[0] = new Player(Print.EnterName(Messages.sr_EnterName, 1), 0);
        string i_StrNumOfPlayers = Print.PrintMessageAndGetStrin(Messages.sr_NumOfPlayers);
      
        while (!Logic.CheckNumOfPlayers(i_StrNumOfPlayers, out o_NumOfPlayers))
        {
            i_StrNumOfPlayers = Print.PrintMessageAndGetStrin(Messages.sr_NumOfPlayers);
        }


        // $G$ CSS-999 (-3) You should have used constants\enum here.
        if (i_StrNumOfPlayers == "2")
        {
            i_NamePlayer2 = Print.EnterName(Messages.sr_EnterName, 2);
        }

        while(!o_Exit)
        {
            i_Player[0].Score = 0;
            Board i_GameBoard = createBoard();
            Logic.FillMatrix(i_GameBoard);
            Logic.CreatePlayer(ref i_Player[1], o_NumOfPlayers, i_GameBoard, i_NamePlayer2);
            gameplay(i_Player, i_GameBoard, out o_Exit);
        }
    }

    private static void gameplay(Player[] i_Player, Board i_GameBoard, out bool o_Exit)
    {
        o_Exit = false;
        int i_PlayerTurn = 1;
        Print.PrintCells(i_GameBoard);
        while(i_Player[0].Score + i_Player[1].Score != i_GameBoard.NumOfTickets && !o_Exit)
        {
            bool v_CurrentScore = userTurn(i_GameBoard, i_Player, i_PlayerTurn, out o_Exit);
            i_PlayerTurn = Logic.PlayerTurn(i_Player, i_PlayerTurn, v_CurrentScore);
        }

        if(!o_Exit)
        {
            finalScore(i_Player);

            if(PlayAgain())
            {
                o_Exit = true;
            }
        }
    }

    private static bool userTurn(Board i_GameBoard, Player[] i_Players, int i_PlayerTurn, out bool o_Quit)
    {
        int i_NumOfFlips = 0;
        o_Quit = false;
        string i_PlayerGuess;
        int[] o_GuessCol = new int[2];
        int[] i_GuessRow = new int[2];
        if(i_Players[1].TypeOfPlayer == 0 || i_PlayerTurn == 1)
        {
            while(i_NumOfFlips != 2)
            {
                i_PlayerGuess = Print.PlayerGuess(Messages.sr_EnterGuess, i_Players[i_PlayerTurn - 1].Name, i_NumOfFlips + 1);
                i_PlayerGuess = checkGuessValid(i_PlayerGuess, i_GameBoard);
                if (i_PlayerGuess == "Q")
                {
                    o_Quit = true;
                    break;
                }

                i_GuessRow[i_NumOfFlips] = Logic.StringToMatrixLocation(i_PlayerGuess, out o_GuessCol[i_NumOfFlips]);
                i_GameBoard.Matrix[i_GuessRow[i_NumOfFlips], o_GuessCol[i_NumOfFlips]].IndexFlipped = 1;
                if (i_NumOfFlips == 0)
                {
                   Print.PrintCells(i_GameBoard);
                }

                ++i_NumOfFlips;
            }
        }

        if(i_Players[1].TypeOfPlayer == 1 && i_PlayerTurn == 1)
        {
            AI.AIMemWrite(i_Players[1], i_GuessRow, o_GuessCol);
        }
        else if(i_Players[1].TypeOfPlayer == 1 && i_PlayerTurn == 2)
        {
            Thread.Sleep(1000);
            i_GuessRow = AI.ComputerAIGuess(i_Players[1], out o_GuessCol);
            i_GameBoard.Matrix[i_GuessRow[0], o_GuessCol[0]].IndexFlipped = 1;
            Print.PrintCells(i_GameBoard);
            i_GameBoard.Matrix[i_GuessRow[1], o_GuessCol[1]].IndexFlipped = 1;
            AI.AIMemWrite(i_Players[1], i_GuessRow, o_GuessCol);
            Thread.Sleep(3000);
        }

        return quitGame(o_Quit, i_GuessRow, o_GuessCol, i_GameBoard);
    }

    private static bool quitGame(bool i_StopGame, int[] i_GuessRow, int[] i_GuessCol, Board i_GameBoard)
    {
        bool v_Stop = false;
        if(i_StopGame)
        {
            v_Stop = true;
        }
        else
        {
            v_Stop = printTurn(i_GuessRow, i_GuessCol, i_GameBoard);
        }

        return v_Stop;
    }

    private static bool printTurn(int[] i_Rows, int[] i_Cols, Board i_GameBoard)
    {
        bool v_Score = false;
        if(i_GameBoard.Matrix[i_Rows[0], i_Cols[0]].Letter == i_GameBoard.Matrix[i_Rows[1], i_Cols[1]].Letter)
        {
            Logic.UpdateBoard(i_GameBoard, i_Rows, i_Cols, 2);
            v_Score = true;
            Print.PrintCells(i_GameBoard);
        }
        else
        {
           Print.PrintCells(i_GameBoard);
           Thread.Sleep(2000);
           Console.Clear();
           Logic.UpdateBoard(i_GameBoard, i_Rows, i_Cols, 0);
           Print.PrintCells(i_GameBoard);
        }

        return v_Score;
    }

    private static string checkGuessValid(string i_StringGuess, Board i_GameBoard)
    {
        int i_IsValid = -2;
        while(i_IsValid != 5 && i_IsValid != -1)
        {
            i_IsValid = Logic.UserErrors(i_StringGuess, i_GameBoard);
            if(i_IsValid != 5 && i_IsValid != -1)
            {
                eErrorCode i_Error = (eErrorCode)i_IsValid;
                i_StringGuess = Print.PrintUserErrors(i_Error);
            }
        }

        return i_StringGuess;
    }

    private static Board createBoard()
    {
        int o_NumOfCols;
        int o_NumOfRows;
        string i_RowsString = Print.NumOfColsOrRows("rows");
        string i_ColsString = Print.NumOfColsOrRows("cols");
        while(!Logic.CheckNumOfRowAndCols(i_RowsString, i_ColsString, out o_NumOfRows, out o_NumOfCols))
        {
            Print.PrintMessage(Messages.sr_IllegalRowsAndCols);
            i_RowsString = Print.NumOfColsOrRows("rows");
            i_ColsString = Print.NumOfColsOrRows("cols");
        }

        return new Board(o_NumOfRows, o_NumOfCols);
    }

    private static void finalScore(Player[] i_Player)
    {
        if(i_Player[0].Score > i_Player[1].Score)
        {
            Print.Winner(Messages.sr_Winner, i_Player[0].Name, i_Player[0].Score, i_Player[1].Score);
        }
        else if(i_Player[0].Score < i_Player[1].Score)
        {
            Print.Winner(Messages.sr_Winner, i_Player[1].Name,i_Player[1].Score, i_Player[0].Score);
        }
        else
        {
            Print.PrintMessage(Messages.sr_Draw);
        }
    }

    public static bool PlayAgain()
    {
        int i_OutPutIndex = 2;
        while(i_OutPutIndex == 2)
        {
            i_OutPutIndex = Logic.EndGameOrRepeat(Print.PrintMessageAndGetStrin(Messages.sr_AnotherGame));
            if(i_OutPutIndex == 2)
            {
               Print.PrintMessage(Messages.sr_InvalidInput);
            }
        }

        return i_OutPutIndex == 0;
    }
}