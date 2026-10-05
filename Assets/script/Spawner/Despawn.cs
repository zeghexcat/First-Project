using UnityEditor.Tilemaps;
using UnityEngine;

public abstract class Despawn : ZcMonobehaviour
{
    protected virtual void FixedUpdate()
    {
        this.Despawning();
    }

    protected virtual void Despawning()
    {
        if (!this.CanDespawn()) return;
        this.DespawnObject();
    }

    protected virtual bool CanDespawn()
    {
        return false;
    }

    public virtual void DespawnObject()
    {
        gameObject.SetActive(false);
    }
}
