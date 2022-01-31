using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public ParticleSystem impactPS;
    public LayerMask enemyLayer;
    public int attackDamage = 25;
    public float speed = 20f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        else if (collision.gameObject.layer == enemyLayer)
        {
            try
            {
                collision.GetComponent<Enemy>().Damage(Mathf.RoundToInt(attackDamage * PlayerPrefs.GetFloat("AttackDamage")));
            }
            catch
            {
                collision.GetComponent<Boss>().Damage(Mathf.RoundToInt(attackDamage * PlayerPrefs.GetFloat("AttackDamage")));
            }
        }
        Instantiate(impactPS, transform.position, transform.rotation);
        CameraShake.Instance.Shake(2.5f, .16f);
        Destroy(gameObject);
    }
}
