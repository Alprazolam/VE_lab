using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour {
	private Rigidbody cart;

	// initialization
	void Start ()
	{
		cart = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		
	}

	IEnumerator OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9) //check if object is grabbable
		{
			yield return new WaitForSecondsRealtime(1); //delay so object can fall down
			other.transform.parent = cart.transform;
		}
	}
}