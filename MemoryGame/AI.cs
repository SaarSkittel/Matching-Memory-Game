using System;
using System.Collections.Generic;
using System.Threading;

public static class AI
{

   
    public static int[] ComputerAIGuess(Player i_Computer, out int[] o_GuessCols)
    {
        int[] i_GuessRows = new int[2];
        int io_NumOfRepeats = 0;
        o_GuessCols = new int[2];
        int i_NumOfGuess = 0;
        ComputerAIReturnMatching(i_Computer, ref i_GuessRows, ref o_GuessCols, ref io_NumOfRepeats);
        char i_Letter = '0';
        if (io_NumOfRepeats == 0)
        {
            while (i_NumOfGuess < 2)
            {
                Random i_Guess = new Random();
                o_GuessCols[i_NumOfGuess] = i_Guess.Next(i_Computer.MemoryBoard.NumOfCols);
                i_GuessRows[i_NumOfGuess] = i_Guess.Next(i_Computer.MemoryBoard.NumOfRows);
                if (i_Computer.MemoryBoard.Matrix[i_GuessRows[i_NumOfGuess], o_GuessCols[i_NumOfGuess]].IndexFlipped == 0)
                {
                    i_Letter = i_Computer.MemoryBoard.Matrix[i_GuessRows[i_NumOfGuess], o_GuessCols[i_NumOfGuess]].Letter;
                    AIUpdateBoard(i_Computer.MemoryBoard, i_GuessRows[i_NumOfGuess], o_GuessCols[i_NumOfGuess]);
                    if (i_NumOfGuess == 0)
                    {
                        if (i_Computer.MemoryBoard.Matrix[i_GuessRows[i_NumOfGuess], o_GuessCols[i_NumOfGuess]].IndexFlipped == 2)
                        {
                            ++io_NumOfRepeats;
                            ComputerAIReturnMatching(i_Computer, ref i_GuessRows, ref o_GuessCols, ref io_NumOfRepeats);
                        }
                    }

                    if (io_NumOfRepeats == 2)
                    {
                        i_NumOfGuess = 2;
                    }
                    else
                    {
                        ++i_NumOfGuess;
                        ++io_NumOfRepeats;
                    }
                }
            }
        }

        return i_GuessRows;
    }

    public static void ComputerAIReturnMatching(Player i_Comp, ref int[] i_GuessRows, ref int[] io_GuessCols, ref int io_NumOfRepeats)
    {
        int i_NumOfGuess = 0;
        char i_Letter = i_Comp.MemoryBoard.Matrix[i_GuessRows[i_NumOfGuess], io_GuessCols[i_NumOfGuess]].Letter;
        for(int i = 0; i < i_Comp.MemoryBoard.NumOfRows; ++i)
        {
            for(int j = 0; j < i_Comp.MemoryBoard.NumOfCols; ++j)
            {
                if(i_Comp.MemoryBoard.Matrix[i, j].IndexFlipped == 2 && io_NumOfRepeats == 0)
                {
                    i_GuessRows[i_NumOfGuess] = i;
                    io_GuessCols[i_NumOfGuess] = j;
                    ++io_NumOfRepeats;
                    i_Letter = i_Comp.MemoryBoard.Matrix[i, j].Letter;
                    ++i_Comp.MemoryBoard.Matrix[i, j].IndexFlipped;
                }
                else if(io_NumOfRepeats == 1 && i_Comp.MemoryBoard.Matrix[i, j].Letter == i_Letter && (i_GuessRows[i_NumOfGuess] != i || io_GuessCols[i_NumOfGuess] != j))
                {
                    i_NumOfGuess = 1;
                    i_GuessRows[io_NumOfRepeats] = i;
                    io_GuessCols[io_NumOfRepeats] = j;
                    ++i_NumOfGuess;
                    ++i_Comp.MemoryBoard.Matrix[i, j].IndexFlipped;
                    ++io_NumOfRepeats;
                    break;
                }
            }
        }
    }

    // if IndexFlipped = 0- never been flipped,
    // if IndexFlipped = 1- been flipped befor but match did't,
    // if IndexFlipped = 2- both been flipped but match was't used
    // and if IndexFlipped = 3- match already found.
    public static void AIMemWrite(Player i_Comp, int[] i_Rows, int[] i_Cols)
    {
        if(i_Comp.MemoryBoard.Matrix[i_Rows[0], i_Cols[0]].Letter == i_Comp.MemoryBoard.Matrix[i_Rows[1], i_Cols[1]].Letter)
        {
            i_Comp.MemoryBoard.Matrix[i_Rows[0], i_Cols[0]].IndexFlipped = 3;
            i_Comp.MemoryBoard.Matrix[i_Rows[1], i_Cols[1]].IndexFlipped = 3;
        }
        else
        {
            for(int i = 0; i < 2; ++i)
            {
                AIUpdateBoard(i_Comp.MemoryBoard, i_Rows[i], i_Cols[i]);
            }
        }
    }

    public static void AIUpdateBoard(Board i_AIBoard, int i_Row, int i_Col)
    {
        for(int i = 0; i < i_AIBoard.NumOfRows; ++i)
        {
            for(int j = 0; j < i_AIBoard.NumOfCols; ++j)
            {
                if(i_AIBoard.Matrix[i_Row, i_Col].Letter == i_AIBoard.Matrix[i, j].Letter && (i_Row != i || i_Col != j))
                {
                    if(i_AIBoard.Matrix[i, j].IndexFlipped == 1)
                    {
                        i_AIBoard.Matrix[i, j].IndexFlipped = 2;
                        i_AIBoard.Matrix[i_Row, i_Col].IndexFlipped = 2;
                    }
                    else if(i_AIBoard.Matrix[i, j].IndexFlipped == 0)
                    {
                        i_AIBoard.Matrix[i_Row, i_Col].IndexFlipped = 1;
                    }
                }
            }
        }
    }
}