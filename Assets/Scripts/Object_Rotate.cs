using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Object_Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.DORotate(transform.eulerAngles + new Vector3(0, 3, 0), 0.1f);
	}
}
