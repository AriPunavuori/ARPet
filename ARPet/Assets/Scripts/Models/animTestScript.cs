using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animTestScript : MonoBehaviour {
    Animator m_Animator;


    void Start() {
        m_Animator = GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.J)) {
            m_Animator.Play("spook");
            print("Eek!");
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            m_Animator.Play("laugh");
            print("Hihi!");
        }
    }
}
