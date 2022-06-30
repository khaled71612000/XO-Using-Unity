using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int whoTurn; //0 = x and 1 == o
    public int turnCount; // no of turns played
    public GameObject[] turnIcons; //displays whos turn it isi 
    public Sprite[] playIcons; // 0 = x icon and 1 = y icon
    public Button[] tictactoeSpaces; //playable space for our game 
    public int[] markedSpaces; //ID which space is marked by players
    public TextMeshProUGUI winnerText; // Hold The text of The winner Text
    public GameObject[] winningLine; // holds all the different lines for streaks
    public GameObject winnerPanel;
    public int xPlayerScore;
    public int oPlayerScore;
    public TextMeshProUGUI xPlayerScoreText;
    public TextMeshProUGUI oPlayerScoreText;
    public Button xPlayerButton;
    public Button oPlayerButton;
    public AudioSource buttonClickAudio;

    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whoTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            //make buttons clickable
            tictactoeSpaces[i].interactable = true;
            //make buttons spritee null
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    //Add to Button listen function then put no from 0-8 as paramters
    //so button 5 if clicked excute
    public void TicTacToeButtton(int whichNo)
    {
        xPlayerButton.interactable = false;
        oPlayerButton.interactable = false;
        tictactoeSpaces[whichNo].image.sprite = playIcons[whoTurn];
        tictactoeSpaces[whichNo].interactable = false;

        //So make 0 and 1 to 1 and 2 so avoid logic errors
        markedSpaces[whichNo] = whoTurn + 1;
        turnCount++;

        //atleast 4 moves to win
        if (turnCount > 4)
        {
           bool isWinner = WinnerCheck();
            if (turnCount == 9 && isWinner == false)
            {
                tie(); 
            }

        }

        if (whoTurn == 0)
        {
            whoTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else { whoTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }
    bool WinnerCheck()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for (int i = 0; i < solutions.Length; i++)
        {
            //if whoever turn lets say player 0 or 1 as u plus 1 
            //so u got 3 that means player 0 wins
            if (solutions[i] == 3 * (whoTurn + 1))
            {
                //make sure solution are lined witth same winning streaks
                winnerDisplay(i);
                //sol found
                return true;
            }
        }
        return false;

    }
    void winnerDisplay(int indexIn)
    {
        winnerPanel.gameObject.SetActive(true);
        if(whoTurn == 0)
        {
            xPlayerScore++;
            xPlayerScoreText.text = xPlayerScore.ToString();
            winnerText.text = "Player X wins!";

        }else if(whoTurn == 1)
        {
            oPlayerScore++;
            oPlayerScoreText.text = oPlayerScore.ToString();
            winnerText.text = "Player O wins!";
        }
        winningLine[indexIn].SetActive(true);

        //disable game after win
        //isntead show panel on top
        //for(int i =0; i <tictactoeSpaces.Length; i++)
        //{
        //    tictactoeSpaces[i].interactable = false;
        //}
    }
    public void Rematch ()
    {
        GameSetup();
        for (int i =0; i<winningLine.Length; i++)
        {
            winningLine[i].SetActive(false);
        }
        winnerPanel.SetActive(false);
        xPlayerButton.interactable = true;
        oPlayerButton.interactable = true;
    }
    public void Restart()
    {
        Rematch();
        xPlayerScore = 0;
        oPlayerScore = 0;
        xPlayerScoreText.text = "0";
        oPlayerScoreText.text = "0";

    }
    public void SwitchPlayer(int whichPlayer)
    {
        if(whichPlayer == 0)
        {
            whoTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        } else if (whichPlayer == 1)
        {
            whoTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
    }
    
    void tie ()
    {
        winnerPanel.SetActive(true);
        winnerText.text = "PLAYERS TIE!";
    }

    public void PlayButtonClick()
    {
        buttonClickAudio.Play();
    }
}

