using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

namespace Game.Enemy
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private BaseEnemy baseEnemy;

        // Update is called once per frame
        void Update()
        {
            EnemyMovementAnimation();
        }

        void EnemyMovementAnimation(){
            if(baseEnemy.GetMoveDelta().magnitude > 0f){
                anim.SetBool("Walk",true);
            }else
                anim.SetBool("Walk",false);
        }
        public void DeathAnimation(){
            anim.SetTrigger("Death");
            NOOD.NoodyCustomCode.StartDelayFunction(baseEnemy.DestroySelf, anim.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
