using System.IO;
using System.Threading;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyShooterType
{
    Horizontal,
    Vertical,
    Stationary
}
public class EnemyShooterMovement : MonoBehaviour
{
    [SerializeField]
    private EnemyShooterType enemyType = EnemyShooterType.Horizontal;
    private float min_pos, max_pos;
    private Vector3 minPos,maxPos;
    [SerializeField]
    private float changingPos_Delay = 2f;
    private float changingPos_Timer;
    private Vector3 startPos;
    private Vector3 targetPos;
    [SerializeField]
    private float moveSpeed = 0.75f;
    private bool changedPosition;
    private Vector3 myScale;
    //
    private EnemyShooterController enemyShooterController;
    private bool playerInRange;
    [SerializeField]
    private float shootTimeDelay = 2f;
    private float shootTimer;
    private Transform playerTransform;
    [SerializeField]
    private Transform bulletSpawnPos;
    private BaseEnemy enemyHealth;
    [SerializeField]
    private EnemyBatchHandler enemyBatch;

    private void Awake() {
        startPos = transform.position;
        if(enemyType == EnemyShooterType.Horizontal){
            min_pos = transform.GetChild(0).transform.localPosition.x;
            max_pos = transform.GetChild(1).transform.localPosition.x;
        }
        else if(enemyType == EnemyShooterType.Vertical){
            min_pos = transform.GetChild(0).transform.localPosition.y;
            max_pos = transform.GetChild(1).transform.localPosition.y;
        }
        else
        {
            minPos = transform.GetChild(0).transform.position;
            maxPos = transform.GetChild(1).transform.position;
            transform.position = maxPos;
        }
        changingPos_Timer = Time.time + changingPos_Delay;
        enemyShooterController = GetComponent<EnemyShooterController>();
        enemyHealth = GetComponent<BaseEnemy>();
    }   
    private void OnDisable() {
        if(!enemyHealth.IsAlive())
            enemyBatch.RemoveShooterEnemy(this);
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
    void EnemyMovement()
    {
        if(enemyType == EnemyShooterType.Horizontal){
            if(!changedPosition){
                float xPos = Random.Range(min_pos,max_pos);
                targetPos = startPos + Vector3.right*xPos;
                changedPosition = true;
            }
        }
        else if(enemyType == EnemyShooterType.Vertical){
            if(!changedPosition){
                float yPos = Random.Range(min_pos,max_pos);
                targetPos = startPos + Vector3.up*yPos;
                changedPosition = true;
            }
        }
        else
        {
            if(!changedPosition)
            {
            targetPos = maxPos == targetPos ? minPos : maxPos;
            changedPosition = true;
            }
        }
        if(Vector3.Distance(transform.position,targetPos) <= 0.05f)
        {
            if(changingPos_Timer < Time.time)
            {
                changedPosition = false;
                changingPos_Timer = Time.time + changingPos_Delay;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position,
            targetPos,Time.deltaTime*moveSpeed);
        HandleFacingDirection();
    }
    void HandleFacingDirection(){
        myScale = transform.localScale;
        if(targetPos.x > transform.position.x)
            myScale.x = Mathf.Abs(myScale.x);
        if(targetPos.x < transform.position.x)
            myScale.x = -Mathf.Abs(myScale.x);
        transform.localScale = myScale;
    }
    void CheckToShoot(){
        if(playerInRange){
            if(Time.time > shootTimer){
                shootTimer = Time.time + shootTimeDelay;
                Vector2 direction = (playerTransform.position - bulletSpawnPos.position).normalized;
                enemyShooterController.Shoot(direction,bulletSpawnPos.position);
            }
        }
    }
    public void SetPlayerInRange(bool inRange){
        playerInRange = inRange;
    }
}


