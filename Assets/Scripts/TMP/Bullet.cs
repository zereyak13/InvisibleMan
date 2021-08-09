using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieMarc.EnemyVision;
public class Bullet : MonoBehaviour
{
    //public Brush brush;
    //public bool RandomChannel = false;
    //private bool onlyOnce = false;

    [SerializeField] GameObject explosionEffect;

    ParticleSystem.MainModule settings;

    Material bulletMat;
    
    private void Awake()
    {
        settings = explosionEffect.transform.Find("SmokeClouds").GetComponent<ParticleSystem>().main;
        bulletMat = GetComponent<MeshRenderer>().material;
    }
    void Start()
    {
        SetBulletColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        settings.startColor = bulletMat.GetColor("_Color");
        GameObject explosionEffectGO = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        //explosionEffectGO.transform.SetParent(this.gameObject.transform);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //Playere çarparsa görülebilir yap.
        if (collision.gameObject.CompareTag(Defs.Instance.playerTag))
        {
            GameObject.FindGameObjectWithTag(Defs.Instance.playerTag).gameObject.GetComponent<VisionTarget>().visible = true;
        }
        

        Destroy(gameObject, 2f);
    }


    public void SetBulletColor()
    {
        int colorIndex = Random.Range(0, 4);
        switch (colorIndex)
        {
            case 0:
                GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1.0f, 0.64f, 0.0f)); 
                break;
            case 1:
                GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                break;
            case 2:
                GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                break;
            case 3:
                GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
                break;
        }

        GetComponent<CollisionPainter>().brush.splatChannel = colorIndex;
    }
}
