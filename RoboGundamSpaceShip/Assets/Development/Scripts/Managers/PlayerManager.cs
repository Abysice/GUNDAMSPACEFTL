﻿// This class will keep track of the player prefabs and such and such
// NOTE THAT RPCS AND COMMANDS WILL NOT WORK ON THIS CLASS
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

	#region Public Variables
	public GameObject m_ship;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private GameObject m_localCamera;

	private Transform[] m_spawns;
	
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
		m_spawns = new Transform[3];
		m_localCamera = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().mainCamera);
		DontDestroyOnLoad(m_localCamera);
	}
	//runs every frame
	public void Update()
	{

	}
	#endregion

	#region Public Methods
	
	public void SpawnPrefabs()
	{
		//spawn the ship
		m_ship = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().shipPrefab);
		NetworkServer.Spawn(m_ship);
		//NetworkServer.AddPlayerForConnection(NetworkServer.connections[0], m_ship, 1);
		Transform l_spawns = m_ship.transform.Find("Spawns");
		int count = 0;
		foreach (Transform child in l_spawns)
		{
			m_spawns[count] = child;
			count++;
		}

		//spawn the players
		for (int i = 0; i < NetworkServer.connections.Count; i++)
		{
			Debug.Log("spawnin a dude for :" + NetworkServer.connections[i]);
			GameObject l_player = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().playerPrefab);
			l_player.transform.position = m_spawns[i].position;
			
			NetworkServer.AddPlayerForConnection(NetworkServer.connections[i], l_player, 0);
			//l_player.transform.parent = m_ship.transform;
		}
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
