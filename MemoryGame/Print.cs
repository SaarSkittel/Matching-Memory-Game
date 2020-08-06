using System;
using System.Collections.Generic;
using System.Text;

public static class Print
{
    public static string EnterName(string i_Message, int i_PlayerNum)
    {
        Console.WriteLine(i_Message, i_PlayerNum);
        return Console.ReadLine();
    }

    public static string PrintMessageAndGetStrin(string i_Message)
    {
        Console.WriteLine(i_Message);
        return Console.ReadLine();
    }

    public static string PlayerGuess(string i_Message, string i_PlayersName, int i_GuessNum)
    {
        Console.WriteLine(i_Message, i_PlayersName, i_GuessNum);
        return Console.ReadLine();
    }

    public static void Winner(string i_Message, string i_PlayerName, int i_ScoreWinner, int i_ScoreLoser)
    {
        Console.WriteLine(i_Message, i_PlayerName, i_ScoreWinner, i_ScoreLoser);
    }

    public static void PrintMessage(string i_Message)
    {
        Console.WriteLine(i_Message);
    }

    public static string NumOfColsOrRows(string i_ColsOrRows)
    {
        Console.WriteLine(Messages.sr_EnterNumOfRowsOrCols, i_ColsOrRows);
        return Console.ReadLine();
    }



    public static void PrintCells(Board i_Board)
    {
        Console.Clear();
        char i_ColumnLetter = 'A';
        int i = 0;
        int i_LineNumber = 1;
        StringBuilder i_Line = new StringBuilder();
        i_Line.Insert(0, "\n");
        i_Line.Insert(1, ' ');
        i_Line.Insert(2, "=", (4 * i_Board.NumOfCols) + 1);
        string i_LineSeparator = i_Line.ToString();
        for(int j = 0; j < i_Board.NumOfCols; ++j)
        {
            Console.Write("   {0}", i_ColumnLetter++);
        }

        foreach(Cell c in i_Board.Matrix)
        {
            if(i % i_Board.NumOfCols == 0)
            {
                Console.WriteLine(i_LineSeparator);
                Console.Write("{0}|", i_LineNumber++);
            }

            if(c.IndexFlipped == 0)
            {
                Console.Write("   |");
            }
            else
            {
                Console.Write(" {0} |", c.Letter);
            }

            ++i;
        }

        Console.WriteLine(i_LineSeparator);
    }
    
    public static string PrintUserErrors(eErrorCode i_Error)
    {

        switch (i_Error)
        {
            case eErrorCode.WorngLength:
                Console.WriteLine(Messages.sr_WorngLength);
                break;
            case eErrorCode.InvalidInput:
                Console.WriteLine(Messages.sr_InvalidInput);
                break;
            case eErrorCode.InvalidLetter:
                Console.WriteLine(Messages.sr_InvalidLetter);
                break;
            case eErrorCode.InvalidDigit:
                Console.WriteLine(Messages.sr_InvalidDigit);
                break;
            case eErrorCode.OutOfRange:
                Console.WriteLine(Messages.sr_OutOfRange);
                break;
            case eErrorCode.AlreadyInUse:
                Console.WriteLine(Messages.sr_AlreadyInUse);
                break;
        }

        return Console.ReadLine();
    }

}