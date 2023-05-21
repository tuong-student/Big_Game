using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Save;

public class ResetUI : MonoBehaviour
{
    public void NewGame()
    {
        SaveJson.DeleteJson(SaveModels.SaveFile.PlayerSave.ToString());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TryAgain()
    {
        SaveJson.DeleteJson(SaveModels.SaveFile.PlayerSave.ToString());
        SaveJson.DeleteJson(SaveModels.SaveFile.GameSystemSave.ToString());
        SaveJson.DeleteJson(SaveModels.SaveFile.WeaponSave.ToString());
    }
}
