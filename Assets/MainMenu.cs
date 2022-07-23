using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject PlayPanel;
    public GameObject MenuPanel;
    public int N;

    void Start()
    {
        for (int i = 0; i < N; i++)
        {
            if (i > PlayerPrefs.GetInt("Levels"))
            {
                PlayPanel.transform.GetChild(i + 1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void Play()
    {
        PlayPanel.SetActive(!PlayPanel.activeSelf);
        MenuPanel.SetActive(!MenuPanel.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void StartLevel(int lvl)
    {
        if (lvl - 1 <= PlayerPrefs.GetInt("Levels"))
            SceneManager.LoadScene(lvl);
    }
}
