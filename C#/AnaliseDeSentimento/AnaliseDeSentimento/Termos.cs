using System;
using System.Collections.Generic;

namespace AnaliseDeSentimento
{
    public class Termos
    {
        public EnumTermos Tipo { get; init; }
        public List<string> Items { get; private set; }

        public Termos(LerArquivo arquivo, EnumTermos tipoTermos)
        {
            Items = arquivo.Linhas ?? new List<string>();
            Tipo = tipoTermos;
        }
    }
}
