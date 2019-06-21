using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneFlow : MonoBehaviour
{
    public Image Fade;

    string currentSceneName;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSceneName == "MenuScene")//si est en la escena del menu
        {

        }
        if (currentSceneName == "Intro1")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(ChangeScene("Intro2"));
            }
        }
        if (currentSceneName == "Intro2")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(ChangeScene("MenuScene"));
            }
        }
    }

    public IEnumerator ChangeScene(string sceneName, float delay = 0)
    {
        yield return new WaitForSeconds(delay);//si es necesario un delay
        Fade.GetComponent<Animator>().enabled = true;//animar fade out
        yield return new WaitForSeconds(1.0f);//esperar a la animacion de fadeOut
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);//cargar escena
    }
}
