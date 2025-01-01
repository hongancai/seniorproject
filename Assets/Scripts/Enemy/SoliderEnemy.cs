using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoliderEnemy : MonoBehaviour
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
        gameObject.transform.eulerAngles = new Vector3(30, 0, 0);
        agent.SetDestination(traget.transform.position);
        animator.Play("R_move"); 
    }
}
