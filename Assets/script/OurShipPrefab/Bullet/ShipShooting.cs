using System;
using Unity.VisualScripting;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    //thay thế
    [SerializeField] protected BulletSpawner bulletSpawner;
    //[SerializeField] protected Transform bulletPrefab;

    [SerializeField] protected float shootDelay = 0.2f;
    protected float shootTimer = 0f;

    private void FixedUpdate()
    {
        this.Shooting();
    }

    protected virtual void Shooting()
    {
        if (!InputManager.Instance.IsShooting) return;

        //xử lý dellay đạn
        this.shootTimer += Time.fixedDeltaTime;
        if (this.shootTimer < this.shootDelay) return;
        this.shootTimer = 0f;

        //lấy vị trí chuẩn của tàu
        Transform shipTransform = transform.parent != null ? transform.parent : transform;
        //lấy vị trí chuẩn góc xoay tàu và mũi tàu
        Vector3 spawnPos =  shipTransform.position + shipTransform.up * 0.8f;
        Quaternion spawnRot = shipTransform.rotation;

        //Transform newBullet = Instantiate(this.bulletPrefab, spawnPos, spawnRot);
        //newBullet.gameObject.SetActive(true);       
        Debug.Log("Dang Ban");

        if (this.bulletSpawner != null)
        {
            BulletSpawner.Instance.Spawn(BulletSpawner.bulletOne, spawnPos, spawnRot);
        }

    }

    
}
