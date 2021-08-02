using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //Turret Fire Var
    [SerializeField] private GameObject bulletPrefab;
   
    private Animator Turret2Animator;
    private float timerToFire = 0.4f , maxTimeToFire =1f;

    //Turret Rotation Var
    [SerializeField] private Transform upperSideOfTurret;
    [SerializeField] private Transform firePos;

    private bool turretTurningRight = true;

    private const float ROTATION_LIMIT = .25f;// Quaternion 0-1 arası
    private const float ROTATE_SPEED = 20;
    void Start()
    {
        Turret2Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateTurret();
        FireModeActive();
        
    }

    private void RotateTurret()
    {
        if (turretTurningRight)
        {
            upperSideOfTurret.transform.Rotate(Vector3.forward * ROTATE_SPEED * Time.deltaTime);
            if (upperSideOfTurret.transform.rotation.z > ROTATION_LIMIT)
            {
                turretTurningRight = false;
            }
        }       
        else
        {
            upperSideOfTurret.transform.Rotate(-Vector3.forward * ROTATE_SPEED * Time.deltaTime);
            if (upperSideOfTurret.transform.rotation.z < -ROTATION_LIMIT)
            {
                turretTurningRight = true;
            }
        }
    }

    private void FireModeActive()
    {
        timerToFire -= Time.deltaTime;

        if (timerToFire <0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePos.position , Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(firePos.forward * 10, ForceMode.Impulse);

            Turret2Animator.SetTrigger("fire");

            timerToFire = maxTimeToFire;
        }

    }
}
