using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -----------------------------------------------------------------------------
// Unit Animator State
// -----------------------------------------------------------------------------

public enum UnitAnimatorState
{
    Idle,
    Run,
    Jump,
    Fall,
    Attack,
    Die
}

public struct UnitAnimator
{
    public Animator animator { get; set; }

    public void setAnimation(UnitAnimatorState state)
    {
        animator.SetInteger("state", (int) state);
    }

    public UnitAnimatorState getAnimation()
    {
        return (UnitAnimatorState) animator.GetInteger("state");
    }
}

// -----------------------------------------------------------------------------
// Unit Stats
// -----------------------------------------------------------------------------

public struct UnitStat
{
    [SerializeField] public float walkSpeed { get; set; }
    [SerializeField] public float jumpForce { get; set; }
    [SerializeField] public uint lifeNum { get; set; }

    public void setStat(float walkSpeed, float jumpForce, uint lifeNum)
    {
        this.walkSpeed = walkSpeed;
        this.jumpForce = jumpForce;
        this.lifeNum = lifeNum;
    }
}

// -----------------------------------------------------------------------------
// Unit Base Class
// -----------------------------------------------------------------------------

public class Unit : MonoBehaviour
{
    // Components --------------------------------------------------------------

    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;

    public UnitAnimator animator;
    public UnitStat stat;

    // Flags -------------------------------------------------------------------

    public bool isAttacking;
    Vector3 lastDir;

    // Private Helpers ---------------------------------------------------------

    private IEnumerator AttackWaitForStop() {
        yield return new WaitForSeconds(1.15f);
        isAttacking = false;
    }

    // Public API --------------------------------------------------------------

    private void Move(Vector3 dir) {
        transform.position = Vector3.MoveTowards(
            transform.position,
            transform.position + dir,
            stat.walkSpeed * Time.deltaTime);
    }

    public void Walk(Vector3 dir) {
        spriteRenderer.flipX = dir.x < 0f;
        lastDir = dir;

        animator.setAnimation(UnitAnimatorState.Run);
        Move(dir);
    }

    public void Fall() {
        // animator.setAnimation(UnitAnimatorState.Fall);
        Move(lastDir);
    }

    public void Jump() {
        animator.setAnimation(UnitAnimatorState.Jump);
        rigidBody.AddForce(transform.up * stat.jumpForce, ForceMode2D.Impulse);
    }

    public void Idle() {
        lastDir = new Vector3(0f, 0f, 0f);
        animator.setAnimation(UnitAnimatorState.Idle);
    }

    public void Attack() {
        animator.setAnimation(UnitAnimatorState.Attack);
        isAttacking = true;

        StartCoroutine(AttackWaitForStop());
    }

    public void Suffer() {
        if (--stat.lifeNum < 1) {
            this.enabled = false;
            Destroy(this.gameObject);
        }
    }
}
