using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1ShotPosition : ShotPosition
{
    [SerializeField]
    private Bullet bullet;

    public void Shot()
    {
        Pattern2(bullet, 3, 10, 180, 1);
    }
}
