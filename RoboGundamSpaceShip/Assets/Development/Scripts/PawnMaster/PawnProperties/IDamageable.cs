// Interface class for all objects that are "Damageable"
//
// Written by: Bryan Ramoul
using UnityEngine;
using System.Collections;

public interface IDamageable <T> {

    //to be called when the Damageable object's has successfully taken damage
    void Damage(T damageTaken);
}
