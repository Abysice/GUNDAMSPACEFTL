﻿using UnityEngine;
using System.Collections;

public class GameStateLoading : GameStateBase
{
	public GameStateLoading(GameStateManager p_gameStateManager)
	{
		m_gameStateManager = p_gameStateManager;
	}
 

	public override void EnterState(Enums.GameStateNames p_prevState)
	{
		Application.LoadLevel(Managers.GetInstance().GetGameProperties().LevelScene);
		Debug.Log("Entered Loading State");

	}

	public override void UpdateState()
	{
		//add some loading screen shenanigans before this
		m_gameStateManager.ChangeGameState(Enums.GameStateNames.GS_03_INPLAY);
	}

	public override void ExitState(Enums.GameStateNames p_nextState)
	{
		//Managers.GetInstance().GetLoadManager().SpawnPlayer();
		//Managers.GetInstance().GetLoadManager().SpawnCamera();
		//Managers.GetInstance().GetGUIManager().LoadGameGUI();
	}
}