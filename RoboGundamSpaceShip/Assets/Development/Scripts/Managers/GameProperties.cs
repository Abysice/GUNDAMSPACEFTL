// Game Properties class, for keeping public prefabs and variables that 
// need to be loaded into the game be more easily accessed
//
using UnityEngine;
using System.Collections;

public class GameProperties : MonoBehaviour {

	private const string m_levelScene = "LEVEL_SCENE";
	private const string m_lobbyScene = "LOBBY_SCENE";
	private const string m_menuScene = "MAIN_MENU";


	public string LevelScene
	{
		get { return m_levelScene; }
	}
	public string MenuScene
	{
		get { return m_menuScene; }
	}
	public string LobbyScene
	{
		get { return m_lobbyScene; }
	}

	public GameObject MainMenu;
	public GameObject LobbyMenu;
	public GameObject mainCamera;
	public GameObject playerPrefab;
	public GameObject shipPrefab;
	public GameObject mechaPrefab;
    public GameObject parallaxPrefab;
	public GameObject cannonPrefab;
	public GameObject pointdefensePrefab;
	public GameObject playerManager;
	public GameObject flakBarrage;
}
