using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class S1Mgr : MonoBehaviour
{
    public GameObject prefabs;

    public enum MyState
    {
        Idle,
        PlacingTower,
        DragTower,
    }

    private MyState currentState;
    public Button btnHero;

    private GameObject cacheTowwer;


    void Start()
    {
        currentState = MyState.Idle;
        btnHero.onClick.AddListener(OnBtnHeroClick);
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

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Vector2 myVector = Input.mousePosition;
            //myVector.x -= 5;
            //Mouse.current.WarpCursorPosition(myVector);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(btnHero.GetComponent<RectTransform>(),
                    Input.mousePosition))
            {
                OnBtnHeroClick();
            }
        }
    }

    private void OnBtnHeroClick()
    {
        currentState = MyState.PlacingTower;

        Debug.Log("開始放置");
    }

    /*---------------  Function  -------------------*/

    private void ProcessIdle()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name.Contains("后頭風獅爺"))
                {
                    // cache 
                    cacheTowwer = hit.transform.gameObject;
                    //該狀態
                    currentState = MyState.DragTower;
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
                Vector3 placePosition = hit.point;
                placePosition.y = 0.5f;
                
                if (hit.transform.gameObject.GetComponent<RoadTag>() != null)
                {
                    GameObject temp = Instantiate(prefabs);
                    temp.transform.localScale = Vector3.one;
                    temp.transform.localEulerAngles = new Vector3(30, 0, 0);
                    temp.transform.position = placePosition;
                    
                    currentState = MyState.Idle; //改變狀態!!!
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
                cacheTowwer.transform.position = dragPosition;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            cacheTowwer = null;
            currentState = MyState.Idle;
        }
    }
}
