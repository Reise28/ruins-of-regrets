using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    // Fields ------------------------------------------------------------------

    public static Player Instance { get; set; }
    bool isGrounded;

    // Unity API ---------------------------------------------------------------

    public void Awake() {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.animator.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        this.stat.setStat(3f, 0.2f, 3);

        Player.Instance = this;
    }

    public void FixedUpdate() {
        Vector3 ground_pos = new Vector3(
            transform.position.x,
            transform.position.y - 0.6f,
            transform.position.z);

        isGrounded = Physics2D.OverlapCircleAll(ground_pos, 0.1f).Length > 1;
    }

    public void Update() {
        if (isGrounded) {
            if (Input.GetButton("Horizontal"))
                Walk(transform.right * Input.GetAxis("Horizontal"));
            if (Input.GetButton("Jump"))
                Jump();
            if (!Input.GetButton("Horizontal") && !Input.GetButton("Jump") && !isAttacking)
                Idle();
        } else {
            Fall();
        }

        if (Input.GetButton("Fire1"))
            Attack();
    }
}
