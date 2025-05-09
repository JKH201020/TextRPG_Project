﻿using Firstprogram;
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

public class Store // 상점 아이템
{
    public List<Item> List { get; set; } // 상점 아이템 목록

    public Store()
    {
        List = new List<Item> // 아이템 리스트 생성
            {
            new Item("천갑옷", "방어력", 0, 2, "천으로 만든 싼 갑옷입니다.", 500), //장비 객체화, 아이템 추가
            new Item("수련자 갑옷", "방어력", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000),
            new Item("무쇠갑옷", "방어력", 0, 9, " 무쇠로 만들어져 튼튼한 갑옷입니다.", 2000),
            new Item("스파르타의 갑옷", "방어력", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500),
            new Item("낡은 검", "공격력", 2, 0, " 쉽게 볼 수 있는 낡은 검 입니다.", 600),
            new Item("청동 도끼", "공격력", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500 ),
            new Item("스파르타의 창", "공격력", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000),
            new Item("죽 창", "공격력", 14, 0, "극강의 공격력을 가진 창.", 4444)
            };
    }

    public static Store itemStatus = new Store(); // itemStatus 초기화, 상점Store 구매 완료 표시를 위해 생성

    public void storeUpdate() // 상점에 아이템을 초기화
    {
        itemStatus.List = new List<Item>() // 구매 이후에 쓸 아이템 리스트
            {
            new Item("천갑옷", "방어력", 0, 2, "천으로 만든 싼 갑옷입니다.", 500), //장비 객체화, 아이템 추가
            new Item("수련자 갑옷", "방어력", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000),
            new Item("무쇠갑옷", "방어력", 0, 9, " 무쇠로 만들어져 튼튼한 갑옷입니다.", 2000),
            new Item("스파르타의 갑옷", "방어력", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500),
            new Item("낡은 검", "공격력", 2, 0, " 쉽게 볼 수 있는 낡은 검 입니다.", 600),
            new Item("청동 도끼", "공격력", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500 ),
            new Item("스파르타의 창", "공격력", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000),
            new Item("죽 창", "공격력", 14, 0, "극강의 공격력을 가진 창.", 4444)
            };
    }
}