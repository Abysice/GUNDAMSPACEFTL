using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EnemyBulletBehaviour : BulletBehaviour {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ship")
		{
			if (isServer)
			{
				IDamageable L_target = (IDamageable)other.GetComponent(typeof(IDamageable));
				L_target.Damage(m_damagePoints);
				NetworkServer.Destroy(gameObject);
			}
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
