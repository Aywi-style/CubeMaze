public class Cell
{
    public readonly CellPosition Position;
    public int DistanceToStart { private set; get; } = -1;
    public bool IsProcessed { private set; get; } = false;

    public Cell(CellPosition position)
    {
        Position = position;
    }

    public Cell(int xPosition, int yPosition)
    {
        Position = new CellPosition()
        {
            X = xPosition,
            Y = yPosition
        };
    }

    public bool TrySetDistanceToStart(int distanceToStart)
    {
        if (IsProcessed)
        {
            return false;
        }
        else
        {
            DistanceToStart = distanceToStart;
            IsProcessed = true;
            return true;
        }
    }

    public static bool CellIsNeighbors(Cell firstCell, Cell secondCell)
    {
        if (firstCell.DistanceToStart >= secondCell.DistanceToStart - 1 && firstCell.DistanceToStart <= secondCell.DistanceToStart + 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool CellIsNeighbors(int firstDistanceToStart, int secondDistanceToStart)
    {
        if (firstDistanceToStart >= secondDistanceToStart - 1 && firstDistanceToStart <= secondDistanceToStart + 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
