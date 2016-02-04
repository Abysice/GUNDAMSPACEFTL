//All enums centralized here
//
//

using UnityEngine;
using System.Collections;

public class Enums : MonoBehaviour {

    public enum GameStateNames
    {
        GS_00_NULL = -1,
        GS_01_MENU = 0,
		GS_02_LOBBY = 1,
        GS_03_LOADING, 
        GS_04_INPLAY,
        GS_05_CLEANUP
    };

	public enum PlayerStateNames
	{
		PS_00_NULL = -1,
		PS_01_IDLE = 0,
		PS_02_CAPTAIN = 1,
		PS_03_GUNNER,
		PS_04_PILOT
	};

}
