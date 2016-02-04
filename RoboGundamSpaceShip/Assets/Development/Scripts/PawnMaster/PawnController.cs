using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class PawnController : NetworkBehaviour {
	#region Public Variables
	public float PLAYER_MOVE_MULTIPLIER = 0.5f; //make a const later
	public float CAMERA_LERP_MULTIPLIER = 1.0f;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	[SyncVar] private Vector2 m_playerPosition; 
	[SyncVar] private Vector2 m_moveVec;
	private Vector2 m_oldinput;
	private GameObject m_PlayerCamera;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		transform.parent = Managers.GetInstance().GetPlayerManager().m_ship.transform;
		m_playerPosition = transform.localPosition;
		//spawn local Camera
		m_PlayerCamera = Managers.GetInstance().GetPlayerManager().GetPlayerCamera();
		m_PlayerCamera.transform.position = transform.position;
		m_PlayerCamera.transform.parent = transform.parent;
	}

	//this is mostly placeholder code
	public void Update()
	{
		if(isServer)
			UpdateMovePosition();//set the player's movement direction depending on their input

		if (!isLocalPlayer)
		{
			transform.localPosition = Vector2.MoveTowards(transform.localPosition, m_playerPosition, PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
			return;
		}
		
		DoLocalMovement();

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
	//set the player's movement on the server
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

	//preform local movement for clients before sending info to the server
	private void DoLocalMovement()
	{
		m_oldinput = m_moveVec;
		
		//DO SOME RAYCAST SHIT HERE
		m_moveVec = Vector2.zero;

		if (Input.GetKey(KeyCode.UpArrow))
			m_moveVec = new Vector2(0, 1);
		if (Input.GetKey(KeyCode.DownArrow))
			m_moveVec = new Vector2(0, -1);
		if (Input.GetKey(KeyCode.RightArrow))
			m_moveVec = new Vector2(1, m_moveVec.y);
		if (Input.GetKey(KeyCode.LeftArrow))
			m_moveVec = new Vector2(-1, m_moveVec.y);
		
		

		RaycastHit2D hit = Physics2D.Raycast(transform.position, m_moveVec, 0.8f);
		if (hit.collider != null)
		{
			//Debug.Log("HIT DISTANCE " + hit.distance + "NORMAL : " + hit.normal);
			m_moveVec = Vector2.zero;
		}

		//send input vec to server
		if (m_oldinput != m_moveVec || hit.collider != null)
			CmdUpdateInput(m_moveVec);

		Vector2 l_localPos = transform.localPosition;
		if (m_moveVec.y == 1)
			l_localPos += (new Vector2(0, 1) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		else if (m_moveVec.y == -1)
			l_localPos += (new Vector2(0, -1) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		if (m_moveVec.x == 1)
			l_localPos += (new Vector2(1, 0) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		else if (m_moveVec.x == -1)
			l_localPos += (new Vector2(-1, 0) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);


		transform.localPosition = Vector2.MoveTowards(transform.localPosition, l_localPos, PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		m_PlayerCamera.transform.position = Vector2.Lerp(m_PlayerCamera.transform.position, transform.position, CAMERA_LERP_MULTIPLIER * Time.deltaTime);
	}
	#endregion
}
