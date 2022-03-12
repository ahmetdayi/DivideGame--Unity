using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//bunu koymadan fadeout methodundak� kodu cal�st�ramay�z.
using UnityEngine.SceneManagement;//sahneler aras� gec�s yapab�lmek �c�n bunu ekled�k.


public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startBtn, exitBtn;
   
    void Start()
    {
        FadeOut();
    }

    void FadeOut()//butonlar�n seffafl�g� sondayd� gorunur hale get�recez.
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
