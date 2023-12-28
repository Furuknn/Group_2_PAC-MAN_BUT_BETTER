using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghosts[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public int score {  get; private set; }
    public int lives { get; private set; }

    void Start()
    {
        NewGame();
    }
    void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
        
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetRound();
    }

    void ResetRound()
    {
      for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(true);
        }
      this.pacman.gameObject.SetActive(true);
    }

    void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
    }

    void SetScore(int score)
    {
        this.score = score;
    } 

    void SetLives(int lives)
    {
        this.lives = lives;
    }

    void GhostEaten(Ghosts ghost)
    {
        SetScore(this.score + ghost.points);
    }

    void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            Invoke(nameof(ResetRound), 3f);
        }else{
            GameOver();
        }
    }

}
