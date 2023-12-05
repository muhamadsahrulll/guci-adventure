using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D body;
    private Vector2 axisMovement;
    private Animator anim;
    private BoxCollider2D coll;

    private enum MovementState { idle, walk}

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        axisMovement.x = Input.GetAxisRaw("Horizontal");
        axisMovement.y = Input.GetAxisRaw("Vertical");

        SetAnimationState();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        body.velocity = axisMovement.normalized * speed;
        flipping();
    }

    private void SetAnimationState()
    {
        if (axisMovement != Vector2.zero)
        {
            // Set state to "walk" if there is movement
            anim.SetInteger("state", 1);
        }
        else
        {
            // Set state to "idle" if there is no movement
            anim.SetInteger("state", 0);
        }
    }

    private void flipping()
    {
        bool movingLeft = axisMovement.x < 0;
        bool movingRight = axisMovement.x > 0;

        if (movingLeft)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y);
        }

        else if (movingRight)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y);
        }
    }
}
