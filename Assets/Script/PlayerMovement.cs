using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;
    private Vector2 axisMovement;
    public float speed = 5;
    private Animator anim;
    private BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

        moveLeft = false;
        moveRight = false;
        moveUp = false;
        moveDown = false;
    }

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

    // Update is called once per frame
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
        Flipping();
    }

    private void SetAnimationState()
    {
        if (axisMovement != Vector2.zero)
        {
            anim.SetInteger("state", 1);
        }
        else
        {
            anim.SetInteger("state", 0);
        }
    }

    private void Flipping()
    {
        bool movingLeft = axisMovement.x < 0;
        bool movingRight = axisMovement.x > 0;

        if (movingLeft)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, 1f);
        }
        else if (movingRight)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, 1f);
        }
    }
}
