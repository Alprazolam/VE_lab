using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTutorial : MonoBehaviour {

    [SerializeField]
    private List<GameObject> tutorialItems;

    public static bool tutorialVisible = true;

    public bool tutorial = true;

    private bool itemsActive = true;

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    // Use this for initialization
    void Start () {
		
	}

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update () {
        // if the tutorial should be visible and the items haven't activated
        if (tutorialVisible && !itemsActive) {
            //Activate the items
            for(int i=0; i<tutorialItems.Count; i++)
            {
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
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu) && tutorial == true)
        {
            UCL.COMPGV07.Logging.KeyDown();
            ControllerTutorial.tutorialVisible = !ControllerTutorial.tutorialVisible;
            tutorial = false;
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu) && tutorial == false)
        {
            tutorial = true;
        }
    }
}
