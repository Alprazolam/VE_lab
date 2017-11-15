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

	void OnTriggerEnter(Collider other)
	{
		other.transform.parent = cart.transform;
	}
}