using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private GameObject HowToPlay;
    [SerializeField] private GameObject MenuButtons;
    // Start is called before the first frame update
    void Start()
    {
        HowToPlay.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        GameController.Instance.InitializeGame();
    }
    public void OpenHowToPlay()
    {
        HowToPlay.SetActive(true);
        MenuButtons.SetActive(false);
    }

    public void CloseHowToPlay()
    {
        HowToPlay.SetActive(false);
        MenuButtons.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
