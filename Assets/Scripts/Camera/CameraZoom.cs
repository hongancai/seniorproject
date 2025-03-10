using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // 連結到你的 Cinemachine Virtual Camera
    public float zoomSpeed = 20f;                 // 縮放速度
    public float minFOV = 18f;                    // 最小視角
    public float maxFOV = 30f;                    // 最大視角
    public float minOrthoSize = 5f;               // 正交模式最小大小
    public float maxOrthoSize = 50f;              // 正交模式最大大小
    
    private InputMaster _inputMaster;
    private void Awake()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Enable();
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }

    private void OnDisable()
    {
        _inputMaster.Disable();
    }

    private void Update()
    {
        // 滑鼠滾輪輸入
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        AdjustZoom(-scrollInput * zoomSpeed);

        // 手把按鍵輸入 - 檢查是否持續按住而不只是按下的瞬間
        if (_inputMaster.Zoom.ZoomIn.IsPressed())  // 使用IsPressed()代替WasPressedThisFrame()
        {
            AdjustZoom(-zoomSpeed * Time.deltaTime);
        }
        else if (_inputMaster.Zoom.ZoomOut.IsPressed())  // 使用IsPressed()代替WasPressedThisFrame()
        {
            AdjustZoom(zoomSpeed * Time.deltaTime);
        }
        
    }

    private void AdjustZoom(float zoomAmount)
    {
        var lens = virtualCamera.m_Lens;

        if (lens.Orthographic)
        {
            // 正交模式
            lens.OrthographicSize = Mathf.Clamp(
                lens.OrthographicSize + zoomAmount,
                minOrthoSize,
                maxOrthoSize
            );
        }
        else
        {
            // 透視模式
            lens.FieldOfView = Mathf.Clamp(
                lens.FieldOfView + zoomAmount,
                minFOV,
                maxFOV
            );
        }

        virtualCamera.m_Lens = lens; // 更新 Cinemachine 鏡頭參數
    }
}
