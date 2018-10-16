using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMove : MonoBehaviour {

    public GameObject obj;
	
	// Update is called once per frame
	void Update () {
		
	}



    public void OnPress()
    {
        Debug.LogError(" transform.localPosition.x " + transform.localPosition.x);
       //obj.transform.Translate(new Vector3(transform.localPosition.x +  3, transform.localPosition.y, 0));
        Debug.LogError(" transform.localPosition.x " + transform.localPosition.x);
    }
}
