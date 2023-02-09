using System;
using System.Collections.Generic;

public static class MazeGenerator
{
    public static Maze LastGeneratedMaze;

    public static int Width { private set; get; }
    public static int Height { private set; get; }

    public static Maze Generate(int width, int height)
    {
        LastGeneratedMaze = new Maze();

        Width = width;
        Height = height;

        LastGeneratedMaze.Cells = new Cell[Width, Height];

        GenerateCellGrid();

        FindStartCell();

        SaveDistanceFromCellsToStart();

        FindFinishCell();

        return LastGeneratedMaze;
    }

    private static void GenerateCellGrid()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                LastGeneratedMaze.Cells[x, y] = new Cell(x, y);
            }
        }
    }

    private static void FindStartCell()
    {
        Cell startCell;

        var random = new Random();

        bool horizontalStart = 0 == random.Next(2);

        if (horizontalStart)
        {
            bool bottomStart = 0 == random.Next(2);

            if (bottomStart)
            {
                startCell = LastGeneratedMaze.Cells[random.Next(Width), 0];
            }
            else
            {
                startCell = LastGeneratedMaze.Cells[random.Next(Width), Height - 1];
            }
        }
        else
        {
            bool leftStart = 0 == random.Next(2);

            if (leftStart)
            {
                startCell = LastGeneratedMaze.Cells[0, random.Next(Height)];
            }
            else
            {
                startCell = LastGeneratedMaze.Cells[Width - 1, random.Next(Height)];
            }
        }

        LastGeneratedMaze.Start = startCell;

        LastGeneratedMaze.Start.TrySetDistanceToStart(0);
    }

    private static void SaveDistanceFromCellsToStart()
    {
        var currentCell = LastGeneratedMaze.Start;

        var random = new Random();

        var stack = new Stack<Cell>();

        do
        {
            var unprocessedNeighborsCells = new List<Cell>();

            var x = currentCell.Position.X;
            var y = currentCell.Position.Y;

            #region Search unprocessed neighbors
            if (x > 0 && !LastGeneratedMaze.Cells[x - 1, y].IsProcessed)
            {
                unprocessedNeighborsCells.Add(LastGeneratedMaze.Cells[x - 1, y]);
            }
            if (y > 0 && !LastGeneratedMaze.Cells[x, y - 1].IsProcessed)
            {
                unprocessedNeighborsCells.Add(LastGeneratedMaze.Cells[x, y - 1]);
            }
            if (x < Width - 1 && !LastGeneratedMaze.Cells[x + 1, y].IsProcessed)
            {
                unprocessedNeighborsCells.Add(LastGeneratedMaze.Cells[x + 1, y]);
            }
            if (y < Height - 1 && !LastGeneratedMaze.Cells[x, y + 1].IsProcessed)
            {
                unprocessedNeighborsCells.Add(LastGeneratedMaze.Cells[x, y + 1]);
            }
            #endregion

            if (unprocessedNeighborsCells.Count > 0)
            {
                Cell nextNeighborCell = unprocessedNeighborsCells[random.Next(unprocessedNeighborsCells.Count)];

                stack.Push(nextNeighborCell);
                currentCell = nextNeighborCell;

                nextNeighborCell.TrySetDistanceToStart(stack.Count);
            }
            else
            {
                currentCell = stack.Pop();
            }

        } while (stack.Count > 0);
    }

    private static void FindFinishCell()
    {
        Cell furthest = LastGeneratedMaze.Cells[0, 0];

        for (int x = 0; x < LastGeneratedMaze.Cells.GetLength(0); x++)
        {
            if (LastGeneratedMaze.Cells[x, 0].DistanceToStart > furthest.DistanceToStart)
            {
                furthest = LastGeneratedMaze.Cells[x, 0];
            }
            if (LastGeneratedMaze.Cells[x, Height - 1].DistanceToStart > furthest.DistanceToStart)
            {
                furthest = LastGeneratedMaze.Cells[x, Height - 1];
            }
        }

        for (int y = 0; y < LastGeneratedMaze.Cells.GetLength(1); y++)
        {
            if (LastGeneratedMaze.Cells[0, y].DistanceToStart > furthest.DistanceToStart)
            {
                furthest = LastGeneratedMaze.Cells[0, y];
            }
            if (LastGeneratedMaze.Cells[Width - 1, y].DistanceToStart > furthest.DistanceToStart)
            {
                furthest = LastGeneratedMaze.Cells[Width - 1, y];
            }
        }

        LastGeneratedMaze.Finish = furthest;
    }
}
