using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] ButtonElement buttonRef;

    private void Start()
    {
        buttonRef.gameObject.SetActive(false);
        CreateBtn();
    }

    void CreateBtn()
    {
        for (int i = 0; i < 3; i++)
        {
            ButtonElement newBtn = Instantiate<ButtonElement>(buttonRef, this.transform);
            newBtn.gameObject.SetActive(true);
            Upgrade newUpgrade = UpgradeMaster.RandomUpgrade();
            newBtn.SetBtn(newUpgrade, () =>
            {
                if (GoldManager.i.MinusGold(newUpgrade.goldNeed))
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
