using UnityEngine;
using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour, IAttackable
{
	[Header("Default Preference")]
	public float health = 100f;
	public float moveSpeed = 6f;
	public float jumpPower = 300f;
	public float damage = 20f;

	private int __leftRemainingBullets;
	private int __rightRemainingBullets;

	[Header("Key Mapping")]
	public HandRole jumpHandRole;
	public ControllerButton jumpControllerButton;

	private Animator __animator;
	private GameController __gameController;
	private Transform __cameraTransform;

	private GameObject __currCastedObj = null;
	private bool __dead = false;

	private ControllerManagerSample __controllerManager;
	private Rigidbody __rigidbody;

	private AudioSource __gunshotAudioSource;
	private AudioSource __jumpAudioSource;
	private List<AudioSource> __damagedAudioSources;

	private void Awake()
	{
		__animator = GetComponent<Animator>();
		__rigidbody = GetComponent<Rigidbody>();
		__cameraTransform = Camera.main.transform;

		AudioSource[] audioSources = GetComponents<AudioSource>();

		__gunshotAudioSource = audioSources[0];
		__jumpAudioSource = audioSources[1];

		__damagedAudioSources = new List<AudioSource> { audioSources[2], audioSources[3] };
	}

	private void Start()
	{
		__gameController = GameController.getInstance();
		__controllerManager = GetComponentInChildren<ControllerManagerSample>();

		ViveInput.AddListenerEx(
				HandRole.LeftHand, ControllerButton.FullTrigger, ButtonEventType.Down, __onLeftTriggerDown);

		ViveInput.AddListenerEx(
				HandRole.RightHand, ControllerButton.FullTrigger, ButtonEventType.Down, __onRightTriggerDown);

		ViveInput.AddListenerEx(
				jumpHandRole, jumpControllerButton, ButtonEventType.Down, __onJumpKeyDown);
	}

	private void FixedUpdate()
	{
		float hMovement = ViveInput.GetAxisEx(HandRole.RightHand, ControllerAxis.PadX);
		__onPadXTouched(Mathf.Clamp(hMovement, -1.0f, 1.0f));

		float vMovement = ViveInput.GetAxisEx(HandRole.RightHand, ControllerAxis.PadY);
		__onPadYTouched(Mathf.Clamp(vMovement, -1.0f, 1.0f));
	}

	private void __onPadXTouched(float amount)
	{
		Vector3 moveDir = (__cameraTransform.right * amount);
		transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);
	}

	private void __onPadYTouched(float amount)
	{
		Vector3 moveDir = (__cameraTransform.forward * amount);
		transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);
	}

	private void __onLeftTriggerDown()
	{
		if (__controllerManager.leftLaserPointerActive && ShotLeftBullet())
		{
			__gunshotAudioSource.Play();
			__checkAndGiveDamage();
		}
	}

	private void __onRightTriggerDown()
	{
		if (__controllerManager.rightLaserPointerActive && ShotRightBullet())
		{
			__gunshotAudioSource.Play();
			__checkAndGiveDamage();
		}
	}

	private void __onJumpKeyDown()
	{
		__jumpAudioSource.Play();
		__rigidbody.AddForce(transform.up * jumpPower);
	}

	public void SupplyLeftBullets(int numBullets)
	{
		__leftRemainingBullets += numBullets;
		__gameController.UpdateLeftBulletBoard(__leftRemainingBullets);
	}

	public void SupplyRightBullets(int numBullets)
	{
		__rightRemainingBullets += numBullets;
		__gameController.UpdateRightBulletBoard(__rightRemainingBullets);
	}

	public bool ShotLeftBullet()
	{
		if (__leftRemainingBullets <= 0)
			return false;

		__leftRemainingBullets--;
		__gameController.UpdateLeftBulletBoard(__leftRemainingBullets);

		return true;
	}

	public bool ShotRightBullet()
	{
		if (__rightRemainingBullets <= 0)
			return false;

		__rightRemainingBullets--;
		__gameController.UpdateRightBulletBoard(__rightRemainingBullets);

		return true;
	}

	private void __checkAndGiveDamage()
	{
		if ((__currCastedObj != null) && __currCastedObj.tag == "EnemyBodyPart")
		{
			EnemyBodyPartController controller = __currCastedObj.GetComponent<EnemyBodyPartController>();
			controller.GiveDamage(damage);
		}
	}

	public void NotifyCurrentCastedObj(GameObject obj)
	{
		__currCastedObj = obj;
	}

	public void GiveDamage(float damage)
	{
		if (__dead)
			return;

		health -= damage;
		__gameController.UpdatePlayerHealthBar(health);

		if (health <= 0)
		{
			Die();
			__gameController.GameOver();
		}
		else
			__damagedAudioSources[Random.Range(0, 2)].Play();
	}

	public bool IsAlive()
	{
		return !__dead;
	}

	public void Die()
	{
		__dead = true;
		__animator.SetTrigger("Dead");
	}
}
