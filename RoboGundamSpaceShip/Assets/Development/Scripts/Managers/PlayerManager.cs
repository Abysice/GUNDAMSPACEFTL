// This class will keep track of the player prefabs and such and such
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

	#region Public Variables
	public GameObject m_ship;
	public GameObject m_gundam;
	public GameObject[] m_cannons;
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
		m_cannons = new GameObject[4];
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
		SpawnPlayerShip(); //spawn the main ship
		SetupSpawnPoints(); //setup the spawn points for the other things (ship must be spawned first)
		SpawnPlayers(); //spawn player prefabs
		SpawnMecha(); // spawn the mecha in the ship
		SpawnShipTurrets(); //spawn the ship turrets
		
		
 

	}

	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	//spawn the ship
	private void SpawnPlayerShip()
	{
		m_ship = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().shipPrefab);
		NetworkServer.Spawn(m_ship);
	}

	//setup the spawn points from the ship
	private void SetupSpawnPoints()
	{
		Transform l_spawns = m_ship.transform.Find("Spawns");
		int count = 0;
		foreach (Transform child in l_spawns)
		{
			m_spawns[count] = child;
			count++;
		}
	}
	
	//spawn the player prefabs
	private void SpawnPlayers()
	{
		for (int i = 0; i < NetworkServer.connections.Count; i++)
		{
			//Debug.Log("spawnin a dude for :" + NetworkServer.connections[i]);
			GameObject l_player = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().playerPrefab, m_spawns[i].position, Quaternion.identity);
			NetworkServer.AddPlayerForConnection(NetworkServer.connections[i], l_player, 0);
		}
	}

	//spawn the mecha inside the ship
	private void SpawnMecha()
	{
		m_gundam = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().mechaPrefab, m_spawns[3].position, Quaternion.identity);
		NetworkServer.Spawn(m_gundam);
	}

	//spawn the turrets
	private void SpawnShipTurrets()
	{
		for (int i = 0; i < 4; i++)
		{
			m_cannons[i] = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().cannonPrefab, m_spawns[i + 4].position, Quaternion.identity);
			NetworkServer.Spawn(m_cannons[i]);
			m_cannons[i].GetComponent<TurretController>().RpcSetup();
		}
	}

	#endregion
}
