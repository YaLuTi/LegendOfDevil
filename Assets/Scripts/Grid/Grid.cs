using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public int GridType = 0;
    int OldGridType = -1;
    public bool choosing = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

            switch (GridType)
            {
                case 0:
                    this.GetComponent<MeshRenderer>().materials[0].color = Color.white;
                    break;
                case 1:
                    this.GetComponent<MeshRenderer>().materials[0].color = Color.green;
                    break;
            }
            OldGridType = GridType;
        

        if (choosing)
        {
            this.GetComponent<MeshRenderer>().materials[0].color = Color.red;
        }
	}
}
