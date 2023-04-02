using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetObjectDeactivate : MonoBehaviour
{

    #region Bool
    public bool whenCollisionEnter;
    public bool afterSecond;
    #endregion

    #region float
    public float second = 1;
    #endregion

    private void OnEnable()
    {
        if (afterSecond)
            StartCoroutine(DelayCall());
    }

    IEnumerator DelayCall()
    {
        yield return new WaitForSeconds(second);
        Deactive();
    }

    void Deactive()
    {
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (whenCollisionEnter)
        {
            Deactive();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (whenCollisionEnter)
        {
            Deactive();
        }
    }
}
