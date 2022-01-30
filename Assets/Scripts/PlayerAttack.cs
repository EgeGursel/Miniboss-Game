using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackArea;
    public float attackRange = 0.65f;
    public LayerMask enemyLayer;
    public int attackDamage = 15;
    public float attackCooldown = 0.4f;
    private bool attackCD = true;
    public Animator weaponAnimator;
    PlayerPickUp playerPickUp;

    void Start()
    {
        playerPickUp = GetComponent<PlayerPickUp>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerPickUp.weaponActive)
        {
            Attack();
        }
    }
    IEnumerator AttackCooldown()
    {
        attackCD = false;
        yield return new WaitForSeconds(attackCooldown);
        attackCD = true;
    }
    void Attack()
    {
        if (attackCD)
        {
            if (weaponAnimator.gameObject.name == "Katana")
            {
                // DETECT ENEMIES IN RANGE OF ATTACK
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackArea.position, attackRange, enemyLayer);

                // DAMAGE ENEMIES
                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Enemy>().Damage(Mathf.RoundToInt(attackDamage * PlayerPrefs.GetFloat("AttackDamage")));
                }
            }
            else if (weaponAnimator.gameObject.name == "Bow")
            {
                return;
            }
            weaponAnimator.SetTrigger("Attack");
            CameraShake.Instance.Shake(2f, .16f);
            StartCoroutine(AttackCooldown());
        }
    }
}
