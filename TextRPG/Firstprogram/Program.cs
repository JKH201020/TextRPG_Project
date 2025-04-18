using System.Security.Cryptography.X509Certificates;

namespace Firstprogram
{
    internal class Program
    {
        static void Main(string[] args) // main문
        {
            GameManager h = new GameManager();
            Player player = new Player();

            Console.WriteLine("당신의 이름은 무엇인가요?");
            player.Name = Console.ReadLine(); // 이름 입력

            h.Home(player); // Home으로 이동
        }
    }
}