using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UERandom = UnityEngine.Random;

public class CardActionFour : MonoBehaviour, ICardAction
{
    [SerializeField]
    private Cell[] arrayCellsTaxInspection;

    public void Activate()
    {
        List<Action> listSupports = new List<Action>()
        {
            FirstAction,
            SecondAction,
            ThirdAction,
            FourthAction,
            FifthAction,
            SixthAction,
            SeventhAction,
            EighthAction,
            NinthAction,
            TenthAction,
            EleventhAction,
            TwelfthAction,
            FourteenthAction,

            
        };

        listSupports[UERandom.Range(0, listSupports.Count)].Invoke();
    }

    private void FirstAction()
    {
        CreatorMessage.Instanse.CreateMessage("Код в мешке", "Получите 500р от каждого игрока");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        SingletonDataset.Instanse.QueueManager.Players.ForEach(player =>
        {
            if (player.IdPlayer != activePlayer.IdPlayer)
            {
                player.Money -= 500;
                activePlayer.Money += 500;
            }
        });

    }

    private void SecondAction()
    {
        bool allCellBuyed = false;
        foreach (var cell in SingletonDataset.Instanse.Cells)
        {
            if (cell.TypeCell == TypeCell.Default && !cell.Owner)
            {
                allCellBuyed = false;
                break;
            }
        }
        if ((allCellBuyed))
        {
            return;
        }

        CreatorMessage.Instanse.CreateMessage("Код в мешке", "Переместитесь на любое свободное поле и купите его");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;

        bool flag = true;
        Cell randomCell = null;
        while (flag)
        {
            randomCell = SingletonDataset.Instanse.Cells[UERandom.Range(0, SingletonDataset.Instanse.Cells.Count)];
            if (!randomCell.Owner && randomCell.TypeCell == TypeCell.Default)
                flag = false;
        }
        SingletonDataset.Instanse.PlayerMovement.MovePlayer(randomCell);
        SingletonDataset.Instanse.PurchaseManager.BuyCell(randomCell);

    }

    private void ThirdAction()
    {
        CreatorMessage.Instanse.CreateMessage("Код в мешке", "Получите 50% скидки на покупку любой собственности");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.CountDiscountOnBuyCell += 1;
    }

    private void FourthAction()
    {
        CreatorMessage.Instanse.CreateMessage("Код в мешке", "Переместитесь на поле Налоговая инспекция и заплатить штраф");
        var taxCell = arrayCellsTaxInspection[UERandom.Range(0, arrayCellsTaxInspection.Length)];
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        SingletonDataset.Instanse.PlayerMovement.MovePlayer(taxCell);

        activePlayer.Money -= (activePlayer.Money / 100) * 50;
    }

    private void FifthAction()
    {
        CreatorMessage.Instanse.CreateMessage("Код в мешке", "Потеряйте одну из своих нефтяных компаний");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;

        List<Cell> countOilCompanies = new List<Cell>();
        activePlayer.BuyedCells.ForEach(cell =>
        {
            if (cell.Street.NameStreet == "Нефтянка")
            {
                countOilCompanies.Add(cell);
            }
        });

        if (countOilCompanies.Count == 0)
            return;

        var randomCell = countOilCompanies[UERandom.Range(0, countOilCompanies.Count)];
        randomCell.RemoveOwner();
    }

    private void SixthAction()
    {
        CreatorMessage.Instanse.CreateMessage("Код в мешке", "Получите бонус в размере 200р за прохождение через старт");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Bonus += 200;
    }

    //seventh, eighth, ninth, tenth, eleventh
    private void SeventhAction()
    {
        CreatorMessage.Instanse.CreateMessage("Код в мешке", "Получите 500 рублей за каждую недвижимость связанная с IT ");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;

        List<Cell> ITCompanies = new List<Cell>();
        activePlayer.BuyedCells.ForEach(cell =>
        {
            if (cell.Street.NameStreet == "IT")
            {
                activePlayer.Money += 500;
            }
        });


    }

    private void EighthAction()
    {
        CreatorMessage.Instanse.CreateMessage("Код в мешке", "Получите 200 рублей от каждого игрока, владеющего IT компанией за оказание технической поддержки");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        SingletonDataset.Instanse.QueueManager.Players.ForEach(player =>
        {
            if (player.IdPlayer != activePlayer.IdPlayer)
            {
                bool hasITCompany = false;
                if (player.BuyedCells.Find(x => x.Street.NameStreet == "IT"))
                    hasITCompany = true;

                if(hasITCompany)
                {
                    player.Money -= 200;
                    activePlayer.Money += 200;
                }

            }
        });
    }

    private void NinthAction()
    {

        CreatorMessage.Instanse.CreateMessage("Код в мешке", "Заплатите 500 рублей за переход на отечественное программное обеспечение");
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.TaxInspectionDiscount -= 500;
    }

    private void TenthAction()
    {
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;

        if (activePlayer.BuyedCells.Find(cell => cell.NameCell == "ЦОД"))
        {
            activePlayer.Money += 200;
            CreatorMessage.Instanse.CreateMessage("Код в мешке", "Получите 200 рублей, если вы владелец ЦОД");
        }
        else
        {
            SingletonDataset.Instanse.Cells.Find(cell => cell.NameCell == "ЦОД").Owner.Money += 100;
            activePlayer.Money -= 100;
            CreatorMessage.Instanse.CreateMessage("Код в мешке", "Заплатите 100 рублей игроку, владеющему ЦОД за аренду облачного хранилища");
        }

        

    }

    private void EleventhAction()
    {
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money += 200;
        CreatorMessage.Instanse.CreateMessage("Код в мешке", "Ваша компания разработала новые инновационные технологии. Получите бонус в размере 200 рублей");

    }
    
    private void TwelfthAction()
    {
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        activePlayer.Money += 500;
        CreatorMessage.Instanse.CreateMessage("Код в мешке", "Вы решили инвестировать в молодой перспективный стартап. Получите дивиденды в размере 500 рублей");

    }

    private void ThirteenthAction()
    {
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;
        CreatorMessage.Instanse.CreateMessage("Код в мешке", "Оформи авторские права и получи дополнительный ход");
        SingletonDataset.Instanse.PlayerMovement.ActivePlayerMoved = false;

    }

    private void FourteenthAction()
    {
        var activePlayer = SingletonDataset.Instanse.QueueManager.NowPlayer;

        activePlayer.Money -= SingletonDataset.Instanse.Cells.Find(cell => cell.NameCell == "ИТ-коворкинг").PriceOnStay;
        CreatorMessage.Instanse.CreateMessage("Код в мешке", "Воспользуйся услугами ИТ-коворкинга и оплати ренту");


    }

}
