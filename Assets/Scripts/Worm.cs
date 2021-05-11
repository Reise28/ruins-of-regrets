using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    // TODO: Inherit from Unit and remove it
    UnitStat stat;
    Vector3 dir;

    // TODO: Inherit from Unit and remove it
    public void Walk(Vector3 dir) {
        transform.position = Vector3.MoveTowards(
            transform.position,
            transform.position + dir,
            stat.walkSpeed * Time.deltaTime);
    }

    // Helpers -----------------------------------------------------------------

    Vector3 GetDirection() {
        Vector3 point = transform.position
            + transform.up * 0.1f
            + transform.right * dir.x * 0.7f;

        if (Physics2D.OverlapCircleAll(point, 0.1f).Length > 0)
            dir *= -1f;

        return dir;
    }

    // Unity Events ------------------------------------------------------------

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
            Player.Instance.Suffer();
    }

    // Unity API ---------------------------------------------------------------

    public void Awake() {
        // TODO: Inherit from Unit and uncomment
        // this.rigidBody = GetComponent<Rigidbody2D>();
        // this.animator.animator = GetComponent<Animator>();
        // this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        this.stat.setStat(3f, 0.2f, 3);
        this.dir = transform.right;
    }

    void Update() {
        Walk(GetDirection());
    }
}
