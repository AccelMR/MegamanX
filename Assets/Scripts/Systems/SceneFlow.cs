using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneFlow : MonoBehaviour
{
    public Image MegamanPuntero;
    public Image Blast;
    public Image FadeOut;
    public Font fontBlue;
    public Font fontOrange;
    public Text[] menuTexts = new Text[3];
    int menuIndex = 0;
    string currentSceneName;

    Vector2[] positions = new Vector2[3];

    // Start is called before the first frame update
    void Start()
    {
        positions[0] = new Vector2(-93, -59);
        positions[1] = new Vector2(-93, -77);
        positions[2] = new Vector2(-93, -95);
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
            Menu();

            if (Blast.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("blast") && Blast.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                rechargeBlast();
                rechargeAnim();
            }
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

    void Menu()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) //Aqui reemplazar por el nuevo input
        {
            menuTexts[menuIndex].font = fontBlue;
            menuIndex++;
            if (menuIndex == 3)
            {
                menuIndex = 0;
            }
            else if (menuIndex == -1)
            {
                menuIndex = 2;
            }
            MegamanPuntero.transform.localPosition = positions[menuIndex];
            menuTexts[menuIndex].font = fontOrange;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) //Aqui reemplazar por el nuevo input
        {
            menuTexts[menuIndex].font = fontBlue;
            menuIndex--;
            if (menuIndex == 3)
            {
                menuIndex = 0;
            }
            else if (menuIndex == -1)
            {
                menuIndex = 2;
            }
            menuTexts[menuIndex].font = fontOrange;
            MegamanPuntero.transform.localPosition = positions[menuIndex];
        }

        if (Input.GetKeyDown(KeyCode.Return))//aqui poner el input de boton de start
        {
            MegamanPuntero .GetComponent<Animator>().SetBool("start", true);
            Blast.GetComponent<Animator>().SetBool("start", true);
            Blast.enabled = true;
            switch (menuIndex)
            {
                case 0://start
                    {
                        StartCoroutine(ChangeScene("MainGame", 0.4f));
                        break;
                    }
                case 1://password codes
                    {
                        break;
                    }
                case 2://options
                    {
                        break;
                    }
            }
        }
    }

    IEnumerator ChangeScene(string sceneName, float delay = 0)
    {
        yield return new WaitForSeconds(delay);//esperar a que el blast salga de la pantalla
        FadeOut.GetComponent<Animator>().enabled = true;//animar fade out
        yield return new WaitForSeconds(1.0f);//esperar a la animacion de fadeOut
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);//cargar escena
    }

    void rechargeBlast()
    {
        Blast.GetComponent<Animator>().SetBool("start", false);
        Blast.enabled = false;
    }

    void rechargeAnim()
    {
        MegamanPuntero.GetComponent<Animator>().SetBool("start", false);
    }
}
