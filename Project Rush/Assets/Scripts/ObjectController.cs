using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    //Variables ---------------------------------
    public AnimationCurve testCurve;
    public float distance = 20;
    public float speed = 1;
    public bool vertical;
    public bool horizontal;
    //Variables ---------------------------------

    //Update ---------------------------------
	void Update () {
        if (vertical)
        {
            transform.position = new Vector3(transform.position.x, testCurve.Evaluate((Time.time % testCurve.length) * speed) * distance, transform.position.z);
        }

        if (horizontal)
        {
            transform.position = new Vector3(testCurve.Evaluate((Time.time % testCurve.length) * speed) * distance, transform.position.y, transform.position.z);

        }
    }
    //Update ---------------------------------
}
