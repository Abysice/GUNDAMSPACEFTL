﻿// Controller class for the ship, will be complicated
//
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShipController : NetworkBehaviour {

    #region Public Variables
    public float m_forceMultiplier = 40000f;
    public float m_torqueMultiplier = 400000f;
    public float m_MaxSpeed = 7f;
    public float m_MaxTorque = 5f;
    public Vector2 m_velocity;
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables
    private Vector2 m_direction;
    private Rigidbody2D m_ship_RigidBody;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
        GameObject l_parralax = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().parallaxPrefab);
        l_parralax.GetComponent<ScrollOffset>().m_ship = this.transform;
		Managers.GetInstance().GetPlayerManager().m_ship = gameObject;
        m_ship_RigidBody = GetComponent<Rigidbody2D>();
        m_direction = new Vector2(0, 0);
     
	}
	//runs every frame
	public void Update()
	{
        // will need to be given AssignClientAuthority by the server before you can control
		if (!hasAuthority)
        {
            return;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_direction.y = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            m_direction.y = -1;
        }
        else
        {
            m_direction.y = 0;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_direction.x = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_direction.x = -1;
        }
        else
        {
            m_direction.x = 0;
        }
        //CmdUpdateInput(m_direction);
		m_ship_RigidBody.AddForce(transform.up* m_forceMultiplier*m_direction.y);
        m_ship_RigidBody.AddTorque(-m_direction.x * m_torqueMultiplier);
        m_ship_RigidBody.velocity = Vector2.ClampMagnitude(m_ship_RigidBody.velocity, m_MaxSpeed);
        m_ship_RigidBody.angularVelocity = Mathf.Clamp(m_ship_RigidBody.angularVelocity, -m_MaxTorque, m_MaxTorque);


		m_velocity = m_ship_RigidBody.velocity;
    }

    #endregion

    #region Public Methods
    [Command]
    public void CmdUpdateInput(Vector2 p_input)
    {
        m_ship_RigidBody.AddForce(p_input * m_forceMultiplier);
        m_ship_RigidBody.velocity = Vector2.ClampMagnitude(m_ship_RigidBody.velocity, m_MaxSpeed);

        m_velocity = m_ship_RigidBody.velocity;
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
