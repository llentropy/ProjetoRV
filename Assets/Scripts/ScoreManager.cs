using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private CharacterSpawner _characterSpawner;

    public DateTime _levelDate = new DateTime(2011, 05, 20);

    private int _score = 0;

    public void AnswerOk()
    {
        CheckCorrect(true);
    }

    public void AnswerNope()
    {
        CheckCorrect(false);
    }
    
    private void CheckCorrect(bool answer)
    {
        bool correct = true;

        var id = IDSpawner.Instance._currentId.GetComponent<CharacterID>();
        int now = int.Parse(_levelDate.ToString("yyyyMMdd"));
        int dob = int.Parse(id.Birthday.ToString("yyyyMMdd"));
        int age = (now - dob) / 10000;
        if (age < 18)
        {
            correct = false;
        }
        if (IDSpawner.Instance._currentId.GetComponent<CharacterID>().isPhotoFake)
        {
            correct = false;
        }


        if(correct == answer)
        {
            _score++;
        }
        else
        {
            _score--;
        }


        _characterSpawner._currentCharacter.GetComponent<Character>().CanEnter(answer);


        _scoreText.text = $"Score: {_score}";

        //Destroy(_characterSpawner._currentCharacter);
        //var character = _characterSpawner.SpawnNewCharacter();
        //_characterSpawner.SpawnNewId(character);
    }
}
