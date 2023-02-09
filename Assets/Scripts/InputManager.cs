using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance = null;

    [field: SerializeField]
    public float Horizontal { get; private set; }

    [field: SerializeField]
    public float Vertical { get; private set; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("—цена имеет более одного InputManager! " + name);
        }
    }

    private void Update()
    {
        ListenMoveInput();
    }

    public static InputManager Get()
    {
        return _instance;
    }

    private void ListenMoveInput()
    {
        Horizontal = Input.GetAxis(nameof(Horizontal));
        Vertical = Input.GetAxis(nameof(Vertical));
    }
}
