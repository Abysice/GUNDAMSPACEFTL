﻿// Controller class for the ship, will be complicated
//
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShipController : NetworkBehaviour, IEnterable {

    #region Public Variables
    public float m_forceMultiplier = 40000f;
    public float m_torqueMultiplier = 400000f;
	public float m_rotationSpeed = 0.5f;
    public float m_MaxSpeed = 7f;
    public float m_MaxTorque = 5f;
    public Vector2 m_velocity;
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables
    private Vector2 m_direction;
    private Rigidbody2D m_ship_RigidBody;	
	private GameObject m_PlayerCamera;
	private CameraController m_camCont;
	private NetworkIdentity m_id;
	[SyncVar]
	private bool m_readyForControl = true;

	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
        GameObject l_parralax = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().parallaxPrefab);
        l_parralax.GetComponent<ScrollOffset>().m_ship = transform;
		Managers.GetInstance().GetPlayerManager().m_ship = gameObject;
        m_ship_RigidBody = GetComponent<Rigidbody2D>();
        m_direction = new Vector2(0, 0);
		m_PlayerCamera = Managers.GetInstance().GetGameStateManager().GetPlayerCamera();
		m_camCont = m_PlayerCamera.GetComponent<CameraController>();
		m_id = gameObject.GetComponent<NetworkIdentity>();
        
	}
	//runs every frame
	public void Update()
	{
        // will need to be given AssignClientAuthority by the server before you can control
        if (!hasAuthority)
            return;

		if (isServer && m_id.clientAuthorityOwner == null) //prevent server default authority bug
			return;

		if (m_readyForControl)
			HandleMovement();
    }

    #endregion

    #region Public Methods

	public void OnControlled()
	{

	}

	public void OnUnControlled()
	{

	}

	public void ServerPowerDown()
	{
		m_readyForControl = false;
	}

	public void ServerFinishPowerDown()
	{
		m_readyForControl = true;
	}

    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
	private void HandleMovement()
	{
		//Camera stuff
		m_PlayerCamera.GetComponent<CameraController>().m_camSize = 30;

		//rotate towards mouse
		Vector3 l_mpos = Input.mousePosition;
		l_mpos = Camera.main.ScreenToWorldPoint(l_mpos);
		l_mpos = l_mpos - transform.position;

		float l_angle = Mathf.Atan2(l_mpos.y, l_mpos.x) * Mathf.Rad2Deg;

		Quaternion l_rot = Quaternion.AngleAxis(l_angle - 90.0f, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, l_rot, m_rotationSpeed);
	
		m_direction = Vector2.zero;

		if (Input.GetKey(KeyCode.W))
			m_direction.y = 1;
		else if (Input.GetKey(KeyCode.S))
			m_direction.y = -1;
		if (Input.GetKey(KeyCode.D))
			m_direction.x = 1;
		else if (Input.GetKey(KeyCode.A))
			m_direction.x = -1;
		
		m_ship_RigidBody.AddForce(transform.up * m_forceMultiplier * m_direction.y);
		m_ship_RigidBody.AddForce(transform.right * m_forceMultiplier * m_direction.x);
		//m_ship_RigidBody.AddTorque(-m_direction.x * m_torqueMultiplier);
		m_ship_RigidBody.velocity = Vector2.ClampMagnitude(m_ship_RigidBody.velocity, m_MaxSpeed);
		//m_ship_RigidBody.angularVelocity = Mathf.Clamp(m_ship_RigidBody.angularVelocity, -m_MaxTorque, m_MaxTorque);

		}



    #endregion
}
