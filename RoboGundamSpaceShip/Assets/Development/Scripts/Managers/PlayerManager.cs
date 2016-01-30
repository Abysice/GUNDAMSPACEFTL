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
	private GameObject m_localCamera;
	private GameObject m_ship;
	#endregion

	#region Accessors
	public GameObject GetPlayerCamera()
	{
		return m_localCamera;
	}
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_localCamera = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().mainCamera);
		DontDestroyOnLoad(m_localCamera);
		ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().playerPrefab);
	}
	//runs every frame
	public void Update()
	{

	}
	#endregion

	#region Public Methods
	
	public void SpawnPlayers()
	{
		for (int i = 0; i < NetworkServer.connections.Count; i++)
		{
			Debug.Log("spawnin a dude for :" + NetworkServer.connections[i]);
			GameObject l_player = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().playerPrefab);
			l_player.transform.parent = m_ship.transform;
			//add spawn point stuff here
			NetworkServer.AddPlayerForConnection(NetworkServer.connections[i], l_player, 0);
		}
	}

	public void SpawnShip()
	{
		m_ship = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().shipPrefab);
		NetworkServer.Spawn(m_ship);
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
