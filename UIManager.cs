using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesIMG;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score:  " + 0;
        _gameOverText.text = "";
        _restartText.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore(int score)
    {
        _scoreText.text = "Score:  " + score;
    }
    public void UpdateLives(int lives)
    {
        _livesIMG.sprite = _liveSprites[lives];
    }
    public void GameOver()
    {
        _gameOverText.text = "GAME OVER";
        _restartText.text = "PRESS R KEY TO RESTART";
        StartCoroutine(GameOverflickr());
    }
    private IEnumerator GameOverflickr()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.75f);
            if(_gameOverText.text == "")
            {
                _gameOverText.text = "GAME OVER";
            }
            else
            {
                _gameOverText.text = "";
            }
        }
       // if (Input.GetKeyDown(KeyCode.R) && Time.time > nextFire)
       // {
       //     Restart();
        //}
    }



}
