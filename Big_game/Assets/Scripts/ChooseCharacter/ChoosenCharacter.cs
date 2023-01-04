using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosenCharacter : MonoBehaviour
{
    [SerializeField] Animator anim;
    int animIndex;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (anim)
            SetBoolTrue(LocalDataManager.playerNumber.ToString());
    }

    public void ChangePlayer()
    {
        if (animIndex != LocalDataManager.playerNumber)
        {
            anim.SetTrigger("Change");
            animIndex = LocalDataManager.playerNumber;
            SetBoolTrue(animIndex.ToString());
        }
    }

    public void SetBoolTrue(string name)
    { 
        foreach(AnimatorControllerParameter parameter in anim.parameters)
        { 
            if(parameter.name == name & parameter.type == AnimatorControllerParameterType.Bool)
            {
                anim.SetBool(parameter.name, true);
	        }
            else if(parameter.type == AnimatorControllerParameterType.Bool)
            {
                anim.SetBool(parameter.name, false);
	        }
	    }
    }
}
