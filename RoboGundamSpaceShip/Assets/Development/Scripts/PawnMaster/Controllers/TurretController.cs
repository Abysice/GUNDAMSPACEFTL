// this is the controller for the turrets
//
// Written by: Adam Bysice
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TurretController : NetworkBehaviour, IEnterable {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private SpriteRenderer m_renderer;
	private CameraController m_camCont;
	private GameObject m_PlayerCamera;
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
	}
	//runs every frame
	public void Update()
	{
		if (!hasAuthority)
			return;

		m_PlayerCamera.GetComponent<CameraController>().m_camSize = 30;
		//rotate the turrets towards the mouse
		Vector3 l_mpos = Input.mousePosition;
		l_mpos = Camera.main.ScreenToWorldPoint(l_mpos);
		l_mpos = l_mpos - transform.position;
		float angle = Mathf.Atan2(l_mpos.y, l_mpos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle-90.0f, Vector3.forward);
		

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
	public void RpcSetPosition(Vector3 p_pos)
	{
		gameObject.transform.position = p_pos;
		transform.parent = GameObject.Find("ShipPrefab(Clone)").transform;
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
