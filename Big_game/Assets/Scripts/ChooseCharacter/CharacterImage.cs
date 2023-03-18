using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Save;
using Game.Player;
using Game.Common.Manager;

namespace Game.UI.ChooseCharacter
{
    public class CharacterImage : MonoBehaviour 
    {
        public int characterNumber;
        SaveModels.GameSystemModel gameSystemModel;

        public void ChangeCharacterNumber()
        {
            GameManager.GetInstance.GetGameSystemModel().playerNum = this.characterNumber;

            GameManager.GetInstance.CreateOrChangePlayerWithNewSave();
        }
    }
}
