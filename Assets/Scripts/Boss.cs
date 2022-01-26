using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	// HEALTH
	public int maxHealth = 500;
	public int currentHealth;
	public BossHealthBar bossHealthbar;

	// ANIMATIONS & SCENE MANAGEMENT
	public Transform player;
	public Animator bossAnimator;
    public bool isFlipped = true;
	public GameObject sceneLoader;

	private void Start()
    {
		bossAnimator.SetTrigger("Moving");
		currentHealth = maxHealth;
		bossHealthbar.SetMaxHealth(currentHealth);
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
	public void Damage(int damage)
	{
		currentHealth -= damage;
		bossHealthbar.SetHealth(currentHealth);

		if (currentHealth <= 0)
		{
			Die();
		}
	}
	public void Die()
	{
		gameObject.SetActive(false);
		sceneLoader.GetComponent<SceneLoader>().Load("DeathScene 1");
	}

	public void AttackShakeCamera()
    {
		CameraShake.Instance.Shake(3f, .4f);
	}
}