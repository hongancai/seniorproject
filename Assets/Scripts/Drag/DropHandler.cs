using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropHandler : MonoBehaviour,IDropHandler
{
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
    }
}