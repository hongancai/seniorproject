using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RSoldierMgr : MonoBehaviour
{
    public Transform traget;
    public NavMeshAgent agent;
    
    private Animator animator;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

  
    void Update()
    {
        agent.SetDestination(traget.transform.position);
        animator.Play("R_move");
        gameObject.transform.eulerAngles = new Vector3(30, 0, 0);
        var vector3 = gameObject.transform.position;
        vector3.y = 0.66f; //強制更新高度
        gameObject.transform.position = vector3;
    }
}
