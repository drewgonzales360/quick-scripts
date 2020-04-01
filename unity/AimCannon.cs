using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCannon : MonoBehaviour
{
    [SerializeField] public GameObject Periscope;
    public void FixedUpdate()
    {
        Vector3 relativePos = Periscope.transform.localPosition - transform.localPosition;
        transform.localRotation = Quaternion.LookRotation(relativePos, Vector3.up);
    }
}
