using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAggressiveController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "EnemyBodyPart")
		{
			AIEnemy enemy = other.transform.root.GetComponent<AIEnemy>();
			enemy.SetTracingTarget(transform);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "EnemyBodyPart")
		{
			AIEnemy enemy = other.transform.root.GetComponent<AIEnemy>();
			enemy.SetTracingTarget(null);
		}
	}
}
