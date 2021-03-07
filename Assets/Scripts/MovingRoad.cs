using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRoad : MonoBehaviour{

	[SerializeField] private GameManager gameManager;
	
	private SpriteRenderer _sr;
	public float maxSpeed = 3f;
	public float currSpeed;
	public float slowTime = 2f;
	
	private void Start(){
		currSpeed = 0f;
		_sr = GetComponent<SpriteRenderer>();
	}
	
	private void Update(){
		currSpeed = CalculateCurrentSpeed();

		var offset = _sr.material.GetTextureOffset("_MainTex");
		offset.x += Time.deltaTime * currSpeed;
		_sr.material.SetTextureOffset("_MainTex", offset);
	}

	private float CalculateCurrentSpeed(){
		if (IsSlowdown()){
			if(currSpeed > 0f){
				return currSpeed - Time.deltaTime * slowTime;
			}

			return currSpeed;
		}
		
		if(currSpeed < maxSpeed){
			return currSpeed + Time.deltaTime * slowTime;
		}

		return currSpeed;
	}

	private bool IsSlowdown(){
		return !gameManager.PlayerCar.IsAlive;
	}
}