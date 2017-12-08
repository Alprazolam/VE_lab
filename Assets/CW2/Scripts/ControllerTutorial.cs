using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTutorial : MonoBehaviour {

    [SerializeField]

    private List<GameObject> tutorialItems;
	private SteamVR_TrackedObject trackedObj;

	public static bool tutorialVisible = true;
    public bool tutorialSwitch = true;

    private bool itemsActive = true;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Start () { }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

	// Called once a frame
    void Update ()
	{
        if (tutorialVisible && !itemsActive) // Check if tutorial should be visible and if items are inactive
		{
            // Activate the items
            for (int i=0; i<tutorialItems.Count; i++)
            {
                tutorialItems[i].SetActive(true);
            }

            itemsActive = !itemsActive; // Flip the itemActive boolean to prevent additional activations
		}

		else if (!tutorialVisible && itemsActive)
		{
            // Deactivate the items
            for (int i = 0; i < tutorialItems.Count; i++)
			{
                tutorialItems[i].SetActive(false);
            }

            itemsActive = !itemsActive;
        }

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu))
		{
			UCL.COMPGV07.Logging.KeyDown();

			if (tutorialSwitch == true)
			{
				tutorialVisible = !tutorialVisible; // Turn the tutorial on or off
				tutorialSwitch = false; // Disable switching
			}
		}

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu) && tutorialSwitch == false)
        {
            tutorialSwitch = true;
        }
    }
}
