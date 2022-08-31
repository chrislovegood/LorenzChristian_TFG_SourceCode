using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    Transform mCursorVisual;
    public Vector3 mDisplacement;

    void Start()
    {
      Cursor.visible = false;
      mCursorVisual = GetComponent<Transform>();
    }
    

    void Update()
    {
        if(Cursor.visible==true){ Cursor.visible = false; }
        mCursorVisual.position = Input.mousePosition + mDisplacement;
    }
}