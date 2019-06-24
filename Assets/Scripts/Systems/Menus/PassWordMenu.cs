using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PassWordMenu : MonoBehaviour
{
    public SceneFlow sceneFlow;
    public Image Puntero;
    public Image Fade;
    public Image Flecha;

    public Animator[] Monitos = new Animator[12];
    public Image[] NumBlocks = new Image[12];
    int[] NumBlockID = { 0,0,0,0,0,0,0,0,0,0,0,0};
    Vector2[] Positions = new Vector2[12];

    public Sprite[] Numbers = new Sprite[8];

    int menuIndex = 0;
    int columna = 0;
    int fila = 0;

    bool canRandomActivate = true;

    // Start is called before the first frame update
    void Start()
    {
        Positions[0] = new Vector2(-162.53f, 158.969f);
        Positions[1] = new Vector2(-57.3f, 158.969f);
        Positions[2] = new Vector2(48, 158.969f);
        Positions[3] = new Vector2(153.3f, 158.969f);

        Positions[4] = new Vector2(-162.53f, 54);
        Positions[5] = new Vector2(-57.3f, 54);
        Positions[6] = new Vector2(48, 54);
        Positions[7] = new Vector2(153.3f, 54);

        Positions[8] = new Vector2(-162.53f, -49);
        Positions[9] = new Vector2(-57.3f, -49);
        Positions[10] = new Vector2(48, -49);
        Positions[11] = new Vector2(153.3f, -49);

        Monitos[0].SetBool("selected", true);
        Monitos[0].SetBool("walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        Menus();
        if (canRandomActivate)
        {
            canRandomActivate = false;
            StartCoroutine(ActivteRandomMonito());
        }
        Debug.Log(menuIndex);
    }

    void Menus()
    {
        if (!Flecha.enabled)
        {
            if (Input.GetAxisRaw("Vertical") == -1) //Aqui reemplazar por el nuevo input
            {
                fila++;
                DeativateMonito();
                menuIndex = columna + (fila * 4);
                if (menuIndex >= 12)
                {
                    //ir a end
                    ActivarFlecha();
                    fila--;
                    menuIndex = columna + (fila * 4);
                }
                else
                {
                    Puntero.transform.localPosition = Positions[menuIndex];
                    ActivateMonito();
                }
            }
            if (Input.GetAxisRaw("Vertical") == 1) //Aqui reemplazar por el nuevo input
            {
                fila--;
                DeativateMonito();
                menuIndex = columna + (fila * 4);

                if (menuIndex <= -1)
                {
                    //ir a end
                    ActivarFlecha();
                    fila++;
                    menuIndex = columna + (fila * 4);
                }
                else
                {
                    Puntero.transform.localPosition = Positions[menuIndex];
                    ActivateMonito();
                }
            }

            if (Input.GetAxisRaw("Horizontal") == -1) //Aqui reemplazar por el nuevo input
            {
                columna--;
                DeativateMonito();
                menuIndex--;
                if (columna == -1)
                {
                    columna = 3;
                    menuIndex = columna + (fila * 4);
                }
                Puntero.transform.localPosition = Positions[menuIndex];
                ActivateMonito();
            }
            if (Input.GetAxisRaw("Horizontal") == 1) //Aqui reemplazar por el nuevo input
            {
                columna++;
                DeativateMonito();
                menuIndex++;
                if (columna == 4)
                {
                    columna = 0;
                    menuIndex = columna + (fila * 4);
                }
                Puntero.transform.localPosition = Positions[menuIndex];
                ActivateMonito();
            }

            if (Input.GetButtonDown("Shoot"))
            {
                NumBlockID[menuIndex]++;
                if (NumBlockID[menuIndex] >= 8)
                {
                    NumBlockID[menuIndex] = 0;
                }
                NumBlocks[menuIndex].sprite = Numbers[NumBlockID[menuIndex]];
                NumBlocks[menuIndex].SetNativeSize();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                NumBlockID[menuIndex]--;
                if (NumBlockID[menuIndex] <= -1)
                {
                    NumBlockID[menuIndex] = 7;
                }
                NumBlocks[menuIndex].sprite = Numbers[NumBlockID[menuIndex]];
                NumBlocks[menuIndex].SetNativeSize();
            }
        }
        else
        {
            if (Input.GetButtonDown("Shoot"))//aqui poner el input de boton de start
            {
                StartCoroutine(sceneFlow.ChangeScene("MenuScene"));
            }
            else if (Input.GetAxisRaw("Vertical") == 1)
            {
                Flecha.enabled = false;
                Puntero.enabled = true;
                fila = 2;
                menuIndex = columna + (fila * 4);
                Puntero.transform.localPosition = Positions[menuIndex];
                ActivateMonito();
            }
            else if(Input.GetAxisRaw("Vertical") == -1)
            {
                Flecha.enabled = false;
                Puntero.enabled = true;
                fila = 0;
                menuIndex = columna + (fila * 4);
                Puntero.transform.localPosition = Positions[menuIndex];
                ActivateMonito();
            }
        }
    }

    IEnumerator ActivteRandomMonito()//para los monitos que se asoman de manera random
    {
        int randomMonito = Random.Range(0, 11);
        if (randomMonito != menuIndex)
        {
            Monitos[randomMonito].SetBool("randAct", true);
        }
        yield return new WaitForSeconds(1.0f);
        Monitos[randomMonito].SetBool("randAct", false);
        canRandomActivate = true;
    }

    void ActivateMonito()
    {
        Monitos[menuIndex].SetBool("selected", true);
        Monitos[menuIndex].SetBool("walk", true);
    }

    void DeativateMonito()
    {
        Monitos[menuIndex].SetBool("selected", false);
        Monitos[menuIndex].SetBool("walk", false);
    }

    void ActivarFlecha()
    {
        Flecha.enabled = true;
        Puntero.enabled = false;
    }
}
