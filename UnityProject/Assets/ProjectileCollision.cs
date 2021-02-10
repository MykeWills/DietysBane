using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileElement{ Blaster, FlakShotgun, PhotonRifle, ThePiercer, TitanLauncher, HyperBeam, VortexCannon  }
public class ProjectileCollision : MonoBehaviour {
    
    GameObject explosion;
    public ProjectileElement elementType;
    bool blaster;
    bool flak;
    bool photon;
    bool piercer;
    bool titan;
    bool hyper;
    bool vortex;


    public void OnCollisionEnter(Collision collision)
    {
        ObjectCollision(elementType);

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats PlayST = collision.gameObject.GetComponent<PlayerStats>();
            if (blaster)
                PlayST.AddDamageType(PlayerStats.DamageElement.Blaster);
            else if(flak)
                PlayST.AddDamageType(PlayerStats.DamageElement.Flak);
            else if (photon)
                PlayST.AddDamageType(PlayerStats.DamageElement.Photon);
            else if (piercer)
                PlayST.AddDamageType(PlayerStats.DamageElement.Piercer);
            else if (titan)
                PlayST.AddDamageType(PlayerStats.DamageElement.Titan);
            else if (hyper)
                PlayST.AddDamageType(PlayerStats.DamageElement.Hyper);
            else if (vortex)
                PlayST.AddDamageType(PlayerStats.DamageElement.Vortex);
        }
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(expl, 5f);

    }
    public void ObjectCollision(ProjectileElement type)
    {
        switch (type)
        {
            case ProjectileElement.Blaster:
                {
                    blaster = true;
                    explosion = (GameObject)Resources.Load("Prefabs/BlasterExplode", typeof(GameObject));
                    break;
                }
            case ProjectileElement.FlakShotgun:
                {
                    flak = true;
                    explosion = (GameObject)Resources.Load("Prefabs/FlakExplode", typeof(GameObject));
                    break;
                }
            case ProjectileElement.PhotonRifle:
                {
                    photon = true;
                    explosion = (GameObject)Resources.Load("Prefabs/PhotonExplode", typeof(GameObject));
                    break;
                }
            case ProjectileElement.ThePiercer:
                {
                    piercer = true;
                    explosion = (GameObject)Resources.Load("Prefabs/PiercerExplode", typeof(GameObject));
                    break;
                }
            case ProjectileElement.TitanLauncher:
                {
                    titan = true;
                    explosion = (GameObject)Resources.Load("Prefabs/RocketExplode", typeof(GameObject));
                    break;
                }
            case ProjectileElement.HyperBeam:
                {
                    hyper = true;
                    explosion = (GameObject)Resources.Load("Prefabs/HyperExplode", typeof(GameObject));
                    break;
                }
            case ProjectileElement.VortexCannon:
                {
                    vortex = true;
                    explosion = (GameObject)Resources.Load("Prefabs/VortexExplode", typeof(GameObject));
                    break;
                }
        }
    }
}
