using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Script used to load different textMesh object in sequential order at start of game
 * 
 */
public class StartScreen : MonoBehaviour {
    public GameObject[] sharpTexts;

    private GameObject currText;
    private TextTrigger trigger;
    private int textIdx;

    SteamVR_TrackedObject trackedObj;
    SteamVR_TestTrackedCamera trackedCamera;
    //FixedJoint joint;
    Transform playerView;

    /**
     * Load first text mesh onto screen at start
     */
    private void Start()
    {
        textIdx = -1;

        SwitchText();
    }

    void Awake()
    {
        GameObject controller = GameObject.Find("Controller (right)");
        trackedObj = controller.GetComponent<SteamVR_TrackedObject>();
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        playerView = camera.transform;
    }

    /**
     * Text Mesh at desigated trigger will be destroyed so it is no longer in view. If there is a next text mesh
     * to be loaded, than it will be and process will be repeated.
     */
    void FixedUpdate()
    {
        if (!trackedObj)
        {
            return;
        }

        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        


        if (trigger.textActionTrigger == TextTrigger.triggerType.buttonPress)
        {
            // If trigger button is pressed, switch text on screen
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            //if (Input.GetMouseButtonDown(0))
            {
                SwitchText();
            }
        }
        else if (trigger.textActionTrigger == TextTrigger.triggerType.cameraView)
        {

            // If looking near direction, switch text on screen
            if (Vector3.Dot(playerView.forward, (trigger.lookDirection.position - playerView.position).normalized) > 0.83f)
            {
                SwitchText();
            }

        }
        else if (trigger.textActionTrigger == TextTrigger.triggerType.collision)
        {
            Debug.Log(trigger.textActionTrigger);
            Debug.Log(trigger.hasCollided);

            // If text if collided with, then switch text on screen
            if (trigger.hasCollided)
            {
                SwitchText();
            }

        }
        else
        {
            // Do not believe there will be a text with no trigger
        }          
    }

    /**
     * Destroy previous text and get next text mesh to be loaded.
     * Destroy this script if no more text meshes to be loaded.
     */ 
    private void SwitchText()
    {
        if (textIdx + 1 >= sharpTexts.Length)
        {
            Destroy(currText);
            Destroy(this);
            return;
        }

        Destroy(currText);
        currText = GameObject.Instantiate(sharpTexts[textIdx + 1]);
        textIdx += 1;
        trigger = currText.GetComponent<TextTrigger>();
    }


}
