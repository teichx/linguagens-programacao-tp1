using AnaliseDeSentimento.Arquivo;
using AnaliseDeSentimento.Entidades;
using AnaliseDeSentimento.Constantes;
using System.Collections.Generic;

namespace AnaliseDeSentimento
{
    class Program
    {
        const int QuantidadeTestes = 5;

        static void Main(string[] args)
        {
            for (int indiceTeste = 1; indiceTeste < QuantidadeTestes; indiceTeste++)
                ProcessarAvaliacao(indiceTeste);
        }

        static void ProcessarAvaliacao(int indiceTeste)
        {
            var enderecoBase = $"C:/Linguagens-Programacao-TP1/teste{indiceTeste}";
            var enderecoInput = $"{enderecoBase}/input/";
            var enderecoOutput = $"{enderecoBase}/output/";


            var arquivoTermosPositivos = new LerArquivo(enderecoInput + NomeArquivos.TermosPositivos);
            var arquivoTermosNegativos = new LerArquivo(enderecoInput + NomeArquivos.TermosNegativos);
            var arquivoAvaliacoes = new LerArquivo(enderecoInput + NomeArquivos.Reviews);


            var termosPositivos = new Termos(arquivoTermosPositivos, EnumTermos.Positivo);
            var termosNegativos = new Termos(arquivoTermosNegativos, EnumTermos.Negativo);
            var listaRestaurantes = Restaurante.ListaDeRestaurantes(arquivoAvaliacoes);

            listaRestaurantes.ForEach(restaurante =>
            {
                restaurante.ProcessarTermosAvaliacao(termosPositivos);
                restaurante.ProcessarTermosAvaliacao(termosNegativos);
            });

            GerarArquivosSaida(listaRestaurantes);
        }

        static void GerarArquivosSaida(List<Restaurante> listaRestaurantes)
        {
            var arquivoOutputPositivos = new GerarArquivo(EnumClassificacao.Positiva);
            var arquivoOutputNegativo = new GerarArquivo(EnumClassificacao.Negativa);
            var arquivoOutputNeutros = new GerarArquivo(EnumClassificacao.Neutra);
            var arquivoOutputClassificacao = new GerarArquivo();


            arquivoOutputPositivos.EscreverRestaurantes(listaRestaurantes);
            arquivoOutputNegativo.EscreverRestaurantes(listaRestaurantes);
            arquivoOutputNeutros.EscreverRestaurantes(listaRestaurantes);

            arquivoOutputClassificacao.EscreverSaida(new Classificacao(listaRestaurantes));
        }
    }
}
