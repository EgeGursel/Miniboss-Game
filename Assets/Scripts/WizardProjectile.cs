using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardProjectile : MonoBehaviour
{
    public ParticleSystem impactPS;
    int attackDamage = 25;
    public float speed = 7.4f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Collectable"))
        {
            return;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            CameraShake.Instance.Shake(2.5f, .16f);
            collision.GetComponent<Player>().TakeDamage(attackDamage);
        }
        Instantiate(impactPS, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
