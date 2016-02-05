using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameStateLoading : GameStateBase
{
	public GameStateLoading(GameStateManager p_gameStateManager)
	{
		m_gameStateManager = p_gameStateManager;
	}
 

	public override void EnterState(Enums.GameStateNames p_prevState)
	{
		Debug.Log("Entered Loading State");

		m_gameStateManager.ChangeGameState(Enums.GameStateNames.GS_04_INPLAY);
	}

	public override void UpdateState()
	{
		//add some loading screen shenanigans before this
	}

	public override void ExitState(Enums.GameStateNames p_nextState)
	{

	}
}