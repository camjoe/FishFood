using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAction : MonoBehaviour {
    public string activator;
    public Quaternion lookDirection;
    public string button;
    public string seconds;

    public GameObject[] sharpTexts;

    private enum catalyst {anyButton, triggerButton, cameraView, wait, undefined};
    private catalyst myCatalyst;

    SteamVR_TrackedObject trackedObj;
    SteamVR_TestTrackedCamera trackedCamera;
    FixedJoint joint;

    private void Start()
    {
        sharpTexts = GameObject.FindGameObjectsWithTag("text");

        if (activator == "press")
        {
            if (button == "trigger")
            {
                myCatalyst = catalyst.triggerButton;
            }
            else
            {
                myCatalyst = catalyst.anyButton;
            }
        }
        else if (activator == "look")
        {
            myCatalyst = catalyst.triggerButton;
        }
        else if (activator == "time")
        {
            myCatalyst = catalyst.wait;
        }
        else
        {
            myCatalyst = catalyst.undefined;
        }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();


    }

    void FixedUpdate()
    {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);


        if (myCatalyst == catalyst.triggerButton)
        {
            if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                
            }
        }else if (myCatalyst == catalyst.cameraView)
        {
            
        }
    }
}
