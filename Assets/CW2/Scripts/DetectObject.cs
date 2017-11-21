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
		yield return new WaitForSecondsRealtime(1); //delay so object can fall into the cart
		other.transform.parent = cart.transform;
	}
}