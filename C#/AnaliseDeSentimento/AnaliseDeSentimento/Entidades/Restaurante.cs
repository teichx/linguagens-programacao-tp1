using AnaliseDeSentimento.Arquivo;
using AnaliseDeSentimento.Constantes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnaliseDeSentimento.Entidades
{
    public class Restaurante
    {
        public string Nome { get; init; }
        public decimal Estrelas { get; init; }
        public string Avaliacao { get; init; }

        public EnumClassificacao Classificacao { get; private set; }
        public Dictionary<string, int> Termos { get; private set; }
        public int TermosPositivos { get; private set; }
        public int TermosNegativos { get; private set; }


        public Restaurante(ValueTuple<string, decimal, string> tuple)
        {
            var (nome, estrelas, avaliacao) = tuple;

            Nome = nome;
            Estrelas = estrelas;
            Avaliacao = avaliacao;
            Termos = new();
        }

        public Restaurante()
        {
            Termos = new();
        }

        public static List<Restaurante> ListaDeRestaurantes(LerArquivo arquivo)
            => arquivo.Linhas.Select(linha => new Restaurante(SeparaLinha(linha))).ToList();

        static ValueTuple<string, decimal, string> SeparaLinha(string linha)
        {
            var dadosRestaurante = linha.Split(";");

            var nome = dadosRestaurante[0];
            var estrelas = decimal.Parse(dadosRestaurante[1].Replace('.', ','));
            var avaliacao = dadosRestaurante[2];

            return new(nome, estrelas, avaliacao);
        }

        public void ProcessarTermosAvaliacao(Termos termos)
        {
            var avaliacaoTemp = AvaliacaoTemporaria();
            termos.Items.ForEach((termoAtual) =>
            {
                while (avaliacaoTemp.Contains(termoAtual))
                {
                    AdicionarTermo(termoAtual, termos.Tipo);
                    avaliacaoTemp.Remove(termoAtual);
                }
            });

            Classificacao = ClassificarAvaliacao();
        }

        EnumClassificacao ClassificarAvaliacao()
            => (TermosPositivos - TermosNegativos) switch
            {
                > 0 => EnumClassificacao.Positiva,
                0 => EnumClassificacao.Neutra,
                < 0 => EnumClassificacao.Negativa
            };

        IList<string> AvaliacaoTemporaria()
            => Avaliacao
                .ToLower()
                .Split(' ', '!', '.', ',', ':', ';')
                .ToList();

        void AdicionarTermo(string termo, EnumTermos tipo)
        {
            var termoExiste = Termos.TryGetValue(termo, out int quantidadeEncontrada);
            if (termoExiste)
                Termos.Remove(termo);

            Termos.Add(termo, quantidadeEncontrada += 1);

            AdicionarSomatorioTermo(tipo);
        }

        void AdicionarSomatorioTermo(EnumTermos tipo)
        {
            if (EnumTermos.Positivo == tipo)
                TermosPositivos += 1;

            if (EnumTermos.Negativo == tipo)
                TermosNegativos += 1;
        }

        string ExibirTermos()
            => Termos
                .Select(termo => $"{termo.Key}: {termo.Value}; ")
                .Aggregate(string.Empty, (acumulador, atual) => $"{acumulador}{atual}");

        public override string ToString()
            => $"{Nome}\t{TermosPositivos}\t{TermosNegativos}\t{ExibirTermos()}";
    }
}
