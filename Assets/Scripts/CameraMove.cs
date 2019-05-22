using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject followObj;
    Vector3 distance;
    public float smoothTime = 0.3f;
    public float rotateSpeed = 3f;
    public float Xangle = 0.5f;
    private Vector3 v = Vector3.zero;
    Vector3 rotateVector = new Vector3();
    


    void Start()
    {
        distance = followObj.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.SmoothDamp(transform.position, followObj.transform.position - distance, ref v, smoothTime);

    }

    public void SetTarget(GameObject g)
    {
        followObj = g;
    }
}
