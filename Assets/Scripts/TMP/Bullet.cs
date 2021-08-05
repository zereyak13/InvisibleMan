using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;

    ParticleSystem.MainModule settings;

    Material bulletMat;
    void Start()
    {
        settings = explosionEffect.transform.Find("SmokeClouds").GetComponent<ParticleSystem>().main;
        bulletMat = GetComponent<MeshRenderer>().material; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {


        settings.startColor = bulletMat.GetColor("_Color");
        GameObject explosionEffectGO = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosionEffectGO.transform.SetParent(this.gameObject.transform);
        gameObject.GetComponent<MeshRenderer>().enabled = false;


        Destroy(gameObject, 2f);
    }
}
