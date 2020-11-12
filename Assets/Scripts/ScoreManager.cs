using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

public sealed class ScoreManager
{
    private static ScoreManager scoreManager = new ScoreManager();

    public IntReactiveProperty Score { get; set; }

    private ScoreManager()
    {
        Score = new IntReactiveProperty();
    }

    public static ScoreManager GetInstance()
    {
        return scoreManager;
    }


}
