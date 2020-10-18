using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacker
{
    void Attack();
    void Attack(IDamagable target);
    bool InRange();
    void SetTarget(IDamagable target);
    IDamagable GetTarget();

}
