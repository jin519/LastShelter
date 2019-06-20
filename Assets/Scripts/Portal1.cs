using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal1 : MonoBehaviour
{
	public Transform portal3Destination;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
			other.gameObject.transform.position = portal3Destination.position;
	}
}
