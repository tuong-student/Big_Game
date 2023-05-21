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
using Game.Common.Interface;

namespace Game.Common.Manager
{
    public class GameManager : AbstractMonoBehaviour, ISingleton
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

        private EventManager eventManager;

        public static GameManager Create(Transform parent = null)
        {
            return Instantiate<GameManager>(Resources.Load<GameManager>("Prefabs/Manager/GameManger"), parent);
        }

        private void Awake()
        {
            RegisterToContainer();

            gameSystemModel = LoadJson<SaveModels.GameSystemModel>.LoadFromJson(SaveModels.SaveFile.GameSystemSave.ToString());
            if(gameSystemModel == null || gameSystemModel.playerNum == 0)
            {
                // No save before
                gameSystemModel = new SaveModels.GameSystemModel();
                // HideChooseCharacterMenu had already subscribed to OnContinueGame event
            }
            else
            {
                // Has save file already
                CreateOrChangePlayerWithNewSave();
            }
        }

        private void Start()
        {
            eventManager = SingletonContainer.Resolve<EventManager>();
            SingletonContainer.Resolve<GameCanvas>().ActiveChooseCharacterMenu();
            eventManager.OnGenerateLevel.OnEventRaise += Save;

            eventManager.OnGenerateLevelComplete.OnEventRaise += (int number) =>
            {
                level = number;
            };
            eventManager.OnTryAgain.OnEventRaise += CreateOrChangePlayerWithNewSave;
        }

        private void Update()
        {

        }

        public void RestartGame()
        {

        }

        private void OnDisable()
        {
            Dispose();
        }

        protected override void Dispose()
        {
            Clear();
            OnGoldChange = null;
        }

        public SaveModels.GameSystemModel GetGameSystemModel()
        {
            return gameSystemModel;
        }

        public void CreateOrChangePlayerWithNewSave()
        {
            LoadFromSave(); // Load again because after choosing player, the save file has been change
            PlayerScripts currentPlayer = GameObject.FindObjectOfType<PlayerScripts>(true);
            if (currentPlayer == null)
            {
                // Player is not created
                // Create player
                // Change player image (do in player script)
                // Reset player position (do in player script)
                // Active OnContinueGame in EventManager
                currentPlayer = PlayerScripts.Create();
                currentPlayer.ChangePlayerVisualWithPlayerNum(gameSystemModel.playerNum);
                currentPlayer.ResetPosition(0); // This number will not use
                GameObject.FindObjectOfType<EventManager>().GetComponent<EventManager>().OnContinueGame.RaiseEvent();
            }
            else
            {
                // Player is exit
                // Change player image
                currentPlayer.gameObject.SetActive(true);
                currentPlayer.Revive();
                currentPlayer.ChangePlayerVisualWithPlayerNum(gameSystemModel.playerNum);
                GameObject.FindObjectOfType<EventManager>().GetComponent<EventManager>().OnContinueGame.RaiseEvent();
            }
            currentPlayer.OnPlayerDead += () => isEndGame = true;
            this.isEndGame = false;
        }

        private void Save()
        {
            gameSystemModel.gold = this.gold;
            gameSystemModel.level = this.level;

        }

        private void LoadFromSave()
        {
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

        public void RegisterToContainer()
        {
            SingletonContainer.Register(this);
        }

        public void UnregisterToContainer()
        {
            SingletonContainer.UnRegister(this);
        }
    }
}
