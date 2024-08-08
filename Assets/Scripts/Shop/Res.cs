using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Res : MonoBehaviour
{
    public List<Sprite> itemList;

     void Awake()
     {
         GameDB.res = this;
     }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
