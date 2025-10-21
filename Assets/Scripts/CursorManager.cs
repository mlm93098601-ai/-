using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    [Header("锤子敲打前的图片")]
    public Sprite normalIcon;
    [Header("锤子敲下去的图片")]
    public Sprite hitIcon;
    public Image cursorIcon;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            cursorIcon.sprite = hitIcon;
        }
        else
        {
            cursorIcon.sprite = normalIcon;
        }

        cursorIcon.rectTransform.position = Input.mousePosition;
    }
}
