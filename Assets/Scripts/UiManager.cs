using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TextMeshPro scoreText;

    public Slider effectVolume;
    public Slider musicVolume;
    public AudioSource[] effect;
    public AudioSource bgm;

    public Button quitGame;
    public Button Resume;

    public Toggle sound;

    public GameObject setting;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setting.SetActive(true);
        }
    }

}
