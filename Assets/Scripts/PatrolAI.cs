using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    public float jumpForce;
    public LayerMask collidableLayer;
    public BoxCollider2D bodyCollider;
    public Transform groundCheck;
    public float walkSpeed;
    Rigidbody2D rb;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustFlip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
    }
    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, collidableLayer);
        }
    }
    void Patrol()
    {
        if (mustFlip || bodyCollider.IsTouchingLayers(collidableLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;  
    }

    IEnumerator Hop()
    {
        while (mustPatrol)
        {
            rb.AddForce(transform.up * jumpForce);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
