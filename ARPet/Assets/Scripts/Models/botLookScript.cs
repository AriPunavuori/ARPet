using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botLookScript : MonoBehaviour {
    public Transform target;
    public Transform eye;
    public float maxX;
    public float xFactor = 3;
    Vector3 origPos;

	void Start () {
        origPos = eye.position;
	}
	

	void Update () {
        //Debug.DrawLine(target.position, transform.position);
       var localDir = transform.InverseTransformVector(target.position - transform.position);
        localDir.Normalize();
        eye.position = origPos + eye.right * xFactor * localDir.x;
    }
}
