using System;
using System.Collections.Generic;
using System.Text;

public struct Cell
{
    private int m_IndexFlipped;
    private char m_Letter;

    public Cell(char i_Letter)
    {
        this.m_Letter = i_Letter;
        this.m_IndexFlipped = 0;
    }

    public int IndexFlipped
    {
        get
        {
            return this.m_IndexFlipped;
        }

        set
        {
            this.m_IndexFlipped = value;
        }
    }

    public char Letter
    {
        get
        {
            return this.m_Letter;
        }

        set
        {
            this.m_Letter = value;
        }
    }
}
