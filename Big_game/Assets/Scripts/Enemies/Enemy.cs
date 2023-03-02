using System;
using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;
using Game.Player;
using Game.System.Enemy;

namespace Game.Enemy
{
    public class Enemy : BaseEnemy
    {
        [SerializeField] private float  damageCooldownThershold;
        [SerializeField] private float damageAmount = 10f;
        [SerializeField] private float chaseSpeed = 0.8f;   
        [SerializeField] private float turningTimeDelay = 0.2f;

        private float damageCooldownTimer;
        private float lastFollowTime;

        private bool dealthDamageToPlayer;

        private Vector3 playerLastTrackPos;
        private Vector3 startingPos;
        private Vector3 enemyMovementMotion;
        private Vector3 myScale;

        private Transform playerTarget;
        private PlayerScripts pScripts;
        private BaseEnemy enemyHealth;
        private EnemyBatchHandler enemyBatch;

        protected override void Awake()
        {
            base.Awake();
        }
        private void Start() {
            startingPos =  transform.position;
            lastFollowTime = Time.time;
            enemyBatch = transform.GetComponentInParent<EnemyBatchHandler>();
            enemyHealth = GetComponent<BaseEnemy>();
        }
        private void OnDisable() {

        }
        private void Update() {
            if(!playerTarget)
            {
                GameObject temp = GameObject.FindGameObjectWithTag("Player");
                if(temp)
                {
                    playerTarget = GameObject.FindWithTag("Player").transform;
                }
                else return;
                playerLastTrackPos = playerTarget.position;
        
	        }
            if(!IsAlive())
                return;

            HandleFacingDirection();
        }
        private void FixedUpdate() {
            if(!IsAlive())
                return;
            HandleChasingPlayer();
        }
        void HandleChasingPlayer()
        {
            if(HasPlayerTarget){    //! (1)
                if(!dealthDamageToPlayer){  //! (2)
                    ChasePlayer(); //! (3)
                }
                else
                {
                    if(damageCooldownTimer > Time.time) //! (4)
                        enemyMovementMotion = (startingPos - transform.position).normalized * (chaseSpeed * 2f/3f); //! (5)
                    else{
                        dealthDamageToPlayer = false; //! (6)
                        HandleChasingPlayer();  //! (7)
                    }
                }
            }
            else{
                enemyMovementMotion = (startingPos - transform.position).normalized*(chaseSpeed*2f/3f); //! (8)
                if(Vector3.Distance(transform.position, startingPos)<0.1f)  //! (9)
                    enemyMovementMotion = Vector3.zero; //! (10)
            }
            HandleMovement(enemyMovementMotion.x,enemyMovementMotion.y); //! (11)
        }
        //
        void ChasePlayer(){
            if(!playerTarget) return;

            if(Time.time - lastFollowTime > turningTimeDelay) //! (1)
            {
                playerLastTrackPos = playerTarget.position; //! (2)
                lastFollowTime =Time.time; //! (3)
            }
            if(Vector3.Distance(transform.position,playerLastTrackPos) > 0.016f) //! (4)
            {
                enemyMovementMotion = (playerLastTrackPos-transform.position).normalized * chaseSpeed; //! (5)
            }else
                enemyMovementMotion = Vector3.zero; //! (6)
        
        }
        void HandleFacingDirection()
        {
            myScale = transform.localScale; //! (1)
            if(HasPlayerTarget && !dealthDamageToPlayer) //! (2)
            {
                if (playerTarget.position.x > transform.position.x) //! (3)
                    myScale.x = Mathf.Abs(myScale.x); //! (4)
                else if(playerTarget.position.x < transform.position.x) //! (5)
                    myScale.x = -Mathf.Abs(myScale.x); //! (6)
            }
            else{
                  if (startingPos.x > transform.position.x) //! (7)
                    myScale.x = Mathf.Abs(myScale.x); //! (8)
                else if (startingPos.x < transform.position.x) //! (9)
                    myScale.x = -Mathf.Abs(myScale.x); //! (10)
            }
            transform.localScale = myScale; //!(11)
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                damageCooldownTimer = Time.time + damageCooldownThershold;
                dealthDamageToPlayer = true;
                collision.gameObject.GetComponent<BaseCharacter>().Damage(damageAmount);
            }
        }
    }
}
