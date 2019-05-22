using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StageManager : MonoBehaviour {

    public MapData mapData;
    public GameObject grid;
    public int x, y;
    public Vector3 StartPosition;

    public GameObject[,] Grids;
	// Use this for initialization
	void Start ()
    {
        if (mapData.DataArray.Length ==0)
        {
            Debug.Log("Create New");
            Grids = new GameObject[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    GameObject g = Instantiate(grid, StartPosition + new Vector3(i, 0, j), grid.transform.rotation);
                    Grids[i,j] = g;
                }
            }
        }
        else
        {
            Debug.Log("Load Sucess");
            Grids = new GameObject[mapData.DataArray.Length, mapData.DataArray[0].array.Length];
            for (int i = 0; i < mapData.DataArray.Length; i++)
            {
                for (int j = 0; j < mapData.DataArray[0].array.Length; j++)
                {
                    GameObject g = Instantiate(grid, StartPosition + new Vector3(i, 0, j), grid.transform.rotation);
                    g.GetComponent<Grid>().GridType = mapData.DataArray[i].array[j];
                    Grids[i, j] = g;
                }
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Save()
    {
        Debug.Log("Save");

        mapData.name = "New";

        int[,] newData;

        newData = new int[Grids.GetLength(0), Grids.GetLength(1)];

        // mapData.DataArray = new NArrays[Grids.GetLength(0)];

        Debug.Log(mapData.DataArray[0].array[0]);

        /*for (int i = 0; i < Grids.GetLength(0); i++)
        {
            mapData.DataArray[i].array = new int[10];
        }*/

        for (int i = 0; i < Grids.GetLength(0); i++)
        {
            for (int j = 0; j < Grids.GetLength(1); j++)
            {
                newData[i, j] = Grids[i, j].GetComponent<Grid>().GridType;
                mapData.DataArray[i].array[j] = newData[i, j];
            }
        }

        // Debug.Log(mapData.DataArray[2,3]);
        AssetDatabase.SaveAssets();
    }
}
