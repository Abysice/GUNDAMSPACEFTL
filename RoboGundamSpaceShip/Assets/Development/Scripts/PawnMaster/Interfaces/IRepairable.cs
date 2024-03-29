﻿// Interface class for all objects that are "Repaireable"
//
// Written by: Bryan Ramoul
using UnityEngine;
using System.Collections;

public interface IRepairable  {

    //to be called when the Damageable object's has successfully taken damage
    void Repair();
    void FixDamage(int repairAmount);
}
