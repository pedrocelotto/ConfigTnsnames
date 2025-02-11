using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

Console.WriteLine("==== Adicionar Configurações ao Arquivo tnsnames ====");

// Caminho do arquivo .ora
Console.Write("Digite o caminho do arquivo .ora (ou pressione Enter para usar o padrão): ");
string caminhoArquivo = Console.ReadLine() ?? "";
if (string.IsNullOrWhiteSpace(caminhoArquivo))
{
    caminhoArquivo = "C:\\oracle\\product\\11.2.0\\client_1\\network\\admin\\tnsnames.ora";
}

// Verifica se o arquivo existe
if (!File.Exists(caminhoArquivo))
{
    Console.WriteLine("Erro: O arquivo não foi encontrado!");
    return;
}

// Caminho do JSON (na mesma pasta do executável)
string caminhoJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration.json");

if (!File.Exists(caminhoJson))
{
    Console.WriteLine($"Erro: O arquivo JSON '{caminhoJson}' não foi encontrado!");
    return;
}

// Lê e desserializa o JSON
string jsonContent = File.ReadAllText(caminhoJson);
var dados = JsonSerializer.Deserialize<Configuracao>(jsonContent);

if (dados == null || dados.Bancos == null || dados.Bancos.Count == 0)
{
    Console.WriteLine("Erro: Nenhum banco foi encontrado no JSON!");
    return;
}

// Lê os bancos existentes no tnsnames.ora
HashSet<string> bancosExistentes = new HashSet<string>();
foreach (var linha in File.ReadLines(caminhoArquivo))
{
    if (linha.Trim().EndsWith("=")) // Verifica se é um nome de banco
        bancosExistentes.Add(linha.Split('=')[0].Trim());
}

bool adicionouAlgo = false;

using (StreamWriter sw = new StreamWriter(caminhoArquivo, true)) // true = anexar ao arquivo
{
    foreach (string bloco in dados.Bancos)
    {
        string nomeBanco = bloco.Split('=')[0].Trim();
        if (!bancosExistentes.Contains(nomeBanco))
        {
            sw.WriteLine();
            sw.WriteLine(bloco);
            adicionouAlgo = true;
            Console.WriteLine($"Banco '{nomeBanco}' adicionado:\n{bloco}\n");
        }
        else
        {
            Console.WriteLine($"Banco '{nomeBanco}' já existente:\n");
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

class Configuracao
{
    public List<string> Bancos { get; set; } = new List<string>();
}