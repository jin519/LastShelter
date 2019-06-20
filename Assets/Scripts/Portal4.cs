using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal4 : MonoBehaviour
{
	public Transform portal2Destination;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
			other.gameObject.transform.position = portal2Destination.position;
	}
}
