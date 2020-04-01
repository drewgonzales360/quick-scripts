using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public GameObject Projectile;
    public float speed = 10f;
    void Start() {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F)){
            GameObject p = Instantiate(Projectile, this.transform.position, this.transform.rotation);
            Rigidbody rigidBody = p.GetComponent<Rigidbody>();
            rigidBody.velocity = p.transform.forward * speed;
        }
    }
}
