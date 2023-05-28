using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SelectableObject : MonoBehaviour
{
    public bool IsSelected = false;
    public static event Action OnOtherObjectSelected;
    public static event Action OnObjectSelectionComplete;
    public static GameObject SelectedObject { get; private set; }
    void Start()
    {
        OnOtherObjectSelected += DeselectIfOtherObjectSelected;
    }

    void DeselectIfOtherObjectSelected()
    {
        if (IsSelected)
        {
            IsSelected = false;
        }
    }
    public static void InvokeDeselectEvent()
    {
        OnOtherObjectSelected.Invoke();
    }
    void OnMouseDown()
    {
        InvokeDeselectEvent();
        IsSelected = true;
        SelectedObject = gameObject;
        if (OnObjectSelectionComplete != null)
        {
            OnObjectSelectionComplete.Invoke();
        }
    }
}
