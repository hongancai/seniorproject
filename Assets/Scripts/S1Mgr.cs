using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S1Mgr : MonoBehaviour
{
    public AudioClip s1bgm;
    public List<GameObject> lionPrefabs; // 5 隻風獅爺的 Prefab
    public List<Button> lionButtons; // 5 個按鈕

    public enum MyState
    {
        Idle,
        PlacingTower,
        DragTower,
    }

    private MyState currentState;
    private GameObject cacheTower;

    private GameObject selectedPrefab; // 當前選擇的 Prefab

    void Start()
    {
        GameDB.Audio.PlayBgm(s1bgm);
        currentState = MyState.Idle;

        // 初始化按鈕點擊事件
        for (int i = 0; i < lionButtons.Count; i++)
        {
            int index = i; // 避免閉包問題
            lionButtons[i].onClick.AddListener(() => OnLionButtonClick(index));
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case MyState.Idle:
                ProcessIdle();
                break;
            case MyState.PlacingTower:
                ProcessPlacingTower();
                break;
            case MyState.DragTower:
                ProcessDragTower();
                break;
        }
    }

    private void OnLionButtonClick(int index)
    {
        if (index >= 0 && index < lionPrefabs.Count)
        {
            selectedPrefab = lionPrefabs[index];
            currentState = MyState.PlacingTower;
            Debug.Log($"準備放置風獅爺: {lionPrefabs[index].name}");
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
                Debug.Log($"hit.transform.gameObject.name===>{hit.transform.gameObject.name}");
                if (hit.transform.parent.gameObject.name.Contains("風獅爺"))
                {
                    Debug.Log("Hit");
              //  if (hit.transform.gameObject.name.Contains("劉澳風獅爺" + "后水風獅爺" + "塔后風獅爺" + "安崎風獅爺" + "瓊林風獅爺"))
                //{
                    // cache 
                    cacheTower = hit.transform.parent.gameObject;
                    // 該狀態
                    currentState = MyState.DragTower;
                }
            }
        }
    }

    private void ProcessPlacingTower()
    {
        if (Input.GetButtonDown("Fire1") && selectedPrefab != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 placePosition = hit.point;
                placePosition.y = 0.5f;

                if (hit.transform.gameObject.GetComponent<RoadTag>() != null)
                {
                    GameObject temp = Instantiate(selectedPrefab);
                    temp.transform.localScale = Vector3.one;
                    temp.transform.localEulerAngles = new Vector3(30, 0, 0);
                    temp.transform.position = placePosition;

                    currentState = MyState.Idle; // 改變狀態
                    selectedPrefab = null; // 清除選擇
                }
            }
        }
    }

    private void ProcessDragTower()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.GetComponent<RoadTag>() != null)
            {
                Vector3 dragPosition = hit.point;
                dragPosition.y = 0.5f; // 確保 Y 軸固定為 0.5
                cacheTower.transform.position = dragPosition;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            cacheTower = null;
            currentState = MyState.Idle;
        }
    }
}
