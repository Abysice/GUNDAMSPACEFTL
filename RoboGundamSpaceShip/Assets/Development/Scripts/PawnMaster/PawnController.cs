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
	private GameObject m_enterable;
	[SyncVar] private Enums.PlayerStateNames m_playerState;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_playerState = Enums.PlayerStateNames.PS_01_IDLE;
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
		if (m_playerState == Enums.PlayerStateNames.PS_01_IDLE) //normal walking around
		{
			if (isServer)
				UpdateMovePosition();//set the player's movement direction depending on their input

			if (!isLocalPlayer)
			{
				transform.localPosition = Vector2.MoveTowards(transform.localPosition, m_playerPosition, PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
				return;
			}

			DoLocalMovement();
		}
		//request to enter/unenter an object
		if (Input.GetKeyDown(KeyCode.E))
		{
			if(m_enterable)
			{
				//send request to server asking to get in or out
				CmdRequestToEnter();
			}
		}

	}
	#endregion

	#region Public Methods
	public void UpdateEnterable(GameObject p_actor)
	{
		if(!isServer)
			return;
		
		m_enterable = p_actor;
		
		if (m_enterable == null)
			RpcClearEnterable();
		else
			RpcUpdateEnterable(p_actor.GetComponent<NetworkIdentity>().netId);
	}
	
	//update "enterable" for local players
	[ClientRpc]
	public void RpcUpdateEnterable(NetworkInstanceId p_id)
	{
		Debug.Log("Updating the enterable");
		m_enterable = ClientScene.FindLocalObject(p_id);
	}
	//clear any "enterable" objects for the client
	[ClientRpc]
	public void RpcClearEnterable()
	{
		Debug.Log("Clearing the enterable");
		m_enterable = null;
	}

	[Command]
	public void CmdRequestToEnter()
	{
		//change ownership of the ship
		if (m_playerState == Enums.PlayerStateNames.PS_01_IDLE)
		{
			bool butts = Managers.GetInstance().GetPlayerManager().m_ship.GetComponent<NetworkIdentity>().AssignClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToClient);
			Debug.Log("dick docked");
			//tell everyone to change the player state
			m_playerState = Enums.PlayerStateNames.PS_02_CAPTAIN; //HARD CODED TO CAPTAIN
		}
		else
		{
			bool dicks = Managers.GetInstance().GetPlayerManager().m_ship.GetComponent<NetworkIdentity>().RemoveClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToClient);
			Debug.Log("dick undocked");
			m_playerState = Enums.PlayerStateNames.PS_01_IDLE;
		}

	}
	//send update of input to the server
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
