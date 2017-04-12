using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Script used to load different textMesh object in sequential order at start of game
 * 
 */
public class StartScreen : MonoBehaviour {
    public GameObject[] sharpTexts;
    private GameObject currentText;
    private GameObject currClone;
    private TextTrigger trigger;
    private int textIdx;

    SteamVR_TrackedObject trackedObj;
    SteamVR_TestTrackedCamera trackedCamera;
    FixedJoint joint;

    /**
     * Load first text mesh onto screen at start
     */
    private void Start()
    {
        textIdx = 0;

        if (sharpTexts.Length >= 1)
        {
            currentText = sharpTexts[0];
            trigger = currentText.GetComponent<TextTrigger>();
            currentText = GameObject.Instantiate(currentText);
        }
    }

    void Awake()
    {
        GameObject controller = GameObject.Find("Controller (right)");
        trackedObj = controller.GetComponent<SteamVR_TrackedObject>();
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

        if (trigger)
        {
            if (trigger.textActionTrigger == TextTrigger.triggerType.buttonPress)
            {
                if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
                {
                    GameObject.Destroy(currentText);
                    Debug.Log(currentText.name);
                    trigger = GetNextTextTrigger();
                    if (trigger)
                    {
                        currentText = GameObject.Instantiate(currentText);
                    }
                    Debug.Log(currentText.name);
                }
            }
            else if (trigger.textActionTrigger == TextTrigger.triggerType.cameraView)
            {
                // If looking near direction, switch text on screen

            }
            else if (trigger.textActionTrigger == TextTrigger.triggerType.collision)
            {
                // If text if collided with, then switch text on screen

            }
            else
            {
                // Do not believe there will a text with no trigger
            }
        }
        
      
    }

    /**
     * Get next text mesh to be loaded and return its trigger to be start action (destruction)
     */ 
    TextTrigger GetNextTextTrigger()
    {
        if (textIdx + 1 >= sharpTexts.Length)
        {
            return null;
        }

        currentText = sharpTexts[textIdx + 1];
        textIdx += 1;

        return currentText.GetComponent<TextTrigger>();
    }
}
