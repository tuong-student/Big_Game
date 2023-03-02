using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;

namespace Game.UI
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] UpgradeButton buttonRef;

        private void Start()
        {
            buttonRef.gameObject.SetActive(false);
            CreateBtn();
        }

        void CreateBtn()
        {
            Upgrade newUpgrade2 = new Upgrade();
            for (int i = 0; i < 3; i++)
            {
                //newUpgrade2 = newUpgrade;
                Upgrade newUpgrade = UpgradeMaster.RandomUpgrade();
                UpgradeButton newBtn = Instantiate<UpgradeButton>(buttonRef, this.transform);
                newBtn.gameObject.SetActive(true);
                newBtn.SetBtn(newUpgrade, () =>
                {
                    //if (PlayerScripts.GetInstance.Buy(newUpgrade.goldNeed, newUpgrade))
                    //{
                    //    Dispose();
                    //}
                    //else Debug.Log("Not Enough Gold");
                });
            }

        }

        public void Dispose()
        {
            EventManager.GetInstance.OnContinuewGame.RaiseEvent();
            Destroy(this.gameObject);
        }


    }
}
