﻿using DryIoc;
using DryIoc.FastExpressionCompiler.LightExpression;
using Firstprogram;
using System;
using System.ComponentModel.Design;
using System.Net;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static Item;

class GameManager // 위치별 정보
{
    public void Home(Player player) // 0. 메인 화면(나기기)
    {
        string input; //입력값
        bool loop = true; // 루프를 반복한다

        do
        {
            Console.Clear(); // 화면 초기화

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다."); // 간단한 소개 말
            Console.WriteLine("\n1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전 입장\n5. 휴식하기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");

            input = Console.ReadLine(); //  입력값

            switch (input)
            {
                case "1": // 상태 보기
                    Status(player);
                    break;

                case "2": // 인벤토리 이동
                    Inventory(player);
                    break;

                case "3": // 상점 이동
                    Shop(player);
                    break;

                case "4": // 던전 이동
                    Stage(player);
                    break;

                case "5": // 휴식으로 이동
                    Rest(player);
                    break;

                default: // 1 ~ 3 이외의 문자를 입력할 시
                    Message("잘못된 입력입니다.");
                    break;
            }
        } while (loop); // 반복
    }

    public void Status(Player player) // 1. 상태 보기
    {
        string input;
        bool loop = true; // 루프를 반복한다
        int addAtt = 0; // 무기로 증가되는 공격력
        int addDf = 0; // 방어구로 증가되는 방어력

        for (int i = 0; i < player.InvenItem.Count; i++) // 아이템 불러오기
        {
            if (player.InvenItem[i].ItemPur == true && player.InvenItem[i].ItemType == "공격력") // 내가 가지고 있는 무기일 경우
            {
                if (player.InvenItem[i].ItemEquip == true) // 장비를 장착 중인 경우
                {
                    addAtt += player.InvenItem[i].ItemAtt; // 무기로 인한 공격력 증가의 총합
                }
            }
            else // 내가 가지고 있는 방어구일 경우
            {
                if (player.InvenItem[i].ItemEquip == true)
                {
                    addDf += player.InvenItem[i].ItemDf; // 방어구로 인한 방어력 증가의 총합
                }
            }
        }

        do
        {
            Console.Clear();

            Console.WriteLine("상태 보기\n캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {player.Lv.ToString("D2")}"); // 레벨 보기
            Console.WriteLine($"{player.Name} ( {player.Class} )"); // 이름 ( 직업 ) 보기
            Console.WriteLine(addAtt > 0 ? $"Att: {player.Att}" + $" (+{addAtt})" : $"Att: {player.Att}"); // 공격력 보기
            Console.WriteLine(addDf > 0 ? $"Df: {player.Df}" + $" (+{addDf})" : $"Df: {player.Df}"); // 방어력 보기
            Console.WriteLine($"Hp: {player.Hp}"); // 체력 보기
            Console.WriteLine($"Gold: {player.Gold} G\n"); // 소지금 보기
            Console.WriteLine("0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");

            input = Console.ReadLine(); // 입력값

            switch (input)
            {
                case "0": // 나가기
                    loop = false; // 반복 끝
                    break;

                default: // "0" 이외 다른 문자를 입력할 시
                    Message("잘못된 입력입니다.");
                    break;
            }
        } while (loop); //  반복
    }

    public void Inventory(Player player) // 2. 인벤 보기
    {
        string input;
        bool loop = true; // 반복

        do
        {
            Console.Clear();

            Console.WriteLine("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");

            for (int i = 0; i < player.InvenItem.Count; i++) // 아이템 불러오기
            {
                string StrItemEquip = player.InvenItem[i].ItemEquip ? "[E]" : ""; // 아이템을 착용했는지 안 했는지에 따라 [E]를 출력
                if (player.InvenItem[i].ItemType == "공격력") // 무기
                {
                    Console.WriteLine($"- {StrItemEquip}{player.InvenItem[i].ItemName}  | {player.InvenItem[i].ItemType} + {player.InvenItem[i].ItemAtt}  | {player.InvenItem[i].ItemInfo}");
                }
                else // 방어구
                {
                    Console.WriteLine($"- {StrItemEquip}{player.InvenItem[i].ItemName}  | {player.InvenItem[i].ItemType} + {player.InvenItem[i].ItemDf}  | {player.InvenItem[i].ItemInfo}");
                }
            }

            Console.WriteLine("\n1. 장착 관리\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");

            input = Console.ReadLine(); // 게임 시작 화면 입력값

            switch (input)
            {
                case "0": // 나가기
                    loop = false; // 반복 끝
                    Home(player); // 메인 화면으로
                    break;

                case "1": // 장착 관리
                    loop = false; // 반복 끝
                    InvenMana(player);
                    break;

                default: // "0" ~ "1" 이외의 문자를 입력할 시
                    Message("잘못된 입력입니다.");
                    break;
            }
        } while (loop); // 반복
    }

    public void InvenMana(Player player) // 2.1 장착 관리
    {
        string input; // 장착 or 나가기
        bool loop = true; // 루프를 반복한다

        do
        {
            Console.Clear();

            Console.WriteLine("인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");

            for (int i = 0; i < player.InvenItem.Count; i++) // 아이템 불러오기
            {
                string StrItemEquip = player.InvenItem[i].ItemEquip ? "[E]" : ""; // 아이템을 착용했는지 안 했는지에 따라 [E]를 출력

                if (player.InvenItem[i].ItemType == "공격력") // 무기
                {
                    Console.WriteLine($"- {i + 1} {StrItemEquip}{player.InvenItem[i].ItemName}  | {player.InvenItem[i].ItemType} +{player.InvenItem[i].ItemAtt}  | {player.InvenItem[i].ItemInfo}");
                }
                else // 방어구
                {
                    Console.WriteLine($"- {i + 1} {StrItemEquip}{player.InvenItem[i].ItemName}  | {player.InvenItem[i].ItemType} +{player.InvenItem[i].ItemDf}  | {player.InvenItem[i].ItemInfo}");
                }
            }

            Console.WriteLine("\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");

            input = Console.ReadLine();

            if (input == "0") // 0을 입력
            {
                loop = false; // 반복 끝
                Inventory(player); // 인벤으로 이동
            }

            if (int.TryParse(input, out int input2)) // input2에 input의 값을 int형으로 변환
            {
                input2--; // 인덱스는 0부터 시작. 아이템 선택은 1부터 시작

                if (input != "0" && (input2 >= 0 && input2 < player.InvenItem.Count)) // input이 "0"이 아니고, 인덱스 [0] <= input2 <= [lnvenItem 개수]
                {
                    Item selectedItem = player.InvenItem[input2]; // 내가 가진 아이템을 selcetedItem에 저장

                    if (selectedItem.ItemEquip == true) // 선택한 아이템 장착 중인 경우
                    {
                        player.UnE(selectedItem); // 장착 해제
                        Message($"{selectedItem.ItemName} 을(를) 해제했습니다.\n");
                    }
                    else // 선택한 아이템 미장착 중인 경우
                    {
                        for (int i = 0; i < player.InvenItem.Count; i++) // 인벤토리 불러오기
                        {
                            if(player.InvenItem[i].ItemEquip == true) // 인벤토리 i번째 아이템을 착용 중인 경우
                            {
                                if (player.InvenItem[i].ItemType == selectedItem.ItemType) // 내가 선택한 아이템 타입 == i번째 인벤 아이템이 타입
                                {
                                    if (player.InvenItem[i].ItemName != selectedItem.ItemName) // 내가 선택한 아이템 이름 != i번째 인벤 아이템 이름
                                    {
                                        player.UnE(player.InvenItem[i]); // 기존에 장착한 아이템 해제
                                        player.E(selectedItem); // 장착하기 원하는 아이템
                                    }
                                }
                            }
                            else // 인벤토리 i번째 아이템을 착용 중이 아닌 경우
                            {
                                player.E(selectedItem); // 장착하기 원하는 아이템 
                            }
                        }

                        Message($"{selectedItem.ItemName} 을(를) 장착했습니다.\n");
                    }
                }
                else // 그 외에 숫자 입력
                {
                    Message("잘못된 입력입니다.");
                }
            }
            else // 숫자가 아닌 문자를 눌렀을 경우
            {
                Message("잘못된 입력입니다.");
            }
        } while (loop); // 반복
    }

    public static Store itemStatus = new Store(); // itemStatus 초기화, 상점Store 구매 완료 표시를 위해 생성, 게임 전체에서 접근 가능한 정적 객체

    public void Shop(Player player) // 3. 상점
    {
        string input;
        bool loop = true; // 반복한다

        do
        {
            Console.Clear();

            Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine($"[보유 골드]\n{player.Gold} G\n");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < itemStatus.List.Count; i++) // 아이템 불러오기
            {
                string StrItemPur = itemStatus.List[i].ItemPur ? "구매 완료" : $"{itemStatus.List[i].ItemPrice} G"; // 가격을 표시할 건지 구매 완료를 표시할 건지

                if (itemStatus.List[i].ItemType == "공격력")
                {
                    Console.WriteLine($"- {itemStatus.List[i].ItemName}  | {itemStatus.List[i].ItemType} +{itemStatus.List[i].ItemAtt}  | {itemStatus.List[i].ItemInfo}  | {StrItemPur}");
                }
                else
                {
                    Console.WriteLine($"- {itemStatus.List[i].ItemName}  | {itemStatus.List[i].ItemType} +{itemStatus.List[i].ItemDf}  | {itemStatus.List[i].ItemInfo}  | {StrItemPur}");
                }
            }

            Console.WriteLine("\n1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");

            input = Console.ReadLine(); // 입력값

            switch (input)
            {
                case "0": // 나가기
                    loop = false; // 반복 끝
                    break;

                case "1": // 아이템 구매
                    loop = false; // 반복 끝
                    BuyItem(player); // 구매창으로 이동
                    break;

                case "2": // 아이템 판매
                    loop = false; // 반복 끝
                    ItemSell(player); // 판매창으로 이동
                    break;

                default: // "0" ~ "2" 이외의 문자를 입력할 시
                    Message("잘못된 입력입니다.");
                    break;
            }
        } while (loop); // 반복
    }

    public void BuyItem(Player player) // 3.1 아이템 구매
    {
        bool loop = true; // 반복한다

        do
        {
            Console.Clear();

            Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine($"[보유 골드]\n{player.Gold} G\n");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < itemStatus.List.Count; i++) // 아이템 불러오기
            {
                string StrItemPur = itemStatus.List[i].ItemPur ? "구매 완료" : $"{itemStatus.List[i].ItemPrice} G"; // 가격을 표시할 건지 구매 완료를 표시할 건지

                if (itemStatus.List[i].ItemType == "공격력")
                {
                    Console.WriteLine($"- {i + 1} {itemStatus.List[i].ItemName}  | {itemStatus.List[i].ItemType} +{itemStatus.List[i].ItemAtt}  | {itemStatus.List[i].ItemInfo}  | {StrItemPur}"); // 무기 출력
                }
                else
                {
                    Console.WriteLine($"- {i + 1} {itemStatus.List[i].ItemName}  | {itemStatus.List[i].ItemType} +{itemStatus.List[i].ItemDf}  | {itemStatus.List[i].ItemInfo}  | {StrItemPur}"); // 방어구 출력
                }
            }

            Console.WriteLine("\n0. 나가기\n");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

            bool itemBuy = true; // 반복한다

            string input = Console.ReadLine(); // 입력값

            if (int.TryParse(input, out int input2)) // 문자로 입력받은 숫자를 int형으로 변환 -> input: 문자, input2: 숫자
            {
                input2--; // 리스트는 0부터 시작하므로 입력값에서 1 감소

                if (input != "0" && (input2 >= 0 && input2 < itemStatus.List.Count)) // input이 "0"이 아니고,  List[0] <= 입력값 < List[리스트의 개수] 
                {
                    Item BI = itemStatus.List[input2]; // BI(BuyItem)의 줄인말

                    switch (input) // 입력에 따른 선택 
                    {
                        case "1": // 천갑옷
                            if (BI.ItemPur) // 인벤토리에 아이템이 있을 경우
                            {
                                Message("이미 구매한 아이템입니다.");
                            }
                            else if (player.Gold >= BI.ItemPrice) // 소지금 >= 아이템 가격
                            {
                                BI.ItemPur = true; // 아이템 구매했다.
                                player.Gold -= BI.ItemPrice; // 소지금을 아이템 가격만큼 차감
                                player.InvenItem.Add(BI); // 내 아이템에 추가
                                Message($"{BI.ItemName}구매를 완료했습니다.");
                            }
                            else if (player.Gold < BI.ItemPrice) // 소지금 < 아이템 가격
                            {
                                Message("Gold 가 부족합니다.");
                            }
                            break;

                        case "2": // 수련자 갑옷
                            if (BI.ItemPur) // 인벤토리에 아이템이 있을 경우
                            {
                                Message("이미 구매한 아이템입니다.");
                            }
                            else if (player.Gold >= BI.ItemPrice) // 소지금 >= 아이템 가격
                            {
                                BI.ItemPur = true; // 아이템 구매했다.
                                player.Gold -= BI.ItemPrice; // 소지금을 아이템 가격만큼 차감
                                player.InvenItem.Add(BI); // 내 아이템에 추가
                                Message($"{BI.ItemName}구매를 완료했습니다.");
                            }
                            else if (player.Gold < BI.ItemPrice) // 소지금 < 아이템 가격
                            {
                                Message("Gold 가 부족합니다.");
                            }
                            break;

                        case "3": // 무쇠갑옷
                            if (BI.ItemPur) // 인벤토리에 아이템이 있을 경우
                            {
                                Message("이미 구매한 아이템입니다.");
                            }
                            else if (player.Gold >= BI.ItemPrice) // 소지금 >= 아이템 가격
                            {
                                BI.ItemPur = true; // 아이템 구매했다.
                                player.Gold -= BI.ItemPrice; // 소지금을 아이템 가격만큼 차감
                                player.InvenItem.Add(BI); // 내 아이템에 추가
                                Message($"{BI.ItemName}구매를 완료했습니다.");
                            }
                            else if (player.Gold < BI.ItemPrice) // 소지금 < 아이템 가격
                            {
                                Message("Gold 가 부족합니다.");
                            }
                            break;

                        case "4": // 스파르타의 갑옷
                            if (BI.ItemPur) // 인벤토리에 아이템이 있을 경우
                            {
                                Message("이미 구매한 아이템입니다.");
                            }
                            else if (player.Gold >= BI.ItemPrice) // 소지금 >= 아이템 가격
                            {
                                BI.ItemPur = true; // 아이템 구매했다.
                                player.Gold -= BI.ItemPrice; // 소지금을 아이템 가격만큼 차감
                                player.InvenItem.Add(BI); // 내 아이템에 추가
                                Message($"{BI.ItemName}구매를 완료했습니다.");
                            }
                            else if (player.Gold < BI.ItemPrice) // 소지금 < 아이템 가격
                            {
                                Message("Gold 가 부족합니다.");
                            }
                            break;

                        case "5": // 낡은 검
                            if (BI.ItemPur) // 인벤토리에 아이템이 있을 경우
                            {
                                Message("이미 구매한 아이템입니다.");
                            }
                            else if (player.Gold >= BI.ItemPrice) // 소지금 >= 아이템 가격
                            {
                                BI.ItemPur = true; // 아이템 구매했다.
                                player.Gold -= BI.ItemPrice; // 소지금을 아이템 가격만큼 차감
                                player.InvenItem.Add(BI); // 내 아이템에 추가
                                Message($"{BI.ItemName}구매를 완료했습니다.");
                            }
                            else if (player.Gold < BI.ItemPrice) // 소지금 < 아이템 가격
                            {
                                Message("Gold 가 부족합니다.");
                            }
                            break;

                        case "6": // 청동 도끼
                            if (BI.ItemPur) // 인벤토리에 아이템이 있을 경우
                            {
                                Message("이미 구매한 아이템입니다.");
                            }
                            else if (player.Gold >= BI.ItemPrice) // 소지금 >= 아이템 가격
                            {
                                BI.ItemPur = true; // 아이템 구매했다.
                                player.Gold -= BI.ItemPrice; // 소지금을 아이템 가격만큼 차감
                                player.InvenItem.Add(BI); // 내 아이템에 추가
                                Message($"{BI.ItemName}구매를 완료했습니다.");
                            }
                            else if (player.Gold < BI.ItemPrice) // 소지금 < 아이템 가격
                            {
                                Message("Gold 가 부족합니다."); ;
                            }
                            break;

                        case "7": // 스파르타의 창
                            if (BI.ItemPur) // 인벤토리에 아이템이 있을 경우
                            {
                                Message("이미 구매한 아이템입니다.");
                            }
                            else if (player.Gold >= BI.ItemPrice) // 소지금 >= 아이템 가격
                            {
                                BI.ItemPur = true; // 아이템 구매했다.
                                player.Gold -= BI.ItemPrice; // 내 아이템에 추가
                                Message($"{BI.ItemName}구매를 완료했습니다.");
                            }
                            else if (player.Gold < BI.ItemPrice) // 소지금 < 아이템 가격
                            {
                                Message("Gold 가 부족합니다.");
                            }
                            break;

                        case "8": //  죽창
                            if (BI.ItemPur) // 인벤토리에 아이템이 있을 경우
                            {
                                Message("이미 구매한 아이템입니다.");
                            }
                            else if (player.Gold >= BI.ItemPrice) // 소지금 >= 아이템 가격
                            {
                                BI.ItemPur = true; // 아이템 구매했다.
                                player.Gold -= BI.ItemPrice; // 소지금을 아이템 가격만큼 차감
                                player.InvenItem.Add(BI); // 내 아이템에 추가
                                Message($"{BI.ItemName}구매를 완료했습니다.");
                            }
                            else if (player.Gold < BI.ItemPrice) // 소지금 < 아이템 가격
                            {
                                Message("Gold 가 부족합니다.");
                            }
                            break;

                        default: // 그 외 입력
                            Message("잘못된 입력입니다."); //ErrorMessage는 아래 있음
                            break;
                    }
                }
                else if (input == "0")
                {
                    loop = false; // 반복 끝
                    Shop(player); // 상점으로 이동
                }
                else
                {
                    Message("잘못된 입력입니다."); // 숫자로 변환 실패
                }
            }
            else
            {
                Message("잘못된 입력입니다."); // 숫자로 변환 실패
            }
        } while (loop); //  계속 반복
    }

    public void ItemSell(Player player) // 3.2 아이템 판매
    {
        string input;
        bool loop = true; // 반복

        do
        {
            Console.Clear();

            Console.WriteLine("상점 - 아이템 판매 \n필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine($"[보유 골드]\n{player.Gold} G\n");

            for (int i = 0; i < player.InvenItem.Count; i++) // 아이템 불러오기
            {
                string StrItemEquip = player.InvenItem[i].ItemEquip ? "[E]" : ""; // 아이템을 착용했는지 안 했는지에 따라 [E]를 출력

                if (player.InvenItem[i].ItemType == "공격력") // 무기
                {
                    Console.WriteLine($"- {StrItemEquip}{player.InvenItem[i].ItemName}  | {player.InvenItem[i].ItemType} + {player.InvenItem[i].ItemAtt}  | {player.InvenItem[i].ItemInfo}  | {(int)(player.InvenItem[i].ItemPrice * 0.85)}");
                }
                else // 방어구
                {
                    Console.WriteLine($"- {StrItemEquip}{player.InvenItem[i].ItemName}  | {player.InvenItem[i].ItemType} + {player.InvenItem[i].ItemDf}  | {player.InvenItem[i].ItemInfo}  | {(int)(player.InvenItem[i].ItemPrice * 0.85)}");
                }
            }

            Console.WriteLine("\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요. (아이템 판매는 아이템 이름으로 입력)\n>> ");

            bool itemSold = false; // 아이템 판매 여부

            input = Console.ReadLine(); // 입력값

            if (input == "0")
            {
                loop = false;
                Shop(player);
            }
            else
            {
                for (int i = 0; i < player.InvenItem.Count; i++) // 아이템 불러오기
                {
                    if (player.InvenItem[i].ItemName == input) // 아이템 이름과 입력값이 같은지 비교
                    {
                        Item selectedItem = player.InvenItem[i]; // 내가 가진 아이템을 selcetedItem에 저장

                        player.Gold += (int)(player.InvenItem[i].ItemPrice * 0.85f); // 판매 가격은 85%로 설정
                        player.UnE(selectedItem); // 장착 해제
                        Message($"{selectedItem.ItemName} 판매 완료");
                        player.InvenItem.Remove(selectedItem); // 보유 아이템에서 제거              
                        itemSold = true; // 판매 완료
                        break; // 강제 종료
                    }
                }

                if (!itemSold) // 에러를 방지하기 위해 따로 뺌, 판매 안했을 때
                {
                    Message("잘못된 입력입니다.");
                }
            }
        } while (loop); // 반복
    }

    public void Stage(Player player) // 4. 던전
    {
        bool loop = true; // 반복한다
        bool clear = false; // 클리어

        do
        {
            Random rand = new Random();

            int clearProb = rand.Next(0, 101); // 0 ~ 100% 클리어 확률 (Clear Probability)
            int stageDf; // 던전 권장 방어력
            int hpDecre; // 체력 감소
            int reward; // 보상
            int addReward = rand.Next((int)player.Att, ((int)player.Att * 2) + 1); // 공격력  ~ 공격력 * 2 의 % 만큼 추가 보상 획득 
            string stageName; // 스테이지 이름

            Console.Clear();

            Console.WriteLine($"던전입장\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("\n1. 쉬운 던전     | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전     | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전     | 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

            string input = Console.ReadLine(); // 입력값

            switch (input)
            {
                case "0": // 나가기
                    loop = false;
                    break;

                case "1": // 쉬운 던전
                    if (clear == false)
                    {
                        stageDf = 5; // 던전 권장 방어력
                        stageName = "쉬운 던전";
                        reward = 1000; // 1000G

                        if (player.Df >= stageDf) // 내 방어력 >=  던전 권장 방어력
                        {
                            hpDecre = rand.Next(20 - (player.Df - stageDf), 36 - (player.Df - stageDf)); // 20 - (내 방어력 - 권장 방어력) ~ 35 - (내 방어력 - 권장 방어력) 랜덤 체력 감소
                            player.Hp -= hpDecre; // 랜덤으로 체력 감소
                            player.Gold += reward + (int)(reward * ((float)addReward / 100)); // 클리어시 1000G + 추가 보상
                            player.clearCount++; // 클리어 횟수 + 1
                            stageClear(player, stageName, reward, addReward, hpDecre); // 클리어 창으로 이동
                        }
                        else // 내 방어력 <  던전 권장 방어력
                        {
                            if (clearProb >= 41) // 41% 이상일 때 클리어
                            {
                                hpDecre = rand.Next(20 - (player.Df - stageDf), 36 - (player.Df - stageDf)); // 20 - (내 방어력 - 권장 방어력) ~ 35 - (내 방어력 - 권장 방어력) 랜덤 체력 감소
                                player.Hp -= hpDecre; // 랜덤으로 체력 감소
                                player.Gold += reward + (int)(reward * ((float)addReward / 100)); // 클리어시 1000G + 추가 보상
                                player.clearCount++; // 클리어 횟수 + 1
                                stageClear(player, stageName, reward, addReward, hpDecre); // 클리어 창으로 이동
                            }
                            else // 아닐 때
                            {
                                loop = false;
                                hpDecre = rand.Next(20 - (player.Df - stageDf), 36 - (player.Df - stageDf)); // 20 - (내 방어력 - 권장 방어력) ~ 35 - (내 방어력 - 권장 방어력) 랜덤 체력 감소
                                player.Hp -= (int)((float)hpDecre / 2); // 랜덤으로 체력 감소 절반
                                stageFail(player, hpDecre); // 실패 창
                            }
                        }
                    }
                    break;

                case "2": // 일반 던전
                    if (clear == false)
                    {
                        stageDf = 11; // 던전 권장 방어력
                        stageName = "일반 던전";
                        reward = 1700; // 1700G

                        if (player.Df >= stageDf) // 내 방어력 >=  던전 권장 방어력
                        {
                            hpDecre = rand.Next(20 - (player.Df - stageDf), 36 - (player.Df - stageDf)); // 20 - (내 방어력 - 권장 방어력) ~ 35 - (내 방어력 - 권장 방어력) 랜덤 체력 감소
                            player.Hp -= hpDecre; // 랜덤으로 체력 감소
                            player.Gold += reward + (int)(reward * ((float)addReward / 100)); // 클리어시 1700G + 추가 보상
                            player.clearCount++; // 클리어 횟수 + 1
                            stageClear(player, stageName, reward, addReward, hpDecre); // 클리어 창으로 이동
                        }
                        else // 내 방어력 <  던전 권장 방어력
                        {
                            if (clearProb >= 41) // 41% 이상일 때 클리어
                            {
                                hpDecre = rand.Next(20 - (player.Df - stageDf), 36 - (player.Df - stageDf)); // 20 - (내 방어력 - 권장 방어력) ~ 35 - (내 방어력 - 권장 방어력) 랜덤 체력 감소
                                player.Hp -= hpDecre; // 랜덤으로 체력 감소
                                player.Gold += reward + (int)(reward * ((float)addReward / 100)); // 클리어시 1700G + 추가 보상
                                player.clearCount++; // 클리어 횟수 + 1
                                stageClear(player, stageName, reward, addReward, hpDecre); // 클리어 창으로 이동
                            }
                            else // 아닐 때
                            {
                                loop = false;
                                hpDecre = rand.Next(20 - (player.Df - stageDf), 36 - (player.Df - stageDf)); // 20 - (내 방어력 - 권장 방어력) ~ 35 - (내 방어력 - 권장 방어력) 랜덤 체력 감소
                                player.Hp -= (int)((float)hpDecre / 2); // 랜덤으로 체력 감소 절반
                                stageFail(player, hpDecre); // 실패 창
                            }
                        }
                    }
                    break;

                case "3": // 어려운 던전
                    if (clear == false)
                    {
                        stageDf = 17; // 던전 권장 방어력
                        stageName = "어려운 던전";
                        reward = 2500; // 2500G

                        if (player.Df >= stageDf) // 내 방어력 >=  던전 권장 방어력
                        {
                            hpDecre = rand.Next(20 - (player.Df - stageDf), 36 - (player.Df - stageDf)); // 20 - (내 방어력 - 권장 방어력) ~ 35 - (내 방어력 - 권장 방어력) 랜덤 체력 감소
                            player.Hp -= hpDecre; // 랜덤으로 체력 감소
                            player.Gold += reward + (int)(reward * ((float)addReward / 100)); // 클리어시 2500G + 추가 보상
                            player.clearCount++; // 클리어 횟수 + 1
                            stageClear(player, stageName, reward, addReward, hpDecre); // 클리어 창으로 이동                 
                        }
                        else // 내 방어력 <  던전 권장 방어력
                        {
                            if (clearProb >= 41) // 41% 이상일 때 클리어
                            {
                                hpDecre = rand.Next(20 - (player.Df - stageDf), 36 - (player.Df - stageDf)); // 20 - (내 방어력 - 권장 방어력) ~ 35 - (내 방어력 - 권장 방어력) 랜덤 체력 감소
                                player.Hp -= hpDecre; // 랜덤으로 체력 감소
                                player.Gold += reward + (int)(reward * ((float)addReward / 100)); // 클리어시 2500G + 추가 보상
                                player.clearCount++; // 클리어 횟수 + 1
                                stageClear(player, stageName, reward, addReward, hpDecre); // 클리어 창으로 이동
                            }
                            else // 아닐 때
                            {
                                loop = false;
                                hpDecre = rand.Next(20 - (player.Df - stageDf), 36 - (player.Df - stageDf)); // 20 - (내 방어력 - 권장 방어력) ~ 35 - (내 방어력 - 권장 방어력) 랜덤 체력 감소
                                player.Hp -= (int)((float)hpDecre / 2); // 랜덤으로 체력 감소 절반
                                stageFail(player, hpDecre); // 실패 창
                            }
                        }
                    }
                    break;
            }
        } while (loop);
    }

    public void stageClear(Player player, string stageName, int reward, int addReward, int hpDecre) // 4.1 클리어 시 화면
    {
        bool loop = true;

        do
        {
            Console.Clear();

            Console.WriteLine($"던전 클리어\n축하합니다!!\n{stageName}을 클리어 하였습니다.");
            Console.WriteLine("\n[탐험 결과]");
            Console.WriteLine($"체력 {player.Hp + hpDecre} -> {(player.Hp > 0 ? player.Hp : 0)}");
            Console.WriteLine($"Gold {player.Gold - (reward + (int)(reward * ((float)addReward / 100)))} G -> {player.Gold} G");
            Console.WriteLine("\n0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

            string input = Console.ReadLine();

            if (player.Hp > 0) // 현재 체력 > 0
            {
                switch (input)
                {
                    case "0":
                        loop = false;
                        Level(player); // 레벨 업
                        break;

                    default:
                        Message("잘못된 입력입니다.");
                        break;
                }
            }
            else // 현재 체력 = 0
            {
                loop = false;
                End(player); // 죽음의 엔딩으로 이동
            }
        } while (loop);
    }

    public void stageFail(Player player, int hpDecre) // 4.2 던전 클리어 실패
    {
        bool loop = true;

        do
        {
            Console.Clear();

            Console.WriteLine($"던전 클리어 실패\n");
            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {player.Hp + (int)((float)hpDecre / 2)} -> {(player.Hp > 0 ? player.Hp : 0)}");
            Console.WriteLine("\n0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

            string input = Console.ReadLine();

            if (player.Hp > 0) // 현재 체력 > 0
            {
                switch (input)
                {
                    case "0":
                        loop = false;
                        break;

                    default:
                        Message("잘못된 입력입니다.");
                        break;
                }
            }
            else // 현재 체력 = 0
            {
                loop = false;
                End(player); // 죽음의 엔딩으로 이동
            }
        } while (loop);
    }

    public void Rest(Player player) // 5. 휴식
    {
        bool loop = true; // 반복한다

        do
        {
            Console.Clear();

            Console.WriteLine($"휴식하기\n500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드: {player.Gold} G)\n");
            Console.WriteLine("\n1. 휴식하기 \n0. 나가기\n");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

            string input = Console.ReadLine(); // 입력값

            switch (input)
            {
                case "0":
                    loop = false;
                    break;

                case "1":
                    if (player.Gold >= 500 && player.Hp < 100) // 500 G 이상이고 HP가 100 미만일 때
                    {
                        Message("휴식을 완료했습니다. ");
                        player.Gold -= 500; // 500 G 차감
                        player.Hp = 100;
                    }
                    else if (player.Hp == 100)
                    {
                        Message("현재 Hp가 100 입니다.");
                    }
                    else
                    {
                        Message("Gold 가 부족합니다. ");
                    }
                    break;

                default:
                    Message("잘못된 입력입니다.");
                    break;
            }
        } while (loop); // 체력이 100 미만일 때
    }

    static void Level(Player player) // Lv UP
    {
        if (player.clearCount == 1) // 전체 클리어 횟수로 레벨업 설정
        {
            player.Lv = 2;
            player.Att += 0.5f;
            player.Df += 1;
            Message($"\nLV: {(player.Lv) - 1} -> {player.Lv}\n공격력: {player.Att - 0.5f} -> {player.Att}\n방어력: {player.Df - 1} -> {player.Df}"); // 레벨업 문구
        }
        else if (player.clearCount == 3)
        {
            player.Lv = 3;
            player.Att += 0.5f;
            player.Df += 1;
            Message($"\nLV: {(player.Lv) - 1} -> {player.Lv}\n공격력: {player.Att - 0.5f} -> {player.Att}\n방어력: {player.Df - 1} -> {player.Df}"); // 레벨업 문구
        }
        else if (player.clearCount == 6)
        {
            player.Lv = 4;
            player.Att += 0.5f;
            player.Df += 1;
            Message($"\nLV: {(player.Lv) - 1} -> {player.Lv}\n공격력: {player.Att - 0.5f} -> {player.Att}\n방어력: {player.Df - 1} -> {player.Df}"); // 레벨업 문구
        }
        else if (player.clearCount == 10)
        {
            player.Lv = 5;
            player.Att += 0.5f;
            player.Df += 1;
            Message($"\nLV: {(player.Lv) - 1} -> {player.Lv}\n공격력: {player.Att - 0.5f} -> {player.Att}\n방어력: {player.Df - 1} -> {player.Df}"); // 레벨업 문구
        }   
    }

    public void End(Player player) // 죽었을 때 (게임 리셋)
    {
        Console.Clear();

        Message("전사하셨습니다.\n처음부터 다시 시작합니다");

        player.Class = "전사"; // 직업
        player.Lv = 1; // 레벨
        player.Att = 10; // 공격력
        player.Df = 5; // 방어력
        player.Hp = 100; // 체력
        player.Gold = 1500; // 돈
        player.InvenItem = new List<Item>(); // 내 아이템 리스트 생성
        player.clearCount = 0; // 던전 클리어 횟수

        Home(player); // 메인 화면으로
    }

    static void Message(string msg) // 확인용 메세지
    {
        Console.WriteLine($"\n{msg}\n"); // 문구 출력
        Console.WriteLine("아무 키를 누르세요.\n");
        Console.ReadKey(); // 아무 키 입력 받기
    }
}