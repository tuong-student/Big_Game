using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Enemy;

namespace Game.System.Enemy
{
    public enum EnemyTargetType{
        EnableEnemyTarget,
        DisableEnemyTarget
    }

    public class EnenmyTargetController : MonoBehaviour
    {
        private readonly string PLAYER_TAG = "Player";
        [SerializeField] private BossMovement boss;

        private void Start()
        {
            boss.PlayerDetected(false);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(other.CompareTag(PLAYER_TAG))
            {
                if(boss)
                    boss.PlayerDetected(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(PLAYER_TAG))
            {
                if (boss)
                    boss.PlayerDetected(false);
            }
        }
    }
}
