// my network controller inherits from the unity network manager and controls dat shit
//
//

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class NetworkController : NetworkLobbyManager
{


    #region Public Variables
	public string m_ip = "";
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables
	private List<PlayerController> m_playerList;
    #endregion

    #region Accessors
    #endregion

    #region Unity Defaults
    //initialization
    public void Start()
    {
		m_playerList = new List<PlayerController>();
		this.networkPort = Constants.MULTIPLAYER_PORT;
		this.autoCreatePlayer = false;
		this.maxPlayers = 1;
		this.maxPlayersPerConnection = 1;
    }
    //runs every frame
    public void Update()
    {

    }
	//called when the server is started
	public override void OnStartServer()
	{
		//base.OnStartServer();
		Debug.Log("Server Started");
	}

	public override void OnStartClient(NetworkClient client)
	{
		//base.OnStartServer();
		Debug.Log("Client Started");
	}
	//called when another client connects
	public override void OnClientConnect(NetworkConnection p_connection)
	{
		Debug.Log(p_connection.connectionId + "Connected to the Server(client message)");
	}
	//called on server
	public override void OnServerConnect(NetworkConnection conn)
	{
		Debug.Log(conn.connectionId + " Connected to the Server(server message)");

		PlayerController l_newPlayer = new PlayerController();
		l_newPlayer.playerControllerId = (short)conn.connectionId;
		m_playerList.Add(l_newPlayer); // ONLY THE SERVER HAS THIS LIST SO FAR
		
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

	    #endregion

    #region Public Methods

	//called by the gui to host a multiplayer game
	public void HostGameButton()
	{
		StartHost();
		Managers.GetInstance().GetGUIManager().HideMainMenu();
		Managers.GetInstance().GetGameStateManager().ChangeGameState(Enums.GameStateNames.GS_02_SERVERLOBBY);
	}

	//called by the gui to join a multiplayer game
	public void JoinGameButton()
	{
		if (m_ip == "")
			this.networkAddress = Network.player.ipAddress.ToString();
		else
			this.networkAddress = m_ip;
		StartClient();
		Managers.GetInstance().GetGUIManager().HideMainMenu();
		Managers.GetInstance().GetGameStateManager().ChangeGameState(Enums.GameStateNames.GS_03_CLIENTLOBBY);
	}
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
