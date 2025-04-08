using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class REnemyMove : MonoBehaviour
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
    }
}
