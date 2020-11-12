using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UniRx;

/// <summary>
/// PlayerとEnemyの基底クラス
/// </summary>
public abstract class GameAircraft : MonoBehaviour
{
    [SerializeField]
    protected FloatReactiveProperty HP { get; set; }
}
