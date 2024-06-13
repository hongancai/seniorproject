using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform _traget;
    public NavMeshAgent _agent;
    public int hitPoints = 3;
    // Start is called before the first frame update
    void Start()
    {  
      _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_traget.transform.position);
    }

    public void TakeDamage()
    {
        hitPoints--;

        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
