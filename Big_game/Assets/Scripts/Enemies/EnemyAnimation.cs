using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;
    private BaseEnemy enemyMovement;
    private void Awake() {
        anim = GetComponent<Animator>();
        enemyMovement =GetComponent<BaseEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovementAnimation();
    }

    void EnemyMovementAnimation(){
        if(enemyMovement.GetMoveDelta().magnitude > 0f){
            anim.SetBool("Walk",true);
        }else
            anim.SetBool("Walk",false);
    }
    public void DeathAnimation(){
        anim.SetTrigger("Death"); 
    }
}
