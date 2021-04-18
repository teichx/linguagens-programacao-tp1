using AnaliseDeSentimento.Entidades;
using AnaliseDeSentimento.Constantes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnaliseDeSentimento.Arquivo
{
    public class GerarArquivo
    {
        public string NomeArquivo { get; init; }
        public string Endereco { get; init; }
        public EnumClassificacao? TipoAvaliacao { get; init; }

        public GerarArquivo(EnumClassificacao tipoAvaliacao)
        {
            TipoAvaliacao = tipoAvaliacao;
            NomeArquivo = SelecionarNomeArquivo();
            Endereco = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NomeArquivo);
        }

        public GerarArquivo()
        {
            NomeArquivo = SelecionarNomeArquivo();
            Endereco = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NomeArquivo);
        }

        string SelecionarNomeArquivo()
            => TipoAvaliacao switch
            {
                EnumClassificacao.Positiva => NomeArquivos.ClassificadosPositivos,
                EnumClassificacao.Neutra => NomeArquivos.ClassificadosNeutros,
                EnumClassificacao.Negativa => NomeArquivos.ClassificadosNegativos,
                _ => NomeArquivos.Output
            };

        public void EscreverRestaurantes(List<Restaurante> listaRestaurantes)
        {
            if (TipoAvaliacao is null)
                return;

            RemoverArquivoSeExistir();
            using var file = File.CreateText(Endereco);

            listaRestaurantes
                .Where(restaurante => restaurante.Classificacao == TipoAvaliacao)
                .ToList()
                .ForEach(restaurante =>
                {
                    file.WriteLine(restaurante);
                });
        }

        public void EscreverSaida(Classificacao classificacao)
        {
            RemoverArquivoSeExistir();
            using var file = File.CreateText(Endereco);

            file.WriteLine($"Restaurantes classificados corretamente: {classificacao.ClassificadosCorretamente.Count}");
            foreach (var restaurante in classificacao.ClassificadosCorretamente)
                file.WriteLine(restaurante.Nome);

            file.WriteLine();
            file.WriteLine($"Restaurantes classificados incorretamente: {classificacao.ClassificadosIncorretamente.Count}");
            foreach (var restaurante in classificacao.ClassificadosIncorretamente)
                file.WriteLine(restaurante.Nome);

            file.WriteLine();
            file.WriteLine($"Acurácia do modelo: {classificacao.Acuracia}");
        }

        void RemoverArquivoSeExistir()
        {
            if (File.Exists(Endereco))
                File.Delete(Endereco);
        }
    }
}
