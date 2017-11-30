using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public Transform camRigTransform;    // cameraRig position
    public Transform headTransform;    // Player's head position
    public Vector3 teleportOffset;    // Offset from the floor for the reticle
    public LayerMask teleportMask;    // Mask: teleportation allowed
    public LayerMask cantMoveMask;    // Mask: teleportation not allowed

	// Laser
    public GameObject laserPrefab;
    private GameObject laser_ins;    // Laser instance
    private Transform laserTransform;

	// Reticle
    public GameObject reticlePrefab;
    private GameObject reticle_ins;    // Reticle instance
    private Transform reticleTransform;

    private bool ifTeleport;    // True if there's a valid teleport target
    private Vector3 hitPos;    // Point where the raycast hits

    private SteamVR_TrackedObject trackedObj;
    private GameObject Cart;    // Get cart object


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Start()
    {
        // Initialize laser
        laser_ins = Instantiate(laserPrefab);
        laserTransform = laser_ins.transform;

        // Initialize reticle
        reticle_ins = Instantiate(reticlePrefab);
        reticleTransform = reticle_ins.transform;
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        Cart = GameObject.Find("mesh_cart_01");
    }

	// Called once a frame
    private void Update()
    {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
			UCL.COMPGV07.Logging.KeyDown();
			RaycastHit hitPoint;

            bool hit = Physics.Raycast(trackedObj.transform.position, transform.forward, out hitPoint, 100, teleportMask);
            bool hitShelf = Physics.Raycast(trackedObj.transform.position, transform.forward, out hitPoint, 100, cantMoveMask);

            ifTeleport = false; // Disables teleporting outside of the range since this is called once a frame

            if (hit && !hitShelf)     // If there is an intersection with teleportMask and without shelf, show the laser and reticle
            {
                hitPos = hitPoint.point;
                DisplayLaser(hitPoint);
                reticleTransform.position = hitPos + teleportOffset;

                ifTeleport = true;
            }
            else if (!hit && hitShelf) // ************GET RID OF?***********
            {
                //DisplayLaser(hitPoint);
                reticle_ins.SetActive(false);
                laser_ins.SetActive(false);
                ifTeleport = false;
            }
            else // If there is no intersection with teleportMask, set laser and reticle as false
            {
                //DisplayLaser(hitPoint);
                reticle_ins.SetActive(false);
                laser_ins.SetActive(false);
                ifTeleport = false;
            }
            
        }
        else // Touchpad not pressed
        {
            laser_ins.SetActive(false);
            reticle_ins.SetActive(false);
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && ifTeleport == true)
        {
			Teleportation();
        }
	}

    private void DisplayLaser(RaycastHit hitPoint)
    {
        laser_ins.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPos, .5f);
        laserTransform.LookAt(hitPos);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hitPoint.distance);
        reticle_ins.SetActive(true);
    }

    private void Teleportation()
    {
        reticle_ins.SetActive(false);
        Vector3 offset = camRigTransform.position - headTransform.position;
        offset.y = 0;
        camRigTransform.position = hitPos + offset;
        Cart.transform.position = hitPos;
        ifTeleport = false;
    }

}