using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameUIPresenter : MonoBehaviour
{
    [SerializeField]
    private ScoreTextView scoreTextView;

    public void Initialize()
    {
        scoreTextView.Initialize();

        Bind();
    }

    private void Bind()
    {
        ScoreManager.GetInstance().Score
            .Subscribe(value => scoreTextView.OnValueChanged(value));
    }
}
