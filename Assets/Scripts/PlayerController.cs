using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 水平和垂直移動
        float moveInputHorizontal = Input.GetAxis("Horizontal"); // 左右移動
        float moveInputVertical = Input.GetAxis("Vertical"); // 上下移動

        Vector3 move = new Vector3(moveInputHorizontal, 0, moveInputVertical) * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + move);
    }
}
