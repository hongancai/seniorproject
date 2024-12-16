using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1Mgr : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private GameObject currentPlacingTower;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        if (currentPlacingTower != null)
        {
            Ray camray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camray,out RaycastHit hitInfo,100f))
            {
                currentPlacingTower.transform.position = hitInfo.point;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            currentPlacingTower = null;
        }
    }

    public void SetTowerToPlace(GameObject tower)
    {
        currentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
    }
}
