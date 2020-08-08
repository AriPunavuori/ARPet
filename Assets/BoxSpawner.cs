using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour {

    public GameObject box;

public void SpawnBox() {
        Instantiate(box);
    }
}
