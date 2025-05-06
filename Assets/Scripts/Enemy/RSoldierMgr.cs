using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RSoldierMgr : MonoBehaviour
{
    public Transform traget;
    public NavMeshAgent agent;
    
    private float soldierSpeed;
    private Animator animator;
    
    private RSoldierHpHandler hpHandler;
    
    private bool isDead = false;
    void Start()
    {
        soldierSpeed = GameDB.soilder.enemybased.Spd;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = soldierSpeed;
        animator = GetComponent<Animator>();
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
                animator.GetCurrentAnimatorStateInfo(0).IsName("R_die") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("R_dmg");
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
                animator.Play("R_move");
            }
            
            gameObject.transform.eulerAngles = new Vector3(30, 0, 0);
            var vector3 = gameObject.transform.position;
            vector3.y = 0.66f; //強制更新高度
            gameObject.transform.position = vector3;
        }
    }

    public void SetDead()
    {
        isDead = true;
        
        // 停止NavMeshAgent
        if (agent != null && agent.isActiveAndEnabled)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
        
    }
}
