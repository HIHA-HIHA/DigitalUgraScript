using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class CreatorMessage : MonoBehaviour
{
    [SerializeField]
    private Message messagePrefab;

    [SerializeField]
    private Transform messageOrigin;

    [SerializeField]
    private List<Message> messages;

    public static CreatorMessage Instanse;

    private void Awake()
    {
        Instanse = this;
    }


    public void CreateMessage(string name, string description)
    {

        var message = Instantiate(messagePrefab, messageOrigin);
        message.ChangeFields(name, description);

        if (messages.Count > 0)
        {
            message.gameObject.SetActive(false);
        }
        else
        {
            Destroy(message.gameObject, 6f);
        }

        messages.Add(message);

    }


    public void RemoveMessage(Message message)
    {

        if (messages.IndexOf(message) == -1)
            return;

        messages.Remove(message);
        if (messages.Count > 0)
        {
            var firstMessage = messages.First<Message>();
            firstMessage.gameObject.SetActive(true);
            Destroy(firstMessage.gameObject, 3f);

        }
    }

    public void CreateMessage()
    {
        var name = Random.Range(0, 100).ToString();
        var description = Random.Range(101, 200).ToString();
        CreateMessage(name, description);
    }
}
