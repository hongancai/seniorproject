using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LGhostHpHandler : MonoBehaviour
{
    public GameObject coinPrefab;
    public int minCoins = 5;
    public int maxCoins = 7;
    
    // 血條Quad物件引用
    public GameObject healthBarQuad;
    
    private float maxHP;
    private float currentHP;

    private Animator animator;
    private bool isDying = false; // 新增狀態標記，避免重複觸發死亡
    
    void Start()
    {
        animator = GetComponent<Animator>();
        // 從GameDB獲取敵人的最大血量
        maxHP = GameDB.ghost.enemybased.HP;
        currentHP = maxHP;
        
        
        // 確保血條初始化
        if (healthBarQuad != null)
        {
            UpdateHPBar();
        }
        else
        {
            Debug.LogWarning("未設置血條Quad，請在Inspector中設置healthBarQuad！");
        }
    }

  void Update()
    {
        var vector3 = gameObject.transform.position;
        vector3.y = 0.81f; //強制更新高度
        gameObject.transform.position = vector3;
        gameObject.transform.eulerAngles = new Vector3(30, 0, 0);
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            // 每次扣10點血量進行測試
            TakeDamage(10f);
            Debug.Log($"受到10點傷害！當前血量: {currentHP}/{maxHP}");
        }
        
        // 持續更新血條顯示
        if (healthBarQuad != null && !isDying)
        {
            UpdateHPBar();
        }
    }
    
    void UpdateHPBar()
    {
        if (healthBarQuad == null) return;
        
        float hpPercentage = currentHP / maxHP;
        // 在此使用quad的scale來當作血條的變化
        healthBarQuad.transform.localScale = new Vector3(hpPercentage, 1f, 1f);
        // 設定血條的位移，使其從左側減少
        healthBarQuad.transform.localPosition = new Vector3((-0.5f + hpPercentage * 0.5f), healthBarQuad.transform.localPosition.y, 0f);
    }
    
    public void TakeDamage(float damage)
    {
        // 如果已經在死亡過程中，不再接受傷害
        if (isDying) return;
        
        // 計算實際傷害 (考慮防禦力)
        float actualDamage = Mathf.Max(1, damage - GameDB.ghost.enemybased.Def);
        
        currentHP -= actualDamage;
        currentHP = Mathf.Max(0, currentHP); // 確保不會小於0
        
        // 更新血條
        UpdateHPBar();
        
        // 如果血量歸零，處理死亡
        if (currentHP <= 0)
        {
            Die();
        }
        else if (animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("L_dmg"))
        {
            // 只有在不是死亡狀態且沒有播放受傷動畫時才播放受傷動畫
            animator.Play("L_dmg");
            
            // 設置一個短暫的協程來恢復移動動畫
            StartCoroutine(ResumeMovementAnimation());
        }
    }
    
    private IEnumerator ResumeMovementAnimation()
    {
        // 獲取受傷動畫的長度
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        // 等待受傷動畫播放完成
        yield return new WaitForSeconds(stateInfo.length);
        
        // 確保我們不是在死亡狀態，然後恢復移動動畫
        if (!isDying && currentHP > 0)
        {
            animator.Play("L_move");
        }
    }
    
    private void Die()
    {
        // 設置死亡標記
        isDying = true;
        
        // 掉落金幣
        DropCoins();
        
        
        LGhostMgr lghostManager = GetComponentInParent<LGhostMgr>();
        if (lghostManager != null)
        {
            lghostManager.SetDead();
        }
        
        // 播放死亡動畫
        if (animator != null)
        {
            // 確保動畫機設置為死亡狀態
            animator.Play("L_die");
            
            // 設置動畫監聽協程來確保動畫播放完成後才銷毀
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            // 如果沒有動畫器，直接銷毀
            Destroy(gameObject);
        }
        
        // 禁用血條顯示
        if (healthBarQuad != null)
        {
            healthBarQuad.SetActive(false);
        }
    }
    
    private IEnumerator DestroyAfterAnimation()
    {
        // 獲取當前動畫狀態資訊
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        // 計算動畫完成所需的時間
        float animationLength = stateInfo.length;
        
        // 等待動畫播放完成
        yield return new WaitForSeconds(animationLength);
        
        // 直接禁用遊戲物件，確保它不會再顯示
        gameObject.SetActive(false);
        
        // 稍微延遲一下再完全銷毀物件，避免可能的視覺問題
        yield return new WaitForSeconds(0.1f);
        
        // 銷毀物件
        Debug.Log("動畫播放完成，銷毀物件");
        Destroy(gameObject);
    }
    
    private void DropCoins()
    {
        int coinCount = Random.Range(minCoins, maxCoins + 1); // 隨機掉落 1 到 3 個金幣
        for (int i = 0; i < coinCount; i++)
        {
            // 在怪物位置生成金幣
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            Vector3 spawnPosition = transform.position + offset;
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
    
    public void SetHp(float hp)
    {
        currentHP = Mathf.Clamp(hp, 0f, maxHP);
        UpdateHPBar();
    }
    
    // 獲取當前血量百分比
    public float GetHpPercentage()
    {
        return currentHP / maxHP;
    }
    
    // 提供一個方法讓其他腳本檢查是否正在死亡
    public bool IsDying()
    {
        return isDying;
    }
}
