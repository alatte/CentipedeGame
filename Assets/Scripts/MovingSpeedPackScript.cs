using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpeedPackScript : AbstractBonusScript
{
    protected override void Action()
    {
        playerScript.AddShipSpeed();
    }   
}
