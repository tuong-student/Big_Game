using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayer : BaseCharacter
{
    private float moveX,moveY;
    
    private void FixedUpdate() {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        
        
    }
}
