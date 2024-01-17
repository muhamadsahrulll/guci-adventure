using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseMovement : MonoBehaviour
{
    private CameraFollow cameraFollowScript;
    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;
    private Vector2 axisMovement;
    public float speed = 5;
    private Animator anim;

    // Tambahkan dua parameter animasi baru
    private bool isRide;
    private bool isRiding;

    void Start()
    {
        
        cameraFollowScript = Camera.main.GetComponent<CameraFollow>();
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        moveLeft = false;
        moveRight = false;
        moveUp = false;
        moveDown = false;

        // Inisialisasi parameter animasi
        isRide = false;
        isRiding = false;
        //ToggleCameraFollow();
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

    void Update()
    {
        
        axisMovement.x = (moveLeft ? -1 : 0) + (moveRight ? 1 : 0);
        axisMovement.y = (moveDown ? -1 : 0) + (moveUp ? 1 : 0);

        //SetAnimationState();
        Move();
        
    }

    private void Move()
    {

        Vector2 velocity = axisMovement.normalized * speed;
        rb.velocity = velocity;
        anim.SetBool("isRide", true);
        // Tambahkan logika isRiding berdasarkan pergerakan
        isRiding = velocity.magnitude > 0;
        anim.SetBool("isRiding", isRiding);

        Flipping();
    }

    

    private void Flipping()
    {
        bool movingLeft = axisMovement.x > 0;
        bool movingRight = axisMovement.x < 0;

        if (movingLeft)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, 1f);
        }
        else if (movingRight)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, 1f);
        }
    }

    // Getter untuk isRide
    public bool IsRide()
    {
        return isRide;
    }

    // Setter untuk isRide
    public void SetIsRide(bool value)
    {
        isRide = true;
        anim.SetBool("isRide", isRide);
    }

    void ToggleCameraFollow()
    {
        if (cameraFollowScript != null)
        {
            // Ubah status mengikuti atau tidak mengikuti target pada kamera
            cameraFollowScript.SetFollowTarget(!cameraFollowScript.GetFollowTarget());
        }
    }
}
