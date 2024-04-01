using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private GameObject HowToPlay;
    [SerializeField] private GameObject MenuButtons;
    [SerializeField] private GameObject HTPScreen1;
    [SerializeField] private GameObject HTPScreen2;
    void Start()
    {
        HowToPlay.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenHowToPlay()
    {
        HowToPlay.SetActive(true);
        HTPScreen1.SetActive(true);
        HTPScreen2.SetActive(false);
        MenuButtons.SetActive(false);
    }

    public void CloseHowToPlay()
    {
        HowToPlay.SetActive(false);
        MenuButtons.SetActive(true);
    }

    public void GoToPage1()
    {
        HTPScreen1.SetActive(true);
        HTPScreen2.SetActive(false);
    }

    public void GoToPage2()
    {
        HTPScreen2.SetActive(true);
        HTPScreen1.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
