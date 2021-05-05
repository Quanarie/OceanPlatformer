using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private GameObject pickUpEffect;
    private AudioSource soundSource;
    private MeshRenderer coinMesh;

    private void Start()
    {
        soundSource = GetComponent<AudioSource>();
        if (soundSource == null)
            throw new NullReferenceException("Can not find Audio Source component");
        coinMesh = GetComponentInChildren<MeshRenderer>();
        if (coinMesh == null)
            throw new NullReferenceException("Can not find Mesh Renderer component");
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<PlayerScore>().Score += value;

            coinMesh.enabled = false;

            soundSource.PlayOneShot(soundSource.clip);

            GameObject effect = Instantiate(pickUpEffect, transform.position, transform.rotation);
            
            Destroy(effect, effect.GetComponent<ParticleSystem>().main.duration);
            Destroy(gameObject, soundSource.clip.length);
        }
    }
}
