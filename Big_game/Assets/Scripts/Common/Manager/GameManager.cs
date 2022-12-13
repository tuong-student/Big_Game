using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NOOD;

public class GameManager : MonoBehaviorInstance<GameManager>
{
    [SerializeField] Animator nextLevelAnim;
    public bool isEndGame = false;
    bool isAnimation = false;

    public static GameManager Create(Transform parent = null)
    {
        return Instantiate<GameManager>(Resources.Load<GameManager>("Prefabs/Manager/GameManger"), parent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameCanvas.GetInstace.CreateUpgradePanel();
        }

        if (isEndGame && !isAnimation)
        {
            isAnimation = true;
            NoodyCustomCode.StartDelayFunction(() =>
            {
                TransitionAnimation();
            }, 0.5f);
        }
    }

    public void OpenSetting()
    {
        SettingManager.Create();
    }
    
    public void TransitionAnimation()
    {
        nextLevelAnim.SetTrigger("Start");
    }
}
