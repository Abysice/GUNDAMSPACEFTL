/// Placeholder enemy ship controller
///
/// Written By: Adam Bysice + Bryan Ramoul

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class EnemyShipController : NetworkBehaviour, IDamageable {

    #region Public Variables
    public int MAX_HP = 100;
    [SyncVar] public int m_currentHP;
    public Image m_HealthBar;
	public float TURRET_TRACKINGSPEED = 3.0f;
	public float m_speed = 15.0f;
	public float m_fireRate = 1.0f;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private GameObject m_ship;
	private GameObject[] m_cannons;
	private float m_lastshotTime;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults

	void Awake()
	{
	}
	void Start()
	{
        m_currentHP = MAX_HP;
		m_ship = Managers.GetInstance().GetPlayerManager().m_ship;
		m_cannons = new GameObject[2];
		m_cannons[0] = transform.GetChild(0).gameObject;
		m_cannons[1] = transform.GetChild(1).gameObject;
		m_lastshotTime = Time.time;
    }

	// Update is called once per frame
	void Update()
	{
		//rotate turrets towards player ship
		foreach (GameObject cannon in m_cannons)
		{
			Vector3 l_mpos = m_ship.transform.position;
			l_mpos = l_mpos - cannon.transform.position;
			float m_angle = Mathf.Atan2(l_mpos.y, l_mpos.x) * Mathf.Rad2Deg;
			Quaternion l_rot = Quaternion.AngleAxis(m_angle - 90.0f, Vector3.forward);
			cannon.transform.rotation = Quaternion.RotateTowards(cannon.transform.rotation, l_rot, TURRET_TRACKINGSPEED);
		}

		if (isServer)
		{
			if (m_currentHP <= 0)
				NetworkServer.Destroy(gameObject);
			
			if (Time.time > m_lastshotTime)
			{
				m_lastshotTime = Time.time + m_fireRate;
				//shoot code here
				foreach (GameObject cannon in m_cannons)
				{
					GameObject l_bullet = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().enemyBullet, cannon.transform.position, cannon.transform.rotation);
					Vector2 l_velocity = cannon.transform.TransformDirection(Vector2.up * m_speed);
					l_bullet.GetComponent<Rigidbody2D>().velocity = l_velocity;
					NetworkServer.Spawn(l_bullet);
				}
			}

			//test movementcode
			transform.RotateAround(m_ship.transform.position, Vector3.forward, 2 * Time.deltaTime);
			
		}
		m_HealthBar.fillAmount = (float)m_currentHP / (float)MAX_HP;
	}

    #endregion

    #region Public Methods
    public void Damage(int damageTaken)
    {
        m_currentHP -= damageTaken;
        
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
	//returns -1 when to the left, 1 to the right, and 0 for forward/backward
	private int AngleDir(Vector3 p_fwd, Vector3 p_targetDir, Vector3 p_up)
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
