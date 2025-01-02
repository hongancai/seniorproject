using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3Return : MonoBehaviour
{
    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TransitionManager.LoadSceneWithTransition("S1", "S3");
        }
    }
}
