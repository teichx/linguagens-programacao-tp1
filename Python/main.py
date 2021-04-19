from Arquivo.LerArquivo import LerArquivo
from Entidades.Termos import Termos
from Entidades.Restaurante import Restaurante
from Constantes.NomeArquivos import NomeArquivos
from Constantes.EnumTermos import EnumTermos
from Constantes.EnumClassificacao import EnumClassificacao
from Arquivo.GerarArquivo import GerarArquivo
from Entidades.Classificacao import Classificacao

class Main:
  def __init__(self):
    QuantidadeTestes = 1
    self.EnderecoBase = "C:/Linguagens-Programacao-TP1/teste"

    indiceTeste = 1
    while indiceTeste <= QuantidadeTestes:
      self.ProcessarAvaliacao(indiceTeste)
      indiceTeste += 1

  def GerarArquivosSaida(self, listaRestaurantes):
    arquivoOutputPositivos = GerarArquivo(EnumClassificacao.Positiva)
    arquivoOutputNegativo = GerarArquivo(EnumClassificacao.Negativa)
    arquivoOutputNeutros = GerarArquivo(EnumClassificacao.Neutra)
    arquivoOutputClassificacao = GerarArquivo(None)

    arquivoOutputPositivos.EscreverRestaurantes(listaRestaurantes)
    arquivoOutputNegativo.EscreverRestaurantes(listaRestaurantes)
    arquivoOutputNeutros.EscreverRestaurantes(listaRestaurantes)
    arquivoOutputClassificacao.EscreverSaida(Classificacao(listaRestaurantes))


  def ProcessarAvaliacao(self, indiceTeste):
    enderecoInput = self.EnderecoBase + str(indiceTeste) + "/input/"

    arquivoTermosPositivos = LerArquivo(enderecoInput + NomeArquivos.TermosPositivos)
    arquivoTermosNegativos = LerArquivo(enderecoInput + NomeArquivos.TermosNegativos)
    arquivoAvaliacoes = LerArquivo(enderecoInput + NomeArquivos.Reviews)

    termosPositivos = Termos(arquivoTermosPositivos, EnumTermos.Positivo)
    termosNegativos = Termos(arquivoTermosNegativos, EnumTermos.Negativo)

    listaRestaurantes = Restaurante.ListaDeRestaurantes(arquivo=arquivoAvaliacoes)

    for restaurante in listaRestaurantes:
      restaurante.ProcessarTermosAvaliacao(termosPositivos)
      restaurante.ProcessarTermosAvaliacao(termosNegativos)
    
    self.GerarArquivosSaida(listaRestaurantes)

Main()
