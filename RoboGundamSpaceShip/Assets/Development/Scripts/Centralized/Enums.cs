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
		GS_02_SERVERLOBBY = 1,
		GS_03_CLIENTLOBBY = 2,
        GS_04_LOADING, 
        GS_05_INPLAY,
        GS_06_CLEANUP
    };

}
