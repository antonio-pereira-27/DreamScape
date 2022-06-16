using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class NoteAppear : MonoBehaviour
{
    [SerializeField]
    private Image _noteImage;

    [HideInInspector] public bool reveal;

    private void Update()
    {
        _noteImage.enabled = reveal;
    }

   
}
