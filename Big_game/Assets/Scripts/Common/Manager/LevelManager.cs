using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using NOOD;
using Game.System;

namespace Game.Common.Manager
{
    public class LevelManager : MonoBehaviorInstance<LevelManager>
    {
        [SerializeField] private List<GameObject> levels;
        [SerializeField] private GameObject mainMenuDungeon;
        private List<GameObject> activeLevels = new List<GameObject>();
        private bool isFirstTime = true;
        private Portal levelPortal;

        public static LevelManager Create(Transform parent = null)
        {
            return Instantiate<LevelManager>(Resources.Load<LevelManager>("Prefabs/Manager/LevelManager"), parent);
        }

        private void Start()
        {
            mainMenuDungeon = GameObject.Find("Main Menu Dungeon");
            if(mainMenuDungeon == null)
                mainMenuDungeon = Instantiate(mainMenuDungeon);
            ActiveMainMenuLevel();

            EventManager.GetInstance.OnStartGame.OnEventRaise += LoadCurrentLevel;
            EventManager.GetInstance.OnStartGame.OnEventRaise += DeactiveMainMenuLevel;
            EventManager.GetInstance.OnGenerateLevel.OnEventRaise += LoadCurrentLevel;
            EventManager.GetInstance.OnTryAgain.OnEventRaise += LoadCurrentLevel;
        }

        public void NextLevel()
        {
            StartCoroutine(LoadLevel(GameManager.GetInstance.CurrentLevel + 1));
            NoodyCustomCode.StartDelayFunction(() => { InGameUI.GetInstance.CreateUpgradePanel(); }, 1.8f);
        }

        public void ActiveMainMenuLevel()
        {
            foreach (var lv in activeLevels)
            {
                if (lv) Destroy(lv);
	        }
            mainMenuDungeon.SetActive(true);
        }

        public void DeactiveMainMenuLevel()
        {
            mainMenuDungeon.SetActive(false);
        }

        public void LoadCurrentLevel()
        {
            StartCoroutine(LoadLevel(GameManager.GetInstance.CurrentLevel));
        }

        public IEnumerator LoadLevel(int level)
        {
            GameManager.GetInstance.TransitionAnimation();
            
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
            EventManager.GetInstance.OnGenerateLevelComplete.RaiseEvent(level);
        }

        public void OpenPortal()
        {
            if(levelPortal != null)
            { 
                AudioManager.GetInstance.PlaySFX(sound.telePortal);
                levelPortal.Open();
	        }
        }

        public void ClosePortal()
        {
            if(levelPortal != null)
                levelPortal.Close();
        }
    }
}
