namespace SmartNetManager.Models;

public class Dispositivo
{
  public string MacAddress { get; set; } = string.Empty;
  public string EnderecoIP { get; set; } = string.Empty;
  public string Nome { get; set; } = string.Empty;
  public string Fabricante { get; set; } = string.Empty;
  public double ConsumoBanda { get; set; }
  public DateTime DataConexao { get; set; }
}