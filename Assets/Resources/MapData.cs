using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName ="Data/MapData")]
public class MapData : ScriptableObject {
    public string Stagename;
    public NArrays[] DataArray;


}

[System.Serializable]
public class NArrays
{
    public int[] array = new int[10];

}
