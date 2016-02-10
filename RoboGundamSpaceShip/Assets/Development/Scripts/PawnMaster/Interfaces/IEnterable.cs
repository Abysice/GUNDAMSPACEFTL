// Interface class for all objects that are "controllable" by players
//
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;

public interface IEnterable {

	//to be called when the controllable object is given to a player
	void OnControlled();

	void OnUnControlled();

}
