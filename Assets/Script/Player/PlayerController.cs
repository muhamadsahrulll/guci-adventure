using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } }

    [SerializeField] private float moveSpeed = 6f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private PlayerControl playerControl;

    private bool facingLeft = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerControl = new PlayerControl();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControl.Movement.Move.ReadValue<Vector2>();

        // Set animasi state sesuai dengan vector gerakan
        SetAnimationState();
    }

    private void Move()
    {
        // Gerakkan karakter berdasarkan input
        rb.velocity = movement * moveSpeed;

        // Flipping sprite sesuai arah gerakan
        if (movement.x != 0)
        {
            sprite.flipX = movement.x < 0;
            facingLeft = movement.x < 0;
        }
    }

    private void AdjustPlayerFacingDirection()
    {
        // Sudah diimplementasikan dalam fungsi Move
    }

    private void SetAnimationState()
    {
        // Implementasi animasi state dari script A
        if (movement != Vector2.zero)
        {
            anim.SetInteger("state", 1); // Sesuaikan dengan state animasi yang Anda miliki
        }
        else
        {
            anim.SetInteger("state", 0); // Sesuaikan dengan state animasi yang Anda miliki
        }
    }
}
