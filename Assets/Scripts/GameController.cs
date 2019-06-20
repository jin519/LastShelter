using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	[Header("Common")]
	public PlayerController player;
	public WatchTowerController tower;
	public int score;

	[Header("Player")]
	public int bulletSupplyAmount = 30;

	[Header("Enemy")]
	public Transform spawningPosition1;
	public Transform spawningPosition2;
	public float spawningTimePeriod;

	[Header("UI")]
	public ProgressBar towerProgressBar;
	public ProgressBar playerProgressBar;
	public Text roundInfo;
	public Text scoreBoard;
	public Text leftBulletsInfo;
	public Text rightBulletsInfo;
	public GameObject gameOverScreen;

	[Header("Key Mapping")]
	public HandRole minimapActivatorHandRole;
	public ControllerButton minimapActivatorControllerButton;

	private int __round = 0;

	private static GameController __instance;
	private GameObject __minimapObj;
	private EnemySpawner __enemySpawner;
	private AudioSource __bgmAudioSource;
	private AudioSource __explosionAudioSource;

	private bool __minimapActivation = false;
	private float __playerFullHealth;
	private float __towerFullHealth;

	private int __numEnemySpawning = 3;

	public static GameController getInstance()
	{
		return __instance;
	}

	void Awake()
	{
		if (__instance)
		{
			DestroyImmediate(gameObject);
			return;
		}

		__instance = this;

		__minimapObj = GameObject.Find("Player/Camera/Minimap");
		__enemySpawner = GetComponent<EnemySpawner>();


		AudioSource[] audioSources = GetComponents<AudioSource>();
		__bgmAudioSource = audioSources[0];
		__explosionAudioSource = audioSources[1];

		__playerFullHealth = player.health;
		__towerFullHealth = tower.health;
		playerProgressBar.BarValue = 100;
		towerProgressBar.BarValue = 100;

		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		ViveInput.AddListenerEx(
			minimapActivatorHandRole, minimapActivatorControllerButton, ButtonEventType.Click, __onMinimapActive);
	}

	private void Update()
	{
		if ((GameObject.Find("Zombie(Clone)") == null))
			NextRound();
	}

	public void toggleMinimapActivation()
	{
		__minimapActivation = !__minimapActivation;
		__minimapObj.SetActive(__minimapActivation);
	}

	private void __onMinimapActive()
	{
		toggleMinimapActivation();
	}

	public void NextRound()
	{
		__round++;
		roundInfo.text = ("Round " + __round);

		__numEnemySpawning += 2;

		StartCoroutine(__enemySpawner.spawn(__numEnemySpawning, spawningPosition1.position, spawningTimePeriod));
		StartCoroutine(__enemySpawner.spawn(__numEnemySpawning, spawningPosition2.position, spawningTimePeriod));

		player.SupplyLeftBullets(30);
		player.SupplyRightBullets(30);
	}

	public void AdjustScore(int delta)
	{
		score += delta;
		scoreBoard.text = ("Score: " + score);
	}

	public void UpdateLeftBulletBoard(int numBullets)
	{
		leftBulletsInfo.text = ("X " + numBullets);
	}

	public void UpdateRightBulletBoard(int numBullets)
	{
		rightBulletsInfo.text = ("X " + numBullets);
	}

	public void UpdatePlayerHealthBar(float health)
	{
		playerProgressBar.BarValue = ((health / __playerFullHealth) * 100.0f);
	}

	public void UpdateTowerHealthBar(float health)
	{
		towerProgressBar.BarValue = ((health / __towerFullHealth) * 100.0f);
	}

	public void GameOver()
	{
		__bgmAudioSource.Stop();
		__explosionAudioSource.Play();

		tower.ShowDestroyEffect();
		gameOverScreen.SetActive(true);

		if (player.IsAlive())
			player.Die();

		StartCoroutine(Quit());
	}

	public IEnumerator Quit()
	{
		yield return new WaitForSeconds(10.0f);

		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
			Application.OpenURL("http://google.com");
		#else
			Application.Quit();
		#endif
	}
}