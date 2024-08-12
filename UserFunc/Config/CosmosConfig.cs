namespace UserFunc.Config;

public class CosmosConfig{
  public const string SectionName = "CosmosSettings";
  public string Connection { get; set; }= null!;
  public string Key { get; set; }= null!;
  public string Database { get; set; }= null!;
  public string Container { get; set; }= null!;
}