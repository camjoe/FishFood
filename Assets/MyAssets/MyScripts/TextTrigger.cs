using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour {
    public enum triggerType { buttonPress, cameraView, collision, noAction};
    public enum button { trigger, any };

    public triggerType textActionTrigger;
    public button activator;

    public Quaternion direction;
}
