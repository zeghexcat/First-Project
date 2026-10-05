using UnityEngine;

public class InputManager : MonoBehaviour
{
    protected static InputManager instance;
    public static InputManager Instance { get => instance; }

    [SerializeField] protected bool isShooting = false;
    public bool IsShooting { get => isShooting; }

    [SerializeField] protected Vector3 mouseWorldPos;

    public Vector3 MouseWorldPos { get => mouseWorldPos; }
    protected void Awake()
    {
        if (InputManager.instance != null) Debug.LogError("Chi Duoc Dung 1 InputManager");
        InputManager.instance = this;
    }
    void FixedUpdate()
    {
       this.GetMousePos();
        this.GetIsShooting();
    }

    protected virtual void GetMousePos()
    {
        this.mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected virtual void GetIsShooting()
    {
        this.isShooting = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);
    }
}
