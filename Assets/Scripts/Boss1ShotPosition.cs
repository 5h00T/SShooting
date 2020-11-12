using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1ShotPosition : ShotPosition
{
    [SerializeField]
    private Bullet bullet;

    public void Shot1()
    {
        Pattern1(bullet, 1f, transform.eulerAngles.z);
    }

    public void Shot2(float angle)
    {
        Pattern1(bullet, 1.4f, angle);
    }
}
