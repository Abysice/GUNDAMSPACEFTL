// contains easy to find references for the button components in the prefab
//
//

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour {


    #region Public Variables
    public Text m_CurrentIP;
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
        m_CurrentIP.text = Network.player.ipAddress.ToString();

    }
    //runs every frame
    public void Update()
    {

    }
    #endregion

    #region Public Methods
	//called by the gui to host a multiplayer game
	public void HostGameButton()
	{
		Managers.GetInstance().GetNetworkController().HostGameButton();
	}

	//called by the gui to join a multiplayer game
	public void JoinGameButton()
	{
	
		Managers.GetInstance().GetNetworkController().JoinGameButton();
	}

	public void QuitGameButton()
	{
		Managers.GetInstance().GetGUIManager().QuitGameButton();
	}

    public void UserInput(string l_userIP)
    {
        Managers.GetInstance().GetNetworkController().m_ip = l_userIP;
    }

    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
