using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;
using Game.System.Enemy;

namespace Game.Enemy
{
    public class BossMovement : MonoBehaviour
    {
        [SerializeField] private Transform[] movementPos;
        [SerializeField] private float normalSpeed = 0.5f, chasingSpeed = 1f;
        [SerializeField] private float damageAmount = 10f;
        [SerializeField] private float shootTimeDelay = 2f;

        private Vector3 myScale;
        private Vector3 targetPos;
        private Transform playerTarget;

        private BaseEnemy bossHealth;
        private EnemyShooterController enemyShooterController;

        private float speed;
        private float shootTimer;
        private bool isDetected;
        private bool chasePlayer;

        private void Awake()
        {
            bossHealth = GetComponent<BaseEnemy>();
            enemyShooterController = GetComponent<EnemyShooterController>();
        }

        private void Start()
        {
            speed = normalSpeed;
            GetRandMovementPos();
        }

        private void Update()
        {
            if(!playerTarget){
                GameObject temp =  GameObject.FindGameObjectWithTag("Player");
                if(!temp) return;
                else playerTarget = temp.transform;
            }
            if (!playerTarget)
                return;

            if (!bossHealth.IsAlive())
                return;

            HandleFacing();
            HandleShooting();
        }

        private void FixedUpdate()
        {
            if (!playerTarget)
                return;

            if (!bossHealth.IsAlive())
                return;

            HandleMovement();
        }

        private void GetRandMovementPos()
        {
            int randIndex = Random.Range(0, movementPos.Length);

            while (targetPos == movementPos[randIndex].position)
            {
                randIndex = Random.Range(0, movementPos.Length);
            }

            targetPos = movementPos[randIndex].position;
        }

        private void HandleMovement()
        {
            if (!isDetected) return;

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                if (Random.Range(0, 10) > 5)
                {
                    targetPos = playerTarget.position;
                    chasePlayer = true;
                }
                else
                {
                    if (!chasePlayer)
                    {
                        GetRandMovementPos();
                    }
                }
            }
        }

        private void HandleFacing()
        {
            myScale = transform.localScale;
            if (targetPos.x > transform.position.x)
                myScale.x = Mathf.Abs(myScale.x);
            else if (targetPos.x < transform.position.x)
                myScale.x = -Mathf.Abs(myScale.x);
            transform.localScale = myScale;
        }

        private void SetSpeedState(bool detected)
        {
            if (detected)
                speed = chasingSpeed;
            else
                speed = normalSpeed;
        }

        public void PlayerDetected(bool detected)
        {
            isDetected = detected;
            SetSpeedState(detected);
            if (!isDetected)
            {
                chasePlayer = false;
                GetRandMovementPos();
            }
        }

        private void HandleShooting()
        {
            if (isDetected)
            {
                if (Time.time > shootTimer)
                {
                    shootTimer = Time.time + shootTimeDelay;
                    Vector2 direction = (playerTarget.position - transform.position).normalized;
                    enemyShooterController.ShootOnRandom(direction, transform.position);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                chasePlayer = false;
                GetRandMovementPos();
                other.GetComponent<Game.Player.PlayerScripts>().Damage(damageAmount);
            }
        }
    }
}
