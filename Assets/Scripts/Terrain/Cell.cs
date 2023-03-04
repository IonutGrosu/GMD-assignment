public class Cell
{
    // public bool isWater;
    public CellType CellType { get; set; }

    public Cell(CellType cellType)
    {
        this.CellType = cellType;
    }
}

public enum CellType
{
    GRASS,
    SAND,
    WATER
}