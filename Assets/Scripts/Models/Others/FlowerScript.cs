using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour {

    public float hatchTime = 10;
    float hatchTimer;
    public GameObject battery;
    public bool batteryAttached;
	// Use this for initialization
	void Start () {
        hatchTimer = hatchTime;
	}

    // Update is called once per frame
    void Update() {
        hatchTimer -= Time.deltaTime;

        if(hatchTimer < 0) {
            BatteryHatched();
        }
    }

    public void BatteryHatched() {
        if(!batteryAttached) {
            var newBat = Instantiate(battery);
            newBat.transform.position = transform.position + Vector3.up * 2;
            newBat.transform.parent = transform;
            batteryAttached = true;
            hatchTimer = hatchTime;
        }
    }
}
