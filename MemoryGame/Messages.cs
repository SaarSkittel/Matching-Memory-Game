using System;
using System.Collections.Generic;
using System.Text;

public static class Messages
{
    public static readonly string sr_WorngLength = "Input length isn't two please try again:";
    public static readonly string sr_InvalidInput = "Invalid input please try again:";
    public static readonly string sr_InvalidLetter = "The first letter is invalid please try again:";
    public static readonly string sr_InvalidDigit = "The second letter is invalid please try again:";
    public static readonly string sr_OutOfRange = "The cell is not in the range of the board please try again:";
    public static readonly string sr_AlreadyInUse = "The cell is already flipped please try again:";
    public static readonly string sr_EnterName = "Enter name player {0}:";
    public static readonly string sr_NumOfPlayers = "Enter number of players 1 or 2:";
    public static readonly string sr_EnterGuess = "{0},enter your {1} guess a capital letter and a number:";
    public static readonly string sr_EnterNumOfRowsOrCols = "Enter number of {0} between 4-6:";
    public static readonly string sr_IllegalRowsAndCols = "Illegal number of rows or cols please try again.";
    public static readonly string sr_Winner = "The winner is {0}! Score: {1} - {2}";
    public static readonly string sr_Draw = "It is a draw!";
    public static readonly string sr_AnotherGame = "Would you like to play another round? Y/N";
}
