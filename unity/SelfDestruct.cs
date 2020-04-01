using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    public int SecondsToDeath = 10;
    DateTime deathTime;
    void Start()
    {
        // There is a special place in hell for people that don't use UTC.
        TimeSpan lifetime = new TimeSpan(0, 0, SecondsToDeath);
        deathTime = DateTime.UtcNow.Add(lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(DateTime.UtcNow);
        if(deathTime - DateTime.UtcNow < TimeSpan.Zero) {
            Destroy(gameObject);
        }
    }
}
