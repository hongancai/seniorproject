using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QionglinMgr : MonoBehaviour
{
    public GameObject qionglinprefabs;
    public AudioClip placingsfx;
    
    public enum QionglinState
    {
        Idle,
        Placing,
        Cancel,
        Drag,
    }

    private QionglinState currentState;
    public Button btnQionglin;
    public GameObject followQionglinImage;

    private GameObject cache砲塔;

    void Start()
    {
        cache砲塔 = null;
        currentState = QionglinState.Idle;
        btnQionglin.onClick.AddListener(OnBtnQiongClick);
    }

    private void OnBtnQiongClick()
    {
        followQionglinImage.gameObject.SetActive(true);
        currentState = QionglinState.Placing;
        btnQionglin.interactable = false;
        Debug.Log("開始丟瓊林風獅爺喔");
    }


    void Update()
    {
        switch (currentState)
        {
            case QionglinState.Idle:
                ProcessIdle();
                break;
            case QionglinState.Placing:
                ProcessPlacingTower();
                if (Input.GetMouseButtonDown(1)) // 按下右鍵
                {
                    currentState = QionglinState.Cancel; // 切換到取消狀態
                }
                break;
            case QionglinState.Cancel:
                ProcessCancel();
                break;
            case QionglinState.Drag:
                ProcessDargTower();
                if (cache砲塔 != null)
                {
                    RacastAll();
                }
                break;
        }

        if (currentState == QionglinState.Placing)
        {
            followQionglinImage.transform.position = Input.mousePosition;
        }
    }
    

    private void ProcessIdle()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<QionglinTag>() != null)
                {
                    // cache 
                    cache砲塔 = hit.transform.gameObject;
                    //該狀態
                    currentState = QionglinState.Drag;
                }
            }
        }
    }

    private void ProcessPlacingTower()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<RoadTag>() != null)
                {
                    //Vector3 placePosition = hit.point;
                    GameDB.Audio.PlaySfx(placingsfx);
                    GameObject temp = Instantiate(qionglinprefabs);
                    temp.transform.localScale = Vector3.one;
                    temp.transform.localEulerAngles = new Vector3(30, 0, 0);
                    Vector3 position = hit.point;
                    position.y = 0;
                    temp.transform.localPosition = position;
                    followQionglinImage.gameObject.SetActive(false);
                    currentState = QionglinState.Idle; //改變狀態!!!
                }
            }
        }
    }

    private void ProcessCancel()
    {
        // 隱藏跟隨圖片
        followQionglinImage.gameObject.SetActive(false);

        // 重新啟用按鈕
        btnQionglin.interactable = true;

        // 重置狀態
        currentState = QionglinState.Idle;

        Debug.Log("取消放置");
    }

    private void ProcessDargTower()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.GetComponent<RoadTag>() != null)
            {
                Debug.Log(hit.transform.gameObject.name);
                cache砲塔.transform.localPosition = hit.point;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            cache砲塔 = null;
            currentState = QionglinState.Idle;
        }
    }

    void RacastAll()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Debug.Log(hit.transform.gameObject.name);
            hit.transform.gameObject.GetComponent<QionglinTag>();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<RoadTag>() != null)
                {
                    Debug.Log(hit.transform.gameObject.name);
                    cache砲塔.transform.localPosition = hit.point;
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                cache砲塔 = null;
                currentState = QionglinState.Idle;
            }
        }
    }
}