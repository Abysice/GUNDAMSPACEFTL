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
		transform.GetChild(0).parent = Managers.GetInstance().GetPlayerManager().m_ship.transform;
		m_PlayerCamera = Managers.GetInstance().GetPlayerManager().GetPlayerCamera();
		m_camCont = m_PlayerCamera.GetComponent<CameraController>();
		m_rb.isKinematic = true;
	}
	//runs every frame
	public void Update()
	{
		if (!hasAuthority)
		{
			return;
		}
		//set camera zoom while in mecha
		m_PlayerCamera.GetComponent<CameraController>().m_camSize = 10;

		if (Input.GetKey(KeyCode.W))
		{
			m_direction.y = 1;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			m_direction.y = -1;
		}
		else
		{
			m_direction.y = 0;
		}
		if (Input.GetKey(KeyCode.D))
		{
			m_direction.x = 1;
		}
		else if (Input.GetKey(KeyCode.A))
		{
			m_direction.x = -1;
		}
		else
		{
			m_direction.x = 0;
		}
		
		m_rb.AddForce(m_forceMultiplier * m_direction);
		m_rb.velocity = Vector2.ClampMagnitude(m_rb.velocity, m_MaxSpeed);
		m_rb.angularVelocity = Mathf.Clamp(m_rb.angularVelocity, -m_MaxTorque, m_MaxTorque);
	
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
	}

	//tell all clients to reset the mecha
	[ClientRpc]
	public void RpcResetMecha()
	{
		m_rb.isKinematic = true;
		transform.parent = Managers.GetInstance().GetPlayerManager().m_ship.transform;
	}

	//initial position setup
	[ClientRpc]
	public void RpcSetPosition(Vector3 p_pos)
	{
		gameObject.transform.position = p_pos;
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
