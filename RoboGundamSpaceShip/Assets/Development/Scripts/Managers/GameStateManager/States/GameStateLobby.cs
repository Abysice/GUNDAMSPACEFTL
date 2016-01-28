// Script Comments go here
//
//

using UnityEngine;
using System.Collections;

public class GameStateLobby : GameStateBase
{
	//private GameObject m_lobbyCamera = null;

	public GameStateLobby(GameStateManager p_gameStateManager)
	{
		m_gameStateManager = p_gameStateManager;
	}


	public override void EnterState(Enums.GameStateNames p_prevState)
	{
		Debug.Log("Entered Lobby state");
		//lobby placeholder
		//m_lobbyCamera = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().MenuCamera);
		Managers.GetInstance().GetGUIManager().LoadLobbyGUI();//spawn menu gui
	}

	public override void UpdateState()
	{

	}

	public override void ExitState(Enums.GameStateNames p_nextState)
	{

	}
}