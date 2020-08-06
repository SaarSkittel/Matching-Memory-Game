using System;
using System.Collections.Generic;
using System.Text;

public class Player
{
    private string m_PlayersName;
    private int m_Score;
    private int m_TypeOfPlayer;
    private Board m_AIMemoryBoard;

    public Player(string i_Name, int i_TypeOfPlayer)
    {
        this.m_TypeOfPlayer = i_TypeOfPlayer;
        this.m_PlayersName = i_Name;
        this.m_Score = 0;
        this.m_AIMemoryBoard = null;
    }

    public Board MemoryBoard
    {
        get
        {
            return this.m_AIMemoryBoard;
        }

        set
        {
            this.m_AIMemoryBoard = value;
        }
    }

    public int TypeOfPlayer
    {
        get
        {
            return this.m_TypeOfPlayer;
        }

        set
        {
            m_TypeOfPlayer = value;
        }
    }

    public int Score
    {
        get
        {
            return this.m_Score;
        }

        set
        {
            this.m_Score = value;
        }
    }

    public string Name
    {
        get
        {
            return this.m_PlayersName;
        }

        set
        {
            this.m_PlayersName = value;
        }
    }
}
