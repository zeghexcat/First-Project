using UnityEngine;

public class DespawnByTime : Despawn
{
    [SerializeField] protected float lifeTime = 2f;
    [SerializeField] protected float timer = 0f;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.timer = 0f;
    }

    protected override bool CanDespawn()
    {
        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.lifeTime) return false;
        return true;
    }

}
