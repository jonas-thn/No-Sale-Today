using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Text infoText;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Slider audioSlider;
    [SerializeField] Outline playOutline;
    [SerializeField] Outline settingsOutline;
    [SerializeField] Outline quitOutline;
    [SerializeField] Outline backOutline;
    [SerializeField] UiFade uifade;
    [SerializeField] Image storyText;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        
    }

    private void Start()
    {
        playOutline.enabled = false;
        settingsOutline.enabled = false;
        quitOutline.enabled = false;
        backOutline.enabled = false;

        GameObject gameTimer = GameObject.Find("GameManager");
        GameObject mainUI = GameObject.Find("Main UI");

        audioMixer.SetFloat("volume", audioSlider.value);

        if (gameTimer != null)
        {
            Destroy(gameTimer);
        }

        if(mainUI != null)
        {
            Destroy(mainUI);
        }

        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        dropdown.value = (int)QualitySettings.currentLevel;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        uifade.gameObject.SetActive(true);

        StartCoroutine(uifade.FadeIn());
        yield return new WaitForSeconds(2);
        storyText.enabled = true;
        yield return new WaitForSeconds(8);

        SceneManager.LoadScene("Main Scene");
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void ShowInfoText()
    {
        infoText.enabled = true;
    }

    public void HideInfoText()
    {
        infoText.enabled = false;
    }

    public void ShowSettings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void PlayClick()
    {
        audioSource.Play();
    }

    public void SetPlayOutlineTrue()
    {
        playOutline.enabled = true;
    }

    public void SetPlayOutlineFalse()
    {
        playOutline.enabled = false; 
    }

    public void SetQuitOutlineTrue()
    {
        quitOutline.enabled = true;
    }

    public void SetQuitOutlineFalse()
    {
        quitOutline.enabled = false;
    }

    public void SetSettingsOutlineTrue()
    {
        settingsOutline.enabled = true;
    }

    public void SetSettingsOutlineFalse()
    {
        settingsOutline.enabled = false;
    }

    public void SetBackOutlineTrue()
    {
        backOutline.enabled = true;
    }

    public void SetBackOutlineFalse()
    {
        backOutline.enabled = false;
    }
}
