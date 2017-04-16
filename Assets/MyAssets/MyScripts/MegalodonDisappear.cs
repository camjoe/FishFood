using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegalodonDisappear : MonoBehaviour {
    GameObject menu;

	// Use this for initialization
	void Start () {
        menu = GameObject.Find("Menu");
	}
	
	// Update is called once per frame
	void Update () {
		if (!menu.GetComponent("StartScreen"))
        {
            Destroy(gameObject);
        }
	}
}
