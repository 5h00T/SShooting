using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextView : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;
    public TextMeshProUGUI ScoreText
    {
        get { return _scoreText; }
    }

    public void Initialize()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void OnValueChanged(int value)
    {
        _scoreText.text = $"Score:{value}";
    }
}
