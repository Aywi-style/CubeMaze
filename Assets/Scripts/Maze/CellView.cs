using UnityEngine;

public class CellView : MonoBehaviour
{
    [field: SerializeField]
    public GameObject WallNorth { private set; get; }

    [field: SerializeField]
    public GameObject WallSouth { private set; get; }

    [field: SerializeField]
    public GameObject WallWest { private set; get; }

    [field: SerializeField]
    public GameObject WallEast { private set; get; }

    public int Distance;
}
