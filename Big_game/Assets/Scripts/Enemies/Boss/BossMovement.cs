using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField]
    private float normalSpeed = 0.5f, chasingSpeed = 1f;
    private float speed;
    [SerializeField]
    private Transform[] movementPos;
    private Vector3 targetPos;
    private Vector3 myScale;
    private bool isDetected;
    private Transform playerTarget;
    private bool chasePlayer;
    [SerializeField]
    private float damageAmount = 10f;
    private BaseEnemy bossHealth;
    [SerializeField]
    private float shootTimeDelay = 2f;
    private float shootTimer;
    private EnemyShooterController enemyShooterController;

    private void Awake()
    {
        bossHealth = GetComponent<BaseEnemy>();
        enemyShooterController = GetComponent<EnemyShooterController>();
    }

    private void Start()
    {
        speed = normalSpeed;
        GetRandMovementPos();
        playerTarget = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
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
    void GetRandMovementPos()
    {
        int randIndex = Random.Range(0, movementPos.Length);
        while (targetPos == movementPos[randIndex].position)
        {
            randIndex = Random.Range(0, movementPos.Length);
        }
        targetPos = movementPos[randIndex].position;
    }
    void HandleMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            targetPos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            if (isDetected)
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
    }
    void HandleFacing()
    {
        myScale = transform.localScale;
        if (targetPos.x > transform.position.x)
            myScale.x = Mathf.Abs(myScale.x);
        else if (targetPos.x < transform.position.x)
            myScale.x = -Mathf.Abs(myScale.x);
        transform.localScale = myScale;
    }
    void SetSpeedState(bool detected)
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
    void HandleShooting()
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
            other.GetComponent<BaseCharacter>().Damage(damageAmount);
        }
    }
}
