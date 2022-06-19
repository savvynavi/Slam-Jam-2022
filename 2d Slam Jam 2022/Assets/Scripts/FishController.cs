using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class FishController : MonoBehaviour
{
    [SerializeField] protected float swimSpeed;

    private BoxCollider2D fishCollider => this.GetComponent<BoxCollider2D>();
    protected Rigidbody2D rb => this.GetComponent<Rigidbody2D>();
    protected Vector2 input => new Vector2(Input.GetAxis("Horizontal") * swimSpeed, Input.GetAxis("Vertical") * swimSpeed);

    private bool facingLeft = true;
    

    private void FixedUpdate()
    {
        Move();
        FlipSprite(rb.velocity.x);
    }

    protected virtual void Move()
    {
        rb.velocity = input;
    }

    // flips sprite to face move dir
    private void FlipSprite(float horizontal)
    {
        if ((horizontal > 0 && facingLeft) || (horizontal < 0 && !facingLeft))
        {
            facingLeft = !facingLeft;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }
}
