using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiongLinHpHandler : MonoBehaviour
{
    // 血條Quad物件引用
    public GameObject healthBarQuad;
    
    private float maxHP;
    private float currentHP;
    private int currentLevel = 1;

    private bool isDead = false;
    
    public AudioClip hurtsfx;
   
    
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        // 獲取初始最大血量
        maxHP = GameDB.qionglin.based.HP;
        currentHP = maxHP; // 開始時血量滿的
        
        // 輸出初始血量到Console
        Debug.Log($"瓊林初始血量: {maxHP}");
    }

    void Update()
    {
        // 檢查是否升級
        CheckLevelChange();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 每次扣30點血量進行測試
            TakeDamage(30f);
            Debug.Log($"受到30點傷害！當前血量: {currentHP}/{maxHP}");
        }
        // 更新血條顯示
        UpdateHPBar();
    }

    void CheckLevelChange()
    {
        // 檢查當前等級是否改變
        if (GameDB.qionglin.Lv != currentLevel)
        {
            // 計算血量百分比
            float hpPercentage = currentHP / maxHP;
            
            // 更新等級
            currentLevel = GameDB.qionglin.Lv;
            
            // 更新最大血量
            maxHP = GameDB.qionglin.based.HP;
            
            // 保持相同的血量百分比
            currentHP = maxHP * hpPercentage;
            
            // 輸出升級後的血量資訊
            Debug.Log($"瓊林升級到 {currentLevel} 級，新的最大血量: {maxHP}，當前血量: {currentHP}");
        }
    }

    void UpdateHPBar()
    {
        // 計算血量百分比
        float hpPercentage = Mathf.Clamp01(currentHP / maxHP);
        
        // 更新血條的比例
        healthBarQuad.transform.localScale = new Vector3(hpPercentage, 1f, 1f);
        
        // 設定血條的位移 (使血條從左側減少)
        healthBarQuad.transform.localPosition = new Vector3((-0.5f + hpPercentage * 0.5f), healthBarQuad.transform.localPosition.y, 0f);
    }

    // 外部調用的方法，用於設置當前血量
    public void SetHP(float hp)
    {
        currentHP = Mathf.Clamp(hp, 0f, maxHP);
        Debug.Log($"瓊林血量更新為: {currentHP}/{maxHP}");
        
        // 檢查是否死亡
        if (currentHP <= 0 && !isDead)
        {
            Die();
        }
    }

    // 外部調用的方法，用於受到傷害
    public void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }
        float actualDamage = Mathf.Max(1, damage - GameDB.qionglin.based.Def); // 考慮防禦力
        currentHP = Mathf.Max(0, currentHP - actualDamage);
        Debug.Log($"瓊林受到 {actualDamage} 點傷害，剩餘血量: {currentHP}/{maxHP}");
        GameDB.Audio.PlaySfx(hurtsfx);
       
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    { 
        isDead = true;
        animator.Play("die");
        Debug.Log("瓊林已死亡！");
        
    }

    public void Rebirth()
    {
        if (isDead)
        {
            isDead = false;
            // 恢復到最大血量
            currentHP = maxHP;
            
            // 切換到Idle動畫
            animator.Play("Idle");
            
            // 更新UI
            UpdateHPBar();
            
            Debug.Log($"瓊林復活了！當前血量: {currentHP}/{maxHP}");
            
            // 不再需要在此更新 InfoPanel 的按鈕，讓 InfoPanelHandler 負責這個工作
        }
    }

    // 外部調用的方法，用於治療
    public void Heal(float amount)
    {
        amount = 80f;
        currentHP = Mathf.Min(maxHP, currentHP + amount);
        Debug.Log($"瓊林恢復 {amount} 點血量，當前血量: {currentHP}/{maxHP}");
    }
    
    // 檢查是否已死亡
    public bool IsDead()
    {
        return isDead;
    }
}