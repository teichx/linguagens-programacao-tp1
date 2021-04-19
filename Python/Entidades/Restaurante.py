import re
import functools
from Arquivo.LerArquivo import LerArquivo
from Entidades.Termos import Termos
from Constantes.EnumTermos import EnumTermos
from Constantes.EnumClassificacao import EnumClassificacao


class Restaurante:
  def __init__(self, valueTuple):
    nome, estrelas, avaliacao = valueTuple

    self.Nome = nome
    self.Estrelas = estrelas
    self.Avaliacao = avaliacao
    self.Termos = dict()
    self.TermosPositivos = 0
    self.TermosNegativos = 0
    self.Classificacao = EnumClassificacao.Neutra

  @staticmethod
  def ListaDeRestaurantes(arquivo: LerArquivo):
    return list(map(lambda linha: Restaurante(Restaurante.SeparaLinha(linha)), arquivo.Linhas))

  @staticmethod
  def SeparaLinha(linha):
    (nome, estrelasString, avaliacao) = linha.split(';')

    estrelas = float(estrelasString)

    return (nome, estrelas, avaliacao)
   

  def AvaliacaoTemporaria(self):
    return re.split(' |,|\.|\!|\\|;|:', self.Avaliacao.lower())
    
  def AdicionarTermo(self, termoAtual, tipoTermo: EnumTermos):
    existeNoDicionario = termoAtual in self.Termos
    quantidadeTermos = 0

    if(existeNoDicionario):
      quantidadeTermos = self.Termos.get(termoAtual)
      self.Termos.pop(termoAtual)
    
    self.Termos[termoAtual] = quantidadeTermos
    self.AdicionarSomatorioTermo(tipoTermo)

  def AdicionarSomatorioTermo(self, tipoTermo: EnumTermos):
    if tipoTermo is EnumTermos.Positivo:
      self.TermosPositivos += 1

    if tipoTermo is EnumTermos.Negativo:
      self.TermosNegativos += 1

  def ProcessarTermosAvaliacao(self, termos: Termos):
    avaliacaoTemp = self.AvaliacaoTemporaria()

    for termoAtual in termos.Items:
      while termoAtual in avaliacaoTemp:
        self.AdicionarTermo(termoAtual, termos.Tipo)
        avaliacaoTemp.remove(termoAtual)

    self.Classificacao = self.ClassificarAvaliacao()

  def ClassificarAvaliacao(self):
    diferencaDeTermosPositivos = self.TermosPositivos - self.TermosNegativos

    if diferencaDeTermosPositivos > 0:
      return EnumClassificacao.Positiva
      
    if diferencaDeTermosPositivos == 0:
      return EnumClassificacao.Neutra

    if diferencaDeTermosPositivos < 0:
      return EnumClassificacao.Negativa

  def __ListarTermos(self):
    resposta = ''
    for key, value in self.Termos.items():
      resposta += "%s: %d; " % (key, value)

    return resposta

  def __str__(self):
    return "%s\t%d\t%d\t%s" % (self.Nome, self.TermosPositivos, self.TermosNegativos, self.__ListarTermos())
