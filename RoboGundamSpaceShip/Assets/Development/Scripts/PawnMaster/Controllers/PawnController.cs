using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class PawnController : NetworkBehaviour {
	#region Public Variables
	public float PLAYER_MOVE_MULTIPLIER = 0.5f; //make a const later
	public float CAMERA_LERP_MULTIPLIER = 1.0f;
	public float CAMERA_ROTATION_DELTA = 1.0f;
	public float ZOOM_OUT_CAM_SIZE = 30.0f;
	public float ZOOM_IN_CAM_SIZE = 5.0f;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	[SyncVar] private Vector2 m_playerPosition; 
	[SyncVar] private Vector2 m_moveVec;
	private Vector2 m_oldinput;
	private GameObject m_PlayerCamera;
	private CameraController m_camCont;
	private EnterAbility m_enterAbility;
	[SyncVar] private bool m_isPiloting;
	#endregion

	#region Accessors
	public bool isPiloting()
	{
		return m_isPiloting;
	}
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_isPiloting = false;
		transform.parent = Managers.GetInstance().GetPlayerManager().m_ship.transform;
		m_playerPosition = transform.localPosition;
		m_enterAbility = gameObject.GetComponent<EnterAbility>();
		// local Camera
		m_PlayerCamera = Managers.GetInstance().GetGameStateManager().GetPlayerCamera();
		m_PlayerCamera.transform.position = transform.position;
		m_PlayerCamera.transform.parent = transform.parent;
		m_camCont = m_PlayerCamera.GetComponent<CameraController>();
	}

	//this is mostly placeholder code
	public void Update()
	{

			if (isServer)
				UpdateMovePosition();//set the player's movement direction depending on their input

			if (!isLocalPlayer)
			{
				transform.localPosition = Vector2.MoveTowards(transform.localPosition, m_playerPosition, PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
				return;
			}
			
			if (!m_isPiloting) //normal walking around
				DoLocalMovement();

			if(m_isPiloting) //while piloting something
			{
			//	m_PlayerCamera.transform.position = Vector2.Lerp(m_PlayerCamera.transform.position, m_enterAbility.m_enterable.transform.position, Time.deltaTime);
			//	m_PlayerCamera.transform.rotation = Quaternion.RotateTowards(m_PlayerCamera.transform.rotation, Quaternion.identity, CAMERA_LERP_MULTIPLIER);
			}
				
	}

	public void FixedUpdate()
	{
		if (!isLocalPlayer)
			return;
			
		if (m_isPiloting) //while piloting something
		{
			if(m_enterAbility.m_enterable)
				m_PlayerCamera.transform.position = Vector2.Lerp(m_PlayerCamera.transform.position, m_enterAbility.m_enterable.transform.position,CAMERA_LERP_MULTIPLIER* Time.deltaTime);
			else
				m_PlayerCamera.transform.position = Vector2.Lerp(m_PlayerCamera.transform.position, gameObject.transform.position, CAMERA_LERP_MULTIPLIER * Time.deltaTime);

			m_PlayerCamera.transform.rotation = Quaternion.RotateTowards(m_PlayerCamera.transform.rotation, Quaternion.identity, CAMERA_LERP_MULTIPLIER);
		}
	}
	#endregion

	#region Public Methods
	[ClientRpc]
	public void RpcSetToPiloting(NetworkInstanceId p_pawn)
	{
		m_isPiloting = true;
		//tell camera to start zooming out, unparent camera from ship
		if(isLocalPlayer)
		{
			//ClientScene.FindLocalObject(p_pawn).GetComponent<SpriteRenderer>().sortingLayerName = Layers.TurretsLayer;
			GameObject l_enterableThing = ClientScene.FindLocalObject(p_pawn);
			IEnterable l_controller = (IEnterable)l_enterableThing.GetComponent(typeof(IEnterable));
			l_controller.OnControlled();
			m_PlayerCamera.transform.parent = l_enterableThing.transform.parent;
		}
	}

	[ClientRpc]
	public void RpcUnpilotPawn()
	{
		m_isPiloting = false;
		if (isLocalPlayer)
		{
			if (m_enterAbility.m_enterable)
			{ 
				GameObject l_enterableThing = m_enterAbility.m_enterable;
				//l_enterableThing.GetComponent<SpriteRenderer>().sortingLayerName = Layers.ShipLayer;
				IEnterable l_controller = (IEnterable)l_enterableThing.GetComponent(typeof(IEnterable));
				l_controller.OnUnControlled();
				m_PlayerCamera.transform.parent = transform.parent;
			}
			else
			{

			}
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
		m_moveVec = Vector2.zero;

		if (Input.GetKey(KeyCode.W))
			m_moveVec = new Vector2(0, 1);
		if (Input.GetKey(KeyCode.S))
			m_moveVec = new Vector2(0, -1);
		if (Input.GetKey(KeyCode.D))
			m_moveVec = new Vector2(1, m_moveVec.y);
		if (Input.GetKey(KeyCode.A))
			m_moveVec = new Vector2(-1, m_moveVec.y);
		
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.rotation*m_moveVec, 0.8f, Layers.PawnColLayer);
		if (hit.collider != null)
		{
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

		m_PlayerCamera.GetComponent<CameraController>().m_camSize = 5;
		transform.localPosition = Vector2.MoveTowards(transform.localPosition, l_localPos, PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		//make camera follow + rotate with the player
		m_PlayerCamera.transform.position = Vector2.Lerp(m_PlayerCamera.transform.position, transform.position, CAMERA_LERP_MULTIPLIER * Time.deltaTime);
		m_PlayerCamera.transform.rotation = Quaternion.RotateTowards(m_PlayerCamera.transform.rotation, transform.rotation, CAMERA_ROTATION_DELTA*10.0f);
	}
	#endregion
}
