using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<MyType> Tiles;
    public static GameManager Instance;

    public enum Players
    {
        Player_1,
        Player_2
    }

    public TMP_Text currentPlayer_Text;
    public Players currentPlayer;

    private void Start()
    {
        Instance = this;
        restartButtonMAIN.onClick.AddListener(() => { SceneManager.LoadScene(0); });
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        currentPlayer_Text.text = currentPlayer == Players.Player_1 ? "Player 1 Turn" : "Player 2 Turn";

        if (!isGameOver)
            Checker();
    }

    public Image BackGround;
    public Color player1Color;
    public Color player2Color;

    public void SetPlayerTurn()
    {
        currentPlayer = currentPlayer == Players.Player_1 ? Players.Player_2 : Players.Player_1;

        BackGround.color = currentPlayer == Players.Player_1 ? player1Color : player2Color;
    }

    private void Checker()
    {
        CheckColumnWin(9, 3, 1, 2, "Column");
        CheckColumnWin(3, 1, 3, 6, "Row");
        CheckDiagonal();
        CheckForDraw();
    }

    private void CheckForDraw()
    {
        var test = false;
        foreach (var tile in Tiles)
        {
            if (tile.Selection != MyType.mySelection.Default)
            {
                test = true;
            }
            else
            {
                test = false;
                break;
            }
        }

        if (test)
        {
            OnGameOver("DRAW");
        }
    }

    private void CheckColumnWin(int a, int b, int c, int d, String type)
    {
        for (var i = 0; i < a; i += b)
        {
            if (Tiles[i].Selection == MyType.mySelection.Default)
                continue;

            if (Tiles[i].Selection == Tiles[i + c].Selection &&
                Tiles[i + c].Selection == Tiles[i + d].Selection)
            {
                OnGameOver(Tiles[i].Selection == MyType.mySelection.x
                    ? $"Player 1 won by {type}"
                    : $"Player 2 won by {type}");
            }
        }
    }

    private void CheckDiagonal()
    {
        DiagonalChecker(0, 4, 8);
        DiagonalChecker(2, 4, 6);
    }

    private void DiagonalChecker(int a, int b, int c)
    {
        {
            if (Tiles[a].Selection == MyType.mySelection.Default)
                return;

            if (Tiles[a].Selection == Tiles[b].Selection &&
                Tiles[b].Selection == Tiles[c].Selection)
            {
                OnGameOver(Tiles[a].Selection == MyType.mySelection.x
                    ? "Player 1 won by diagonal"
                    : "Player 2 won by diagonal");
            }
        }
    }

    public GameObject gameOverPanel;
    public TMP_Text GameWinText;
    public Button restartButton;
    public Button restartButtonMAIN;
    public bool isGameOver;

    private void OnGameOver(string winPlayer)
    {
        isGameOver = true;
        restartButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
        gameOverPanel.SetActive(true);
        GameWinText.text = winPlayer;
        foreach (var VARIABLE in Tiles)
        {
            VARIABLE.GetComponent<Button>().interactable = false;
        }
    }
}