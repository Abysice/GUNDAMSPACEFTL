// This class will handle GUI events and junk
//
//

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class GUIManager : MonoBehaviour {


    #region Public Variables
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables
	private static GameObject m_mainMenuObject;
    #endregion

    #region Accessors
    #endregion

    #region Unity Defaults
    //initialization
    public void Start()
    {

    }
    //runs every frame
    public void Update()
    {

    }
    #endregion

    #region Public Methods
	//spawn the main menu into the scene
	public void LoadMainMenu()
	{
		m_mainMenuObject = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().MainMenu);
	}

	//called by the gui to quit the game
	public void QuitGameButton()
	{
		Debug.Log("Quittin");
		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
