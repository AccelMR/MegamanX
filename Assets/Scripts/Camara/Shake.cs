using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
  public Animator camAnim;

  // <! Warning >
  /* To integrate this script you need:
   * Create a GameObject Called ShakeManager and put this sprite, asign the Tag 'ScreenShake' and asign the camera to the script
   * 1) Add a new Tag named 'ScreenShake'
   * 2) private Shake shakeEffect;
   * 3) on Start -> shakeEffect = gameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
   * 4) On collision, damage, falling, etc -> Shake.CamShake();
   */
  void camShake()
  {
    camAnim.SetTrigger("ShakeAnim");
  }

}
