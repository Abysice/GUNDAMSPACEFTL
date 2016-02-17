using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FixedRodation : NetworkBehaviour {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
    private Quaternion m_rotation;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults

	void Awake()
	{
        m_rotation = transform.rotation;
    }
	void Start()
	{
		
	}

	// Update is called once per frame
	void LateUpdate()
	{
        transform.rotation = m_rotation;
    }

	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

}
