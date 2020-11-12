using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

interface IMove
{
    UniTaskVoid MoveRoutine(CancellationToken token);
}
