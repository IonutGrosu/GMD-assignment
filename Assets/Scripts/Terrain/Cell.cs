public class Cell
{
    public CellType CellType { get; }

    public Cell(CellType cellType)
    {
        CellType = cellType;
    }
}

public enum CellType
{
    GRASS,
    SAND,
    WATER
}