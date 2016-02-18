// Script description goes here.
//
// Written by: Adam Bysice
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MechaController : NetworkBehaviour, IEnterable {

	#region Public Variables
	public float m_forceMultiplier = 400f;
	public float m_torqueMultiplier = 400f;
	public float m_MaxSpeed = 7f;
	public float m_MaxTorque = 5f;
	public GameObject m_entryConsole;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private Vector2 m_direction;
	private SpriteRenderer m_renderer;
	private Rigidbody2D m_rb;
	private GameObject m_PlayerCamera;
	private CameraController m_camCont;
	private Vector2 m_originalLocal;
	private NetworkIdentity m_id;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_renderer = gameObject.GetComponent<SpriteRenderer>();
		m_rb = gameObject.GetComponent<Rigidbody2D>();
		transform.parent = Managers.GetInstance().GetPlayerManager().m_ship.transform;
		m_PlayerCamera = Managers.GetInstance().GetGameStateManager().GetPlayerCamera();
		m_camCont = m_PlayerCamera.GetComponent<CameraController>();
		m_rb.isKinematic = true;
		m_id = gameObject.GetComponent<NetworkIdentity>();
	}
	//runs every frame
	public void Update()
	{
		//sprite junk, fix later
		if (m_rb.velocity.x >= 0.0f)
			m_renderer.flipX = false;
		else
			m_renderer.flipX = true;

		if (!hasAuthority)
			return;

		if (isServer && m_id.clientAuthorityOwner == null) //prevent server default authority bug
			return;

		//set camera zoom while in mecha
		m_camCont.m_camSize = 10;

		m_direction = Vector2.zero;

		if (Input.GetKey(KeyCode.W))
			m_direction.y = 1;
		else if (Input.GetKey(KeyCode.S))
			m_direction.y = -1;
		if (Input.GetKey(KeyCode.D))
			m_direction.x = 1;
		else if (Input.GetKey(KeyCode.A))
			m_direction.x = -1;
		
		m_rb.AddForce(m_forceMultiplier * m_direction);
		m_rb.velocity = Vector2.ClampMagnitude(m_rb.velocity, m_MaxSpeed);
		m_rb.angularVelocity = Mathf.Clamp(m_rb.angularVelocity, -m_MaxTorque, m_MaxTorque);

		transform.rotation = Quaternion.identity;
		
		//rotate towards direction(maybe?)
		//Vector2 v = m_rb.velocity;
		//float l_angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
		//transform.rotation = Quaternion.AngleAxis(l_angle, Vector3.forward);

	}

	
	#endregion

	#region Public Methods
	public void OnControlled()
	{
		CmdRequestToSetupMecha();
	}

	public void OnUnControlled()
	{
		CmdRequestToResetMecha();
	}

	//ask the server to setup the mecha
	[Command]
	public void CmdRequestToSetupMecha()
	{
		RpcSetupMecha();
	}
	//ask the server to reset the mecha
	[Command]
	public void CmdRequestToResetMecha()
	{
		RpcResetMecha();
	}

	//tell all clients to setup the mecha
	[ClientRpc]
	public void RpcSetupMecha()
	{
		m_rb.isKinematic = false;
		transform.parent = transform.parent.parent;
		transform.rotation = Quaternion.identity;
	}

	//tell all clients to reset the mecha
	[ClientRpc]
	public void RpcResetMecha()
	{
		m_rb.isKinematic = true;
		transform.parent = Managers.GetInstance().GetPlayerManager().m_ship.transform;
		transform.rotation = Managers.GetInstance().GetPlayerManager().m_ship.transform.rotation;
	}

	//initial position setup
	[ClientRpc]
	public void RpcSetPosition(Vector3 p_pos)
	{
		gameObject.transform.position = p_pos;
		m_originalLocal = gameObject.transform.localPosition;
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
