using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTowerController : MonoBehaviour, IAttackable
{
	public float health;
	public ParticleSystem towerExplosionEffect;

	private GameController __gameController;

	void Start()
    {
		__gameController = GameController.getInstance();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void GiveDamage(float damage)
	{
		health -= damage;
		__gameController.UpdateTowerHealthBar(health);

		if (health <= 0)
			__gameController.GameOver();
	}

	public void ShowDestroyEffect()
	{
		Instantiate(towerExplosionEffect, transform.position, transform.rotation);
		gameObject.SetActive(false);
	}
}
