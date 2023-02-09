using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [field: SerializeField]
    private GameObject _mazePrefab;

    [field: SerializeField]
    private GameObject _cellPrefab;

    [SerializeField]
    private GameObject _finishPrefab;

    [SerializeField]
    private GameObject _mazeOnScene;

    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;

    private Maze _maze;

    public void RespawnMaze()
    {
        Destroy(_mazeOnScene);

        SpawnMaze();
    }

    public void SpawnMaze()
    {
        _maze = MazeGenerator.Generate(_width, _height);

        _mazeOnScene = Instantiate(_mazePrefab, Vector3.zero, Quaternion.identity);

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector3 cellPosition = new Vector3(x, 0, y);

                var currentCell = _maze.Cells[x, y];
                var cellView = Instantiate(_cellPrefab, cellPosition, Quaternion.identity, _mazeOnScene.transform);

                #region Deleted needless walls

                if (cellView.TryGetComponent(out CellView cellViewComponent))
                {
                    if (x < _maze.Cells.GetLength(0) - 1 && Cell.CellIsNeighbors(currentCell, _maze.Cells[x + 1, y]))
                    {
                        cellViewComponent.WallEast.SetActive(false);
                    }
                    if (x > 0 && Cell.CellIsNeighbors(currentCell, _maze.Cells[x - 1, y]))
                    {
                        cellViewComponent.WallWest.SetActive(false);
                    }

                    if (y < _maze.Cells.GetLength(1) - 1 && Cell.CellIsNeighbors(currentCell, _maze.Cells[x, y + 1]))
                    {
                        cellViewComponent.WallNorth.SetActive(false);
                    }
                    if (y > 0 && Cell.CellIsNeighbors(currentCell, _maze.Cells[x, y - 1]))
                    {
                        cellViewComponent.WallSouth.SetActive(false);
                    }

                    if (_maze.Cells[x, y] == _maze.Start)
                    {
                        DisableOutsideWall(cellViewComponent, x, y);
                    }
                    if (_maze.Cells[x, y] == _maze.Finish)
                    {
                        DisableOutsideWall(cellViewComponent, x, y);
                    }

                    cellViewComponent.Distance = currentCell.DistanceToStart;
                }
                else
                {
                    Debug.LogError("CellPrefab haven't CellView component!");
                }

                #endregion
            }
        }

        var finishPosition = new Vector3(_maze.Finish.Position.X, 0, _maze.Finish.Position.Y);

        Instantiate(_finishPrefab, finishPosition, Quaternion.identity, _mazeOnScene.transform);
    }

    private void DisableOutsideWall(CellView cellView, int x, int y)
    {
        if (x == 0)
        {
            DisableWall(cellView.WallWest);
        }
        if (y == 0)
        {
            DisableWall(cellView.WallSouth);
        }
        if (x == _width - 1)
        {
            DisableWall(cellView.WallEast);
        }
        if (y == _height - 1)
        {
            DisableWall(cellView.WallNorth);
        }
    }

    private void DisableWall(GameObject wall)
    {
        if (wall.TryGetComponent(out MeshRenderer meshRenderer))
        {
            meshRenderer.enabled = false;
        }
    }
}
