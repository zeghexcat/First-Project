using UnityEngine;

public class BulletByTime : DespawnByTime
{
    public override void DespawnObject()
    {
        BulletSpawner.Instance.Despawn(transform.parent);
    }
}
