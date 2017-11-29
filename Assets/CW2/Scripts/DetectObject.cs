using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour {
	private BoxCollider cart;

	// initialization
	void Start ()
	{
		cart = GetComponent<BoxCollider>();
	}
	
	void FixedUpdate () {
		
	}

	void OnTriggerEnter(Collider other) //IEnumerator
    {
		if (other.gameObject.layer == 9) //check if object is grabbable
		{
			//yield return new WaitForSecondsRealtime(1); //delay so object can fall down
			other.transform.parent = cart.transform;
		}
	}

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.transform.parent = null;
        }
    }

}