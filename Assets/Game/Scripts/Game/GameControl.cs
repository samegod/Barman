using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameControl : MonoBehaviour {
        public int Goals;

        public GameObject LivesContainer;
        public GameObject[] beers;
        public GameObject[] Lives;
        public int currentBeer;
        public GameObject UI;
        public GameObject PauseMenu;
        public GameObject WinMenu;
        public GameObject FailMenu;
        public bool finish;
        public GameObject nextLvlBtn;
        public GameObject commingSoon;
        public BeerFollowCamera cam;

        private bool _switchCam;

        public void Start()
        {
            finish = false;
            _switchCam = false;
            currentBeer = 0;
            Lives = new GameObject[beers.Length];
            Time.timeScale = 1;

            for(int i = 0; i < beers.Length; i++)
            {
                Lives[Lives.Length - 1 - i] = LivesContainer.transform.GetChild(i).gameObject;
            }

            if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
            {
                nextLvlBtn.SetActive(true);
                commingSoon.SetActive(false);
            }else
            {
                nextLvlBtn.SetActive(false);
                commingSoon.SetActive(true);
            }
        }

        public void Update()
        {
            if (_switchCam)
            {
                currentBeer++;
                if (!finish)
                {
                    Lives[currentBeer - 1].SetActive(false);
                    if (currentBeer == beers.Length)
                    {
                        Fail();
                        _switchCam = false;
                    }
                    else
                    {
                        beers[currentBeer].SetActive(true);
                        cam.SetTarget(beers[currentBeer].transform);
                        _switchCam = false;
                    }
                }
            
            }
        }

        public void Pushed()
        {
            StartCoroutine(Wait());
        }

        public void DoIt()
        {
            Goals--;
            if (Goals == 0)
            {
                Debug.Log("FINISH");
                WinMenu.SetActive(true);
                finish = true;
                if (SceneManager.GetActiveScene().buildIndex - 1 == PlayerPrefs.GetInt("Levels"))
                {
                    PlayerPrefs.SetInt("Levels", PlayerPrefs.GetInt("Levels") + 1);
                }
            }
        }

        public void Pause() {
            UI.SetActive(!UI.activeSelf);
            PauseMenu.SetActive(!PauseMenu.activeSelf);
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextLVL()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Menu()
        {
            SceneManager.LoadScene(0);
        }

        public void Fail()
        {
            FailMenu.SetActive(true);
            UI.SetActive(false);
        }

        public IEnumerator Wait()
        {
            yield return new WaitForSeconds(1);
            _switchCam = true;
        }
    
    }
}
