using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameScript : MonoBehaviour {

    WordBank wordBank = new WordBank();

    //Game Objects
    GameObject[] hangman = new GameObject[7]; //array that holds the hangman parts
    GameObject[] hangmanWords = new GameObject[11]; //labels for the words
    public Text winorlose;
    public InputField txtBox;

    string answer;
    int wrongAnswer = 0;
    int correctAnser = 0;
    bool gameLost = false;
    bool gameWon = false;
    public bool cheat = false;

    void Awake ()
    {
        winorlose.enabled = false;

        for(int i=0; i<6; i++)
        {
            hangman[i] = GameObject.Find("HangmanPart" + i.ToString());
        }

        for (int i = 0; i < 11; i++)
        {
            hangmanWords[i] = GameObject.Find("Letter" + i.ToString());
            hangmanWords[i].SetActive(false);
        }

        wordBank.InitializeString();

    }

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 6; i++)
        {
            hangman[i].SetActive(false);
        }
        ChooseWord();
    }
	
	// Update is called once per frame
	void Update () {
        if (wrongAnswer == 1)
        {
            hangman[0].SetActive(true);
        }
        if (wrongAnswer == 2)
        {
            hangman[1].SetActive(true);
        }
        if (wrongAnswer == 3)
        {
            hangman[2].SetActive(true);
        }
        if (wrongAnswer == 4)
        {
            hangman[3].SetActive(true);
        }
        if (wrongAnswer == 5)
        {
            hangman[4].SetActive(true);
        }
        if (wrongAnswer == 6)
        {
            hangman[5].SetActive(true);
        }

        if(gameLost)
        {
            winorlose.GetComponent<Text>().text = "You Lost!\n Game Word was: " + answer;
            winorlose.enabled = true;
            if(!gameWon) cheat = true;
        }
        if (gameWon)
        {
            winorlose.GetComponent<Text>().text = "You Won!";
            winorlose.enabled = true;
            if(cheat == true) winorlose.GetComponent<Text>().text = "Captain Obvious -.-";
        }
    }

    public void EasyClick ()
    {
        hangmanWords[0].GetComponent<Text>().text = answer[0].ToString();
        hangmanWords[answer.Length - 1].GetComponent<Text>().text = answer[answer.Length - 1].ToString();
        correctAnser = 2;
    }

    public void Restart ()
    {
        SceneManager.LoadScene(0);
    }

    void ChooseWord()
    {
        answer = wordBank.words[Random.Range(0,19)];
        for(int i = 0; i < answer.Length; i++)
        {
            hangmanWords[i].SetActive(true);
        }
    }

    //Checking if the TextBox contains the correct answer
   public void GetAnswer()
    {
        if (answer.Contains(txtBox.text))
        {
            char wordAnswer;
            char.TryParse(txtBox.text, out wordAnswer);
            for (int i = 0; i < answer.Length; i++)
            {
                if (answer[i] == wordAnswer && hangmanWords[i].GetComponent<Text>().text != wordAnswer.ToString())
                {
                    hangmanWords[i].GetComponent<Text>().text = wordAnswer.ToString();
                    correctAnser++;
                    if (correctAnser == answer.Length) gameWon = true;
                }
            }
        }
        else
        {
            wrongAnswer++;
            if (wrongAnswer == 6)
            {
                gameLost = true;
            } 
        }
    }
}
