using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemy;

	public IEnumerator spawn(int enemyNumber, Vector3 spawningPosition, float timePeriod)
	{
		GameObject clonedEnemy = null;

		while (enemyNumber > 0)
		{
			clonedEnemy = Instantiate(enemy, spawningPosition, Quaternion.identity);
			clonedEnemy.SetActive(true);

			yield return new WaitForSeconds(timePeriod);
			enemyNumber--;
		}
	}
}
