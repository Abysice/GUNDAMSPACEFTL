using UnityEngine;
using System.Collections;

public class ScrollOffset : MonoBehaviour {

    #region Public Variables
    public Transform m_ship;
    public float m_scrollSpeed = 0.1f;
    public float m_parallax = 20f;
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables
    private MeshRenderer m_mr;
    private Material m_spacePBack;
    private Vector2 m_textureOffset;
    #endregion

    #region Accessors
    #endregion

    #region Unity Defaults
    // Use this for initialization
    void Start () {

        m_mr = GetComponent<MeshRenderer>();
        m_spacePBack = m_mr.material;
		        
	}
	
	// Update is called once per frame
	void Update () {

        m_textureOffset = m_spacePBack.mainTextureOffset;

        m_textureOffset.y = m_ship.position.y/ m_parallax;
        m_textureOffset.x = m_ship.position.x/ m_parallax;
        m_spacePBack.mainTextureOffset = m_textureOffset;

        transform.position = m_ship.position;



    }
    #endregion

    #region Public Methods
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
