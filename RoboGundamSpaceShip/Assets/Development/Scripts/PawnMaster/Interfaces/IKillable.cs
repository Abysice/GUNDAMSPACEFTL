// Interface class for all objects that are "Killable"
//
// Written by: Bryan Ramoul
using UnityEngine;
using System.Collections;

public interface IKillable {

    //to be called when the killable object's HP is less or equal to zero
    void Kill();

}
