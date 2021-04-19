class LerArquivo:
  def __init__(self, nomeArquivo):
    self.NomeArquivo = nomeArquivo
    self.QuantidadeLinhas = 0
    self.Linhas = []
    self.IniciarLeitura()

  def IniciarLeitura(self):
    refArquivo = open(self.NomeArquivo, "r")

    linhaAtual = refArquivo.readline()
    self.QuantidadeLinhas = int(linhaAtual)
    self.ExecutarLeitura(refArquivo)

    refArquivo.close()

  def ExecutarLeitura(self, refArquivo):
    for index in range(self.QuantidadeLinhas):
      linha = refArquivo.readline().replace('\n', '')
      
      self.Linhas.append(linha)
