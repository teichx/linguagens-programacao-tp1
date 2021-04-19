from Entidades.Restaurante import Restaurante
from Constantes.EnumClassificacao import EnumClassificacao

class Classificacao:

  def __init__(self, listaRestaurantes):
    self.ClassificadosCorretamente = []
    self.ClassificadosIncorretamente = []
    self.Acuracia = 0
    self.MedirAcuraciaClassificacao(listaRestaurantes)

  def MedirAcuraciaClassificacao(self, listaRestaurantes):
    for restaurante in listaRestaurantes:
      if self.ClassificacaoCorreta(restaurante):
        self.ClassificadosCorretamente.append(restaurante)
      else:
        self.ClassificadosIncorretamente.append(restaurante)

    self.Acuracia = len(self.ClassificadosCorretamente) / (len(self.ClassificadosCorretamente) + len(self.ClassificadosIncorretamente))

  def ClassificacaoCorreta(self, restaurante: Restaurante):
    if restaurante.Classificacao is EnumClassificacao.Positiva:
      return self.ClassificacaoPositiva(restaurante.Estrelas)

    if restaurante.Classificacao is EnumClassificacao.Neutra:
      return self.ClassificacaoNeutra(restaurante.Estrelas)
    
    if restaurante.Classificacao is EnumClassificacao.Negativa:
      return self.ClassificacaoNegativa(restaurante.Estrelas)

    return true
  
  def ClassificacaoNegativa(self, estrelas):
    return estrelas >= 1 and estrelas < 2.5

  def ClassificacaoNeutra(self, estrelas):
    return estrelas > 2.5 and estrelas < 3.5
  
  def ClassificacaoPositiva(self, estrelas):
    return estrelas >= 3.5 and estrelas <= 5

