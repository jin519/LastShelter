using UnityEngine;
using UnityEngine.EventSystems;
using HTC.UnityPlugin.Vive;

public class EnemyBodyPartController : MonoBehaviour, IAttackable
{
	public enum EnemyBodyPartFatalityType
	{
		FATAL,
		NORMAL,
		MINOR
	}

	public EnemyBodyPartFatalityType bodyPartType;

	private CapsuleCollider __collider;
	private EnemyController __enemyController;
	private GameController __gameController;

	private void Awake()
	{
		__collider = GetComponent<CapsuleCollider>();
		__enemyController = __collider.transform.root.GetComponent<EnemyController>();
		__gameController = GameController.getInstance();
	}

	public void GiveDamage(float damage)
	{
		__enemyController.GiveDamage(damage * __enemyController.GetDamageMultiplier(bodyPartType));
	}
}
