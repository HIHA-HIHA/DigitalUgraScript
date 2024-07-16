using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PageManager : MonoBehaviour
{
    [SerializeField]
    private List<Page> pages;

    public static PageManager Instanse;

    private void Awake()
    {
        Instanse = this;
    }

    public void OpenPage(Page needPage)
    {
        pages.ForEach(page =>
        {

            if (page.NamePage == needPage.NamePage)
            {
                page.OpenPage();
            }
            else
            {
                if (needPage.CloseOtherPages)
                    page.ClosePage();
            }
        });
    }

    public void OpenPage(string needPage)
    {
        bool closeOtherPages = true;
        pages.ForEach(page =>
        {
            if (page.NamePage == needPage)
                closeOtherPages = page.CloseOtherPages;
        });


        pages.ForEach(page =>
        {
            if (page.NamePage == needPage)
            {
                page.OpenPage();
            }
            else
            {
                if (closeOtherPages)
                    page.ClosePage();
            }
        });
    }
}
