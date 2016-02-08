using UnityEngine;
using System.Collections;

public class TurretBase: IDamageable<int>, IKillable, IRepairable<int>{

    public int HP;
    public int fireDamage;
    public float fireRate;
    
    
    public virtual void Fire()
    {

    }

    public void Damage(int damageTaken)
    {

    }

    public void Repair()
    {

    }

    public void FixDamage(int repairAmount)
    {

    }

    public void Kill()
    {

    }
}
