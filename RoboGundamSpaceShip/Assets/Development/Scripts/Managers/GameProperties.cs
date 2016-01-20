// Game Properties class, for keeping public prefabs and variables that 
// need to be loaded into the game be more easily accessed
//
using UnityEngine;
using System.Collections;

public class GameProperties : MonoBehaviour {

	private string m_levelScene = "LEVEL_SCENE";
	private string m_lobbyScene = "LOBBY_SCENE";
	private string m_menuScene = "MAIN_MENU";


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

	public GameObject MenuCamera;
	public GameObject MainMenu;
	public GameObject LobbyMenu;
	public GameObject playerCamera;
	public GameObject PawnMaster;

}
