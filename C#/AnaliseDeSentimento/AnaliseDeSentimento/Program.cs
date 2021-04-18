using AnaliseDeSentimento.Arquivo;
using AnaliseDeSentimento.Entidades;
using AnaliseDeSentimento.Constantes;
using System.Collections.Generic;
using System;

namespace AnaliseDeSentimento
{
    class Program
    {
        const int QuantidadeTestes = 5;
        const string enderecoBase = "C:/Linguagens-Programacao-TP1/teste";

        static void Main(string[] args)
        {
            for (int indiceTeste = 1; indiceTeste < QuantidadeTestes; indiceTeste++)
            {
                ProcessarAvaliacao(indiceTeste);
                ValidarAvaliacao(indiceTeste);
            }
        }

        static void ProcessarAvaliacao(int indiceTeste)
        {
            var enderecoInput = $"{enderecoBase}{indiceTeste}/input/";

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

        static void ValidarAvaliacao(int indiceTeste)
        {
            ValidarArquivoPorNome(indiceTeste, NomeArquivos.ClassificadosNegativos);
            ValidarArquivoPorNome(indiceTeste, NomeArquivos.ClassificadosNeutros);
            ValidarArquivoPorNome(indiceTeste, NomeArquivos.ClassificadosPositivos);
            ValidarArquivoPorNome(indiceTeste, NomeArquivos.Output);
        }

        static void ValidarArquivoPorNome(int indiceTeste, string nomeArquivo)
        {
            var enderecoOutput = $"{enderecoBase}{indiceTeste}/output/";
            var comparadorArquivos = new CompararArquivos
            {
                EnderecoArquivo1 = $"{enderecoOutput}{nomeArquivo}",
                EnderecoArquivo2 = $"{AppDomain.CurrentDomain.BaseDirectory}{nomeArquivo}"
            };

            var iguais = comparadorArquivos.ArquivosIguais();
            string completeIguais = iguais ? "igual" : "diferente";
            Console.WriteLine($"Arquivo de teste {indiceTeste} com resultado {completeIguais} ao esperado");
        }
    }
}
