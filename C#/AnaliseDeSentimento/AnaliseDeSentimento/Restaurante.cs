using System;
using System.Collections.Generic;
using System.Linq;

namespace AnaliseDeSentimento
{
    public class Restaurante
    {
        public string Nome { get; init; }
        public float Estrelas { get; init; }
        public string Avaliacao { get; init; }

        public int Classificacao { get; private set; }
        public Dictionary<string, int> Termos { get; private set; }
        public int TermosPositivos { get; private set; }
        public int TermosNegativos { get; private set; }


        public Restaurante(ValueTuple<string, float, string> tuple)
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

        static ValueTuple<string, float, string> SeparaLinha(string linha)
        {
            var dadosRestaurante = linha.Split(";");

            var nome = dadosRestaurante[0];
            var estrelas = float.Parse(dadosRestaurante[1]);
            var avaliacao = dadosRestaurante[2];

            return new(nome, estrelas, avaliacao);
        }

        public void ProcessarTermosAvaliacao(Termos termos)
        {
            var avaliacaoTemp = AvaliacaoTemporaria();
            termos.Items.ForEach((termoAtual) =>
            {
                while(avaliacaoTemp.Contains(termoAtual))
                {
                    AdicionarTermo(termoAtual, termos.Tipo);
                    avaliacaoTemp.Remove(termoAtual);
                }
            });
        }

        IList<string> AvaliacaoTemporaria()
            => Avaliacao.Split(' ', '!', '.', ',', ':', ';').ToList();

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
                TermosNegativos+= 1;
        }

        string ExibirTermos()
            => Termos
                .Select(termo => $"{termo.Key}: {termo.Value}; ")
                .Aggregate(string.Empty, (acumulador, atual) => $"{acumulador}{atual}");

        public override string ToString()
            => $"{Nome}\t{TermosPositivos}\t{TermosNegativos}\t{ExibirTermos()}";
    }
}
