using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    void Start()
    {
        
    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(30, 0, 0);
        var vector3 = transform.position;
        vector3.y = 0.21f;
        transform.position = vector3;
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