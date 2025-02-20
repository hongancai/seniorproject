using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMgr : MonoBehaviour
{
    public GameObject shopPnl;
    public Button closeshoBtn;

    void Start()
    {
        shopPnl.gameObject.SetActive(false);
        closeshoBtn.onClick.AddListener(OnBtnClose);
    }

    private void OnBtnClose()
    {
        shopPnl.gameObject.SetActive(false);
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<ShopTag>() != null)
                {
                    shopPnl.gameObject.SetActive(true);
                    Debug.Log("測試開商店");
                }
            }
        }
    }
}