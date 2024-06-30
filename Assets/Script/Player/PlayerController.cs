using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviour, IPunObservable
{
    public bool FacingLeft { get { return facingLeft; } }

    [SerializeField] private float moveSpeed = 6f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private PlayerControl playerControl;

    private bool facingLeft = false;
    private PhotonView photonView;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerControl = new PlayerControl();
        photonView = GetComponent<PhotonView>();
        rb.gravityScale = 0; // Disable gravity for 2D top-down
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
        if (photonView.IsMine)
        {
            PlayerInput();
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Move();
        }
    }

    private void PlayerInput()
    {
        movement = playerControl.Movement.Move.ReadValue<Vector2>();
        SetAnimationState();
    }

    private void Move()
    {
        rb.velocity = movement * moveSpeed;

        if (movement.x != 0)
        {
            sprite.flipX = movement.x < 0;
            facingLeft = movement.x < 0;
        }
    }

    private void SetAnimationState()
    {
        if (movement != Vector2.zero)
        {
            anim.SetInteger("state", 1);
        }
        else
        {
            anim.SetInteger("state", 0);
        }
    }

    // Sinkronisasi animasi dan arah pemain
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(anim.GetInteger("state"));
            stream.SendNext(facingLeft);
        }
        else
        {
            int state = (int)stream.ReceiveNext();
            anim.SetInteger("state", state);
            facingLeft = (bool)stream.ReceiveNext();
            sprite.flipX = facingLeft;
        }
    }
}
