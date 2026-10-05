using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    [Header("Moment Settings")]
    [SerializeField] protected float stopDistance = 0.5f;
    [SerializeField] protected float speed = 10f;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    private Vector3 targetPosition;

     private void FixedUpdate()
    {
        this.GetTargetPosition();
        this.CheckAndMoving();
       
    }

    private void Awake()
    {
        // Lấy Rigidbody2D ở bản thân GameObject trước, nếu không có mới tìm ở Parent
        if (rb == null) rb = GetComponentInParent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
    }

    protected virtual void GetTargetPosition()
    {
        this.targetPosition = InputManager.Instance.MouseWorldPos;
        this.targetPosition.z = 0;
    }

    protected virtual void LookAtTarget()
    {
        Vector3 diff = this.targetPosition - transform.parent.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
       
        if (this.rb != null)
        {
            this.rb.MoveRotation(rot_z - 90);
   
        }
        if(anim != null)
        {
            this.anim.SetBool("isMoving", true);
        }
        else
        {
            transform.parent.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        }
    }
    protected virtual void Moving()
    {
        Vector3 dir = (this.targetPosition - transform.parent.position).normalized;

        if (this.rb != null)
        {
            this.rb.linearVelocity = dir * this.speed;
        }
        else
        {
            Vector3 newPos = Vector3.MoveTowards(transform.parent.position, this.targetPosition, this.speed * Time.fixedDeltaTime);
        }
        
    }

    protected virtual void CheckAndMoving()
    {
        float distance = Vector3.Distance(transform.parent.position, this.targetPosition);

        if (distance > stopDistance)
        {
            this.LookAtTarget();
            this.Moving();
        }
        else
        {
            this.StopMove();
        }
    }

    protected virtual void StopMove()
    {
        if (this.rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            this.rb.angularVelocity = 0f;
        }

        if (this.anim != null)
        {
            this.anim.SetBool("isMoving", false);
        }
        
    }
}
