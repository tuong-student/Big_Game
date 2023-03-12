using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Game.UI;
using NOOD;
using Game.Save;
using Game.Player;

namespace Game.Common.Manager
{
    public class GameManager : MonoBehaviorInstance<GameManager>
    {
        #region Event
        public EventHandler<int> OnGoldChange;

        #endregion

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

        public void CreatePlayerIfNeed()
        {
            LoadFromSave(); // Load again because after choosing player, the save file has been change
            PlayerScripts currentPlayer = GameObject.FindObjectOfType<PlayerScripts>();
            if(currentPlayer == null)
            {
                currentPlayer = PlayerScripts.Create();
            }
            currentPlayer.ChangePlayerVisualWithPlayerNum(gameSystemModel.playerNum);
            currentPlayer.ResetPosition(0); // number in ResetPostion(int number) is not be used
            EventManager.GetInstance.OnContinuewGame.RaiseEvent();
        }

        private void Awake()
        {
            LoadFromSave();
            if(gameSystemModel == null || gameSystemModel.playerNum == 0)
            {
                GameCanvas.GetInstance.ActiveChooseCharacterMenu();
                // HideChooseCharacterMenu is subcribe to OnContinueGame event
            }
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
            gold -= amount;
            if (gold < amount)
            {
                return false;
            }
            OnGoldChange?.Invoke(this, gold);
            return true;
        }

        public void AddGold(int amount)
        {
            gold += amount;
            OnGoldChange?.Invoke(this, gold);
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
