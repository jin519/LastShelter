using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal3 : MonoBehaviour
{
	public Transform portal1Destination;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
			other.gameObject.transform.position = portal1Destination.position;
	}
}
