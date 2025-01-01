using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    void Start()
    {
        
    }
    
    public void Seek(Transform target)
    {
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    { 
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World); 
    }
    void HitTarget()
    {
        // 攻擊目標
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            //enemy.TakeDamage();
        }

        Destroy(gameObject);
    }
}
