using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        for (int i = 0; i < 3; i++)
        {
            UpgradeButton newBtn = Instantiate<UpgradeButton>(buttonRef, this.transform);
            newBtn.gameObject.SetActive(true);
            Upgrade newUpgrade = UpgradeMaster.RandomUpgrade();
            newBtn.SetBtn(newUpgrade, () =>
            {
                if (GoldManager.GetInstace.MinusGold(newUpgrade.goldNeed))
                {
                    newBtn.PerformUpgrade();
                    Dispose();
                }
            });
        }

    }

    public void Dispose()
    {
        Destroy(this.gameObject);
    }

    
}
