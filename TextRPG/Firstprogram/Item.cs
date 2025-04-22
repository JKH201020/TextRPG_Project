using Firstprogram;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

public class Item // 아이템
{
    public string ItemName { get; set; } // 아이템 이름을 받아온다.
    public string ItemType { get; set; } // 아이템 타입(공격력, 방어력)을 받아온다..
    public int ItemAtt { get; set; } // 아이템 타입(공격력, 방어력)을 받아온다.
    public int ItemDf { get; set; } // 아이템 타입(공격력, 방어력)을 받아온다.
    public string ItemInfo { get; set; } // 아이템 정보를 받아온다.
    public int ItemPrice { get; set; } // 아이템 가격을 받아온다.
    public bool ItemPur { get; set; } // 아이템 구매 여부를 받아온다.
    public bool ItemEquip { get; set; } // 아이템 장착 여부를 받아온다.

    public Item(string ItmName, string ItmType, int ItmAtt, int ItmDf, string ItmInfo, int ItmPri) // 아이템 정보 저장
    {
        ItemName = ItmName;
        ItemType = ItmType;
        ItemAtt = ItmAtt;
        ItemDf = ItmDf;
        ItemInfo = ItmInfo;
        ItemPrice = ItmPri;
        ItemPur = false;
        ItemEquip = false;
    }
}