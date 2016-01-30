using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class PawnController : NetworkBehaviour {
	#region Public Variables
	public float PLAYER_MOVE_MULTIPLIER = 5.0f; //make a const later
	public float CAMERA_LERP_MULTIPLIER = 1.0f;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	[SyncVar] private Vector2 m_playerPosition; 
	[SyncVar] private Vector2 m_moveVec;
	private Vector2 m_oldinput;
	private GameObject m_PlayerCamera;
	private Rigidbody2D m_rb;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_playerPosition = transform.position;
		//spawn local Camera
		m_PlayerCamera = Managers.GetInstance().GetPlayerManager().GetPlayerCamera();
		m_rb = gameObject.GetComponent<Rigidbody2D>();
	}

	//this is mostly placeholder code
	public void Update()
	{
		if(isServer)
			UpdateMovePosition();//set the player's movement direction depending on their input

		if (!isLocalPlayer)
		{   // update their pos
			transform.position = Vector2.MoveTowards(transform.position, m_playerPosition, PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
			return;
		}
		
		m_oldinput = m_moveVec;

		if (Input.GetKey(KeyCode.UpArrow))
			m_moveVec = new Vector2(0, 1);
		else if (Input.GetKey(KeyCode.DownArrow))
			m_moveVec = new Vector2(0, -1);
		else if (Input.GetKey(KeyCode.RightArrow))
			m_moveVec = new Vector2(1, 0);
		else if (Input.GetKey(KeyCode.LeftArrow))
			m_moveVec = new Vector2(-1, 0);
		else
			m_moveVec = Vector2.zero;
		
		//send input vec to server
		if (m_oldinput != m_moveVec)
			CmdUpdateInput(m_moveVec);

		Vector2 l_localPos = transform.position;
		if (m_moveVec.y == 1)
			l_localPos += (new Vector2(0, 1) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		else if (m_moveVec.y == -1)
			l_localPos += (new Vector2(0, -1) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		if (m_moveVec.x == 1)
			l_localPos += (new Vector2(1, 0) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		else if (m_moveVec.x == -1)
			l_localPos += (new Vector2(-1, 0) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);

		transform.position = Vector2.MoveTowards(transform.position, l_localPos, PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		m_PlayerCamera.transform.position = transform.position; //smooth camera out later
	}
	#endregion

	#region Public Methods
	[Command]
	public void CmdUpdateInput(Vector2 p_input)
	{
		m_moveVec = p_input;
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	private void UpdateMovePosition()
	{
		if (m_moveVec.y == 1)
			m_playerPosition += (new Vector2(0, 1) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		else if (m_moveVec.y == -1)
			m_playerPosition += (new Vector2(0, -1) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		if (m_moveVec.x == 1)
			m_playerPosition += (new Vector2(1, 0) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		else if (m_moveVec.x == -1)
			m_playerPosition += (new Vector2(-1, 0) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
	}
	#endregion
}
