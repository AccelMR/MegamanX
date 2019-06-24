using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Menu : MonoBehaviour
{
    public SceneFlow sceneFlow;
    public Image MegamanPuntero;
    public Image Blast;
    public Image Fade;
    public Font fontBlue;
    public Font fontOrange;

  public bool pressed;

    public Text[] menuTexts = new Text[3];

    int menuIndex = 0;

    Vector2[] positions = new Vector2[3];

    // Start is called before the first frame update
    void Start()
    {
        positions[0] = new Vector2(-150.2f, -86.8f);
        positions[1] = new Vector2(-150.2f, -113.8f);
        positions[2] = new Vector2(-150.2f, -140.8f);
    }

    // Update is called once per frame
    void Update()
    {
        Menus();

        if (Blast.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("blast") && Blast.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            rechargeBlast();
            rechargeAnim();
        }
    }

    void Menus()
    {
        if (Input.GetAxisRaw("Vertical") == -1) //Aqui reemplazar por el nuevo input
        {
            menuTexts[menuIndex].font = fontBlue;
            menuIndex++;
            if (menuIndex == 3)
            {
                menuIndex = 0;
            }
            MegamanPuntero.transform.localPosition = positions[menuIndex];
            menuTexts[menuIndex].font = fontOrange;
        }
        if (Input.GetAxisRaw("Vertical") == 1) //Aqui reemplazar por el nuevo input
        {
            menuTexts[menuIndex].font = fontBlue;
            menuIndex--;
            if (menuIndex == -1)
            {
                menuIndex = 2;
            }
            menuTexts[menuIndex].font = fontOrange;
            MegamanPuntero.transform.localPosition = positions[menuIndex];
        }

        if (Input.GetButtonDown("Shoot"))//aqui poner el input de boton de start
        {
            MegamanPuntero.GetComponent<Animator>().SetBool("start", true);
            Blast.GetComponent<Animator>().SetBool("start", true);
            Blast.enabled = true;
            switch (menuIndex)
            {
                case 0://start
                    {
                        StartCoroutine(sceneFlow.ChangeScene("MainGame", 0.4f));
                        break;
                    }
                case 1://password codes
                    {
                        StartCoroutine(sceneFlow.ChangeScene("Password", 0.4f));
                        break;
                    }
                case 2://options
                    {
                        StartCoroutine(sceneFlow.ChangeScene("Options", 0.4f));
                        break;
                    }
            }
        }
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

//   private int getVertical()
//   {
//     Input.GetAxisRaw("Vertical")
// 
//     return 0;
//   }

}
