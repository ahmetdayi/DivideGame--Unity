using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject SquarePreFab;

    [SerializeField]
    private Transform SquarePanel;

    [SerializeField]
    private Transform questionsPanel;

    [SerializeField]
    Text questionsText;

    [SerializeField]
    Sprite[] squareSpriteArray;

    [SerializeField]
    Transform resultPanel;
    [SerializeField]
    AudioSource audioSource;

    public AudioClip buttonVoice;

    bool isPressTheButton;

    List<int> valueInSquare = new List<int>();

    int dividendValue, divisorValue;//divisor bolen dividend bolunen

    private GameObject[] squares = new GameObject[25];
    private int whichValue;
    private int trueAnswer;
    private int buttonValue;
    private int kalanHak;

    KalanHaklarManager kalanHaklarManager;
    PuanManager puanManager;
    string questionsDifficultLevel;
    GameObject currentSquare;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        resultPanel.GetComponent<RectTransform>().localScale = Vector3.zero;//gorunurlulugunu kapattým
        kalanHak = 3;
        kalanHaklarManager = Object.FindObjectOfType<KalanHaklarManager>();
        puanManager = Object.FindObjectOfType<PuanManager>();
        kalanHaklarManager.KalanHaklarýKontrolEt(kalanHak);
    }

    void Start()
    {
        isPressTheButton = false;
        questionsPanel.GetComponent<RectTransform>().localScale = Vector3.zero;

        MakeSquares();
    }

    private void MakeSquares()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject square = Instantiate(SquarePreFab, SquarePanel);
            square.transform.GetComponent<Button>().onClick.AddListener(() => PressTheButton());

            square.transform.GetChild(1).GetComponent<Image>().sprite = squareSpriteArray[MakeRandomValue(0, squareSpriteArray.Length)];
            squares[i] = square;
        }
        WriteRandomValueInSquare();

        StartCoroutine(DoFadeRoutine());

        Invoke("OpenQuestionsPanel", 2f);
    }

    IEnumerator DoFadeRoutine()
    {
        foreach (var square in squares)
        {
            square.GetComponent<CanvasGroup>().DOFade(1, 0.2f);

            yield return new WaitForSeconds(0.07f);
        }
    }

    void WriteRandomValueInSquare()
    {
        foreach (var square in squares)
        {
            int randomValue = MakeRandomValue(1, 13);
            square.transform.GetChild(0).GetComponent<Text>().text =randomValue.ToString();
            valueInSquare.Add(randomValue);
        }
    }

    int MakeRandomValue(int a, int b)
    {
        int randomValue = Random.Range(a, b);
       

        return randomValue;
    }

    void OpenQuestionsPanel()
    {
        AskTheQuestion();
        isPressTheButton = true;
        questionsPanel.GetComponent<RectTransform>().DOScale(1, 0.5f);
    }

    void AskTheQuestion()
    {
        divisorValue = MakeRandomValue(2, 11);
        whichValue = MakeRandomValue(0, valueInSquare.Count);
        trueAnswer = valueInSquare[whichValue];
        dividendValue = divisorValue * valueInSquare[whichValue];

        ControlQuestionsDifficultlyLevel();

        questionsText.text = dividendValue.ToString() + " : " + divisorValue.ToString();

    }

    private void ControlQuestionsDifficultlyLevel()
    {
        if (dividendValue < 40)
        {
            questionsDifficultLevel = "Easy";
        }
        else if (dividendValue > 40 && dividendValue < 80)
        {
            questionsDifficultLevel = "Middle";

        }
        else
        {
            questionsDifficultLevel = "Hard";
        }
    }

    void PressTheButton()
    {
        if (isPressTheButton)
        {
            audioSource.PlayOneShot(buttonVoice);
            buttonValue = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);

            currentSquare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

            ShowTheResult();
        }
       
    }

    private void ShowTheResult()
    {
        if(trueAnswer == buttonValue)
        {
            currentSquare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            currentSquare.transform.GetChild(0).GetComponent<Text>().enabled = false;
            currentSquare.GetComponent<Button>().interactable = false;
            
            puanManager.AddPuan(questionsDifficultLevel);

            valueInSquare.RemoveAt(whichValue);

            if (valueInSquare.Count > 0)
            {
                OpenQuestionsPanel();
            }
            else
            {
                GameOver();
            }

        }
        else
        {
            kalanHak--;
            kalanHaklarManager.KalanHaklarýKontrolEt(kalanHak);

            if(kalanHak == 0)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        isPressTheButton = false;
        resultPanel.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }
}
