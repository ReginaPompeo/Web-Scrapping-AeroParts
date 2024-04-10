using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebScrappingTest
{
    public class AircraftPart
    {
        public string Name { get; set; } = ""; // Inicializando com um valor padrão não nulo
        public string Price { get; set; } = "";
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            // URL da página a ser feita o web scraping
            string url = "https://www.barataaviation.com.br/produto/24540-150-eaton-cessna-coupling-assy-2/";

            // Criação de um cliente HTTP
            var httpClient = new HttpClient();

            // Faz a requisição GET para a URL
            var html = await httpClient.GetStringAsync(url);

            // Criação de um documento HTML a partir do conteúdo da página
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var nomeDaPecaNode = htmlDocument.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("product_title"));
            string nomeDaPeca = nomeDaPecaNode?.InnerText.Trim() ?? "Nome da peça não encontrado";

            // Extrai o preço da peça
            var precoDaPecaNode = htmlDocument.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("price"));
            string precoDaPeca = precoDaPecaNode?.InnerText.Trim() ?? "Preço da peça não encontrado";


            // Criar um objeto AircraftPart para armazenar os dados
            var aircraftPart = new AircraftPart
            {
                Name = nomeDaPeca,
                Price = precoDaPeca.Split(";").Last()
           };

            // Exibe o nome e o preço da peça
            Console.WriteLine("Nome da Peça do Avião: " + aircraftPart.Name);
            Console.WriteLine("Preço da Peça do Avião: " + aircraftPart.Price);
        }
    }
}
