//Managers singleton, allows for easy access to all manager 
//classes, also handles initialization
//
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Managers : MonoBehaviour {
	
	#region Public Variables
	//Variables
	private static Managers m_instance = null;
	private GameStateManager m_gamestatemanager = null;
	private NetworkController m_networkcontroller = null;
	private GameProperties m_gameproperties = null;
	private PlayerManager m_playermanager = null;
	//private LoadManager m_loadmanager = null;
	//private InputManager m_inputmanager = null;
	private GUIManager m_guimanager = null;

	//private GameObject m_spawnerObject = null;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	#endregion

	#region Accessors
	//Accessors
	public static Managers GetInstance()
	{
		return m_instance;
	}

	public GameStateManager GetGameStateManager()
	{
		return m_gamestatemanager;
	}

	public NetworkController GetNetworkController()
	{
		return m_networkcontroller;
	}

	public GameProperties GetGameProperties()
	{
		return m_gameproperties;
	}

	public PlayerManager GetPlayerManager()
	{
		return m_playermanager;
	}

	public void SetPlayerManager(PlayerManager p_man)
	{
		m_playermanager = p_man;
	}
	//public InputManager GetInputManager()
	//{
	//	return m_inputmanager;
	//}

	public GUIManager GetGUIManager()
	{
		return m_guimanager;
	}

	#endregion

	#region Unity Defaults

	void Awake()
	{
		m_instance = this;
		//m_spawnerObject = new GameObject(); //create a seperate spawner object
		//m_spawnerObject.transform.parent = this.transform;


	}
	// Use this for initialization
	void Start()
	{
		m_gameproperties = m_instance.GetComponent<GameProperties>();
		m_gamestatemanager = gameObject.AddComponent<GameStateManager>();
		m_networkcontroller = gameObject.GetComponent<NetworkController>();
		//m_playermanager = m_spawnerObject.AddComponent<PlayerManager>();
		//m_inputmanager = gameObject.AddComponent<InputManager>();
		m_guimanager = gameObject.AddComponent<GUIManager>();
		m_gamestatemanager.Init();
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

}
