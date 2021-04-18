using System.Collections.Generic;
using System.IO;

namespace AnaliseDeSentimento.Arquivo
{
    public class LerArquivo
    {
        public string NomeArquivo { get; private set; }
        public int QuantidadeLinhas { get; private set; }
        public List<string> Linhas { get; private set; }

        public LerArquivo(string nomeArquivo)
        {
            NomeArquivo = nomeArquivo;
            Linhas = new List<string>();

            IniciarLeitura();
        }

        void IniciarLeitura()
        {
            StreamReader file = new(NomeArquivo);
            string linhaAtual = file.ReadLine();

            if (int.TryParse(linhaAtual, out int quantidadeLinhas))
            {
                QuantidadeLinhas = quantidadeLinhas;
                ExecutarLeitura(file);
            }
        }

        void ExecutarLeitura(StreamReader file)
        {
            for (int i = 0; i < QuantidadeLinhas; i++)
                Linhas.Add(file.ReadLine());
        }
    }
}
