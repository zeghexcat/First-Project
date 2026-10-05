using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BulletSpawner : Spawner
{
    private static BulletSpawner instance;
    public static BulletSpawner Instance => instance;
    public static string bulletOne = "BulletZS1_0";

    protected override void Awake()
    {
        base.Awake();
        if (BulletSpawner.instance != null)
        {
            Debug.LogError("Chỉ được có 1 BulletSpawner!");
            return;
        }
        BulletSpawner.instance = this;
    }
}
