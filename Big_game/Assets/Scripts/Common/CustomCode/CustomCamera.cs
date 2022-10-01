using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    #region Components
    #endregion

    #region Stats
    [SerializeField] float duration, magnitude;
    [SerializeField] float explodeMagnitude;
    [SerializeField] float smoothTime;
    [SerializeField] string targetTag;

    [SerializeField] Vector3 offset;

    [SerializeField] bool isFollow;
    [SerializeField] bool isShake;
    [SerializeField] bool isHeavyShake;
    #endregion

    public static CustomCamera InsCustomCamera;

    void Awake()
    {
        if(InsCustomCamera == null) InsCustomCamera = this;
    }

    private void Update()
    {
        if (isShake) Shake();
        if (isHeavyShake) HeaveShake();
    }

    private void LateUpdate()
    {
        if (isFollow) FollowPlayer();
    }

    void FollowPlayer()
    {
        NOOD.NoodyCustomCode.SmoothCameraFollow(Camera.main.gameObject, smoothTime, GameObject.FindGameObjectWithTag(targetTag).transform, offset);
    }
    
    public void Shake(){
        StartCoroutine(NOOD.NoodyCustomCode.ObjectShake(this.gameObject, duration, magnitude));
    }

    public void HeaveShake(){
        StartCoroutine(NOOD.NoodyCustomCode.ObjectShake(this.gameObject, duration, explodeMagnitude));
    }
}
