using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float attackCooldown = 1f;
    
    private Animator animator;         // 動畫控制器
    private float lastAttackTime;

    void Start()
    {
        GameDB.playerAtk = 10;
        animator = GetComponent<Animator>(); // 獲取動畫控制器
    }

    void Update()
    {
        // 當滑鼠左鍵點擊時且攻擊冷卻時間已過
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            PerformAttack();
            lastAttackTime = Time.time; // 記錄此次攻擊時間
        }
    }

    void PerformAttack()
    {
        // 播放攻擊動畫
        if (animator != null)
        {
            animator.SetTrigger("Attack"); // 設置 "Attack" 觸發器，播放攻擊動畫
        }

        // 發射 Raycast 檢測是否點擊到敵人
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 取得從攝影機射出的 Ray
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
        {
            // 確認點擊的是敵人
            //EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            //if (enemyHealth != null)
            {
                //enemyHealth.TakeDamage(attackDamage); // 對敵人造成傷害
            }
        }
    }
}