using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Firstprogram
{
    internal class Program
    {
        static void Main(string[] args) // 첫 시작 화면
        {
            Console.Clear();

            // 인스턴스 이름(h, player)을 사용하여 객체의 멤버(필드, 메서드등) 접근할 수 있다.
            GameManager h = new GameManager(); // GameManager 클래스의 인스턴스 생성 / 인스턴스 참조 h 라는 변수
            Player player = new Player(); // Player 클래스의 인스턴스 생성

            Console.WriteLine("당신의 이름은 무엇인가요?");
            player.Name = Console.ReadLine(); // 이름 입력

            h.Home(player); // Home으로 이동
        }
    }
}