using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActionTwoTaxInspection: MonoBehaviour, ICardAction
{
    public void Activate()
    {
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;


        var tempMoney = (Mathf.FloorToInt(activePlayer.Money / 100) * 10) / (2 * activePlayer.TaxInspectionDiscount);


        CreatorMessage.Instanse.CreateMessage("Налоговая инспекция", "Игрок должен заплатить налоги за свои владения: "+tempMoney);


        var tempPlayerMoney = activePlayer.Money - tempMoney;
        activePlayer.Money = Mathf.Clamp(tempPlayerMoney, 0, int.MaxValue);

    }


}
