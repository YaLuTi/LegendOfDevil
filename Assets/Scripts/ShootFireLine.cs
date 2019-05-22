using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShootFireLine : MonoBehaviour {

    LineRenderer lineRenderer;
    float x, y;
    public Vector3 start;
    public Vector3 end;
	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        x = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(x < 1)
        {
            x += Time.deltaTime;
        }
        else
        {
            x = 1;
        }
        y = Mathf.Sqrt(1 - Mathf.Pow((0.1f - x), 2f)) + (0.6f - x) * x * 2.5f;
        lineRenderer.widthMultiplier = y / 10f;
	}
}
