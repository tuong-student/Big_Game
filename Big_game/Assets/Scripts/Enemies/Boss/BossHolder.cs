using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Base;

namespace Game.System.Enemy
{
    public class BossHolder : MonoBehaviour
    {
        [SerializeField] BaseEnemy boss;
        [SerializeField] Portal door;

        void Start()
        {
            if(door) door.Close();
        }

        void Update()
        {
            if(boss.IsAlive() == false) Destroy(this.gameObject);
        }

        void OnDestroy()
        {
            if(door)
            {
                door.Open();
            }
        }
    }
}
