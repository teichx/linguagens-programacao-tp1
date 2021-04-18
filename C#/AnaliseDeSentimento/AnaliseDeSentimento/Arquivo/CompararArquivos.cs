using System.IO;

namespace AnaliseDeSentimento.Arquivo
{
    public class CompararArquivos
    {
        public string EnderecoArquivo1 { get; init; }
        public string EnderecoArquivo2 { get; init; }

        public CompararArquivos(string enderecoArquivo1, string enderecoArquivo2)
        {
            EnderecoArquivo1 = enderecoArquivo1;
            EnderecoArquivo2 = enderecoArquivo2;
        }

        public bool ArquivosIguais()
        {
            try
            {
                using var arquivo1 = File.OpenText(EnderecoArquivo1);
                using var arquivo2 = File.OpenText(EnderecoArquivo1);

                string linhaArquivo1 = string.Empty;
                string linhaArquivo2 = string.Empty;

                while (linhaArquivo1 is not null && linhaArquivo2 is not null)
                {
                    linhaArquivo1 = arquivo1.ReadLine();
                    linhaArquivo2 = arquivo2.ReadLine();

                    if (linhaArquivo1 != linhaArquivo2)
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
