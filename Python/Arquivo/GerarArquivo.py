import os
from Constantes.EnumClassificacao import EnumClassificacao
from Constantes.NomeArquivos import NomeArquivos
from Entidades.Classificacao import Classificacao


class GerarArquivo:
  def __init__(self, tipoAvaliacao):
    self.TipoAvaliacao = tipoAvaliacao
    self.NomeArquivo = self.SelecionarNomeArquivo()
    self.Endereco = os.path.dirname(os.path.realpath(__file__)) + "/" + self.NomeArquivo

  def SelecionarNomeArquivo(self):
    if self.TipoAvaliacao is EnumClassificacao.Positiva:
      return NomeArquivos.ClassificadosPositivos

    if self.TipoAvaliacao is EnumClassificacao.Neutra:
      return NomeArquivos.ClassificadosNeutros

    if self.TipoAvaliacao is EnumClassificacao.Negativa:
      return NomeArquivos.ClassificadosNegativos

    return NomeArquivos.Output
    
  def EscreverRestaurantes(self, listaRestaurantes):
    if self.TipoAvaliacao is None:
      return

    arquivo = open(self.Endereco, "w")
    for restaurante in listaRestaurantes:
      if restaurante.Classificacao is self.TipoAvaliacao:
        arquivo.write(str(restaurante) + "\n")

  def EscreverSaida(self, classificacao: Classificacao):
    arquivo = open(self.Endereco, "w")
    arquivo.write("Restaurantes classificados corretamente: %d" % (len(classificacao.ClassificadosCorretamente)) + "\n")

    for restaurante in classificacao.ClassificadosCorretamente:
      arquivo.write(restaurante.Nome + "\n")

    arquivo.write("\nRestaurantes classificados incorretamente: %d" % (len(classificacao.ClassificadosIncorretamente)) + "\n")

    for restaurante in classificacao.ClassificadosIncorretamente:
      arquivo.write(restaurante.Nome + "\n")

    arquivo.write("\nAcurácia do modelo: %.2f" % (classificacao.Acuracia) + "\n")
