using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Spawner : ZcMonobehaviour
{
    [Header("Spawner Base Settings")]
    [SerializeField] protected List<Transform> prefabs = new List<Transform>();
    [SerializeField] protected List<Transform> poolObjs = new List<Transform>();
    [SerializeField] protected Transform holder; // Nơi chứa các object clone ra cho gọn Hierarchy

    [Header("Pool Limits")]
    [SerializeField] protected int maxPoolSize = 10;

    protected override void Awake()
    {
        base.Awake();
        this.LoadPrefabs();
        this.LoadHolder();
    }
    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;
        Transform prefabsObj = transform.Find("Prefabs");
        if (prefabsObj == null) return;

        foreach (Transform prefab in prefabsObj)
        {
            this.prefabs.Add(prefab);
        }
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        if (this.holder == null)
        {
            GameObject newHolder = new GameObject("Holder");
            newHolder.transform.SetParent(transform);
            this.holder = newHolder.transform;
        }
    }

    public virtual Transform Spawn(string prefabName, Vector3 position, Quaternion rotation)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null) 
        {
            Debug.LogError("Không Tìm Thấy Prefabs đạn: " + prefabName);
            return null;
        }

        Transform newObj = this.GetObjectFromPool(prefab);

        if (newObj == null)
        {
            return null;
        }

        newObj.SetPositionAndRotation(position, rotation);
        newObj.SetParent(this.holder);
        newObj.gameObject.SetActive(true);

        return newObj;
    }

    protected virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform obj in this.poolObjs)
        {
            if (obj == null) continue;
            if (!obj.gameObject.activeSelf && obj.name == prefab.name)
            {
                return obj;
            }
        }

        if (this.poolObjs.Count >= this.maxPoolSize)
        {
            Debug.LogWarning("đã đặt giới hạn tối đa" + this.maxPoolSize + " viên đan trong Pool!");
            return null;
        }

        Transform newObj = Instantiate(prefab);
        newObj.name = prefab.name; // Giữ nguyên tên để match pool sau này
        this.poolObjs.Add(newObj);
        return newObj;
    }

    protected virtual Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name == prefabName) return prefab;
        }
        return null;
    }

    public virtual void Despawn(Transform obj)
    {
        obj.gameObject.SetActive(false);
    }
}
