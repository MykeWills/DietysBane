using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawn : MonoBehaviour {

    public float respawnTime = 15;
    private float timeAcquired;
    private Vector3 startLoc;
    [HideInInspector]
    public bool active = true;
    [HideInInspector]
    public List<Transform> objects;
    void Start()
    {
        foreach (Transform child in transform)
        {
            objects.Add(child);
        }
        timeAcquired = Time.time;
    }

    public void Obtained()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].gameObject.SetActive(false);
        }
        active = false;
        timeAcquired = Time.time;
    }

    private void Update()
    {
        if (!active)
        {
            if (Time.time > timeAcquired + respawnTime)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    objects[i].gameObject.SetActive(true);
                }
                active = true;
            }
        }
    }
}
