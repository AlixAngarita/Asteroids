using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class GameController : MonoBehaviour {
 
    public GameObject asteroid;
 
    private int score;
    private int highestScore;
    private int asteroidsRemaining;
    private int lives;
    private int wave;
    private int increaseEachWave = 4;
 
    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public Text highestScoreText;
    public GameObject GameOverPanel;
    public GameObject player;
 
    void Start () {
        StartGame();
    }
 
    void Update () {
 
        // Quit with ESC
        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
 
    }

    public void play()
    {
        highestScore = PlayerPrefs.GetInt ("highestScore", 0);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        StartGame();
    }
 
    public void StartGame(){
 
        score = 0;
        lives = 3;
        wave = 1;

        if (GameOverPanel != null)
            GameOverPanel.SetActive(false);
        if (player != null)
            player.SetActive(true);
        SpawnAsteroids();
        
        //HUD
        if (waveText != null){
            scoreText.text = "SCORE: " + score;
            highestScoreText.text = "HIGH SCORE: " + highestScore;
            livesText.text = "LIVES: " + lives;
            waveText.text = "WAVE: " + wave;
        }
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
 
    void SpawnAsteroids(){
 
        DestroyExistingAsteroids();
        asteroidsRemaining = (wave * increaseEachWave);
 
        for (int i = 0; i < asteroidsRemaining; i++) {
            // Spawn an asteroid
            Instantiate(asteroid, new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-5.0f, 5.0f), 0), Quaternion.Euler(0,0,Random.Range(-0.0f, 359.0f)));
        }
        if (waveText != null)
            waveText.text = "WAVE: " + wave;
    }
 
    public void IncrementScore(){
        score++;
        scoreText.text = "SCORE: " + score;
        if (score > highestScore) {
            highestScore = score;
            highestScoreText.text = "HIGH SCORE: " + highestScore;
 
            // Save the new highestScore
            PlayerPrefs.SetInt ("highestScore", highestScore);
            
        }
 
        //Next wave if all asteroids are cleared
        if (asteroidsRemaining < 1) {
            wave++;
            SpawnAsteroids();
        }
    }
 
    public void DecrementLives(){
        lives--;
        livesText.text = "LIVES: " + lives;
 
        // Has player run out of lives?
        if (lives < 1) {
            // Restart the game
            //StartGame();
            GameOver();
        }
    }

    private void GameOver()
    {
        player.SetActive(false);
        GameOverPanel.SetActive(true);
    }
 
    public void DecrementAsteroids(){
        asteroidsRemaining--;
    }
 
    public void SplitAsteroid(){
        // Two extra asteroids
        // - big one
        // + 3 little ones
        // = 2
        asteroidsRemaining+=2;
    }
 
    void DestroyExistingAsteroids(){
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Large Asteroid");
 
        foreach (GameObject current in asteroids) {
            GameObject.Destroy (current);
        }
 
        GameObject[] asteroids2 = GameObject.FindGameObjectsWithTag("Small Asteroid");
 
        foreach (GameObject current in asteroids2) {
            GameObject.Destroy (current);
        }
    }
 
}