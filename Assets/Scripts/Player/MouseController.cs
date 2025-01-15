using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    public float sensitivity = 1.0f;
    public float smooth = 0.5f;

    private Vector2 currentMousePos;
    private Vector2 targerMousePos;
    
    private InputMaster _inputMaster; 
    void Start()
    {
        _inputMaster = new InputMaster();
    }

    
    void Update()
    {
        
    }
}
