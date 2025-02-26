using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // 投射物預製件
    public Transform firePoint; // 投射物發射點
    public float fireRate = 1f; // 發射速率
    private float fireCooldown = 0f; // 發射冷卻時間
    private Transform target; // 目標

    private Animator animator;

    void Update()
    {
        if (target != null)
        {
            gameObject.transform.eulerAngles = new Vector3(30, 0, 0);
            // 瞄準目標
            Vector3 direction = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            fireCooldown -= Time.deltaTime;

            if (fireCooldown <= 0f)
            {
                
                Fire();
                fireCooldown = 1f / fireRate;
            }
        }
    }

    public void TargetEnemy(Transform enemy)
    {
        target = enemy;
    }

    void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.Seek(target);
        }
    }
}