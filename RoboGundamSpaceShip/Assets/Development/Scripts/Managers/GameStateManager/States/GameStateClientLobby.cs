
using UnityEngine;
using System.Collections;

public class GameStateClientLobby : GameStateBase
{
	public GameStateClientLobby(GameStateManager p_gameStateManager)
	{
		m_gameStateManager = p_gameStateManager;
	}


	public override void EnterState(Enums.GameStateNames p_prevState)
	{
		Debug.Log("Entered Lobby (as a client)");
	}

	public override void UpdateState()
	{

	}

	public override void ExitState(Enums.GameStateNames p_nextState)
	{

	}
}