using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    #region Public Variables
    public float m_deathDelay;
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
        Destroy(gameObject, m_deathDelay);
	}

	// Update is called once per frame
	void Update()
	{

	}

	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

}
