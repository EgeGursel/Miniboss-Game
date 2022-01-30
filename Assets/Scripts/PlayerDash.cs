using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    PlayerPickUp playerPickUp;
    private Rigidbody2D rb;
    private KeyCode lastKeyCode;
    private bool dashCD = true;
    private float InitialTouch;
    private float touchDelay = 0.3f;
    public float dashDistance = 15f;
    private float dashCooldown = 0.5f;

    void Start()
    {
        playerPickUp = GetComponent<PlayerPickUp>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (playerPickUp.dashActive)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (Time.time < InitialTouch + touchDelay && lastKeyCode == KeyCode.A)
                {
                    StartDash(-1);
                }
                lastKeyCode = KeyCode.A;
                InitialTouch = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (Time.time < InitialTouch + touchDelay && lastKeyCode == KeyCode.D)
                {
                    StartDash(1);
                }
                lastKeyCode = KeyCode.D;
                InitialTouch = Time.time;
            }
        }
    }
    IEnumerator DashCooldown()
    {
        dashCD = false;
        yield return new WaitForSeconds(dashCooldown);
        dashCD = true;
    }
    void StartDash(int direction)
    {
        if (dashCD)
        {
            StartCoroutine(Dash(direction));
            StartCoroutine(DashCooldown());
        }
    }
    IEnumerator Dash(float direction)
    {
        float gravity = rb.gravityScale;
        rb.gravityScale = 0.5f;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = gravity;
        yield return new WaitForSeconds(10f);
    }
}
