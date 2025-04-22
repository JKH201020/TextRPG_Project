//using System.Data;
//using System.Security.Cryptography.X509Certificates;

//namespace Firstprogram
//{
//    class Player // 플레이어 클래스
//    {
//        public string Name { get; set; } // 이름을 받아옴
//        public string Class { get; set; } // 직업 받아옴
//        public int Lv { get; set; } // 레벨 받아옴
//        public float Att { get; set; } // 공격력 받아옴
//        public int Df { get; set; } // 방어력 받아옴
//        public int Hp { get; set; } // 체력 받아옴
//        public int Gold { get; set; } // 돈 받아옴
//        public int clearCount { get; set; } // 던전 클리어 횟수 받아오기
//        public List<Item> InvenItem { get; set; } // 내 아이템 목록 받아옴

//        public Player() // 플레이어 정보
//        {
//            Name = " "; // 이름
//            Class = "전사"; // 직업
//            Lv = 1; // 레벨
//            Att = 10; // 공격력
//            Df = 5; // 방어력
//            Hp = 100; // 체력
//            Gold = 1500; // 돈
//            clearCount = 0; // 던전 클리어 횟수
//            InvenItem = new List<Item>(); // 내 아이템 리스트 생성
//        }

//        public void E(Item item) // 아이템 장착
//        {
//            if (!item.ItemEquip) // 아이템 장착 해제 했을 때
//            {
//                item.ItemEquip = true; // 장착
//                Att += item.ItemAtt; // 플레이어 공격력 + 무기 공격력
//                Df += item.ItemDf; // 플레이어 방어력 + 방어구 방어력
//            }
//        }

//        public void UnE(Item item) // 아이템 장착 해제
//        {
//            if (item.ItemEquip) // 아이템 장착 했을 때
//            {
//                item.ItemEquip = false; // 해제
//                Att -= item.ItemAtt; // 플레이어 공격력 - 무기 공격력
//                Df -= item.ItemDf; // 플레이어 방어력 - 방어구 방어력
//            }
//        }
//    }
//}
