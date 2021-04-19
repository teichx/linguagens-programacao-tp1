from Constantes.EnumTermos import EnumTermos
from Arquivo.LerArquivo import LerArquivo

class Termos:
  def __init__(self, arquivo: LerArquivo, tipoTermos: EnumTermos):
    self.Items = arquivo.Linhas
    self.Tipo = tipoTermos