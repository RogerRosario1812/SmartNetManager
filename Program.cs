using SmartNetManager.Models;
using SmartNetManager.Services;
using SmartNetManager.Utils;

SmartNetService service = new();

Console.Clear();

while (true)
{
  Console.Clear();

  Console.ForegroundColor = ConsoleColor.Cyan;

  Console.WriteLine("=========================================");
  Console.WriteLine("         SMARTNET MANAGER");
  Console.WriteLine("=========================================");
  Console.ResetColor();

  Console.WriteLine();

  Console.WriteLine("1  - Reservar IP");
  Console.WriteLine("2  - Adicionar dispositivo à fila");
  Console.WriteLine("3  - Processar fila");
  Console.WriteLine("4  - Dashboard");
  Console.WriteLine("5  - Desfazer última ação");
  Console.WriteLine("6  - Relatório da fila");
  Console.WriteLine("7  - Relatório ativos");
  Console.WriteLine("8  - Firewall Apple");
  Console.WriteLine("9  - Firewall > 100Mbps");
  Console.WriteLine("10 - Listar reservas");
  Console.WriteLine("0  - Sair");

  Console.WriteLine();
  Console.Write("Escolha uma opção: ");

  string opcao = Console.ReadLine() ?? "";

  switch (opcao)
  {
    case "1":
      ReservarIp(service);
      break;

    case "2":
      AdicionarFila(service);
      break;

    case "3":
      ProcessarFila(service);
      break;

    case "4":
      Dashboard(service);
      break;

    case "5":
      Desfazer(service);
      break;

    case "6":
      RelatorioFila(service);
      break;

    case "7":
      RelatorioAtivos(service);
      break;

    case "8":
      FirewallApple(service);
      break;

    case "9":
      FirewallConsumo(service);
      break;

    case "10":
      Reservas(service);
      break;

    case "0":
      return;

    default:
      Console.WriteLine("Opção inválida.");
      ConsoleHelper.Pausar();
      break;
  }
}
void ReservarIp(SmartNetService service)
{
  ConsoleHelper.Titulo("RESERVA DE IP");

  Console.Write("MAC: ");
  string mac = Console.ReadLine()!;

  Console.Write("IP: ");
  string ip = Console.ReadLine()!;

  service.ReservarIp(mac, ip);

  ConsoleHelper.Pausar();
}
void AdicionarFila(SmartNetService service)
{
  ConsoleHelper.Titulo("ADICIONAR À FILA");

  Dispositivo dispositivo = new();

  Console.Write("Nome: ");
  dispositivo.Nome = Console.ReadLine()!;

  Console.Write("MAC: ");
  dispositivo.MacAddress = Console.ReadLine()!;

  Console.Write("IP: ");
  dispositivo.EnderecoIP = Console.ReadLine()!;

  Console.Write("Fabricante: ");
  dispositivo.Fabricante = Console.ReadLine()!;

  Console.Write("Consumo Mbps: ");
  dispositivo.ConsumoBanda =
      Convert.ToDouble(Console.ReadLine());

  service.AdicionarFila(dispositivo);

  ConsoleHelper.Pausar();
}
void ProcessarFila(SmartNetService service)
{
  ConsoleHelper.Titulo("PROCESSAR FILA");

  service.ProcessarFila();

  ConsoleHelper.Pausar();
}
void Dashboard(SmartNetService service)
{
  ConsoleHelper.Titulo("DASHBOARD");

  service.ExibirDashboard();

  ConsoleHelper.Pausar();
}
void Desfazer(SmartNetService service)
{
  ConsoleHelper.Titulo("DESFAZER AÇÃO");

  service.DesfazerUltimaAcao();

  ConsoleHelper.Pausar();
}
void RelatorioFila(SmartNetService service)
{
  ConsoleHelper.Titulo("RELATÓRIO DA FILA");

  service.RelatorioFila();

  ConsoleHelper.Pausar();
}
void RelatorioAtivos(SmartNetService service)
{
  ConsoleHelper.Titulo("DISPOSITIVOS ATIVOS");

  service.RelatorioAtivos();

  ConsoleHelper.Pausar();
}
void FirewallApple(SmartNetService service)
{
  ConsoleHelper.Titulo("FIREWALL APPLE");

  service.AplicarFirewall(
      d => d.Fabricante.Equals(
          "Apple",
          StringComparison.OrdinalIgnoreCase));

  ConsoleHelper.Pausar();
}
void FirewallConsumo(SmartNetService service)
{
  ConsoleHelper.Titulo("FIREWALL >100 MBPS");

  service.AplicarFirewall(
      d => d.ConsumoBanda > 100);

  ConsoleHelper.Pausar();
}
void Reservas(SmartNetService service)
{
  ConsoleHelper.Titulo("RESERVAS DHCP");

  service.ListarReservas();

  ConsoleHelper.Pausar();
}