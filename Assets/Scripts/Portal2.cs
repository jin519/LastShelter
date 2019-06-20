using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2 : MonoBehaviour
{
	public Transform portal4Destination;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
			other.gameObject.transform.position = portal4Destination.position;
	}
}
