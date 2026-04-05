using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public Slider musicVolume;
    public Slider effectVolume;

    public Toggle isMute;

    public AudioSource[] effect;
    public AudioSource bgm;

    public GameObject setting;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setting.SetActive(true);
        }
    }


    public void QuitGame()
    {
        Debug.Log("종료");
    }
    public void ResumeGame()
    {
        Debug.Log("재개");
        setting.SetActive(false);
    }

    public void musicVolumeSet()
    {

    }

    public void effectVolumeSet()
    {

    }

    public void isMuteSet()
    {

    }
}
