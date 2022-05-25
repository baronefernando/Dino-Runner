using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public static Rigidbody2D rb;
    public Animator animator;
    public static byte jumpForce;
    bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpForce = 15;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    void Jump()
    {
        if (/*Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended*/ Input.GetMouseButton(0) && rb.velocity.y == 0) //Player is on the ground
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); //Jump
        }
        if(rb.velocity.y <= 11)
        {
            animator.SetTrigger("run");
        }
    }
}
