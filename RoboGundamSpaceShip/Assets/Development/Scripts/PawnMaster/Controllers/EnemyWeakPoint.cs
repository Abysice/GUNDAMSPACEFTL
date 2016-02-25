// Script description goes here.
//
// Written By: Adam Bysice

using UnityEngine;
using System.Collections;

public class EnemyWeakPoint : MonoBehaviour, IDamageable {

	#region Public Variables
	public int m_BonusDamage = 0;
	public int m_DamageMultiplier = 1;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	#endregion

	#region Public Methods
	public void Damage(int damageTaken)
	{
		IDamageable l_target = (IDamageable)transform.parent.GetComponent(typeof(IDamageable));
		if (l_target != null)
			l_target.Damage((damageTaken * m_DamageMultiplier) + m_BonusDamage);

		Debug.Log("MASSIVE DAMAGE");
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
