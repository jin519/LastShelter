using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIconPoser : MonoBehaviour
{
	private Transform __cameraTransform;

	private void Awake()
	{
		__cameraTransform = Camera.main.transform;
	}

	void Update()
    {
		__calcRotation();
	}

	private void __calcRotation()
	{
		Vector3 myRight = Vector3.Cross(Vector3.up, __cameraTransform.forward);
		if (myRight.magnitude < 0.01)
			return;

		Vector3 myForward = Vector3.Cross(myRight, Vector3.up);
		transform.rotation = Quaternion.LookRotation(Vector3.down, myForward);
	}
}
