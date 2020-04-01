using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Shields : MonoBehaviour
{
    private float CurrentShieldsStrength;
    public float BaseHealth = 10;
    public float MaxShieldsStrength = 100;
    public float ShieldRegenerationRate = 5f;
    public int secondsUntilShieldsRegenerate = 3;
    TimeSpan shieldCooldown;
    DateTime shieldRegenerate;

    void Awake() {
        shieldCooldown = new TimeSpan(0, 0, secondsUntilShieldsRegenerate);
        CurrentShieldsStrength = MaxShieldsStrength;
    }

    void OnTriggerEnter(Collider other){
        CurrentShieldsStrength -= other.gameObject.GetComponent<Projectile>().DestrutivePower;
        if( CurrentShieldsStrength < 0 ) {
            BaseHealth += CurrentShieldsStrength;
            shieldRegenerate = DateTime.UtcNow.Add(shieldCooldown);
        }

        if( BaseHealth <= 0) {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (shieldRegenerate - DateTime.UtcNow < TimeSpan.Zero && CurrentShieldsStrength < MaxShieldsStrength) {
            CurrentShieldsStrength += Time.deltaTime * ShieldRegenerationRate;
        }
    }
}
