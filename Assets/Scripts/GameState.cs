using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private static GameState _instance;

    [SerializeField]
    private Canvas _mainMenu;

    [SerializeField]
    private Canvas _levelEndMenu;

    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private MazeSpawner _mazeSpawner;

    private GameObject _playerOnScene;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("More that one GameStates on scene!");
        }
    }

    public static GameState Instance()
    {
        return _instance;
    }

    public void StartGame()
    {
        _mazeSpawner.SpawnMaze();
        CreatePlayer();
        DisableCanvas(_mainMenu);
    }

    public void RetryGame()
    {
        _mazeSpawner.RespawnMaze();

        var startPosition = new Vector3(MazeGenerator.LastGeneratedMaze.Start.Position.X,
                                        0,
                                        MazeGenerator.LastGeneratedMaze.Start.Position.Y);
        _playerOnScene.transform.position = startPosition;

        DisableCanvas(_levelEndMenu);
    }

    public void FinishGame()
    {
        EnableCanvas(_levelEndMenu);
    }

    private void CreatePlayer()
    {
        var startPosition = new Vector3(MazeGenerator.LastGeneratedMaze.Start.Position.X,
                                        0,
                                        MazeGenerator.LastGeneratedMaze.Start.Position.Y);

        _playerOnScene = Instantiate(_playerPrefab, startPosition, Quaternion.identity);
    }

    private void EnableCanvas(Canvas canvas)
    {
        canvas.enabled = true;
    }

    private void DisableCanvas(Canvas canvas)
    {
        canvas.enabled = false;
    }
}
