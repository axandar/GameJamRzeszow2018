using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public Sprite[] sprites;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ChangetoIndex(int index) {
        Sprite sprite = sprites[index];
        gameObject.GetComponent<Image>().sprite = sprite;
    }
}
