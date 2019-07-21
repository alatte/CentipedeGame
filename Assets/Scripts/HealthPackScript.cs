using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : AbstractBonusScript
{
    protected override void Action()
    {
        playerScript.AddLife();
    }  
}
