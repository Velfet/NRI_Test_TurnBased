using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEnemy : UnitBase
{
    public override void BattleSetUp()
    {
        UnitType = MyEnum.UnitType.Enemy;
        base.BattleSetUp();
    }
}
