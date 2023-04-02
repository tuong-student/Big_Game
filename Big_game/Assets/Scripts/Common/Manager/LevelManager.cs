using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using NOOD;
using Game.System;
using Game.Common.Interface;

namespace Game.Common.Manager
{
    public class LevelManager : MonoBehaviour, ISingleton
    {
        [SerializeField] private List<GameObject> levels;
        [SerializeField] private GameObject mainMenuDungeon;
        private List<GameObject> activeLevels = new List<GameObject>();
        private bool isFirstTime = true;
        private Portal levelPortal;
        private GameManager gameManager;
        private EventManager eventManager;

        public static LevelManager Create(Transform parent = null)
        {
            return Instantiate<LevelManager>(Resources.Load<LevelManager>("Prefabs/Manager/LevelManager"), parent);
        }

        void Awake()
        {
            RegisterToContainer();
        }

        private void Start()
        {
            eventManager = SingletonContainer.Resolve<EventManager>();
            gameManager = SingletonContainer.Resolve<GameManager>();
            mainMenuDungeon = GameObject.Find("Main Menu Dungeon");
            if(mainMenuDungeon == null)
                mainMenuDungeon = Instantiate(mainMenuDungeon);
            ActiveMainMenuLevel();

            eventManager.OnStartGame.OnEventRaise += LoadCurrentLevel;
            eventManager.OnStartGame.OnEventRaise += DeactivateMainMenuLevel;
            eventManager.OnGenerateLevel.OnEventRaise += LoadCurrentLevel;
            eventManager.OnTryAgain.OnEventRaise += LoadCurrentLevel;
        }

        public void NextLevel()
        {
            StartCoroutine(LoadLevel(gameManager.CurrentLevel + 1));
            NoodyCustomCode.StartDelayFunction(() => { SingletonContainer.Resolve<InGameUI>().CreateUpgradePanel(); }, 1.8f);
        }

        public void ActiveMainMenuLevel()
        {
            foreach (var lv in activeLevels)
            {
                if (lv) Destroy(lv);
	        }
            mainMenuDungeon.SetActive(true);
        }

        public void DeactivateMainMenuLevel()
        {
            mainMenuDungeon.SetActive(false);
        }

        public void LoadCurrentLevel()
        {
            StartCoroutine(LoadLevel(gameManager.CurrentLevel));
        }

        public IEnumerator LoadLevel(int level)
        {
            gameManager.TransitionAnimation();
            
            isFirstTime = false;
            yield return new WaitForSeconds(1f);
            if (level <= 0) level = 1;
            if (level > levels.Count) level = levels.Count;

            foreach (var lv in activeLevels)
            {
                if (lv) Destroy(lv);
            }

            activeLevels.Add(Instantiate(levels[level - 1]));
            levelPortal = FindObjectOfType<Portal>(false);
            eventManager.OnGenerateLevelComplete.RaiseEvent(level);
        }

        public void OpenPortal()
        {
            if(levelPortal != null)
            { 
                SingletonContainer.Resolve<AudioManager>().PlaySFX(sound.teleportPortal);
                levelPortal.Open();
	        }
        }

        public void ClosePortal()
        {
            if(levelPortal != null)
                levelPortal.Close();
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
