using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WindManager : NetworkBehaviour {

    #region Public Variables
    public float m_windForce = 4000f;
    [SyncVar]
    public Vector2 m_windDirection; 
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults

	void Awake()
	{
	}
	void Start()
	{
        m_windDirection = new Vector2(0, 0);
        Managers.GetInstance().SetWindManager(this);

    }

	// Update is called once per frame
	void Update()
	{
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_windDirection = new Vector2(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_windDirection = new Vector2(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_windDirection = new Vector2(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_windDirection = new Vector2(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            m_windDirection = new Vector2(0, 0);
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
