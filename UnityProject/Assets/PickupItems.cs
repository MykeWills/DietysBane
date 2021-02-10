using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectItem { BlasterAmmo }
public class PickupItems : MonoBehaviour {

    PlayerWeapon playWeap;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        playWeap = other.gameObject.GetComponent<PlayerWeapon>();
    //        playWeap.GainAmmo(PlayerWeapon.GunAmmo.Blaster, 50);
    //        Destroy(gameObject);
    //    }
    //}
}
