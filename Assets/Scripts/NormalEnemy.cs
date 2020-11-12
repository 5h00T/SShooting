using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public abstract class NormalEnemy : Enemy, IMove, IAttack
{
    public abstract UniTaskVoid MoveRoutine(CancellationToken token);
    public abstract UniTaskVoid AttackRoutine(CancellationToken token);
}