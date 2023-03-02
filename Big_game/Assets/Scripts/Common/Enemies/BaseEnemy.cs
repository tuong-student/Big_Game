using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;
using Game.Player;
using Game.System.Enemy;
using Game.Enemy;

namespace Game.Base
{
    public class BaseEnemy : MonoBehaviour
    {
        private PlayerScripts playerScripts;
        private int gold = 10;
        private Vector3 moveDelta;
        private RaycastHit2D movementHit;
        private BoxCollider2D myCollider;
        private Animator anim;
        private EnemyBatchHandler enemyBatchHandler;
        private bool isDeadAnimationPlayed = false;

        [SerializeField] private EnemyAnimation enemyAnimation;
        [SerializeField] private float maxHealth = 100f ;
        [SerializeField] protected float health;
        [SerializeField] private Vector2 velocity;

        private bool _hasPlayerTarget;
        public bool HasPlayerTarget{ get { return _hasPlayerTarget; } set { _hasPlayerTarget = value; } }

        protected virtual void Awake() {
            enemyBatchHandler = GetComponentInParent<EnemyBatchHandler>();
            myCollider = GetComponent<BoxCollider2D>();

            myCollider.enabled = true;
            health = maxHealth;
        }

        public void publicAwake(){
            Awake();
        }

        private void OnDestroy()
        {
            if(enemyBatchHandler != null)
                enemyBatchHandler.CheckUnlockDoor();
        }

        public virtual void HandleMovement(float x, float y){
            moveDelta = new Vector3(x, y,0f);   //! (1)
        
            movementHit = Physics2D.BoxCast(transform.position,myCollider.size,0f,
            new Vector2(0f,moveDelta.y),Mathf.Abs(moveDelta.y*Time.deltaTime),  
            LayerMask.GetMask("Blocking")); //! (2)

            if(!movementHit.collider)   //! (3)
                transform.Translate(0f,moveDelta.y*Time.deltaTime,0f);  //! (4)
        
            movementHit = Physics2D.BoxCast(transform.position,myCollider.size,0f,
            new Vector2(moveDelta.x,0f),Mathf.Abs(moveDelta.y*Time.deltaTime),
            LayerMask.GetMask("Blocking"));   //! (5)

            if(!movementHit.collider)   //! (6)
                transform.Translate(moveDelta.x*Time.deltaTime,0f,0f);  //! (7)
         
        }

        public Vector3 GetMoveDelta(){
            return moveDelta;
        }

        private void FixedUpdate() {
            transform.Translate(moveDelta * Time.deltaTime);
        }
    
        public void AddGold()
        {
            // Add Gold
            GameManager.GetInstance.AddGold(gold);
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if(!IsAlive() && isDeadAnimationPlayed == false)
            {
                Die();
            }
        }

        protected void Die()
        {
            isDeadAnimationPlayed = true;
            enemyAnimation.DeathAnimation();
            myCollider.enabled = false;
            AddGold();
        }

        public void DestroySelf()
        {
            Destroy(this.gameObject);
        }

        public bool IsAlive()
        {
            return health > 0 ? true:false; 
        }
    }

    public enum EnemyType
    {
        enemy1,
        enemy2
    }
}