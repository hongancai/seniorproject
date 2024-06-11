using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody _rb;

    private InputMaster _inputMaster;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _inputMaster = new InputMaster();
        _inputMaster.Enable();
    }

    void Update()
    {
        Vector2 vector2 = _inputMaster.Player.Movement.ReadValue<Vector2>();
        Debug.Log(vector2);
        Vector3 move = Vector3.zero;

        // 檢查按鍵輸入並設置移動向量
        if (Input.GetKey(KeyCode.A))
        {
            move -= new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            move += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            move -= new Vector3(0, 0, 1);
        }

        // 設置角色的速度
        _rb.MovePosition(transform.position + move * moveSpeed * Time.deltaTime);
    }
}