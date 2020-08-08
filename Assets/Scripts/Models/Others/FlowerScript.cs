<<<<<<< HEAD:Assets/Scripts/Models/Others/FlowerScript.cs
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour {

    public float hatchTime = 10;
    float hatchTimer;
    public GameObject battery;
    public bool batteryAttached;
	// Use this for initialization
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour {

    public float hatchTime = 10;
    public float hatchTimer;
    public GameObject battery;
    public bool batteryAttached;
	// Use this for initialization
>>>>>>> c5991e646145c1830fee939d0e66a147eea824cc:ARPet/Assets/Scripts/Models/Others/FlowerScript.cs
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
<<<<<<< HEAD:Assets/Scripts/Models/Others/FlowerScript.cs
            var newBat = Instantiate(battery);
            newBat.transform.position = transform.position + Vector3.up * 2;
=======
            var newBat = Instantiate(battery);
            newBat.transform.position = transform.position + Vector3.up * 0.43f;
>>>>>>> c5991e646145c1830fee939d0e66a147eea824cc:ARPet/Assets/Scripts/Models/Others/FlowerScript.cs
            newBat.transform.parent = transform;
            batteryAttached = true;
            hatchTimer = hatchTime;
        }
    }
}
