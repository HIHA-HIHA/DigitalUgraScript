using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class Message : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TMP_Text nameField;

    [SerializeField]
    private TMP_Text description;

    public TMP_Text Description { get => description; set => description = value; }
    public TMP_Text NameField { get => nameField; set => nameField = value; }

    private void Awake()
    {
        ChangeFields("", "");
    }

    private void OnDestroy()
    {
        CreatorMessage.Instanse.RemoveMessage(this);
    }

    public void ChangeFields(string name, string description)
    {
        Description.text = description;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        CreatorMessage.Instanse.RemoveMessage(this);
        Destroy(gameObject);
    }
}
