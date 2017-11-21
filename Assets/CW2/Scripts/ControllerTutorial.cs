using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTutorial : MonoBehaviour {

    [SerializeField]
    private List<GameObject> tutorialItems;

    public static bool tutorialVisible = true;

    private bool itemsActive = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // if the tutorial should be visible and the items haven't activated
        if (tutorialVisible && !itemsActive) {
            //Activate the items
            for(int i=0; i<tutorialItems.Count; i++){
                tutorialItems[i].SetActive(true);
            }

            // flip the itemActive boolean to prevent additional activations
            itemsActive = !itemsActive;
        }else if(!tutorialVisible && itemsActive){
            //deactivate the items

            for (int i = 0; i < tutorialItems.Count; i++)
            {
                tutorialItems[i].SetActive(false);
            }
            itemsActive = !itemsActive;
        }
	}
}
