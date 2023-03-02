using System.IO;
using System.Threading;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System.Enemy;
using Game.Base;

namespace Game.Enemy
{
    public enum EnemyShooterType
    {
        Horizontal,
        Vertical,
        Stationary
    }
    public class EnemyShooter : MonoBehaviour
    {
        [SerializeField] private EnemyShooterType enemyType = EnemyShooterType.Horizontal;
        [SerializeField] private float changingPos_Delay = 2f;
        [SerializeField] private float moveSpeed = 0.75f;

        private Vector3 startPos;
        private Vector3 targetPos;
        private Vector3 myScale;

        private Vector3 pos_1, pos_2;
        private float changingPos_Timer;
        private bool changedPosition;
        
        [SerializeField] private float shootTimeDelay = 2f;
        private float shootTimer;

        private EnemyShooterController enemyShooterController;
        private bool playerInRange;

        [SerializeField] private Transform bulletSpawnPos;
        private Transform playerTransform;
        private BaseEnemy enemyHealth;

        [SerializeField] private EnemyBatchHandler enemyBatch;
        private EnemySpawner enemySpawner;

        private void Awake() {
            enemyShooterController = GetComponent<EnemyShooterController>();
            enemySpawner = GetComponentInParent<EnemySpawner>();
            enemyBatch = GetComponentInParent<EnemyBatchHandler>();
            enemyHealth = GetComponent<BaseEnemy>();
        }

        private void Start()
        {
            startPos = this.transform.localPosition;

            pos_1 = enemySpawner.GetRandomPosition();
            pos_2 = enemySpawner.GetRandomPosition();
            
            changingPos_Timer = Time.time + changingPos_Delay;
            
        }

        private void OnDisable() {

        }

        private void Update() {
            if(!playerTransform){
                GameObject temp =  GameObject.FindGameObjectWithTag("Player");
                if(!temp) return;
                else playerTransform = temp.transform;
            }
            if(!enemyHealth.IsAlive())
                return;
            if(!playerTransform)
                return;
            CheckToShoot();
        }

        private void FixedUpdate() {
            if(!enemyHealth.IsAlive())
                return;
            if(!playerTransform)
                return;
            EnemyMovement();   
        }

        private void EnemyMovement()
        {
            if(enemyType == EnemyShooterType.Horizontal){
                if(!changedPosition){
                    float xPos = Random.Range(pos_1.x, pos_2.x);
                    targetPos = new Vector3(xPos, startPos.y, startPos.z);
                    changedPosition = true;
                }
            }
            else if(enemyType == EnemyShooterType.Vertical){
                if(!changedPosition){
                    float yPos = Random.Range(pos_1.y, pos_2.y);
                    targetPos = new Vector3(startPos.x, yPos, startPos.z);
                    changedPosition = true;
                }
            }
            else
            {
                if(!changedPosition)
                {
                    targetPos = pos_1 == targetPos ? pos_2 : pos_1;
                    changedPosition = true;
                }
            }

            if(Vector3.Distance(this.transform.position, targetPos) <= 0.05f)
            {
                if(changingPos_Timer < Time.time)
                {
                    changedPosition = false;
                    changingPos_Timer = Time.time + changingPos_Delay;
                    pos_1 = enemySpawner.GetRandomPosition();
                    pos_2 = enemySpawner.GetRandomPosition();
                }
            }

            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, Time.deltaTime * moveSpeed);
            HandleFacingDirection();
        }

        private void HandleFacingDirection(){
            myScale = transform.localScale;
            if(targetPos.x > transform.position.x)
                myScale.x = Mathf.Abs(myScale.x);
            if(targetPos.x < transform.position.x)
                myScale.x = -Mathf.Abs(myScale.x);
            transform.localScale = myScale;
        }

        private void CheckToShoot(){
            if(playerInRange){
                if(Time.time > shootTimer){
                    shootTimer = Time.time + shootTimeDelay;
                    Vector2 direction = (playerTransform.position - bulletSpawnPos.position).normalized;
                    enemyShooterController.Shoot(direction, bulletSpawnPos.position);
                }
            }
        }

        public void SetPlayerInRange(bool inRange){
            playerInRange = inRange;
        }
    }
}
