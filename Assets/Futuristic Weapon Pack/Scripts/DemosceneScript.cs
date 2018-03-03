using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemosceneScript : MonoBehaviour {

	public GameObject cube;
	public float rotationSpeed; 
	public GameObject[] weapons;

	int count = -1;
	bool rotation = true;

	void Start () {
		weapons [count + 1].SetActive (true);
		count++;
	}

	void FixedUpdate () {
		if(rotation == true)
		cube.transform.Rotate (0, 0.1f * rotationSpeed, 0);

	}

	public void NextWeapon ()
	{
		if (count == 23) {
			weapons [count].SetActive (false);
			count = 0;
		}
		weapons [count].SetActive (false);
		weapons [count + 1].SetActive (true);
		count++;
	}

	public void SwitchRotation ()
	{
		if (rotation == true)
			rotation = false;
		else if (rotation == false)
			rotation = true;
		
	}


}
