using System.Collections;
using System.Threading;
using UnityEngine;
using static EnemyBodyPartController;

public class EnemyController : MonoBehaviour, IAttackable
{
	[Header("Default Preferences")]
	public float health;
	public float movingTurnSpeed;
	public float stationaryTurnSpeed;
	public float groundCheckDistance;
	public int scoreForKilling;
	public float damage;

	[Header("Damage Policy")]
	public float fatalDamageMultiplier;
	public float normalDamageMultiplier;
	public float minorDamageMultiplier;

	private GameController __gameController;
	private Animator __animator;

	private float __turnAmount;
	private float __forwardAmount;
	private Vector3 __groundNormal;

	private bool __attacking = false;
	private bool __attacking_sync = false;
	private bool __dead = false;

	public void move(Vector3 amount)
	{
		if (amount.magnitude > 1f)
			amount.Normalize();

		amount = transform.InverseTransformDirection(amount);

		RaycastHit hitInfo;
		Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance);
		__groundNormal = hitInfo.normal;

		amount = Vector3.ProjectOnPlane(amount, __groundNormal);
		__turnAmount = Mathf.Atan2(amount.x, amount.z);
		__forwardAmount = amount.z;

		__applyExtraTurnRotation();

		__animator.SetBool("CatchTarget", false);
		__attacking = false;
	}

	public void OnCollisionEnter(Collision collision)
	{
		switch (collision.gameObject.tag)
		{
			case "Tower":
				Attack(collision.gameObject.GetComponent<WatchTowerController>());
				break;

			case "Player":
				Attack(collision.gameObject.GetComponent<PlayerController>());
				break;
		}
	}

	private IEnumerator __damageLoop_delayedEnter(IAttackable attackable)
	{
		while (__attacking_sync)
			yield return new WaitForSeconds(.1f);

		StartCoroutine(__damageLoop(attackable));
	}

		private IEnumerator __damageLoop(IAttackable attackable)
	{
		__attacking_sync = true;

		while (__attacking)
		{
			attackable.GiveDamage(damage);
			yield return new WaitForSeconds(__animator.GetCurrentAnimatorStateInfo(0).length);
		}

		__attacking_sync = false;
	}

	public void Attack(IAttackable attackable)
	{
		__animator.SetBool("CatchTarget", true);
		__attacking = true;

		if (__attacking_sync)
			StartCoroutine(__damageLoop_delayedEnter(attackable));
		else
			StartCoroutine(__damageLoop(attackable));
	}

	private void Awake()
	{
		__animator = GetComponent<Animator>();
	}

	private void Start()
	{
		__gameController = GameController.getInstance();
	}

	private void __applyExtraTurnRotation()
	{
		float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, __forwardAmount);
		transform.Rotate(0, __turnAmount * turnSpeed * Time.deltaTime, 0);
	}

	public IEnumerator __postDie()
	{
		__animator.SetTrigger("Dead");

		yield return new WaitForSeconds(__animator.GetCurrentAnimatorStateInfo(0).length);
		Destroy(gameObject);
	}

	public void GiveDamage(float damage)
	{
		if (__dead)
			return;

		health -= damage;

		if (health <= 0f)
			Die();
	}

	public void Die()
	{
		__dead = true;
		__gameController.AdjustScore(scoreForKilling);

		StartCoroutine(__postDie());
	}

	public float GetDamageMultiplier(EnemyBodyPartFatalityType bodyPartType)
	{
		switch (bodyPartType)
		{
			case EnemyBodyPartFatalityType.FATAL:
				return fatalDamageMultiplier;

			case EnemyBodyPartFatalityType.NORMAL:
				return normalDamageMultiplier;

			case EnemyBodyPartFatalityType.MINOR:
				return minorDamageMultiplier;
		}

		return 0f;
	}
}
