using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teach : MonoBehaviour
{
    public Button teachBtn;
    public GameObject teachpnl;
    public Button closeBtn;
    
    void Start()
    {
        teachpnl.SetActive(false);
        teachBtn.onClick.AddListener(TeachClick);
        closeBtn.onClick.AddListener(CloseClick);
    }

    private void CloseClick()
    {
        teachpnl.SetActive(false);
    }

    private void TeachClick()
    {
        teachpnl.SetActive(true);
    }


    void Update()
    {
        
    }
    
}
