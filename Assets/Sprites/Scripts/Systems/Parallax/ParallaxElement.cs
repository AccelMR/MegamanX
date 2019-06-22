using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

  public enum ScrollingType
  {
    NONE = 0,
    HORIZONTAL,
    VERTICAL
  }

  [RequireComponent(typeof(SpriteRenderer))]
  public class ParallaxElement : MonoBehaviour
  {
    [Tooltip("Makes background repeat itself infinitely in given direction.")]
    public ScrollingType scrolling = ScrollingType.NONE;

    [Tooltip("How Many Scrolling Elements Should be in this layer.")]
    public int scrollingSize = 3;

    [Tooltip("Set to true to make far away objects move faster than nearby objects.")]
    public bool cylindricalParallax = false;

    private SpriteRenderer[] images;
    
    private int leftIndex;
    private int rightIndex;

    private Vector2 imageSize;
    
    public void ParallaxMove(Vector3 parallax)
    {
      if (scrolling != 0)
      {
        foreach (var item in images)
        {
          item.transform.Translate(parallax);
        }
      }
      else
      {
        transform.Translate(parallax);
      }
    }

    public void StartUpScrolling()
    {
      Assert.AreNotEqual(ScrollingType.NONE, scrolling);

      Vector3 position = transform.position;
      images = new SpriteRenderer[scrollingSize];

      var backSprite = GetComponent<SpriteRenderer>();
      imageSize = backSprite.bounds.size;

      var backgrounds = new GameObject[scrollingSize - 1];

      int i;
      for (i = 0; i < scrollingSize / 2; i++)
      {
        backgrounds[i] = new GameObject(gameObject.name + "(P" + (i + 1) + ")");
        images[i] = backgrounds[i].CopyComponent(backSprite);

        backgrounds[i].transform.SetParent(transform.parent);
        backgrounds[i].transform.localScale = transform.localScale;
      }

      images[i] = backSprite;

      for (; i < scrollingSize - 1; i++)
      {
        backgrounds[i] = new GameObject(gameObject.name + "(P" + (i + 2) + ")");
        images[i + 1] = backgrounds[i].CopyComponent(backSprite);

        backgrounds[i].transform.SetParent(transform.parent);
        backgrounds[i].transform.localScale = transform.localScale;
      }

      for (i = 0; i < backgrounds.Length; i++)
      {
        int scrollAmount = (i - scrollingSize / 2) + ((i < scrollingSize / 2) ? 0 : 1);
        if (scrolling == ScrollingType.HORIZONTAL)
        {
          backgrounds[i].transform.localPosition = transform.localPosition +
                                                   new Vector3(imageSize.x, 0) *
                                                   scrollAmount;
        }
        else if (scrolling == ScrollingType.VERTICAL)
        {
          backgrounds[i].transform.localPosition = transform.localPosition +
                                                   new Vector3(0, imageSize.y) *
                                                   scrollAmount;
        }
      }

      leftIndex = 0;
      rightIndex = scrollingSize - 1;
    }

    public void UpdateScrolling(Rect camRect)
    {
      Assert.AreNotEqual(ScrollingType.NONE, scrolling);

      if (scrolling == ScrollingType.HORIZONTAL)
      {
        while (camRect.xMax > SpriteLocalToWorld(images[rightIndex]).xMax)
        {
          ScrollLeft();
        }
        while (camRect.xMin < SpriteLocalToWorld(images[leftIndex]).xMin)
        {
          ScrollRight();
        }
      }
      else if(scrolling == ScrollingType.VERTICAL)
      {
        while (camRect.yMax > SpriteLocalToWorld(images[rightIndex]).yMax)
        {
          ScrollDown();
        }
        while (camRect.yMin < SpriteLocalToWorld(images[leftIndex]).yMin)
        {
          ScrollUp();
        }
      }
    }

    private void ScrollDown()
    {
      images[leftIndex].transform.position = images[rightIndex].transform.position -
                                             Vector3.down * (imageSize.y);

      rightIndex = leftIndex;
      leftIndex++;
      if (leftIndex > scrollingSize - 1)
      {
        leftIndex = 0;
      }
    }

    private void ScrollUp()
    {
      images[rightIndex].transform.position = images[leftIndex].transform.position -
                                              Vector3.up * (imageSize.y);

      leftIndex = rightIndex;
      rightIndex--;
      if (rightIndex < 0)
      {
        rightIndex = scrollingSize - 1;
      }
    }

    private void ScrollRight()
    {
      images[rightIndex].transform.position = images[leftIndex].transform.position -
                                              Vector3.right * (imageSize.x);

      leftIndex = rightIndex;
      rightIndex--;
      if (rightIndex < 0)
      {
        rightIndex = scrollingSize - 1;
      }
    }

    private void ScrollLeft()
    {
      images[leftIndex].transform.position = images[rightIndex].transform.position -
                                             Vector3.left * (imageSize.x);

      rightIndex = leftIndex;
      leftIndex++;
      if (leftIndex > scrollingSize - 1)
      {
        leftIndex = 0;
      }
    }

    // TODO: Move this somewhere else in case it's needed again
    private Rect SpriteLocalToWorld(SpriteRenderer sp)
    {
      Rect rect = new Rect();
      rect.max = sp.transform.position + sp.bounds.size / 2f;
      rect.min = sp.transform.position - sp.bounds.size / 2f;
      return rect;
    }
  }

#if UNITY_EDITOR
[CustomEditor(typeof(ParallaxElement)), CanEditMultipleObjects]
public class ParallaxEditor : Editor
{
  override public void OnInspectorGUI()
  {
    var parallax = target as ParallaxElement;

    parallax.scrolling = (ScrollingType)EditorGUILayout.EnumPopup(
      "Scrolling",
      parallax.scrolling
    );

    if (parallax.scrolling != 0)
    {
      parallax.scrollingSize = EditorGUILayout.IntSlider(
        "Scrolling Size",
        parallax.scrollingSize,
        3, 20
      );
    }

    parallax.cylindricalParallax = GUILayout.Toggle(
      parallax.cylindricalParallax,
      "Cylindrical Parallax"
    );
  }
}
  #endif