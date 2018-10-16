using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMove : MonoBehaviour {

    public GameObject obj;
	
	// Update is called once per frame
	void Update () {
		
	}



    public void OnPress(bool isPress)
    {
        if (isPress)
        {
            obj.transform.Translate(new Vector3(transform.localPosition.x, -(transform.localPosition.y + 3), 0));
        }
    }
}
