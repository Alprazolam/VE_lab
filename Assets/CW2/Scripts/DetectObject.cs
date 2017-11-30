using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
	private BoxCollider cart;

	void Start ()
	{
		cart = GetComponent<BoxCollider>();
	}
	
	void Update () { }

	void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.layer == 9) // Check if object is grabbable
		{
			other.transform.parent = cart.transform; // Make an object inside a cart its child
		}
	}

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.transform.parent = null; // Break the relationship when the object is outside the cart
        }
    }
}