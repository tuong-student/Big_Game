using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : AbstractMonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            LevelManager.GetInstace.NextLevel();
        }
    }
}
