using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using NOOD;
using NOOD.NoodCamera;
using Game.Player;
using Game.UI;

namespace Game.Common.Manager
{
    public class Main : MonoBehaviorInstance<Main>
    {
        Transform respawnPos;
        PlayerScripts player;
        CameraFollow mainCamera;

        private void Start()
        {
            GameManager.Create();
            SelectCharacter();
            PlayGame();
        }

        public void SelectCharacter()
        {
            StartCoroutine(CO_SelectCharacter());
        }

        public void PlayGame()
        {
            StartCoroutine(CO_PlayGame());
        }

        public IEnumerator CO_SelectCharacter()
        {
            GameManager.GetInstance.TransitionAnimation();
            yield return new WaitForSeconds(1f); 
            Clear();
            ChooseCharacterManager.Create().AddTo(this);
        }

        public IEnumerator CO_PlayGame()
        {
            GameManager.GetInstance.TransitionAnimation();
            yield return new WaitForSeconds(1f);
            Clear();
            if (Camera.main != null) Destroy(Camera.main.gameObject);
            mainCamera = Instantiate(Resources.Load<CameraFollow>("Prefabs/Game/Player/Main Camera"), null);

            DontDestroyOnLoad(EventManager.Create().gameObject);
            LevelManager.Create();
            WeaponManager.Create();
            AudioManager.Create();
            GameCanvas.Create();

            PoolingManager.Create().AddTo(this);
            ExplodeManager.Create().AddTo(this);
            UIManager.Create().AddTo(this);
        }

        public void NewGame()
        {
            StartCoroutine(CO_NewGame());
        }

        public IEnumerator CO_NewGame()
        {
            GameCanvas.GetInstance.DeactiveAllMenu();
            GameManager.GetInstance.TransitionAnimation();
            yield return new WaitForSeconds(1f);
            Clear();
            mainCamera.transform.position = new UnityEngine.Vector3(0, 0, -10);
            LevelManager.GetInstance.ActiveMainMenuLevel();

            PoolingManager.Create().AddTo(this);
            ExplodeManager.Create().AddTo(this);
            UIManager.Create().AddTo(this);

            yield return new WaitForSeconds(1.2f);
            GameCanvas.GetInstance.ActiveChooseCharacterMenu();
        }

        public void GenerateNewLevel()
        {
            StartCoroutine(Co_GenerateNewLevel());
        }

        private IEnumerator Co_GenerateNewLevel()
        {
            yield return new WaitForSeconds(1.2f);
            Clear();
            PoolingManager.Create().AddTo(this);
            ExplodeManager.Create().AddTo(this);
            UIManager.Create().AddTo(this);
            respawnPos = GameObject.Find("RespawnPos").transform;
            player.transform.position = respawnPos.transform.position;
        }

        private void SpawnPlayerIfNeed()
        { 
            if(respawnPos == null) respawnPos = GameObject.Find("RespawnPos").transform;
            if (GameObject.FindObjectOfType<PlayerScripts>() == null)
            {
                player = (PlayerScripts)PlayerScripts.Create();
                player.transform.position = respawnPos.transform.position;
            }
        }
    }
}
