using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {  
      _agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(GameObject.Find("Tower").transform.position);
    }
}
