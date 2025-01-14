using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private InputMaster _inputMaster;
    private Animator animator;
    

    private string lastDirection = "L"; // 預設為左邊
    private float normalSpeed = 3.0f;   // 正常移動速度
    private float sprintSpeed = 4.0f; // 加速移動速度

    public AudioClip playerwaik;
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
        Movement();
    }

    private void Movement()
    {
        Vector3 move = Vector3.zero;
        bool isMoving = false;
        
        // 控制器輸入
        Vector2 gamepadInput = _inputMaster.Player.Movement.ReadValue<Vector2>();
        bool isSprinting = _inputMaster.Player.Sprint.IsPressed()|| Input.GetKey(KeyCode.LeftShift);; // 使用控制器 RB 鍵檢查

        if (gamepadInput != Vector2.zero)
        {
            move = new Vector3(gamepadInput.x, 0, gamepadInput.y);
            isMoving = true;
            UpdateDirection(gamepadInput.x);
        }

        // 鍵盤輸入
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
        {
            move += Vector3.left;
            UpdateDirection(-1);
            isMoving = true;
            //GameDB.Audio.PlaySfx(playerwaik);
        }
        if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
        {
            move += Vector3.right;
            UpdateDirection(1);
            isMoving = true;
            //GameDB.Audio.PlaySfx(playerwaik);
        }
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
        {
            move += Vector3.forward;
            isMoving = true;
            //GameDB.Audio.PlaySfx(playerwaik);
        }
        if (Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))
        {
            move += Vector3.back;
            isMoving = true;
            //GameDB.Audio.PlaySfx(playerwaik);
        }

        // 移動向量規範化
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        // 更新動畫
        UpdateAnimation(isMoving, isSprinting);

        // 設置移動速度並移動角色
        float speed = isSprinting ? sprintSpeed : normalSpeed;
        _rb.MovePosition(transform.position + move * (speed * Time.deltaTime));
    }

    private void UpdateDirection(float xInput)
    {
        if (xInput > 0)
        {
            lastDirection = "R";
        }
        else if (xInput < 0)
        {
            lastDirection = "L";
        }
    }

    private void UpdateAnimation(bool isMoving, bool isSprinting)
    {
        if (isMoving)
        {
            // 移動時，根據方向和是否加速選擇動畫
            string animationToPlay = isSprinting
                ? (lastDirection == "R" ? "R_run" : "L_run")
                : (lastDirection == "R" ? "R_walk" : "L_walk");

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationToPlay))
            {
                animator.Play(animationToPlay);
            }
        }
        else
        {
            // 停止移動時播放 Idle 動畫
            _rb.velocity = new Vector3(0,0,0);
            string idleAnimation = lastDirection == "R" ? "R_idle" : "L_idle";
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(idleAnimation))
            {
                animator.Play(idleAnimation);
            }
        }
    }
}
