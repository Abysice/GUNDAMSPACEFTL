// Script Comments go here
//
//

using UnityEngine;
using System.Collections;

public class GameStateServerLobby : GameStateBase
{
	public GameStateServerLobby(GameStateManager p_gameStateManager)
	{
		m_gameStateManager = p_gameStateManager;
	}


	public override void EnterState(Enums.GameStateNames p_prevState)
	{
		Debug.Log("Entered Lobby (as a server)");
	}

	public override void UpdateState()
	{

	}

	public override void ExitState(Enums.GameStateNames p_nextState)
	{

	}
}