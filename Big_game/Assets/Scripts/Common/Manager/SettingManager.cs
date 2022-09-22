using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Create(Transform parent = null)
    {
        return Instantiate<SettingManager>(Resources.Load<SettingManager>("Prefabs/Game/Manager/SettingManager"), parent);
    }
}
