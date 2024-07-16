using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UERandom = UnityEngine.Random;



public class CardActionOneTypeSupports : MonoBehaviour, ICardAction
{

    public void Activate()
    {
        List<Action> listSupports = new List<Action>()
        {
            GetFirstSupport,
            GetSecondSupport,
            GetThridSupport,
            GetFourthSupport,
            GetFifthSupport,
        };

        listSupports[UERandom.Range(0, listSupports.Count)].Invoke();
    }

    

    private void GetFirstSupport()
    {
        CreatorMessage.Instanse.CreateMessage("Меры поддержки", "Получите грант в размере 1000 рублей на развитие вашего ИТ-проекта.");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money += 1000;
    }

    private void GetSecondSupport()
    {
        CreatorMessage.Instanse.CreateMessage("Меры поддержки", "Вам можно будет снизить налог на 50 % при попадании на поле «Налоговая инспекция».");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.TaxInspectionDiscount += 1;
    }

    private void GetThridSupport()
    {
        CreatorMessage.Instanse.CreateMessage("Меры поддержки", "Ваши компании освобождаются от плановых государственных и муниципальных проверок получите 500 рублей.");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money += 500;
    }

    private void GetFourthSupport()
    {
        CreatorMessage.Instanse.CreateMessage("Меры поддержки", "Пройди обучение в школе креативных индустрий и получи дополнительный ход");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        SingletonDataset.Instanse.PlayerMovement.ActivePlayerMoved = false;
    }

    private void GetFifthSupport()
    {
        CreatorMessage.Instanse.CreateMessage("Меры поддержки", "Получите финансирование стартапа - получите 100 рубле на развитие");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;

        activePlayer.Money += 100;
    }

}
