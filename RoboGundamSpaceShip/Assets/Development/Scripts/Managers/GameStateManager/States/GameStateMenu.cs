using UnityEngine;
using System.Collections;

public class GameStateMenu : GameStateBase
{
	public GameStateMenu(GameStateManager p_gameStateManager)
	{
		m_gameStateManager = p_gameStateManager;
	}


	public override void EnterState(Enums.GameStateNames p_prevState)
	{
		Debug.Log("Entered Menu State");
		//Spawn Menu placeholder
		GameObject.Instantiate(Managers.GetInstance().GetGameProperties().MenuCamera); 
		Managers.GetInstance().GetGUIManager().LoadMainMenu(); //spawn menu gui
	}

	public override void UpdateState()
	{
		
	}

	public override void ExitState(Enums.GameStateNames p_nextState)
	{

	}
}