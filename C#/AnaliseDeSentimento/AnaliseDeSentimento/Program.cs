using System;

namespace AnaliseDeSentimento
{
    class Program
    {
        static void Main(string[] args)
        {
            int indiceTeste = 1;
            var a = $"C:/Users/Temístocles/Documents/Portifolio/C#/analise-de-sentimento-csharp/teste{indiceTeste}/input";
            var reviews = "reviews.txt";
            var nomeArquivoTermosPositivos = "termos_positivos.txt";
            var nomeArquivoTermosNegativos = "termos_negativos.txt";

            var arquivoTermosPositivos = new LerArquivo($"{a}/{nomeArquivoTermosPositivos}");
            var arquivoTermosNegativos = new LerArquivo($"{a}/{nomeArquivoTermosNegativos}");

            var termosPositivos = new Termos(arquivoTermosPositivos, EnumTermos.Positivo);
            var termosNegativos = new Termos(arquivoTermosNegativos, EnumTermos.Negativo);
            var restaurantes = Restaurante.ListaDeRestaurantes(new LerArquivo($"{a}/{reviews}"));


            for(int i = 0; i < restaurantes.Count; i++)
            {
                restaurantes[i].ProcessarTermosAvaliacao(termosPositivos);
                restaurantes[i].ProcessarTermosAvaliacao(termosNegativos);
                Console.WriteLine(restaurantes[i]);
            }
        }
    }
}
