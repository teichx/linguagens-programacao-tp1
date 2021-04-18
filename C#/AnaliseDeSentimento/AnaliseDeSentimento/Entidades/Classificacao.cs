using AnaliseDeSentimento.Constantes;
using System.Collections.Generic;

namespace AnaliseDeSentimento.Entidades
{
    public class Classificacao
    {
        public IReadOnlyList<Restaurante> ClassificadosCorretamente { get; private set; }
        public IReadOnlyList<Restaurante> ClassificadosIncorretamente { get; private set; }
        public decimal Acuracia { get; private set; }

        public Classificacao(List<Restaurante> listaRestaurantes)
        {
            MedirAcuraciaClassificacao(listaRestaurantes);
        }

        void MedirAcuraciaClassificacao(List<Restaurante> listaRestaurantes)
        {
            List<Restaurante> classificadosCorretamente = new();
            List<Restaurante> classificadosIncorretamente = new();

            listaRestaurantes.ForEach(restaurante =>
            {
                (ClassificadoCorretamente(restaurante) 
                    ? classificadosCorretamente
                    : classificadosIncorretamente
                ).Add(restaurante);
            });

            ClassificadosCorretamente = classificadosCorretamente;
            ClassificadosIncorretamente = classificadosIncorretamente;

            Acuracia = (decimal)ClassificadosCorretamente.Count / (ClassificadosCorretamente.Count + ClassificadosIncorretamente.Count);
        }

        bool ClassificadoCorretamente(Restaurante restaurante)
            => (restaurante.Classificacao) switch
            {
                EnumClassificacao.Positiva => ClassificacaoPositiva(restaurante.Estrelas),
                EnumClassificacao.Neutra => ClassificacaoNeutra(restaurante.Estrelas),
                EnumClassificacao.Negativa => ClassificacaoNegativa(restaurante.Estrelas),
                _ => false
            };

        bool ClassificacaoNegativa(decimal estrelas)
            => estrelas >= (decimal)1 && estrelas < (decimal)2.5;

        bool ClassificacaoNeutra(decimal estrelas)
            => estrelas >= (decimal)2.5 && estrelas < (decimal)3.5;

        bool ClassificacaoPositiva(decimal estrelas)
            => estrelas >= (decimal)3.5 && estrelas <= 5;
    }
}
