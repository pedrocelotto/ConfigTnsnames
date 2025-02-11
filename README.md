# Adicionar Configurações ao tnsnames.ora

Esta aplicação em **C#** permite adicionar automaticamente configurações de bancos de dados ao arquivo **tnsnames.ora**, utilizando um arquivo **JSON** como fonte de configuração.

## Funcionalidades

- Lê configurações de bancos de dados a partir de um arquivo **configuration.json**.
- Verifica se as configurações já existem no **tnsnames.ora**.
- Adiciona apenas novas configurações ao arquivo **tnsnames.ora**.

## Como Usar

1. **Baixe ou clone o repositório**:

   ```sh
   git clone https://github.com/pedrocelotto/ConfigTnsnames.git
   cd ConfigTnsnames
   ```

2. **Prepare o arquivo de configuração**:

   - Crie um arquivo `configuration.json` na raiz do projeto.
   - O formato esperado é:

   ```json
   {
      "Bancos": [
         "TESTEBANCO =\n  (DESCRIPTION =\n    (ADDRESS = (PROTOCOL = TCP)(HOST = 123.456.789.012)(PORT = 1521))\n    (CONNECT_DATA =\n      (SERVER = DEDICATED)\n      (SERVICE_NAME = TESTE)\n    )\n  )"
      ]
   }
   ```

3. **Execute a aplicação**
   **Obs.: A aplicação só ser executada quando executada como administrador.**:

   ```sh
   dotnet run
   ```

   Ou, se já possuir o `.exe` gerado:

   ```sh
   ./NomeDoExecutavel.exe
   ```

5. \*\*Informe o caminho do **`tnsnames.ora`** ou pressione **Enter** para usar o padrão:

   ```
   Digite o caminho do arquivo .ora (ou pressione Enter para usar o padrão):
   ```

6. **O programa verifica e adiciona os blocos ao **`JSON`** caso não existam.**

## Como Gerar o Executável (.exe)

Para distribuir a aplicação como um arquivo **.exe**, use:

```sh
 dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None -p:DebugSymbols=false
```

Isso gerará um **.exe** na pasta `bin/Release/net8.0/win-x64/publish/`.

## Tecnologias Utilizadas

- **C#** (.NET 8)
- **JSON** para configurações
- **Manipulação de arquivos**
