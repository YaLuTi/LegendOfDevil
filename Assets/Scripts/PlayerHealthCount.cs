using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthCount : MonoBehaviour {

    public bool active = false;
    public Text hpCounter = null;
    public Text apCounter = null;
    public int[] currentHealth = new int[4];
    public int[] currentAction = new int[4];
    ChangeCharacter selectedCharacter;

    void Start ()
    { 
        selectedCharacter = gameObject.GetComponent<ChangeCharacter>();
    }
	

	void Update ()
    {
        //hpCounter.text = "HP:" + currentHealth[selectedCharacter.selected].ToString();
        //apCounter.text = "AP:" + currentAction[0].ToString();
        //hpCounter.text = (selectedCharacter.selected).ToString();
    }
    
}
