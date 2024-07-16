using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Page : MonoBehaviour
{
    public string NamePage;

    [SerializeField]
    private GameObject pageGameObject;

    [SerializeField]
    private UnityEvent onClosePage;

    [SerializeField]
    private UnityEvent onOpenPage;

    [SerializeField]
    private bool closeOtherPages;

    public bool CloseOtherPages { get => closeOtherPages; private set => closeOtherPages = value; }

    public void OpenPage()
    {
        if (pageGameObject)
        {
            pageGameObject.SetActive(true);
            onOpenPage?.Invoke();
        }
    }

    public void ClosePage()
    {
        if (pageGameObject)
        {
            pageGameObject.SetActive(false);
            onClosePage?.Invoke();
        }
    }
}
