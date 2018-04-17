namespace GoogleARCore.HelloAR
{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;


public class TragetPos : MonoBehaviour {

	// Use this for initialization
	//public GameObject _targetParent;
	public GameManager _GM;
	void Start () {
			Debug.Log("Pos.....");
		
		_GM=GameObject.Find("GameManager").GetComponent<GameManager>();
		//_HC = GameObject.Find("HelloARController").GetComponents<HelloARController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.y < (-15)){
			Debug.Log("Pos.....");
		_GM.startRespawn();
			Destroy(this.gameObject);
		}
	}
}
}