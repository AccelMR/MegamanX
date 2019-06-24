using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OptionsMenu : MonoBehaviour
{
    public SceneFlow sceneFlow;
    public Image Fade;
    public Font fontBlue;
    public Font fontOrange;

    public Text[] OptionsTexts = new Text[10];

    int menuIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Menus();
    }

    void Menus()
    {
        if (Input.GetAxisRaw("Vertical") == -1) //Aqui reemplazar por el nuevo input
        {
            OptionsTexts[menuIndex].font = fontBlue;
            menuIndex++;
            if (menuIndex == 10)
            {
                menuIndex = 0;
            }
            OptionsTexts[menuIndex].font = fontOrange;
        }
        if (Input.GetAxisRaw("Vertical") == 1) //Aqui reemplazar por el nuevo input
        {
            OptionsTexts[menuIndex].font = fontBlue;
            menuIndex--;
            if (menuIndex == -1)
            {
                menuIndex = 9;
            }
            OptionsTexts[menuIndex].font = fontOrange;
        }

        if (Input.GetButtonDown("Shoot"))//aqui poner el input de boton de start
        {
            switch (menuIndex)
            {
                case 9://start
                    {
                        StartCoroutine(sceneFlow.ChangeScene("MenuScene"));
                        break;
                    }
            }
        }
    }
}
