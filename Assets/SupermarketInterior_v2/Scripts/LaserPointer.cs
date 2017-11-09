using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {


    public Transform cameraRigTransform;    // The position of cameraRig center
    public Transform headTransform;         // The position of player's head
    public Vector3 teleportReticleOffset;   // Offset from the floor for the reticle to avoid z-fighting
    public LayerMask teleportMask;  // Mask to filter out areas where teleports are allowed

    private SteamVR_TrackedObject trackedObj;

    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;

    public GameObject teleportReticlePrefab;
    private GameObject reticle;
    private Transform teleportReticleTransform;

    private Vector3 hitPoint;  // Point where the raycast hits
    private bool shouldTeleport;  // True if there's a valid teleport target

    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


    // Use this for initialization
    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;

            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);

                //Show teleport reticle
                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;

                shouldTeleport = true;
            }
        }
        else
        {
            laser.SetActive(false);
            reticle.SetActive(false);
        }

        // If press trigger & valid teleport position found
        if (Controller.GetHairTriggerDown() && shouldTeleport)
        {
            Teleport();
        }
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);  //Show the laser
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);    // Move laser to the middle between the controller and the position that ray hit
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
        // Scale laser so it fits exactly between the controller & the hit point
    }

    private void Teleport()
    {
        shouldTeleport = false;
        reticle.SetActive(false);
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;

        cameraRigTransform.position = hitPoint + difference;
    }

}
