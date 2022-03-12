using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//bunu koymadan fadeout methodundaký kodu calýstýramayýz.
using UnityEngine.SceneManagement;//sahneler arasý gecýs yapabýlmek ýcýn bunu ekledýk.


public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startBtn, exitBtn;
   
    void Start()
    {
        FadeOut();
    }

    void FadeOut()//butonlarýn seffaflýgý sondaydý gorunur hale getýrecez.
    {
        startBtn.GetComponent<CanvasGroup>().DOFade(1,0.8f);
        exitBtn.GetComponent<CanvasGroup>().DOFade(1, 0.8f).SetDelay(0.8f);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGameLevel()
    {
        SceneManager.LoadScene("GameLevel");
    }
}
