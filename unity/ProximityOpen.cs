using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityOpen : MonoBehaviour
{

    Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other){
        animator.SetTrigger("ProximityEnter");
    }

    void OnTriggerExit(Collider other){
        animator.SetTrigger("ProximityExit");
    }
}
