using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NOOD;

public class UIManager : MonoBehaviour
{
    public static UIManager Create(Transform parent = null)
    {
        return Instantiate<UIManager>(Resources.Load<UIManager>("Prefabs/Manager/UIManager"), parent);
    }
}
