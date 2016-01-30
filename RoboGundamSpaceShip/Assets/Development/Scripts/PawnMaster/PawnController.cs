using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class PawnController : NetworkBehaviour {
	#region Public Variables
	public float PLAYER_MOVE_MULTIPLIER = 5.0f; //make a const later
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	[SyncVar] private Vector2 m_playerPosition; //synchronized over the network
	private GameObject m_PlayerCamera;
	private Rigidbody2D m_rb;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_playerPosition = transform.position;
		//spawn local Camera
		m_PlayerCamera = Managers.GetInstance().GetPlayerManager().GetPlayerCamera();
		m_rb = gameObject.GetComponent<Rigidbody2D>();
	}
	//runs every frame
	public void FixedUpdate()
	{
		if (!isLocalPlayer) //is this my player? return if its not mine
		{
			return;
		}
		
		//placeholder movement
		if (Input.GetKey(KeyCode.UpArrow))
			m_playerPosition += (new Vector2(0, 1) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		else if (Input.GetKey(KeyCode.DownArrow))
			m_playerPosition += (new Vector2(0, -1) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		if (Input.GetKey(KeyCode.RightArrow))
			m_playerPosition += (new Vector2(1, 0) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);
		else if (Input.GetKey(KeyCode.LeftArrow))
			m_playerPosition += (new Vector2(-1, 0) * PLAYER_MOVE_MULTIPLIER * Time.deltaTime);

		m_rb.MovePosition(m_playerPosition);
		//transform.position = m_playerPosition;
		m_PlayerCamera.transform.position = Vector2.Lerp(m_PlayerCamera.transform.position, m_playerPosition, PLAYER_MOVE_MULTIPLIER);
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
