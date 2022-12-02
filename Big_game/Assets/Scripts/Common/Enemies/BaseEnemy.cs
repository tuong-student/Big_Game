using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    PlayerScripts playerScripts;
    float gold = 10f;
    private Vector3 moveDelta;
    private RaycastHit2D movementHit;
    private BoxCollider2D myCollider;

    private Vector2 velocity;
    private bool _hasPlayerTarget;
    public bool HasPlayerTarget{
        get {return _hasPlayerTarget; }
        set{_hasPlayerTarget = value;}

    }
    protected virtual void  Awake() {
        myCollider = GetComponent<BoxCollider2D>();
        walkSpeed =1;
    }

    public void publicAwake(){
        Awake();
    }

    public virtual void HandleMovement(float x, float y){
        moveDelta = new Vector3(x*walkSpeed, y*walkSpeed,0f);   //! (1)
        
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
        transform.Translate(moveDelta*Time.deltaTime);
    }
    
    public void AddGold()
    {
        GoldManager.i.AddGold(gold);
    }
    
    public override void Die()
    {
        AddGold();
        base.Die();
    }
}

public enum EnemyType
{
    enemy1,
    enemy2
}