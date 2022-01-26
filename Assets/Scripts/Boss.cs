using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	Enemy enemy;
	/* HEALTH
	public int bossMaxHealth = 100;
	public int bossCurrentHealth;
	public HealthBar bossHealthBar;
	*/

	// ANIMATIONS
	public Transform player;
	public Animator bossAnimator;
    public bool isFlipped = true;
    private void Start()
    {
		enemy = GetComponent<Enemy>();
		bossAnimator.SetTrigger("Moving");
		// bossCurrentHealth = bossMaxHealth;
		// bossHealthBar.SetMaxHealth(bossCurrentHealth);
	}
    public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}
}