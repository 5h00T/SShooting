using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ShotPosition : MonoBehaviour
{
    /// <summary>
    /// angle度の方向に1発
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="angle"></param>
    protected void Pattern1(Bullet bullet, float speed, float angle)
    {
        Vector3 bulletAngle = new Vector3(0, 0, angle);
        Bullet bulletClone = Instantiate(bullet, transform.position, Quaternion.Euler(bulletAngle));
        bulletClone.Speed = speed;
        // BulletArray.Add(obj);
    }

    /// <summary>
    /// centerAngle度を中心として間隔をintervalAngle度のnWay弾
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="nWay"></param>
    /// <param name="intervalAngle"></param>
    /// <param name="centerAngle"></param>
    /// <param name="speed"></param>
    protected void Pattern2(Bullet bullet, int nWay, float intervalAngle, float centerAngle, float speed)
    {
        float angle = centerAngle - nWay * intervalAngle / 2 + intervalAngle / 2;

        for (int i = 0; i < nWay; i++)
        {
            Vector3 bulletAngle = new Vector3(0, 0, angle);
            Bullet bulletClone = Instantiate(bullet, transform.position, Quaternion.Euler(bulletAngle));
            bulletClone.Speed = speed;
            angle += intervalAngle;
        }
    }
}

