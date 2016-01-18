// my network controller inherits from the unity network manager and controls dat shit
//
//

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkController : NetworkManager
{


    #region Public Variables
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
	public override void OnClientConnect(NetworkConnection conn)
	{
		base.OnClientConnect(conn);
		Debug.Log("Connected to the Server");
	}
    #endregion

    #region Public Methods

	//called by the gui to host a multiplayer game
	public void HostGameButton()
	{
		this.StartHost();
	}

	//called by the gui to join a multiplayer game
	public void JoinGameButton()
	{
		this.StartClient();
	}
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
