using UnityEngine;
using System.Collections;

public class GameStateMenu : GameStateBase
{
	//private GameObject m_menuCamera;

	public GameStateMenu(GameStateManager p_gameStateManager)
	{
		m_gameStateManager = p_gameStateManager;
	}


	public override void EnterState(Enums.GameStateNames p_prevState)
	{
		Debug.Log("Entered Menu State");
		//m_menuCamera = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().MenuCamera); 
		Managers.GetInstance().GetGUIManager().LoadMenu(); //spawn menu gui
	}

	public override void UpdateState()
	{
		
	}

	public override void ExitState(Enums.GameStateNames p_nextState)
	{

	}
}