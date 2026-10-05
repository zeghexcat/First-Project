using UnityEngine;

public class ZcMonobehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        this.LoadComponents();
        this.ResetValues();
    }

    protected virtual void Awake()
    {
        this.LoadComponents();
    }
    
    protected virtual void OnEnable()
    {

    }
    //tự động gán components
    protected virtual void LoadComponents()
    {

    }

    //khởi tạo giá trị mặc định cho biến
    protected virtual void ResetValues()
    {

    }
}
