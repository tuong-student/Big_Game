using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Void Int Event")]
public class VoidIntEventChannelSO : ScriptableObject
{
    public UnityAction<int> OnEventRaise;
    public void RaiseEvent(int number)
    {
        OnEventRaise?.Invoke(number);
    }
}
