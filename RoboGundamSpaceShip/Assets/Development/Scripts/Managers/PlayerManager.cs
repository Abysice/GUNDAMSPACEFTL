﻿// This class will keep track of the player prefabs and such and such
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

	#region Public Variables
	public GameObject m_ship;
	public GameObject m_gundam;
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
		m_spawns = new Transform[20];
		//m_localCamera = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().mainCamera);
		Managers.GetInstance().SetPlayerManager(this);
		DontDestroyOnLoad(gameObject);
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
		
		//Spawn Mecha
		m_gundam = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().mechaPrefab);
		NetworkServer.Spawn(m_gundam);
		m_gundam.transform.position = m_spawns[3].position;
		m_gundam.GetComponent<MechaController>().RpcSetPosition(m_gundam.transform.position);
		
	}

	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
