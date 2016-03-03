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
	public GameObject[] m_pointDefense;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private Transform[] m_spawns;
	
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults

	//initialization
	public void Start()
	{
		m_spawns = new Transform[20];
		m_cannons = new GameObject[4];
		m_pointDefense = new GameObject[6];
		//m_localCamera = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().mainCamera);
		Managers.GetInstance().SetPlayerManager(this);
		DontDestroyOnLoad(gameObject);

		ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().shipPrefab);
		ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().playerPrefab);
		ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().mechaPrefab);
		ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().playerManager);
		ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().cannonPrefab, SpawnCannonHandler, UnSpawnHandler);
		ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().pointdefensePrefab, SpawnPointDefenseHandler, UnSpawnHandler);
		ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().enemyBullet);
		ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().bulletPrefab);
		ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().flakBarrage);
		ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().BulletExplosion);
		ClientScene.RegisterPrefab(Managers.GetInstance().GetGameProperties().enemyPlaceholderArmor);
		
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
		SpawnPointDefense(); //spawn the ship point defense
		//spawn level goes here
		SpawnFrienemies(); //spawn the enemies for the level
	}

	#region SpawnHandlers
	
	public GameObject SpawnCannonHandler(Vector3 position, NetworkHash128 assetID)
	{
		GameObject l_cannon = (GameObject)GameObject.Instantiate(Managers.GetInstance().GetGameProperties().shipPrefab, position, Quaternion.identity);
		l_cannon.transform.parent = GameObject.Find("ShipPrefab(Clone)").transform;
		return l_cannon;
	}

	public GameObject SpawnPointDefenseHandler(Vector3 position, NetworkHash128 assetID)
	{
		GameObject l_cannon = (GameObject)GameObject.Instantiate(Managers.GetInstance().GetGameProperties().shipPrefab, position, Quaternion.identity);
		l_cannon.transform.parent = GameObject.Find("ShipPrefab(Clone)").transform;
		return l_cannon;
	}

	public void UnSpawnHandler(GameObject gameObject)
	{
		Destroy(gameObject);
		return;
	}

	#endregion

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
			m_cannons[i].transform.parent = GameObject.Find("ShipPrefab(Clone)").transform;
			NetworkServer.Spawn(m_cannons[i]);
		}
	}

	//spawn the pointdefenceguns
	private void SpawnPointDefense()
	{
		for (int i = 0; i < 6; i++)
		{
			m_pointDefense[i] = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().pointdefensePrefab, m_spawns[i + 8].position, Quaternion.identity);
			m_pointDefense[i].transform.parent = GameObject.Find("ShipPrefab(Clone)").transform;
			NetworkServer.Spawn(m_pointDefense[i]);
		}
	}

	//spawn some test Enemies
	private void SpawnFrienemies()
	{
		GameObject l_enemy = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().enemyPlacholderPrefab, m_ship.transform.position + new Vector3(30.0f, 0, 0), Quaternion.identity);
		NetworkServer.Spawn(l_enemy);
		Vector3 l_pos = l_enemy.transform.FindChild("WeakSpot").position;
		GameObject l_armor = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().enemyPlaceholderArmor, l_pos, Quaternion.identity);
		l_armor.transform.parent = l_enemy.transform;
		NetworkServer.Spawn(l_armor);
		l_armor.GetComponent<EnemyArmor>().RpcSetup(l_enemy.GetComponent<NetworkIdentity>().netId);
	}

	#endregion
}
