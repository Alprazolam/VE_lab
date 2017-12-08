using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTask : MonoBehaviour {
    Collider collider;

    // Use this for initialization
    void Start () {
        collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnTriggerEnter(Collider collider)
    {
        GameObject.Find("New Text").SetActive(false);
        GameObject.Find("New Text (1)").SetActive(true);
    }
}
