using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour
{
    public Image Puntero;
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
        if (currentSceneName == "MenuScene")
        {
            Menu();
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
            Puntero.transform.localPosition = positions[menuIndex];
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
            Puntero.transform.localPosition = positions[menuIndex];
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            switch (menuIndex)
            {
                case 0://start
                    {
                        StartCoroutine(ChangeScene("MainGame"));
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

    IEnumerator ChangeScene(string sceneName)
    {
        FadeOut.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
