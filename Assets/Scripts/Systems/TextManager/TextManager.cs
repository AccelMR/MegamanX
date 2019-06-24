using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public struct Dialogo
    {
        public Sprite avatar;//imagen del personaje
        public string line;//textos
        public int size;//cuantas nuevas lineas tiene
        public int indexFont;
        public bool bigPanel;
        public Dialogo(Sprite ava, string text, int sz, int font, bool bigpanel)
        {
            avatar = ava;
            line = text;
            size = sz;
            indexFont = font;
            bigPanel = bigpanel;
        }
    }

    public struct Scene
    {
        public Dialogo[] dialogos;
        public int currentDialog;

        Scene(Dialogo[] dialog)
        {
            dialogos = dialog;
            currentDialog = 0;
        }
    }

    public Text m_text;
    public Text m_flecha;
    Vector2 m_initialTextPosition;

    public Canvas m_canvas;

    public Image m_panel;
    public Image m_avaImg;

    public Sprite[] m_avatars;

    public Sprite[] m_MegaManTalk;
    public Sprite[] m_ZeroTalk;

    Dialogo[] m_dialogos;
    public Font[] m_fonts;
    public AudioSource m_audio;

    public float m_normalCharSpeed = 0.06f; //lower is faster
    public float m_fastCharSpeed = 0.02f; //lower is faster
    float m_currentCharSpeed = 0.06f; //lower is faster

    bool endDialog = false;
    bool closingDialog = false;
    bool isFast = false;
    bool canChange = false;

    bool cantalk = true;
    bool already = false;


    const int ScenesSize = 1; //Numero de Escenas
    Scene[] escenas = new Scene[ScenesSize]; //una escena es un conjunto de dialogos seguidos
    int currentScene = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_initialTextPosition = m_text.transform.position;

        //ejemplos de construccion de dialogos
        escenas[0].dialogos = new Dialogo[1];//cuantos dialogos va a tener
        escenas[0].dialogos[0] = new Dialogo(m_avatars[1], "Gracias por jugar", 4, 0, false);//Creacion del dialogo

    }

    // Update is called once per frame
    void Update()
    {
        currentScene = Mathf.Clamp(currentScene, 0, escenas.Length);
        Debug.Log(currentScene);

        

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (endDialog && !canChange)//si ya termino de mostrar lso dialogos y el jugador presiona X. cerrar el dialogo
            {
                canChange = true;
                if (escenas[currentScene].currentDialog == escenas[currentScene].dialogos.Length -1)
                {
                    StartCoroutine(closeDialog());
                }
                else //avanzar al sig dialogo
                {
                    escenas[currentScene].currentDialog++;
                    StartCoroutine(nextDialog(escenas[currentScene].dialogos[escenas[currentScene].currentDialog], false));
                }
            }
            if (!isFast)// si no se ha acelerado el dialogo
            {
                m_currentCharSpeed = m_fastCharSpeed;//disminuir el tiempo entre aparicion caracteres
            }
        }

        if (closingDialog)
        {
            m_text.transform.position = Vector2.Lerp(m_text.transform.position, new Vector2(m_text.transform.position.x, m_text.transform.position.y + 80), Time.deltaTime * 2);
        }

        if (cantalk && !endDialog && currentScene < escenas.Length)
        {
            cantalk = false;
            StartCoroutine(TalkAnimation());
        }
    }

    IEnumerator TalkAnimation()
    {
        if (escenas[currentScene].dialogos[escenas[currentScene].currentDialog].avatar.name == m_avatars[0].name)
        {
            m_avaImg.sprite = m_MegaManTalk[0];
            yield return new WaitForSeconds(0.1f);
            m_avaImg.sprite = m_MegaManTalk[1];
            yield return new WaitForSeconds(0.1f);
            m_avaImg.sprite = m_MegaManTalk[2];
            yield return new WaitForSeconds(0.1f);
            m_avaImg.sprite = m_MegaManTalk[3];
            yield return new WaitForSeconds(0.1f);
        }
        else if (escenas[currentScene].dialogos[escenas[currentScene].currentDialog].avatar.name == m_avatars[2].name)
        {
            m_avaImg.sprite = m_ZeroTalk[0];
            yield return new WaitForSeconds(0.1f);
            m_avaImg.sprite = m_ZeroTalk[1];
            yield return new WaitForSeconds(0.1f);
            m_avaImg.sprite = m_ZeroTalk[2];
            yield return new WaitForSeconds(0.1f);
            m_avaImg.sprite = m_ZeroTalk[3];
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            //no hacer nada
        }

        cantalk = true;
    }

    IEnumerator createDialog(Dialogo dialog, bool keepPanel) //indexLine, numero de lineas, avatar(0 mega man, 1 vile, 2 zero). 
    {
        m_text.transform.SetParent(m_canvas.transform);
        //m_text.rectTransform.localScale = Vector2.one;
       // m_flecha.rectTransform.localPosition = new Vector3(71, -dialog.size * 6, 0);
        //colocar el cuadro de la imagen de quien habla
        if (!dialog.bigPanel)
        {
            //m_avaImg.rectTransform.localPosition = new Vector3(63.4f, 25.8f, 0);
        }
        else//si es un panel grande
        {
           // m_avaImg.rectTransform.localPosition = new Vector3(63.4f, 8.3f, 0);
        }

        if (dialog.avatar == m_avatars[0])//si esta hablando mega man ponerlo a la izquierda
        {
            m_avaImg.rectTransform.localPosition = new Vector3(-67.4f, m_avaImg.rectTransform.localPosition.y, 0);
        }

        m_avaImg.sprite = dialog.avatar;//cambiar la imagen de quien habla

        m_text.transform.position = m_initialTextPosition;
        m_text.font = m_fonts[dialog.indexFont]; //cambiar el color del texto

        m_panel.enabled = true;//activar el panel
        m_panel.GetComponent<Animator>().enabled = true;

        m_panel.GetComponent<Animator>().SetBool("bigPanel", dialog.bigPanel);//animacion del panel segun el tamano de las lineas

        yield return new WaitForSeconds(2.1f);//esperar a que se acabe la animacion del panel
        m_avaImg.gameObject.SetActive(true);//activar la imagen de quien habla

        m_currentCharSpeed = m_normalCharSpeed;
        foreach (char c in dialog.line)//mostar caracter por caracter
        {
            m_text.text += c;
            m_audio.Play();
            yield return new WaitForSeconds(m_currentCharSpeed);
        }
        m_flecha.gameObject.SetActive(true);//cuando acabe de mostrar todo el dialogo activar la flecha para cerrar o continuar al sig dialogo
        endDialog = true;
        canChange = false;
    }

    IEnumerator closeDialog()
    {
        closingDialog = true;
        m_flecha.gameObject.SetActive(false);
        m_panel.GetComponent<Animator>().SetBool("close", true);
        m_avaImg.gameObject.SetActive(false);

        m_text.transform.SetParent(m_panel.transform);

        yield return new WaitForSeconds(2.2f);

        closingDialog = false;
        endDialog = false;
        isFast = false;
        m_panel.GetComponent<Animator>().SetBool("close", false);
        m_panel.GetComponent<Animator>().enabled = false;
        m_panel.enabled = false;
        m_text.text = "";
        m_text.transform.position = m_initialTextPosition;
        currentScene++;
    }

    IEnumerator nextDialog(Dialogo dialog, bool keepPanel)
    {
        m_text.transform.SetParent(m_panel.transform);
        closingDialog = true;
        m_flecha.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        closingDialog = false;
        endDialog = false;
        m_panel.GetComponent<Animator>().SetBool("close", false);
        isFast = false;
        m_text.text = "";
        m_text.transform.position = m_initialTextPosition;

        m_text.transform.SetParent(m_canvas.transform);
        m_text.rectTransform.localScale = Vector2.one;
        m_flecha.rectTransform.localPosition = new Vector3(71, -dialog.size * 6, 0);
        //colocar el cuadro de la imagen de quien habla
        if (!dialog.bigPanel)
        {
            m_avaImg.rectTransform.localPosition = new Vector3(63.4f, 25.8f, 0);
        }
        else//si es un panel grande
        {
            m_avaImg.rectTransform.localPosition = new Vector3(63.4f, 8.3f, 0);
        }

        if (dialog.avatar == m_avatars[0])//si esta hablando mega man ponerlo a la izquierda
        {
            m_avaImg.rectTransform.localPosition = new Vector3(-67.4f, m_avaImg.rectTransform.localPosition.y, 0);
        }

        m_avaImg.sprite = dialog.avatar;//cambiar la imagen de quien habla

        m_text.transform.position = m_initialTextPosition;
        m_text.font = m_fonts[dialog.indexFont]; //cambiar el color del texto

        m_panel.enabled = true;//activar el panel
        m_panel.GetComponent<Animator>().enabled = true;
        
        m_panel.GetComponent<Animator>().SetBool("bigPanel", dialog.bigPanel);//animacion del panel segun el tamano de las lineas

        m_avaImg.gameObject.SetActive(true);//activar la imagen de quien habla

        m_currentCharSpeed = m_normalCharSpeed;
        foreach (char c in dialog.line)//mostar caracter por caracter
        {
            m_text.text += c;
            m_audio.Play();
            yield return new WaitForSeconds(m_currentCharSpeed);
        }
        m_flecha.gameObject.SetActive(true);//cuando acabe de mostrar todo el dialogo activar la flecha para cerrar o continuar al sig dialogo
        endDialog = true;
        canChange = false;
    }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player" && !already)
    {
      m_panel.enabled = false;
      already = true;
      if (escenas[currentScene].dialogos.Length > 1)
      {
        StartCoroutine(createDialog(escenas[currentScene].dialogos[0], false));
      }
      else
      {
        StartCoroutine(createDialog(escenas[currentScene].dialogos[0], false));
      }
    }
  }
}
