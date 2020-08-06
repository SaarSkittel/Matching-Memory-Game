using System;
public static class Logic
{

    public static void FillMatrix(Board i_BoardToFill)
    {
        Random i_RandomNum = new Random();
        int[] i_LetterToInsert = new int[i_BoardToFill.NumOfTickets];
        for(int i = 0; i < i_BoardToFill.NumOfRows; i++)
        {
            for(int j = 0; j < i_BoardToFill.NumOfCols; ++j)
            {
                int k = i_RandomNum.Next(i_BoardToFill.NumOfTickets);
                while(i_LetterToInsert[k] == 2)
                {
                    k = i_RandomNum.Next(i_BoardToFill.NumOfTickets);
                }

                i_BoardToFill.Matrix[i, j].Letter = (char)('A' + k);
                ++i_LetterToInsert[k];
            }
        }
    }

    public static bool CheckNumOfPlayers(string i_StrToCheck, out int o_NumOfPlayers)
    {
        bool v_NumberCurrect = true;
        if(!(int.TryParse(i_StrToCheck, out o_NumOfPlayers) && (o_NumOfPlayers == 1 || o_NumOfPlayers == 2)))
        {
            v_NumberCurrect = false;
        }

        return v_NumberCurrect;
    }


    public static void CreatePlayer(ref Player io_CreatPlayer2, int i_TypePlayer2, Board i_BoardToCopy, string i_Name)
    {
        if(i_TypePlayer2 == 2)
        {
            io_CreatPlayer2 = new Player(i_Name, 0);
        }
        else
        {
            io_CreatPlayer2 = new Player("Computer", 1);
            io_CreatPlayer2.MemoryBoard = new Board(i_BoardToCopy);
        }
    }

    public static void UpdateBoard(Board i_BoardToSet, int[] i_Rows, int[] i_Cols, int i_UpdateValue)
    {
        for(int i = 0; i < 2; ++i)
        {
            i_BoardToSet.Matrix[i_Rows[i], i_Cols[i]].IndexFlipped = i_UpdateValue;
        }
    }

    public static int UserErrors(string i_StrigToCheck, Board i_GameBoard)
    {
        int i_ErrorNumber;
        if(i_StrigToCheck == "Q")
        {
            i_ErrorNumber = -1;
        }
        else if(i_StrigToCheck.Length != 2)
        {
            i_ErrorNumber = 0;
        }
        else if(!char.IsUpper(i_StrigToCheck[0]) && !char.IsDigit(i_StrigToCheck[1]))
        {
            i_ErrorNumber = 1;
        }
        else if(!char.IsUpper(i_StrigToCheck[0]))
        {
            i_ErrorNumber = 2;
        }
        else if(!char.IsDigit(i_StrigToCheck[1]))
        {
            i_ErrorNumber = 3;
        }
        else if((i_StrigToCheck[0] < 'A' || i_StrigToCheck[0] >= i_GameBoard.NumOfCols + 'A') || (i_StrigToCheck[1] <= '0' || i_StrigToCheck[1] > i_GameBoard.NumOfRows + '0'))
        {
            i_ErrorNumber = 4;
        }
        else
        {
            int o_ColGuess;
            int i_RowGuess = StringToMatrixLocation(i_StrigToCheck, out o_ColGuess);
            if(i_GameBoard.Matrix[i_RowGuess, o_ColGuess].IndexFlipped != 0)
            {
                i_ErrorNumber = 6;
            }
            else
            {
                i_ErrorNumber = 5;
            }
        }

        return i_ErrorNumber;
    }

    public static bool CheckNumOfRowAndCols(string i_RowsString, string i_ColsString, out int o_NumOfRows, out int o_NumOfCols)
    {
        bool v_InputCurrect = true;
        bool v_IsLegalRows = int.TryParse(i_RowsString, out o_NumOfRows);
        bool v_IsLegalCols = int.TryParse(i_ColsString, out o_NumOfCols);
        if(!((v_IsLegalCols && v_IsLegalRows) && ((o_NumOfCols * o_NumOfRows) % 2 == 0) && (Between4And6(o_NumOfRows) && Between4And6(o_NumOfCols))))
        {
            v_InputCurrect = false;
        }

        return v_InputCurrect;
    }

    public static int StringToMatrixLocation(string i_StringToConvert, out int o_Col)
    {
        o_Col = (int)i_StringToConvert[0] - 'A';
        int i_Row = (int)char.GetNumericValue(i_StringToConvert[1]) - 1;
        return i_Row;
    }

    public static bool Between4And6(int i_NumToCheck)
    {
        bool v_IsValid = false;
        if(i_NumToCheck > 3 && i_NumToCheck < 7)
        {
            v_IsValid = true;
        }

        return v_IsValid;
    }

    public static int EndGameOrRepeat(string i_StrToCheck)
    {
        int i_Decision = 2;
        if(i_StrToCheck == "N")
        {
            i_Decision = 0;
        }
        else if(i_StrToCheck == "Y")
        {
            i_Decision = 1;
        }

        return i_Decision;
    }

    public static int PlayerTurn(Player[] i_Player, int i_Turn, bool i_Success)
    {
        if(i_Turn == 1)
        {
            if(i_Success)
            {
                i_Player[0].Score++;
            }
            else
            {
                i_Turn++;
            }
        }
        else
        {
            if(i_Success)
            {
                i_Player[1].Score++;
            }
            else
            {
                i_Turn--;
            }
        }

        return i_Turn;
    }
}