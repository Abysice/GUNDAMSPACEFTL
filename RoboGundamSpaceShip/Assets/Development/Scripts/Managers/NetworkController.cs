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
	private List<NetworkClient> m_playerList;
    #endregion

    #region Accessors
    #endregion

    #region Unity Defaults
    //initialization
    public void Start()
    {
		m_playerList = new List<NetworkClient>();
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
		base.OnServerAddPlayer(conn, playerControllerId);
		Debug.Log("a player has been added");
		
	}
	//called by clients when they connect
	public override void OnStartClient(NetworkClient client)
	{
		Debug.Log("client connected");
		base.OnStartServer();
		//Debug.Log("Client connected: " + client.connection.connectionId);
		m_playerList.Add(client); // ONLY THE SERVER HAS THIS LIST SO FAR
	}
	//called when another client connects
	public override void OnClientConnect(NetworkConnection p_connection)
	{
		Debug.Log(p_connection.connectionId + " Connected to the Server(client message)");
	}
	//called on server
	public override void OnServerConnect(NetworkConnection conn)
	{
		Debug.Log(conn.connectionId + " Connected to the Server(server message)");
	}
	//called on server when error ocurrs
	public override void OnServerError(NetworkConnection conn, int errorCode)
	{
		Debug.Log("CONNECTION ERROR:" + errorCode);
	}

	//called on client when error occurs
	public override void OnClientError(NetworkConnection conn, int errorCode)
	{
		Debug.Log("CONNECTION ERROR: " + errorCode);
	}
	//called on each client when server closes
	public override void OnStopClient()
	{
		Debug.Log("server closed");
		Managers.GetInstance().GetGameStateManager().ChangeGameState(Enums.GameStateNames.GS_01_MENU);
	}
	//called on the server when the scene changes to the next
	public override void OnServerSceneChanged(string sceneName)
	{
		base.OnServerSceneChanged(sceneName);
	}
	//called on the client when the scene changes to the next
	public override void OnClientSceneChanged(NetworkConnection conn)
	{
		base.OnClientSceneChanged(conn);
		//move state machine once scene's have loaded
		if (networkSceneName == Managers.GetInstance().GetGameProperties().LobbyScene)
			Managers.GetInstance().GetGameStateManager().ChangeGameState(Enums.GameStateNames.GS_02_LOBBY);
		else if (networkSceneName == Managers.GetInstance().GetGameProperties().LevelScene)
		{
			Managers.GetInstance().GetGameStateManager().ChangeGameState(Enums.GameStateNames.GS_03_LOADING);
			
		}
			
		
	}

	#endregion

    #region Public Methods

	//called by the gui to host a multiplayer game
	public void HostGameButton()
	{
		NetworkClient temp = StartHost();
		SceneManager.LoadScene(Managers.GetInstance().GetGameProperties().LobbyScene);
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
		SceneManager.LoadScene(Managers.GetInstance().GetGameProperties().LobbyScene);
		ServerChangeScene(Managers.GetInstance().GetGameProperties().LevelScene);
	}
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
