// my network controller inherits from the unity network manager and controls dat shit
//
//

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NetworkController : NetworkManager
{
    #region Public Variables
	public string m_ip = "";
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
		this.networkPort = Constants.MULTIPLAYER_PORT;
    }
    //runs every frame
    public void Update()
    {

    }
	//called when the server is started
	public override void OnStartServer()
	{
		base.OnStartServer();
		Debug.Log("Server Started");
	}

	//called by the server when a player is addded.
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		//base.OnServerAddPlayer(conn, playerControllerId);
		Debug.Log("a player has been added");
		
	}
	//called by clients when they connect
	public override void OnStartClient(NetworkClient client)
	{
		//Debug.Log("OnStartClient, ID = " + client.connection.connectionId);
		//Managers.GetInstance().GetPlayerManager().AddPlayer(client); // ONLY THE SERVER HAS THIS LIST SO FAR
	}
	//called when another client connects
	public override void OnClientConnect(NetworkConnection p_connection)
	{
		base.OnClientConnect(p_connection);
		Debug.Log("OnClientConnect " + p_connection.connectionId);
		
	}
	//called on server
	public override void OnServerConnect(NetworkConnection p_connection)
	{
		base.OnServerConnect(p_connection);
		Debug.Log("OnServerConnect " + p_connection.connectionId);
		Managers.GetInstance().GetPlayerManager().AddPlayer(p_connection); // ONLY THE SERVER HAS THIS LIST SO FAR
	}
	//called on server when error ocurrs
	public override void OnServerError(NetworkConnection p_connection, int errorCode)
	{
		base.OnServerError(p_connection, errorCode);
		Debug.Log("CONNECTION ERROR:" + errorCode);
	}

	//called on client when error occurs
	public override void OnClientError(NetworkConnection p_connection, int errorCode)
	{
		base.OnClientError(p_connection, errorCode);
		Debug.Log("CONNECTION ERROR: " + errorCode);
	}
	//called on each client when server closes
	public override void OnStopClient()
	{
		base.OnStopClient();
		Debug.Log("OnStopClient");
		Managers.GetInstance().GetGameStateManager().ChangeGameState(Enums.GameStateNames.GS_01_MENU);
	}
	//called on the server when the scene changes to the next
	public override void OnServerSceneChanged(string sceneName)
	{
		base.OnServerSceneChanged(sceneName);
	}
	//called on the client when the scene changes to the next
	public override void OnClientSceneChanged(NetworkConnection p_connection)
	{
		//base.OnClientSceneChanged(conn);
		//move state machine once scene's have loaded
		if (networkSceneName == Managers.GetInstance().GetGameProperties().LobbyScene)
			Managers.GetInstance().GetGameStateManager().ChangeGameState(Enums.GameStateNames.GS_02_LOBBY);
		else if (networkSceneName == Managers.GetInstance().GetGameProperties().LevelScene)
		{
			Managers.GetInstance().GetGameStateManager().ChangeGameState(Enums.GameStateNames.GS_03_LOADING);
			//spawn the players
			Managers.GetInstance().GetPlayerManager().SpawnPlayers();
		}
			
		
	}

	#endregion

    #region Public Methods

	//called by the gui to host a multiplayer game
	public void HostGameButton()
	{
		NetworkClient temp = StartHost();
		//SceneManager.LoadScene(Managers.GetInstance().GetGameProperties().LobbyScene);
		ServerChangeScene(Managers.GetInstance().GetGameProperties().LobbyScene);
		
	}

	//called by the gui to join a multiplayer game
	public void JoinGameButton()
	{
		if (m_ip == "")
			this.networkAddress = Network.player.ipAddress.ToString();
		else
			this.networkAddress = m_ip;
		StartClient();
	}

	public void BeginMatchButton()
	{
		Debug.Log("MATCH STARTED");
		ServerChangeScene(Managers.GetInstance().GetGameProperties().LevelScene);

	}
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
