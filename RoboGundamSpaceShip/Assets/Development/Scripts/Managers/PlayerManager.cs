// This class will keep track of the player prefabs and such and such
//
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{

	}
	//runs every frame
	public void Update()
	{

	}
	#endregion

	#region Public Methods
	[Command]
	public void CmdSpawnPlayer()
	{
		GameObject l_player = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().playerPrefab);
		ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().playerPrefab);
		NetworkServer.Spawn(l_player);
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
