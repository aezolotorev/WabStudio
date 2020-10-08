using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;
public class GameManager : MonoBehaviour
{
    public GameObject gameOverObj;
    public GameObject settingsMenu;
    public GameObject levelcomplete;
    public PlayerMovement playerMovement;
    public Text diamondsText;
    public int diamonds = 0;
    private const string banner = "ca-app-pub-7412699532104812/6212400035";


    public void GameOver()
    {
        gameOverObj.SetActive(true);
    }
    public void StartGame()
    {
        gameOverObj.SetActive(false);
        SceneManager.LoadScene(0);            
    }

    public void AddCoin(int number)
    {
        diamonds += number;
        diamondsText.text = diamonds.ToString();
    }
    public void Settings()
    {
        settingsMenu.SetActive(true);
        Time.timeScale = 0F;
       

    }
    public void Resume()
    {
        settingsMenu.SetActive(false);
        Time.timeScale = 1F;
        BannerView bannerView = new BannerView(banner, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("F5695C1E268D083B").Build();
        bannerView.LoadAd(request);
    }

    public void Levelcomplete()
    {
        levelcomplete.SetActive(true);
    }


}
