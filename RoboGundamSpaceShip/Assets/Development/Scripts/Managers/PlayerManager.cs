// This class will keep track of the player prefabs and such and such
//
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private List<NetworkConnection> m_playerList;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_playerList = new List<NetworkConnection>();
	}
	//runs every frame
	public void Update()
	{

	}
	#endregion

	#region Public Methods
	public void AddPlayer(NetworkConnection p_newPlayer)
	{
		//make sure its not already there
		foreach (NetworkConnection client in m_playerList)
		{
			if (p_newPlayer.connectionId == client.connectionId)
				return;
		}
		m_playerList.Add(p_newPlayer);
	}

	public void SpawnPlayers()
	{
		foreach (NetworkConnection client in m_playerList)
		{
			Debug.Log("spawnin a dude for :" + client.connectionId);
			GameObject l_player = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().playerPrefab);
			NetworkServer.AddPlayerForConnection(client, l_player, 0);
		}
		
	}

	[Command]
	public void CmdSpawnPlayer()
	{
		//NetworkServer.AddPlayerForConnection()
		//GameObject l_player = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().playerPrefab);
		//ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().playerPrefab);
		//NetworkServer.Spawn(l_player);
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
