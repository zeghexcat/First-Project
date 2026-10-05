using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BulletFly : ZcMonobehaviour
{
    [SerializeField] protected int forwardSpeed = 20;
    [SerializeField] protected Rigidbody2D rb;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
    }

    protected virtual void LoadRigidbody()
    {
        if (this.rb != null) return;
        this.rb = GetComponent<Rigidbody2D>();
    }
    protected override void OnEnable()
    {
        // Reset vận tốc khi đạn được lấy ra từ Pool
        if (this.rb != null)
        {
            this.rb.linearVelocity = Vector2.zero;
        }

    }

    protected void FixedUpdate()
    {
        this.Flying();
    }

    protected virtual void Flying()
    {
        if (this.rb == null) return;
        this.rb.linearVelocity = transform.up * this.forwardSpeed;
    }
}
