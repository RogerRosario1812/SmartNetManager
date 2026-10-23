namespace SmartNetManager.Utils;

public static class ConsoleHelper
{
  public static void Titulo(string titulo)
  {
    Console.Clear();

    Console.ForegroundColor = ConsoleColor.Cyan;

    Console.WriteLine("=========================================");
    Console.WriteLine("         SMARTNET MANAGER");
    Console.WriteLine("=========================================");

    Console.ResetColor();

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"► {titulo}");
    Console.ResetColor();

    Console.WriteLine();
  }

  public static void Pausar()
  {
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ResetColor();

    Console.ReadKey(true);
  }
}