using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {


    public Transform camRigTransform;    // The position of cameraRig center
    public Transform headTransform;         // The position of player's head
    public Vector3 teleportOffset;   // Offset from the floor for the reticle to avoid z-fighting
    public LayerMask teleportMask;  // Mask to filter out areas where teleports are allowed

    public GameObject laserPrefab;
    private GameObject laser_ins;   // laser instance
    private Transform laserTransform;

    public GameObject reticlePrefab;
    private GameObject reticle_ins;  // reticle instance
    private Transform reticleTransform;

    private bool ifTeleport;  // True if there's a valid teleport target
    private Vector3 hitPos;  // Point where the raycast hits

    private SteamVR_TrackedObject trackedObj;


    private SteamVR_Controller.Device Controller
    {
        get{
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private void Start()
    {
        //initialize laser
        laser_ins = Instantiate(laserPrefab);    
        laserTransform = laser_ins.transform;

        //initialize reticle
        reticle_ins = Instantiate(reticlePrefab);
        reticleTransform = reticle_ins.transform;
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update()
    {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)){
            RaycastHit hitPoint;

            bool hit = Physics.Raycast(trackedObj.transform.position, transform.forward, out hitPoint, 100, teleportMask);
            if (hit)     // if there is a intersection with teleportMask, show the laser and reticle
            {
                hitPos = hitPoint.point;
                displayLaser(hitPoint);
                reticleTransform.position = hitPos + teleportOffset;

                ifTeleport = true;
            }else {      // if there is no intersection with teleportMask, set laser and reticle as false
                laser_ins.SetActive(false);
                reticle_ins.SetActive(false);
            }
        }
        else {           // if the touchPad is not pressed
            laser_ins.SetActive(false);
            reticle_ins.SetActive(false);
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && ifTeleport) {
            teleportation();
        }
    }

    private void displayLaser(RaycastHit hitPoint) {
        laser_ins.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPos, .5f);
        laserTransform.LookAt(hitPos);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hitPoint.distance);
        reticle_ins.SetActive(true);
    }

    private void teleportation() {
        reticle_ins.SetActive(false);
        Vector3 offset = camRigTransform.position - headTransform.position;
        offset.y = 0;
        camRigTransform.position = hitPos + offset;
        ifTeleport = false;
    }

}
