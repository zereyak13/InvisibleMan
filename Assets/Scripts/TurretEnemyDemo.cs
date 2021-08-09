using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.EnemyVision
{
    /// <summary>
    /// Demo script on how to link animations and use Enemy events
    /// </summary>

    [RequireComponent(typeof(EnemyVision))]
    public class TurretEnemyDemo : MonoBehaviour
    {
        public GameObject exclama_prefab;
        public GameObject death_fx_prefab;

        private EnemyVision enemy;
        private Animator animator;

        private Animator turret1Animator;
        [SerializeField] private Transform upperSideOfTurret;
        [SerializeField] private Transform firepos;
        public Brush brush;

        void Start()
        {
            turret1Animator = GetComponent<Animator>();

            animator = GetComponentInChildren<Animator>();
            enemy = GetComponent<EnemyVision>();
            enemy.onDeath += OnDeath;
            enemy.onAlert += OnAlert;
            enemy.onSeeTarget += OnSeen;
            enemy.onDetectTarget += OnDetect;
            enemy.onTouchTarget += OnTouch;

        }

        void Update()
        {
            if (animator != null && enemy.GetEnemy() != null)
            {
                animator.SetBool("Move", enemy.GetEnemy().GetMove().magnitude > 0.5f || enemy.GetEnemy().GetRotationVelocity() > 10f);
                animator.SetBool("Run", enemy.GetEnemy().IsRunning());
            }
        }

        //Can be either because seen or heard noise
        private void OnAlert(Vector3 target)
        {
            if (exclama_prefab != null)
                Instantiate(exclama_prefab, transform.position + Vector3.up * 2f, Quaternion.identity);
            if (animator != null)
                animator.SetTrigger("Surprised");
        }

        private void OnSeen(VisionTarget target, int distance)
        {
            //Add code for when target get seen and enemy get alerted, 0=touch, 1=near, 2=far, 3=other
            //Debug.Log(target.gameObject.name);
            //Debug.Log(distance);

            Vector3 moveDir = target.transform.position - upperSideOfTurret.transform.position;
            upperSideOfTurret.transform.rotation = Quaternion.Slerp(upperSideOfTurret.transform.rotation, Quaternion.LookRotation( moveDir) , 0.15f);


            //FireToPlayer();


        }

        private void OnDetect(VisionTarget target, int distance)
        {
            //Add code for when the enemy detect you as a threat (and start chasing), 0=touch, 1=near, 2=far, 3=other
            //Debug.Log(target.gameObject.name);
            //Debug.Log(distance);

           
        }

        private void OnTouch(VisionTarget target)
        {
            //Add code for when you get caughts
        }

        private void OnDeath()
        {
            if (death_fx_prefab)
                Instantiate(death_fx_prefab, transform.position + Vector3.up * 0.5f, death_fx_prefab.transform.rotation);
        }

        private float timerToFire = 1f;
        private float maxTimeToFire = 1f;


        private void FireToPlayer() //Zaten sadece player detect oluyor.
        {
            RaycastHit hit;
            if (Physics.Raycast(firepos.position, firepos.transform.forward, out hit, 1000))
            {
                timerToFire -= Time.deltaTime;

                if (timerToFire < 0)
                {
                    Debug.Log("Fired");
                    turret1Animator.SetTrigger("fire");

                    PaintTarget paintTarget = hit.collider.gameObject.GetComponent<PaintTarget>();
                    if (paintTarget != null)
                    {
                        PaintTarget.PaintObject(paintTarget, hit.point, hit.normal, brush);
                    }
                    timerToFire = maxTimeToFire;
                }
            }
        }
    }
}