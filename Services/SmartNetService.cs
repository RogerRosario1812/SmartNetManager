using SmartNetManager.Models;

namespace SmartNetManager.Services;

public class SmartNetService
{

  private Dictionary<string, string> reservasIp = new();


  private Queue<Dispositivo> filaAutenticacao = new();


  private List<Dispositivo> dispositivosAtivos = new();


  private Stack<string> auditoria = new();


  // =============================
  // MISSÃO 1
  // =============================

  public void ReservarIp(string mac, string ip)
  {
    if (reservasIp.ContainsKey(mac))
    {
      Console.WriteLine("MAC já cadastrado.");
      return;
    }

    reservasIp.Add(mac, ip);

    Console.WriteLine("Reserva criada com sucesso.");
  }

  public void ListarReservas()
  {
    foreach (var item in reservasIp)
    {
      Console.WriteLine($"MAC: {item.Key} | IP: {item.Value}");
    }
  }

  // =============================
  // MISSÃO 2
  // =============================

  public void AdicionarFila(Dispositivo dispositivo)
  {
    filaAutenticacao.Enqueue(dispositivo);

    Console.WriteLine("Dispositivo adicionado à fila.");
  }

  public void ProcessarFila()
  {
    if (filaAutenticacao.Count == 0)
    {
      Console.WriteLine("Fila vazia.");
      return;
    }

    var dispositivo = filaAutenticacao.Dequeue();

    dispositivo.DataConexao = DateTime.Now;

    dispositivosAtivos.Add(dispositivo);

    auditoria.Push($"AUTENTICAÇÃO -> {dispositivo.Nome} - {DateTime.Now}");

    Console.WriteLine("Dispositivo autenticado.");
  }

  // =============================
  // MISSÃO 3
  // =============================

  public void ExibirDashboard()
  {
    Console.ForegroundColor = ConsoleColor.Green;

    Console.WriteLine("==== RESUMO DA REDE ====");

    Console.ResetColor();

    var total = dispositivosAtivos.Sum(x => x.ConsumoBanda);

    Console.WriteLine($"Banda Total: {total:F2} Mbps");

    Console.WriteLine();

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("TOP 3 CONSUMO");
    Console.ResetColor();

    foreach (var item in dispositivosAtivos.OrderByDescending(x => x.ConsumoBanda).Take(3))
    {
      Console.WriteLine($"{item.Nome,-20} {item.ConsumoBanda,10:F2} Mbps");
    }

    Console.WriteLine();

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("FABRICANTES");
    Console.ResetColor();

    foreach (var grupo in dispositivosAtivos.GroupBy(x => x.Fabricante))
    {
      Console.WriteLine($"{grupo.Key,-15} {grupo.Count()}");
    }

    Console.WriteLine();

    var suspeitos = dispositivosAtivos.Where(x => x.ConsumoBanda > 500);

    foreach (var dispositivo in suspeitos)
    {
      Console.ForegroundColor = ConsoleColor.Red;

      Console.WriteLine($"ALERTA -> {dispositivo.Nome} está consumindo {dispositivo.ConsumoBanda:F2} Mbps");

      Console.ResetColor();
    }
  }

  // =============================
  // MISSÃO 4
  // =============================

  public void DesfazerUltimaAcao()
  {
    if (auditoria.Count == 0)
    {
      Console.WriteLine("Nada para desfazer.");
      return;
    }

    Console.WriteLine(
        $"Revertido: {auditoria.Pop()}");
  }

  // =============================
  // MISSÃO 5
  // =============================

  public void ImprimirRelatorio(IEnumerable<Dispositivo> colecao)
  {
    foreach (var item in colecao)
    {
      Console.WriteLine(
          $"{item.Nome} | " +
          $"{item.Fabricante} | " +
          $"{item.EnderecoIP} | " +
          $"{item.ConsumoBanda} Mbps");
    }
  }

  public void RelatorioFila()
  {
    ImprimirRelatorio(filaAutenticacao);
  }

  public void RelatorioAtivos()
  {
    ImprimirRelatorio(dispositivosAtivos);
  }

  // =============================
  // MISSÃO 6
  // =============================

  public void AplicarFirewall(
      Func<Dispositivo, bool> regraBloqueio)
  {
    var bloqueados = dispositivosAtivos.Where(regraBloqueio).ToList();

    foreach (var dispositivo in bloqueados)
    {
      dispositivosAtivos.Remove(dispositivo);

      auditoria.Push($"FIREWALL -> {dispositivo.Nome}");
    }

    Console.WriteLine($"{bloqueados.Count} dispositivo(s) bloqueado(s).");
  }
}