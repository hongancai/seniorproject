using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    void Start()
    {
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameDB.money += 10;
            GameDB.coinPos = Vector3.zero;// 在GameDB中重置金幣位置
            GameDB.Save();
            Destroy(gameObject);
        }
    }
}