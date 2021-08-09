using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //Turret Fire Var
    [SerializeField] private GameObject bulletPrefab;
   
    private Animator Turret2Animator;
    private float timerToFire = 0.4f;

    //Turret Rotation Var
    [SerializeField] private Transform upperSideOfTurret;
    [SerializeField] private Transform firePos;

    private float timerToRotate;

    private bool rotateDir;

    private const float ROTATE_SPEED = 20;
    //Options for Designer
    [Header("Turret Options")]
    [SerializeField] private float rotateDuration = 3f;
    [SerializeField] private float TurretPower = 10;
    [SerializeField] private float maxTimeToFire;

    void Start()
    {
        Turret2Animator = GetComponent<Animator>();
        timerToRotate = rotateDuration / 2;
    }

    // Update is called once per frame
    void Update()
    {
        RotateTurret();
        FireModeActive();
        
    }

    private void RotateTurret()
    {
        timerToRotate -= Time.deltaTime;

        if(timerToRotate < 0)
        {
            timerToRotate = rotateDuration;
            rotateDir = !rotateDir;
        }

        if (rotateDir)
        {
            upperSideOfTurret.transform.Rotate(Vector3.forward * ROTATE_SPEED * Time.deltaTime);
        }
        else
        {
            upperSideOfTurret.transform.Rotate(-Vector3.forward * ROTATE_SPEED * Time.deltaTime);
        }
    }

    private void FireModeActive()
    {
        timerToFire -= Time.deltaTime;

        if (timerToFire <0)
        {
            //Color curColor = new Color(
            //                       Random.Range(0f, 1f),
            //                       Random.Range(0f, 1f),
            //                       Random.Range(0f, 1f)
            //                       );
            //orange =0, red =1, green =2, blue =3
            GameObject bullet = Instantiate(bulletPrefab, firePos.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody>().AddForce(firePos.forward * TurretPower, ForceMode.Impulse);

            Turret2Animator.SetTrigger("fire");

            timerToFire = maxTimeToFire;
        }

    }
}
