using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject explosionEffectGO = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosionEffectGO.transform.SetParent(this.gameObject.transform);
        gameObject.GetComponent<MeshRenderer>().enabled = false;


        Destroy(gameObject, 2f);
    }
}
