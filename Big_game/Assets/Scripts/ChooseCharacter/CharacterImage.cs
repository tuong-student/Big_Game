using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterImage : MonoBehaviour 
{
    public int characterNumber;

    public void ChangeCharacterNumber()
    {
        LocalDataManager.playerNumber = characterNumber;
    }
}
