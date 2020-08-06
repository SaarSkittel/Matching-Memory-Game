public class Board
{
    private Cell[,] m_BoardMatrix;
    private int m_NumOfTickets;
    private int m_NumOfRows;
    private int m_NumOfCols;

    public Board(int i_Rows, int i_Cols)
    {
        this.m_BoardMatrix = new Cell[i_Rows, i_Cols];
        this.m_NumOfCols = i_Cols;
        this.m_NumOfRows = i_Rows;
        this.m_NumOfTickets = (i_Rows * i_Cols) / 2;
    }

    public Board(Board i_BoardToCopy)
    {
        this.m_NumOfCols = i_BoardToCopy.NumOfCols;
        this.m_NumOfRows = i_BoardToCopy.NumOfRows;
        this.m_NumOfTickets = i_BoardToCopy.NumOfTickets;
        this.m_BoardMatrix = new Cell[i_BoardToCopy.NumOfRows, i_BoardToCopy.NumOfCols];
        for(int i = 0; i < i_BoardToCopy.NumOfRows; ++i)
        {
            for(int j = 0; j < i_BoardToCopy.NumOfCols; ++j)
            {
                this.m_BoardMatrix[i, j] = i_BoardToCopy.Matrix[i, j];
            }
        }
    }

    public Cell[,] Matrix
    {
        get
        {
            return this.m_BoardMatrix;
        }

        set
        {
            this.m_BoardMatrix = value;
        }
    }

    public int NumOfTickets
    {
        get
        {
            return this.m_NumOfTickets;
        }
    }

    public int NumOfRows
    {
        get
        {
            return this.m_NumOfRows;
        }
    }

    public int NumOfCols
    {
        get
        {
            return this.m_NumOfCols;
        }
    }
}