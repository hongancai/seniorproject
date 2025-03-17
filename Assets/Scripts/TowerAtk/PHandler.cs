using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class PHandler : MonoBehaviour
{
    
    
    private float count;

    private IList<GameObject> MList;

    
    void Start()
    {
        MList = new List<GameObject>();
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
          
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (!MList.Contains(other.gameObject)
            && other.gameObject.name != "Plane")
        {
            MList.Add(other.gameObject);
        }

        count++;
        if (count > 100)
        {
            // Debug.Log($"OnTriggerStay:{other.gameObject.name}");
            Debug.Log($"{MList.Count}");

            var r = from q in MList
                orderby Vector3.Distance(this.transform.localPosition,
                    q.transform.localPosition) ascending 
                select q;

            if (r.Any())
            {
                GameObject first = r.First();
                Debug.Log($"NEARS :{first.name}");
            }


            // atk ... 


            MList.Clear();
            count = 0;
        }
    }
}