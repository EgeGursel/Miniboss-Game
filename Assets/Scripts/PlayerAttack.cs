using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // ATTACK
    public LayerMask enemyLayer;
    public float attackCooldown = 0.4f;
    private bool attackCD = true;
    private GameObject dBox;

    // MELEE
    public Transform attackArea;
    public float attackRange;
    public int attackDamage = 30;
    public Animator meleeAnimator;

    // RANGED
    public Transform bow;
    public Transform firePoint;
    public Animator rangedAnimator;
    public GameObject arrowPrefab;

    private void Start()
    {
        dBox = GameObject.FindWithTag("DBox");
    }

    void Update()
    {
        if (Time.timeScale == 0 || dBox.transform.localPosition.y > -130)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1") && PlayerPrefs.GetInt("Katana") == 1)
        {
            Attack();
        }
        else if (Input.GetButtonDown("Fire1") && PlayerPrefs.GetInt("Bow") == 1)
        {
            Shoot();
        }
    }
    public void CheckWeapons()
    {
        if (PlayerPrefs.GetInt("Katana") == 1)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Bow") == 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    IEnumerator AttackCooldown()
    {
        attackCD = false;
        yield return new WaitForSeconds(attackCooldown);
        attackCD = true;
    }
    private void Attack()
    {
        if (attackCD)
        {
            AudioManager.instance.Play("meleeattack");
            // DETECT ENEMIES IN RANGE OF ATTACK
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackArea.position, attackRange, enemyLayer);

            // DAMAGE ENEMIES
            foreach (Collider2D enemy in hitEnemies)
            {
                try
                {
                    enemy.GetComponent<Enemy>().Damage(Mathf.RoundToInt(attackDamage * PlayerPrefs.GetFloat("AttackDamage")));
                }
                catch
                {
                    enemy.GetComponent<Boss>().Damage(Mathf.RoundToInt(attackDamage * PlayerPrefs.GetFloat("AttackDamage")));
                }
            }
            meleeAnimator.SetTrigger("Attack");
            CameraShake.Instance.Shake(2f, .16f);
            StartCoroutine(AttackCooldown());
        }
    }
    private void Shoot()
    {
        if (attackCD)
        {
            AudioManager.instance.Play("rangedattack");
            Instantiate(arrowPrefab, firePoint.position, bow.rotation);
            rangedAnimator.SetTrigger("Attack");
            StartCoroutine(AttackCooldown());
        }
    }
}
