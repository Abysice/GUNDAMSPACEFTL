using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TurretFire : NetworkBehaviour {

    #region Public Variables
    public int m_fireRate;
    public int m_speed;
    public GameObject m_bullet;
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables

    #endregion

    #region Accessors
    #endregion

    #region Unity Defaults
    // Use this for initialization
    void Start () {

        //m_projectile = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasAuthority)
            return;

        if (Input.GetMouseButton(0))
        {
            GameObject l_projectile = (GameObject)Instantiate(m_bullet,transform.position,transform.rotation);
            Rigidbody2D l_rb = l_projectile.GetComponent<Rigidbody2D>();
            l_rb.velocity = transform.TransformDirection(Vector2.up*m_speed);
        }
	
	}
    #endregion

    #region Public Methods
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
