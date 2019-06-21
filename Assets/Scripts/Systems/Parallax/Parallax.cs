using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parallax : MonoBehaviour
{
  [Tooltip("Camera to use for the parallax. Defaults to main camera.")]
  public Camera parallaxCamera;

  [Tooltip("Enable movement left and right.")]
  public bool EnableHorizontal = true;

  [Tooltip("Enable movement up and down.")]
  public bool EnableVertical = true;

  [Tooltip("How intense will parallax be.")]
  [Range(1, 2)]
  public float parallaxIntensity = 10f / 9f;

  [Tooltip("Base Z position all objects should be in front of.")]
  public float parallaxPosition = -10;

  private Vector3 prevCameraPosition;
  private ParallaxElement[] parallaxElements;
  private float[] parallaxScales;

  // Start is called before the first frame update
  void Start()
  {
    if (null == parallaxCamera)
    {
      parallaxCamera = Camera.main;
    }
    prevCameraPosition = parallaxCamera.transform.position;

    parallaxElements = gameObject.GetComponentsInChildren<ParallaxElement>();
    parallaxScales = new float[parallaxElements.Length];

    for (int i = 0; i < parallaxElements.Length; i++)
    {
      float depth = (parallaxElements[i].transform.position.z + parallaxPosition);

      if (parallaxElements[i].cylindricalParallax)
      {
        parallaxScales[i] = -depth * parallaxIntensity / 5f;
      }
      else
      {
        parallaxScales[i] = Mathf.Pow(parallaxIntensity, -depth) - 1;
      }

      if (parallaxElements[i].scrolling != 0)
      {
        parallaxElements[i].StartUpScrolling();
      }
    }

  }

  // Update is called once per frame
  void Update()
  {
    if (parallaxCamera.transform.position != prevCameraPosition)
    {
      Vector3 camPos = parallaxCamera.transform.position;
      Rect camRect = new Rect();
      camRect.max = parallaxCamera.ViewportToWorldPoint(
        new Vector3(1, 1, parallaxCamera.nearClipPlane)
      );
      camRect.min = parallaxCamera.ViewportToWorldPoint(
        new Vector3(0, 0, parallaxCamera.nearClipPlane)
      );

      for (int i = 0; i < parallaxElements.Length; i++)
      {
        Vector3 parallax = Vector3.zero;

        if (EnableHorizontal)
        {
          parallax.x = (prevCameraPosition.x - camPos.x) * parallaxScales[i];
        }

        if (EnableVertical)
        {
          parallax.y = (prevCameraPosition.y - camPos.y) * parallaxScales[i];
        }

        if (parallaxElements[i].scrolling != 0)
        {
          parallaxElements[i].UpdateScrolling(camRect);
        }

        parallaxElements[i].ParallaxMove(parallax);
      }

      prevCameraPosition = camPos;
    }
  }
}

