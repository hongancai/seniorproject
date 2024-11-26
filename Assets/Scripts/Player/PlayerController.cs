using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private InputMaster _inputMaster;
    private Animator animator;

    // 用來記錄最後的移動方向
    private string lastDirection = "L"; // 預設為左邊
    private float normalSpeed = 3.0f; // 正常移動速度
    private float sprintSpeed = 6.0f; // 加速移動速度

    void Start()
    {
        GameDB.playerSpd = normalSpeed;
        _rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        _inputMaster = new InputMaster();
        _inputMaster.Enable();
    }

    void Update()
    {
        Vector3 move = Vector3.zero;
        bool isMoving = false;

        // 讀取控制器的輸入
        Vector2 gamepadInput = _inputMaster.Player.Movement.ReadValue<Vector2>();
        bool isSprinting = Keyboard.current.shiftKey.isPressed; // 檢查 Shift 鍵是否被按下

        if (gamepadInput != Vector2.zero)
        {
            move = new Vector3(gamepadInput.x, 0, gamepadInput.y);
            isMoving = true;

            // 判斷最後方向
            if (gamepadInput.x > 0)
            {
                lastDirection = "R";
            }
            else if (gamepadInput.x < 0)
            {
                lastDirection = "L";
            }
        }

        // 處理鍵盤輸入
        if (Input.GetKey(KeyCode.A))
        {
            move -= new Vector3(1, 0, 0);
            lastDirection = "L";
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += new Vector3(1, 0, 0);
            lastDirection = "R";
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            move += new Vector3(0, 0, 1);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move -= new Vector3(0, 0, 1);
            isMoving = true;
        }

        // 根據移動狀態和加速狀態播放相應的動畫
        if (isMoving)
        {
            // 判斷是走路還是跑步
            if (isSprinting)
            {
                // 按住 Shift 時播放 run 動畫
                if (lastDirection == "R")
                {
                    animator.Play("R_run");
                }
                else if (lastDirection == "L")
                {
                    animator.Play("L_run");
                }
            }
            else
            {
                // 正常移動時播放 walk 動畫
                if (lastDirection == "R")
                {
                    animator.Play("R_walk");
                }
                else if (lastDirection == "L")
                {
                    animator.Play("L_walk");
                }
            }
        }
        else
        {
            _rb.velocity = new Vector3(0, 0, 0);
            // 停止移動時，根據最後的方向播放對應的 idle 動畫
            if (lastDirection == "R")
            {
                animator.Play("R_idle");
            }
            else if (lastDirection == "L")
            {
                animator.Play("L_idle");
            }
        }

        // 設置角色的速度，根據是否加速決定速度
        float speed = isSprinting ? sprintSpeed : normalSpeed;
        _rb.MovePosition(transform.position + move * (speed * Time.deltaTime));
    }
}
