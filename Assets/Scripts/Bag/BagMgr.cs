using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagMgr : MonoBehaviour
{
    public GameObject bagPanel;     
    public Button bagCloseButton; 
    public bool isShow;

    void Start()
    {
        bagPanel.SetActive(false);
        bagCloseButton.onClick.AddListener(CloseBagPnl);
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            isShow = !isShow;
            ToggleBag();
        }
    }

    public void ToggleBag()
    {
        bagPanel.SetActive(true);
        
    }

    public void CloseBagPnl()
    {
        bagPanel.SetActive(false);
    }
}