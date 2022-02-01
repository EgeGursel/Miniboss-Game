using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rb;
    private KeyCode lastKeyCode;
    private bool dashCD = true;
    private float InitialTouch;
    private float touchDelay = 0.2f;
    public float dashDistance = 15f;
    private float dashCooldown = 0.5f;
    public bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("DashActive") > 0)
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
        isDashing = true;
        float gravity = rb.gravityScale;
        rb.gravityScale = 0.4f;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.15f);
        rb.gravityScale = gravity;
        isDashing = false;
    }
}
