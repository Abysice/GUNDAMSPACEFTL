// my network controller inherits from the unity network manager and controls dat shit
//
//

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class NetworkController : NetworkManager
{


    #region Public Variables
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
		NetworkManager.singleton.networkPort = Constants.MULTIPLAYER_PORT;
		NetworkManager.singleton.autoCreatePlayer = false;
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
	//called when a client connects (includes the local server)
	public override void OnClientConnect(NetworkConnection p_connection)
	{
		base.OnClientConnect(p_connection);
		Debug.Log(p_connection.connectionId + " Connected to the Server");
		
		PlayerController l_newPlayer = new PlayerController();
		l_newPlayer.playerControllerId = (short)p_connection.connectionId;
		m_playerList.Add(l_newPlayer);
	}
    #endregion

    #region Public Methods

	//called by the gui to host a multiplayer game
	public void HostGameButton()
	{
		this.StartHost();
		Managers.GetInstance().GetGUIManager().HideMainMenu();
		Managers.GetInstance().GetGameStateManager().ChangeGameState(Enums.GameStateNames.GS_02_SERVERLOBBY);
	}

	//called by the gui to join a multiplayer game
	public void JoinGameButton()
	{
		this.StartClient();
		Managers.GetInstance().GetGUIManager().HideMainMenu();
		Managers.GetInstance().GetGameStateManager().ChangeGameState(Enums.GameStateNames.GS_03_CLIENTLOBBY);
	}
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
