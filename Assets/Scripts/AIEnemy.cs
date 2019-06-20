using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
	public Transform destinationTarget;

	private UnityEngine.AI.NavMeshAgent __agent;
	private EnemyController __enemyController;
	private Transform __currentTarget;

	private void Awake()
	{
		__agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		__enemyController = GetComponent<EnemyController>();
	}

	private void Start()
	{
		__agent.updateRotation = false;
		__agent.updatePosition = true;

		SetTracingTarget(destinationTarget);
	}

	private void FixedUpdate()
	{
		if (__currentTarget != null)
			__agent.SetDestination(__currentTarget.position);

		if (__agent.remainingDistance > __agent.stoppingDistance)
			__enemyController.move(__agent.desiredVelocity);
		else
			__enemyController.move(Vector3.zero);
	}

	public void SetTracingTarget(Transform target)
	{
		if (target == null)
			__currentTarget = destinationTarget;
		else
			__currentTarget = target;
	}
}
