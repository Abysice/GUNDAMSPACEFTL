// this is the controller for the turrets
//
// Written by: Adam Bysice
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TurretController : NetworkBehaviour, IEnterable {

	#region Public Variables
	public float TRACKING_SPEED = 5.0f;

	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private SpriteRenderer m_renderer;
	private CameraController m_camCont;
	private GameObject m_PlayerCamera;
	private GameObject m_ship;
	private bool m_rightSide = false;
	[SyncVar]
	private float m_angle = 90.0f;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_renderer = gameObject.GetComponent<SpriteRenderer>();
		m_PlayerCamera = Managers.GetInstance().GetGameStateManager().GetPlayerCamera();
		m_camCont = m_PlayerCamera.GetComponent<CameraController>();
		m_ship = Managers.GetInstance().GetPlayerManager().m_ship;
		
		Vector2 l_shipPos = m_ship.transform.position;
		Vector2 l_turretPos = gameObject.transform.position;
		//figure out which side your on
		if (l_shipPos.x < l_turretPos.x)
			m_rightSide = true;
		else
			m_rightSide = false;
	}
	//runs every frame
	public void Update()
	{
		if(isServer)
		{
			if(gameObject.GetComponent<NetworkIdentity>().clientAuthorityOwner == null)
				transform.rotation = Quaternion.RotateTowards(transform.rotation, m_ship.transform.rotation, TRACKING_SPEED);
		}

		if (!hasAuthority)
		{
			Quaternion l_rotate = Quaternion.AngleAxis(m_angle - 90.0f, Vector3.forward);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, l_rotate, TRACKING_SPEED);
			return;
		}
		
		m_PlayerCamera.GetComponent<CameraController>().m_camSize = 30;
		//rotate the turrets towards the mouse
		Vector3 l_mpos = Input.mousePosition;
		l_mpos = Camera.main.ScreenToWorldPoint(l_mpos);
		l_mpos = l_mpos - transform.position;
	
		//returns -1 when to the left, 1 to the right
		int l_ret = AngleDir(l_mpos, m_ship.transform.up, m_PlayerCamera.transform.forward);
		if ((m_rightSide && l_ret == 1) || (!m_rightSide && l_ret == -1))
			m_angle = Mathf.Atan2(l_mpos.y, l_mpos.x) * Mathf.Rad2Deg;
		
		CmdSetRotation(m_angle);

		Quaternion l_rot = Quaternion.AngleAxis(m_angle - 90.0f, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, l_rot, TRACKING_SPEED);
	}
	#endregion

	#region Public Methods
	public void OnControlled()
	{
	
	}

	public void OnUnControlled()
	{
	
	}


	//initial position setup
	[ClientRpc]
	public void RpcSetup()
	{
		transform.parent = GameObject.Find("ShipPrefab(Clone)").transform;
	}


	[Command(channel = 1)]
	public void CmdSetRotation(float p_rot)
	{
		m_angle = p_rot;
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	//returns -1 when to the left, 1 to the right, and 0 for forward/backward
	public int AngleDir(Vector3 p_fwd, Vector3 p_targetDir, Vector3 p_up)
	{
		Vector3 l_perp = Vector3.Cross(p_fwd, p_targetDir);
		float l_dir = Vector3.Dot(l_perp, p_up);

		if (l_dir > 0.0f)
			return 1;
		else if (l_dir < 0.0f)
			return -1;
		else
			return 0;

	}  
 
	#endregion
}
