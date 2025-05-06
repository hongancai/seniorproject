using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class TowerAtk : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        animator.Play("atk");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }
}
