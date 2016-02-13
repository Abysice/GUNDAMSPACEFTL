using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TurretFireAbility : NetworkBehaviour {

    #region Public Variables
    public float m_fireRate;
    public int m_speed;
    public GameObject m_bullet;
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables
    private Transform m_spawnLoacation;
    private float m_nextFire;
    #endregion

    #region Accessors
    #endregion

    #region Unity Defaults
    // Use this for initialization
    void Start () {

        m_spawnLoacation = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasAuthority)
            return;
        if (Input.GetMouseButton(0) && Time.time > m_nextFire)
        {
            m_nextFire = Time.time + m_fireRate;
            //GameObject l_projectile = (GameObject)Instantiate(m_bullet, m_spawnLoacation.position, m_spawnLoacation.rotation);
            //Rigidbody2D l_rb = l_projectile.GetComponent<Rigidbody2D>();
            //l_rb.velocity = transform.TransformDirection(Vector2.up*m_speed);
            CmdRequestBullet(m_spawnLoacation.position, m_spawnLoacation.rotation);
        }
	
	}
    #endregion

    #region Public Methods
    [Command]
    public void CmdRequestBullet(Vector2 p_position, Quaternion p_rotation)
    {
        GameObject l_projectile = (GameObject)Instantiate(m_bullet, p_position, p_rotation);
        l_projectile.GetComponent<BulletBehaviour>().m_velocity = transform.TransformDirection(Vector2.up * m_speed);
        NetworkServer.Spawn(l_projectile);

    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
