using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMgr : MonoBehaviour
{
    public AudioClip openshopsfx;
    public AudioClip btnsfx;
    public GameObject shopPnl;
    public Button closeshoBtn;

    void Start()
    {
        shopPnl.gameObject.SetActive(false);
        closeshoBtn.onClick.AddListener(OnBtnClose);
    }

    private void OnBtnClose()
    {
        Time.timeScale = 1f;
        shopPnl.gameObject.SetActive(false);
        GameDB.Audio.PlaySfx(btnsfx);
    }


    void Update()
    {
        if (shopPnl.activeSelf)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<ShopTag>() != null)
                {
                    Time.timeScale = 0f;
                    GameDB.Audio.PlaySfx(openshopsfx);
                    shopPnl.gameObject.SetActive(true);
                    Debug.Log("測試開商店");
                }
            }
        }
    }
}