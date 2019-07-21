using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpeedPackScript : AbstractBonusScript
{
    protected override void Action()
    {
        playerScript.AddShotSpeed();
    }   
}
