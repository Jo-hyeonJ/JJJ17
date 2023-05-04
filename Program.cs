using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace JJJ17
{
    class Item
    {
        string id;
        string name;
        int level;
        int price;

        public Item(string id, string name, int level, int price)
        {
            this.id = id;
            this.name = name;
            this.level = level;
            this.price = price;
        }

        // 프로퍼티를 식형식으로 선언
        public int Price => price; // 프로퍼티로 get을 설정해주는 것과 같은 결과가 나온다.
        public string ID => id;

        public override string ToString() => name;

        public int CompareTo(Item other)
        {
            // 실제로는 아이템의 가치 혹은 어떠한 비교값을 계산
            // 두 string 값을 비교하는 식
            return id.CompareTo(other.id);
        }

    }

    class Inventory
    {
        Item[] item;

        public Item this[int index]
        {
            get => item[index];
        }

    }
    class Inven
    {
        public void AddItem(string str)
        {
            Console.WriteLine("아이템 추가 " + str);
        }
    }

    static class Popup // 객체 생성을 안해도 되는 형태로 스태틱으로 만든다.
    {
        public static void ShowPopupBuy(string item, Inven inven)
        {
            Console.WriteLine(item + "을 구매하시겠습니까");

            while (true)
            {
                Console.WriteLine("1. 예 / 2. 아니오");
                // 사용자의 입력 키의 정보를 반환하는 함수 ↓ => 매개변수 bool은 T:입력값을 출력하겠다. F:출력하겠다.
                ConsoleKeyInfo keyinfo = Console.ReadKey(true);

                if (keyinfo.Key == ConsoleKey.D1)
                {
                    inven.AddItem(item);
                    break;

                }
                else if (keyinfo.Key == ConsoleKey.D2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력");
                }
            }

        }

        public static bool ShowExit()
        {
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("게임을 종료하시겠습니까");
                    Console.WriteLine("예/ 아니오");
                }
            }
            return true;
        }
        // #이벤트 (검색)
        // Show는 팝업의 지시문인 context와
        // 반환값이 없고 bool값을 매개변수로 받는 함수를 받는다.
        public static void Show(string context, Action<bool> onConfirm)
        {
            // context 로 지시문 전달, confirm으로 수락 여부 체크
            Console.Clear();
            Console.WriteLine(context);
            Console.WriteLine("1. yes / 2. no");
            while (true)
            {
                ConsoleKeyInfo keyinfo = Console.ReadKey(true);

                if (keyinfo.Key == ConsoleKey.D1)
                {
                    // null이 아니면 호출하겠다는 뜻. ?로 null인지 여부를 체크하고 .Invoke로 null이 아니면 실행한다. (구문을 완성하기 위한 키워드)
                    onConfirm?.Invoke(true);
                    break;

                }
                else if (keyinfo.Key == ConsoleKey.D2)
                {
                    onConfirm?.Invoke(true);
                    break;
                }
            }

        }

        public static void Show(string context, Action<int> onSelect, params string[] options)
        {
            Console.WriteLine(context);
            Console.WriteLine("---------------");

            for(int i = 0; i < options.Length; i++)
                Console.WriteLine($"{i + 1}. {options[i]}");

            Console.WriteLine("---------------");


            while(true)
            {
            ConsoleKey input = Console.ReadKey(true).Key;
               for (int i = 0; i < options.Length; i++)
                {
                    if(input== ConsoleKey.D1 +i)
                    {
                        onSelect?.Invoke(i+1);
                        return;
                    }
                
                }
            }

        }

    }


    internal class Program
    {
        // 문 형식 : {}를 통해 내용을 작성하는 방법

        static void Say()
        {
            Console.WriteLine("ㅎㅇ");
        }
        // 식 형식 : =>를 통해 값을 반환하거나 짧은 내용을 작성하는 방법
        static void Say2() => Console.WriteLine("ㅎㅇ2");

        static int Sum(int x, int y) => x + y; // => 다음으로 오는 식인 x+y가 자동으로 반환된다.

        static void Main(string[] args)
        {
            // 람다식
            #region
            // => 함수를 선언하는 또 다른 방법 / 식형과 문형이 있다.
            Func<int, int, string> SumToString;
            SumToString = (int a, int b) =>
            {
                return (a + b).ToString();
            };
            // 함수명도 반환형도 없다. 이름이 없는 임시 함수

            // 식형식으로 더 간단하게 선언 가능하다.
            SumToString = (int a, int b) => (a + b).ToString();

            // 매개 변수 자료형을 입력하지 않아도 형식 유추를 통해 매개변수가 자동으로 선언된다.
            SumToString = (a, b) => (a + b).ToString();

            string str = SumToString(10, 20);
            Console.WriteLine(str);


            Action<double> onFunc1 = (a) => Console.WriteLine(a * 2);
            onFunc1(3.1415);
            #endregion

            #region
            List<Item> items = new List<Item>();
            items.Add(new Item("AE002", "슈퍼볼", 0, 1500));
            items.Add(new Item("AE004", "상처약", 0, 300));
            items.Add(new Item("AE005", "좋은 상처약", 0, 1000));
            items.Add(new Item("AE001", "몬스터볼", 0, 1000));
            items.Add(new Item("AE003", "하이퍼볼", 0, 2000));

            Console.WriteLine($"정렬 전 : {string.Join(",", items)}");

            // Comparison<T> 델리게이트 형식
            // Array.Sort<T>(T[] array, Comparison<T> comparison);
            // public delegate int Comparison<in T>(T x, T y);  / Comparison이 수용한 델리게이트

            // 식형식으로 구현한 Item자료형 정렬방법
            items.Sort((x, y) => x.ID.CompareTo(y.ID));

            Console.WriteLine($"정렬 후 : {string.Join(",", items)}");
            #endregion

            bool isRun = true;
            Inven inven = new Inven();
            while (isRun)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey input = Console.ReadKey(true).Key;
                    switch (input)
                    {
                        // 람다식을 활용한 방법
                        case ConsoleKey.P:
                            Popup.Show("몬스터볼을 구매하시겠습니까", (isConfirm) =>
                            {
                                if (isConfirm)
                                {
                                    inven.AddItem("몬스터볼");
                                }

                            });
                            break;
                        // 클래스 내부의 멤버함수를 사용한 방법
                        case ConsoleKey.Escape:
                            // 종료 키를 누르고 팝업 나오고 승인을 한다면 Run값을 false로 만들어 게임을 종료한다.
                            if (Popup.ShowExit())
                            { isRun = false; }
                            break;

                        case ConsoleKey.T:
                            Popup.Show("교환신청을 하시겠습니까", (isConfirm) =>
                            {
                                if (isConfirm)
                                { Console.WriteLine("교환신청을 합니다."); }
                            });
                            break;
                        case ConsoleKey.J:
                            string[] server = new string[] { "프로키온", "에버그레이스", "베아트리스", "안타레스" };
                            Popup.Show("서버 선택",
                                (select) =>
                                Console.WriteLine($"선택한 서버 : {server[select-1]}"),
                                server);
                            break;
                    }
                }
            }
        }
    }
}