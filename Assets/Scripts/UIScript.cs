using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour{
	public Sprite[] sprites;

	public void ChangeToIndex(int index){
		var sprite = sprites[index];
		gameObject.GetComponent<Image>().sprite = sprite;
	}
}