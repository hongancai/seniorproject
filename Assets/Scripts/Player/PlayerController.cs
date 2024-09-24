using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody _rb;
   
    private InputMaster _inputMaster;
    private Animator animator;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        _inputMaster = new InputMaster();
        _inputMaster.Enable();
    }

    void Update()
    {
        Vector3 move = Vector3.zero;
        
        Vector2 gamepadInput = _inputMaster.Player.Movement.ReadValue<Vector2>();
        if (gamepadInput != Vector2.zero)
        {
            move = new Vector3(gamepadInput.x, 0, gamepadInput.y);
        }
        
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

        if (move==Vector3.zero)
        {
            animator.Play("idle");
        }
        else
        {
            animator.Play("walk");
        }

        // 設置角色的速度
        _rb.MovePosition(transform.position + move * (moveSpeed * Time.deltaTime));
      
    }
}