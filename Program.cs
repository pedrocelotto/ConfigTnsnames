Console.WriteLine("==== Adicionar Configurações ao Arquivo tnsnames ====");

// Caminho do arquivo .ora
Console.Write("Digite o caminho do arquivo .ora (ou pressione Enter para usar 'tnsnames.ora'): ");
string caminhoArquivo = Console.ReadLine();
if (string.IsNullOrWhiteSpace(caminhoArquivo))
{
    caminhoArquivo = "C:\\oracle\\product\\11.2.0\\client_1\\network\\admin\\tnsnames.ora"; // Nome padrão do arquivo
}

// Verifica se o arquivo existe
if (!File.Exists(caminhoArquivo))
{
    Console.WriteLine("Erro: O arquivo não foi encontrado!");
    return;
}

// Lista dos blocos de configuração que queremos adicionar
List<string> blocos = new List<string>
{
    @"TESTEBANCO =
  (DESCRIPTION =
    (ADDRESS = (PROTOCOL = TCP)(HOST = 123.456.789.012)(PORT = 1521))
    (CONNECT_DATA =
      (SERVER = DEDICATED)
      (SERVICE_NAME = TESTE)
    )
  )"
};

// Lê todo o conteúdo do arquivo .ora
string conteudoArquivo = File.ReadAllText(caminhoArquivo);

bool adicionouAlgo = false;

using (StreamWriter sw = new StreamWriter(caminhoArquivo, true)) // true = anexar
{
    foreach (string bloco in blocos)
    {
        if (!conteudoArquivo.Contains(bloco))
        {
            sw.WriteLine();
            sw.WriteLine(bloco);
            adicionouAlgo = true;
            Console.WriteLine($"Bloco adicionado:\n{bloco}\n");
        }
        else
        {
            Console.WriteLine($"Bloco já existente:\n{bloco}\n");
        }
    }
}

if (adicionouAlgo)
    Console.WriteLine("Novos blocos foram adicionados ao arquivo!");
else
    Console.WriteLine("Nenhuma alteração foi feita, todos os blocos já existiam.");

// Adicionando uma pausa para evitar que a janela feche rapidamente
Console.WriteLine("Pressione Enter para sair...");
Console.ReadLine();
