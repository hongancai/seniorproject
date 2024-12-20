using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // 連結到你的 Cinemachine Virtual Camera
    public float zoomSpeed = 10f;                 // 縮放速度
    public float minFOV = 15f;                    // 最小視角
    public float maxFOV = 30f;                    // 最大視角
    public float minOrthoSize = 5f;               // 正交模式最小大小
    public float maxOrthoSize = 50f;              // 正交模式最大大小
    void Start()
    {
        
    }

    
    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel"); // 滑鼠滾輪輸入
        if (scrollInput != 0)
        {
            var lens = virtualCamera.m_Lens;

            if (lens.Orthographic)
            {
                // 正交模式
                lens.OrthographicSize = Mathf.Clamp(
                    lens.OrthographicSize - scrollInput * zoomSpeed,
                    minOrthoSize,
                    maxOrthoSize
                );
            }
            else
            {
                // 透視模式
                lens.FieldOfView = Mathf.Clamp(
                    lens.FieldOfView - scrollInput * zoomSpeed,
                    minFOV,
                    maxFOV
                );
            }

            virtualCamera.m_Lens = lens; // 更新 Cinemachine 鏡頭參數
        }
    }
}
