// Controller class for the ship, will be complicated
//
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShipController : NetworkBehaviour {

    #region Public Variables
    public float forceMultiplier = 10000f;
    public float MaxSpeed = 7f;
    public Vector2 velocity;
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
        CmdUpdateInput(m_direction);

    }

	public void OnStartAuthority()
	{
		Debug.Log("CONTROL STARTED");
	}

    #endregion

    #region Public Methods
    [Command]
    public void CmdUpdateInput(Vector2 p_input)
    {
        m_ship_RigidBody.AddForce(p_input * forceMultiplier);
        m_ship_RigidBody.velocity = Vector2.ClampMagnitude(m_ship_RigidBody.velocity, MaxSpeed);

        velocity = m_ship_RigidBody.velocity;
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
