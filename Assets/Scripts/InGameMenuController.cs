using Unity.VisualScripting;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    public GameObject menuCanvas;

    public GameObject mainCanvas;

    [SerializeField] AudioSource source;

    void Start()
    {
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("MENU");
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            mainCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }

    public void OnResumeButtonClick()
    {
        menuCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ValueChange(float volume) {
        source.volume = volume;
    }
}






