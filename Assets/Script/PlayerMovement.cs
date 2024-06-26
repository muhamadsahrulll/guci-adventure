using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;
    private Vector2 axisMovement;
    public float speed = 5;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        moveLeft = false;
        moveRight = false;
        moveUp = false;
        moveDown = false;
    }

    // Fungsi untuk mengatur status tombol arah
    public void PointerDownLeft()
    {
        moveLeft = true;
    }

    public void PointerUpLeft()
    {
        moveLeft = false;
    }

    public void PointerDownRight()
    {
        moveRight = true;
    }

    public void PointerUpRight()
    {
        moveRight = false;
    }

    public void PointerDownUp()
    {
        moveUp = true;
    }

    public void PointerUpUp()
    {
        moveUp = false;
    }

    public void PointerDownDown()
    {
        moveDown = true;
    }

    public void PointerUpDown()
    {
        moveDown = false;
    }

    void Update()
    {
        

        axisMovement.x = (moveLeft ? -1 : 0) + (moveRight ? 1 : 0);
        axisMovement.y = (moveDown ? -1 : 0) + (moveUp ? 1 : 0);

        SetAnimationState();
        Move();
    }

    private void Move()
    {
        Vector2 velocity = axisMovement.normalized * speed;
        rb.velocity = velocity;

        // Flipping sprite
        if (axisMovement.x < 0)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, 1f);
        }
        else if (axisMovement.x > 0)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, 1f);
        }
    }

    private void SetAnimationState()
    {
        if (axisMovement != Vector2.zero)
        {
            anim.SetInteger("state", 1); // Sesuaikan dengan state animasi yang Anda miliki
        }
        else
        {
            anim.SetInteger("state", 0); // Sesuaikan dengan state animasi yang Anda miliki
        }
    }

    
}
