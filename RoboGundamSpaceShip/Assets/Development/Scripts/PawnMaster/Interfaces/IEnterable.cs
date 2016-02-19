// Interface class for all objects that are "controllable" by players
//
// Written by: Adam Bysice
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public interface IEnterable {

	//to be called when the controllable object is given to a player
	void OnControlled();
	//to be called when the object has control taken away from a player
	void OnUnControlled();
	
	//ONLY CALLED ON TEH SERVER BELOW HERE

	//to be called before the control is taken away to play any power down/leaving object animations
	//no commands/rpc's should be sent after recieving this
	void ServerPowerDown();
	//// to be called right as control is taken away by the server
	void ServerFinishPowerDown();
}
