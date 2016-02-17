// Interface class for all objects that are "Damageable"
//
// Written by: Bryan Ramoul
using UnityEngine;
using System.Collections;


public interface IDamageable  {

    //to be called when the Damageable object's has successfully taken damage
    void Damage(int damageTaken);
}
