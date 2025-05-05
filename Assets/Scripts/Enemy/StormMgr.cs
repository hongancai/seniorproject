using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StormMgr : MonoBehaviour
{
    public Transform traget;
    public NavMeshAgent agent;
    
    private Animator animator;
    private bool isDead = false;
    
    // 添加對血量處理器的引用
    private StormHpHandler hpHandler;
   
    public enum Storm
    {
        Find,
        Attack
    }
    
    void Start()
    {  
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>(); // 獲取子物件上的Animator
        hpHandler = GetComponentInChildren<StormHpHandler>();
    }

    void Update()
    {
        // 如果已死亡或正在受傷/死亡動畫中，不再執行移動和動畫播放
        if (isDead || (hpHandler != null && hpHandler.IsDying())) 
            return;
        
        // 檢查是否有任何動畫正在播放
        bool isPlayingSpecialAnimation = false;
        
        if (animator != null)
        {
            isPlayingSpecialAnimation = 
                animator.GetCurrentAnimatorStateInfo(0).IsName("die") || 
                animator.GetCurrentAnimatorStateInfo(0).IsName("dmg");
        }
        
        // 檢查目標是否存在且當前沒有播放特殊動畫
        if (traget != null && !isPlayingSpecialAnimation)
        {
            // 設置導航目標
            if (agent != null && agent.isActiveAndEnabled)
            {
                agent.SetDestination(traget.transform.position);
            }
            
            // 只有在沒有播放特殊動畫時才播放移動動畫
            if (animator != null)
            {
                animator.Play("move");
            }
            
            // 保持固定角度
            gameObject.transform.eulerAngles = new Vector3(30, 0, 0);
            
            // 強制更新高度
            var vector3 = gameObject.transform.position;
            vector3.y = 0.67f;
            gameObject.transform.position = vector3;
        }
    }
    
    // 設置死亡狀態的方法，供血量處理器調用
    public void SetDead()
    {
        isDead = true;
        
        // 停止NavMeshAgent
        if (agent != null && agent.isActiveAndEnabled)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
        
        // 注意：我們不在這裡播放死亡動畫，由StormHpHandler負責處理
    }
}