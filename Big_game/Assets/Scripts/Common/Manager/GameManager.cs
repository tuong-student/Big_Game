using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Game.UI;
using NOOD;
using Game.Save;

namespace Game.Manager
{
    public class GameManager : MonoBehaviorInstance<GameManager>
    {
        [SerializeField] Animator nextLevelAnim;
        public bool isEndGame = false;
        bool isAnimation = false;
        private int gold = 0;
        private int level = 1;
        public int CurrentLevel { get { return level; } }

        private SaveModels.GameSystemModel gameSystemModel;

        public static GameManager Create(Transform parent = null)
        {
            return Instantiate<GameManager>(Resources.Load<GameManager>("Prefabs/Manager/GameManger"), parent);
        }

        public void StartGame()
        {
            EventManager.GetInstance.OnStartGame.OnEventRaise?.Invoke();
        }

        private void Awake()
        {
            LoadFromSave();
        }

        private void Start()
        {
            EventManager.GetInstance.OnGenerateLevelComplete.OnEventRaise += (int number) =>
            {
                level = number;
            };
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Tab))
            //{
            //    GameCanvas.GetInstance.CreateUpgradePanel();
            //}

            //if (isEndGame && !isAnimation)
            //{
            //    isAnimation = true;
            //    NoodyCustomCode.StartDelayFunction(TransitionAnimation, 0.5f);
            //}
        }

        private void Save()
        {
            gameSystemModel.gold = this.gold;
            gameSystemModel.level = this.level;

            SaveJson.SaveToJson(gameSystemModel, SaveModels.SaveFile.GameSystemSave.ToString());
        }

        private void LoadFromSave()
        {
            gameSystemModel = LoadJson<SaveModels.GameSystemModel>.LoadFromJson(SaveModels.SaveFile.GameSystemSave.ToString());
            if(gameSystemModel == null)
            {
                gameSystemModel = new SaveModels.GameSystemModel();
                gameSystemModel.gold = this.gold;
                gameSystemModel.level = this.level;
                Save();
            }
            else
            {
                this.gold = gameSystemModel.gold;
                this.level = gameSystemModel.level;
            }
        }

        public bool MinusGold(int amount)
        {
            //if(LocalDataManager.gold >= amount)
            //{
            //    LocalDataManager.gold -= amount;
            //    LocalDataManager.SaveGold();
            //    InGameUI.GetInstance.ResetGoldText();
            //    return true;    
            //}
            //else
            //{
            //    return false;
            //}
            return false;
        }

        public void AddGold(int amount)
        {
            //LocalDataManager.gold += amount;
            //LocalDataManager.SaveGold();
            //InGameUI.GetInstance.ResetGoldText();
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
}
