using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

class Player // 플레이어 클래스
{
    public string Name { get; set; } // 이름을 받아옴
    public string Chad { get; set; } // 직업 받아옴
    public int Lv { get; set; } // 레벨 받아옴
    public int Att { get; set; } // 공격력 받아옴
    public int Df { get; set; } // 방어력 받아옴
    public int Hp { get; set; } // 체력 받아옴
    public int Gold { get; set; } // 돈 받아옴
    public List<Item> InvenItem { get; set; } // 내 아이템 목록 받아옴

    public Player() // 플레이어 정보
    {
        Name = " "; // 이름
        Chad = "전사"; // 직업
        Lv = 1; // 레벨
        Att = 10; // 공격력
        Df = 5; // 방어력
        Hp = 100; // 체력
        Gold = 1500; // 돈
        InvenItem = new List<Item>(); // 내 아이템 리스트 생성
    }

    public void E(Item item) // 장착
    {
        if (!item.itemEquip) // 장착 안했을 때
        {
            item.itemEquip = true; // 장착
            Att += item.itemAtt; // 플레이어 공격력 + 무기 공격력
            Df += item.itemDf; // 플레이어 방어력 + 방어구 방어력
        }
    }

    public void UnE(Item item) // 장착 해제
    {
        if (item.itemEquip) // 장착 안했을 때
        {
            item.itemEquip = false; // 해제
            Att -= item.itemAtt; // 플레이어 공격력 - 무기 공격력
            Df -= item.itemDf; // 플레이어 방어력 - 방어구 방어력
        }
    }
}

class Item // 아이템
{
    public string itemName { get; set; } // 아이템 이름을 받아온다.
    public string itemType { get; set; } // 아이템 타입(공격력, 방어력)을 받아온다..
    public int itemAtt { get; set; } // 아이템 타입(공격력, 방어력)을 받아온다.
    public int itemDf { get; set; } // 아이템 타입(공격력, 방어력)을 받아온다.
    public string itemInfo { get; set; } // 아이템 정보를 받아온다.
    public int itemPrice { get; set; } // 아이템 가격을 받아온다.
    public bool itemPur { get; set; } // 아이템 구매 여부를 받아온다.
    public bool itemEquip { get; set; } // 아이템 장착 여부를 받아온다.

    public Item(string ItmName, string ItmType, int ItmAtt, int ItmDf, string ItmInfo, int ItmPri)
    {
        itemName = ItmName;
        itemType = ItmType;
        itemAtt = ItmAtt;
        itemDf = ItmDf;
        itemInfo = ItmInfo;
        itemPrice = ItmPri;
        itemPur = false;
        itemEquip = false;
    }

    public static Store itemStatus = new Store(); // itemStatus 초기화, 상점Store 구매 완료 표시를 위해 생성, 게임 전체에서 접근 가능한 정적 객체

    public class Store // 상점 아이템
    {
        public List<Item> list { get; set; } // 상점 아이템 목록

        public Store()
        {
            list = new List<Item> // 아이템 리스트 생성
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

        public void storeUpdate() // 상점에 아이템을 초기화
        {
            itemStatus.list = new List<Item>() // 구매 이후에 쓸 아이템 리스트
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
}