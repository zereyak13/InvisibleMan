using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Brush brush;
    public bool RandomChannel = false;
    private bool onlyOnce = false;

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
        //Raycast
        //if (onlyOnce)
        //{
        //    PaintTarget paintTarget = hit.collider.gameObject.GetComponent<PaintTarget>();
        //    if (!paintTarget) return;
        //    PaintObject(paintTarget, hit.point, hit.normal, brush);
        //    onlyOnce = true;


        //Collider
        //    ContactPoint contact = collision.contacts[0];
        //    PaintTarget paintTarget = contact.otherCollider.GetComponent<PaintTarget>();
        //    if (paintTarget != null)
        //    {
        //        if (RandomChannel) brush.splatChannel = Random.Range(0, 4);
        //        PaintTarget.PaintObject(paintTarget, contact.point, contact.normal, brush);
        //    }
        //}


    }
    private void OnCollisionStay(Collision collision)
    {
        if (onlyOnce)
        {
            //PaintTarget paintTarget = hit.collider.gameObject.GetComponent<PaintTarget>();
            //if (!paintTarget) return;
            //PaintObject(paintTarget, hit.point, hit.normal, brush);


            ContactPoint contact = collision.contacts[0];
            PaintTarget paintTarget = contact.otherCollider.GetComponent<PaintTarget>();
            if (paintTarget != null)
            {
                if (RandomChannel) brush.splatChannel = Random.Range(0, 4);
                PaintTarget.PaintObject(paintTarget, contact.point, contact.normal, brush);
            }
            onlyOnce = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        settings.startColor = bulletMat.GetColor("_Color");
        GameObject explosionEffectGO = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosionEffectGO.transform.SetParent(this.gameObject.transform);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        //
        if (onlyOnce)
        {
            //PaintTarget paintTarget = hit.collider.gameObject.GetComponent<PaintTarget>();
            //if (!paintTarget) return;
            //PaintObject(paintTarget, hit.point, hit.normal, brush);
     

            ContactPoint contact = collision.contacts[0];
            PaintTarget paintTarget = contact.otherCollider.GetComponent<PaintTarget>();
            if (paintTarget != null)
            {
                if (RandomChannel) brush.splatChannel = Random.Range(0, 4);
                PaintTarget.PaintObject(paintTarget, contact.point, contact.normal, brush);
            }
            onlyOnce = true;
        }


        Destroy(gameObject, 2f);
    }
}
