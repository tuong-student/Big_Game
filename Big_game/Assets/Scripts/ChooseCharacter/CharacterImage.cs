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

        public void ChangeCharacterNumber()
        {
            SaveModels.GameSystemModel gameSystemModel = LoadJson<SaveModels.GameSystemModel>.LoadFromJson(SaveModels.SaveFile.GameSystemSave.ToString());
            if(gameSystemModel == null)
            {
                gameSystemModel.gold = 0;
                gameSystemModel.level = 1;
                gameSystemModel.playerNum = characterNumber;
            }
            else
            {
                gameSystemModel.playerNum = characterNumber;
            }
            SaveJson.SaveToJson(gameSystemModel, SaveModels.SaveFile.GameSystemSave.ToString());

            GameManager.GetInstance.CreatePlayerIfNeed();
        }
    }
}
