using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kyle : MonoBehaviour {

	// Use this for initialization
	void Start () {

        foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }

        gameObject.GetComponent<Animator>().enabled = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
