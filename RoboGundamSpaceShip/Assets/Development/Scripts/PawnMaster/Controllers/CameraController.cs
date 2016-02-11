// This script controls the camera and moves it around according to the gamestate 
// or the local players status
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour  {

	#region Public Variables
	public float m_camSize;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private GameStateManager m_gman;
	private Camera m_cam;

	private bool m_zooming;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_gman = Managers.GetInstance().GetGameStateManager();
		m_cam = gameObject.GetComponent<Camera>();
		m_camSize = m_cam.orthographicSize;
		DontDestroyOnLoad(this.gameObject);
	}
	//runs every frame
	public void Update()
	{
		if(m_gman.CurrentState == Enums.GameStateNames.GS_01_MENU || m_gman.CurrentState == Enums.GameStateNames.GS_02_LOBBY)
		{
			transform.position = new Vector2(0, 0);
		}
		else if (m_gman.CurrentState == Enums.GameStateNames.GS_04_INPLAY)
		{
			m_cam.orthographicSize = Mathf.Lerp(m_cam.orthographicSize, m_camSize, 0.05f);
		}
	}
	#endregion

	#region Public Methods

	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
