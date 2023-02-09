using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            GameState.Instance().FinishGame();
        }
    }
}
