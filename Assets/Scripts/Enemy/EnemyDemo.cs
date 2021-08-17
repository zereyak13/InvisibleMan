using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.EnemyVision
{
    /// <summary>
    /// Demo script on how to link animations and use Enemy events
    /// </summary>

    [RequireComponent(typeof(EnemyVision))]
    public class EnemyDemo : MonoBehaviour
    {
        public GameObject exclama_prefab;
        public GameObject death_fx_prefab;

        private EnemyVision enemy;
        private Animator animator;

        private float distanceForTarget = 4f;
        private bool detectedTarget;
        private GameObject targetGO;

        [SerializeField] private Transform firePos;
        private float timerToFire = 1.11f;
        private float maxTimeToFire = 1.11f;

        void Start()
        {
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


            

            if (detectedTarget)
            {
                Fire();
                if ((targetGO.transform.position - transform.position).magnitude < distanceForTarget *2)
                {
                    animator.SetBool("Fire", true);
                                      
                }
                else
                {
                    animator.SetBool("Fire", false);
                    detectedTarget = false;
                }
            }

            
        }

        private void Fire()
        {
            timerToFire -= Time.deltaTime;

            RaycastHit hit;
            if (Physics.Raycast(firePos.position, -firePos.transform.up, out hit, 1000))
            {
                if (timerToFire < 0)
                {
                    if (hit.collider.transform.parent.CompareTag(Defs.Instance.playerTag))
                    {
                        Debug.Log("Fired");
                        timerToFire = maxTimeToFire;
                        //Healh --
                        HealthController.Instance.HealtDecrease();

                        /*
                        PaintTarget paintTarget = hit.collider.gameObject.GetComponent<PaintTarget>();
                        if (paintTarget != null)
                        {
                            //GameObject explosionEffectGO = Instantiate(explosionEffect, hit.collider.transform.position, Quaternion.identity);
                            //settings = explosionEffectGO.transform.Find("SmokeClouds").GetComponent<ParticleSystem>().main;
                            //brush.splatChannel = Random.RandomRange
                            //PaintTarget.PaintObject(paintTarget, hit.point, hit.normal, brush);
                        }
                        */
                    }
                }            
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

            detectedTarget = true;
            targetGO = target.gameObject;
            Debug.Log(detectedTarget);
        }

        private void OnDetect(VisionTarget target, int distance)
        {
            //Add code for when the enemy detect you as a threat (and start chasing), 0=touch, 1=near, 2=far, 3=other

            detectedTarget = true;
            targetGO = target.gameObject;
            Debug.Log(detectedTarget);
        }

        private void OnTouch(VisionTarget target)
        {
            //Add code for when you get caughts
            //Game Over
        }

        private void OnDeath()
        {
            if(death_fx_prefab)
                Instantiate(death_fx_prefab, transform.position + Vector3.up * 0.5f, death_fx_prefab.transform.rotation);
        }
    }
}
