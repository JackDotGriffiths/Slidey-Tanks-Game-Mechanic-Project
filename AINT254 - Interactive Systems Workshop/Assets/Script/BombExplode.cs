﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour {

    public GameObject enemyPlayer;
    public GameObject explosionParticles;
    public AudioSource explosionSound;
    // Update is called once per frame
    void Explode()
    {
        var Explode = (GameObject)Instantiate(
         explosionParticles,
         transform.position,
         transform.rotation);

        explosionSound.Play();
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<MeshFilter>() != null)
            {
                child.gameObject.GetComponent<MeshFilter>().mesh = null;
            }
        }
        ExplosionForce();
        Destroy(gameObject, .701f);
        BombPlace.P1BombActive = false;
    }

    void ExplosionForce()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 25f);
        foreach (Collider hit in colliders)
        {
            if (hit.tag == "Player1Bullet" || hit.tag == "Player2Bullet"){
                Debug.Log("Skip");
            }
            else
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(2000, explosionPos, 10f, 3.0F);
                  
            }
        }
    }
}
