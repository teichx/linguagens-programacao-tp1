# Linguagens de programação - TP1 

## Análise de Sentimento

Análise de sentimento consiste na tarefa de se identificar se o sentimento expresso por
um texto possui tendência positiva, negativa ou neutra. Em outras palavras, a ideia
é classificar um texto qualquer de acordo com as palavras que estão nele presentes.
A análise de sentimentos pode ser utilizada tanto para uma análise de satisfação de
alunos em relaçã ao conteúo de disciplinas em fóuns acadêmicos, como exemplo
no fórum do Modle, sendo possível através da mesma evitar possíveis evasões de
alunos de uma disciplina quanto para uma avaliação automática de estabelecimentos
e produtos.
O trabalho prático consiste em realizar uma análise de sentimento de avaliações
de restaurantes. A ideia é classificar uma série de restaurantes em bons, neutros ou
ruins de acordo com a frequência das palavras encontradas em avaliações feitas por
consumidores. Para isso, a implementação deverá seguir as regras abaixo descritas:
  - A entrada é composta por três arquivos que serão fornecidos, cuja extensão
é TXT, recebidos como parâmetro do programa.  O primeiro arquivo contém a lista com as avaliações dos restaurantes. O segundo arquivo contém as palavras cuja classificação é positiva. O terceiro arquivo contém as palavras cuja classificação é negativa;
  - O arquivo que contém a lista de restaurantes possui várias linhas, sendo que a primeira indica o número de restaurantes presentes no arquivo e as demais linhas detém a informação de um restaurante diferente por linha.  Os demais arquivos que contém os termos positivos e negativos seguem estrutura semelhante, onde a primeira linha indica o número de termos do arquivo e as demais possuem os termos, cada um em uma linha, conforme exemplo:
  - 
![image](https://user-images.githubusercontent.com/32073893/115158568-30744780-a065-11eb-8a22-c8b85d9186fd.png)

  -	Deverá ser criada uma estrutura para armazenar os dados dos restaurantes, conforme tabela abaixo. Os dados de cada restaurante estão armazenados no arquivo, separados por ponto e vírgula (;).  Podem ser adicionados á estrutura outros campos que forem necessários.

![image](https://user-images.githubusercontent.com/32073893/115158695-e049b500-a065-11eb-8509-fdb3c2d09ce2.png)

  - A análise de sentimento deverá ser feita com base nos arquivos de termos recebidos como parâmetro.  Para cada termo, deverá ser feita uma contagem do número de vezes que ele aparece, e, no final, caso existam mais termos positivos do que negativos, a classificação da avaliação ser á positiva.  Caso existam mais termos negativos que positivos, o texto deve ser classificado como negativo.  Caso sejam iguais, o texto é classificado como neutro.  Você deve considerar que a análise não deve ser case sensitive, ou seja, um termo poderá ser escrito tanto em maiúsculo quanto em minúsculo.
  -	Você deverá gerar três arquivos de saída cuja extensão é TXT contendo os restaurantes classificados como positivos, negativos e neutros. Cada linha possui os seguintes campos, separados por tabulação ( t): nome do restaurante, número de termos positivos encontrados, número de termos negativos encontrados, lista de termos positivos e lista de termos negativos. Para cada termo positivo ou negativo encontrado,  deverá ser informado o número de ocorrências de cada um deles na avaliação de cada restaurante.  A figura a seguir exemplifica o formato dos arquivos de entrada e os correspondentes de saída:
![image](https://user-images.githubusercontent.com/32073893/115158825-885f7e00-a066-11eb-9d0b-4a1ca61b60ad.png) 

  - Você deverá exibir para o usúario em tela um relatório relacionando as classificações encontradas com as notas de cada restaurante. Para isso, considere que:
  –	classe negativa: restaurante que teve nota no intervalo [1.0, 2.5);
  –	classe neutra: restaurante que teve nota no intervalo [2.5, 3.5);
  –	classe positiva: restaurante que teve nota no intervalo [3.5, 5.0];
  
Assim, esse relatório deverá conter o número de restaurantes classificados corretamente e incorretamente, seguido por uma lista deles.  Uma classificaçãao correta acontece quando um restaurante de uma classe (positiva, negativa ou neutra, de acordo com seu número de estrelas) é de fato classificado como pertencentes a essa classe através da análise dos termos da avaliação, enquanto uma classificação incorreta acontece quando ele é classificado como pertencente qualquer outra classe. Por exemplo, se um restaurante possui nota 2.7 e foi classificado como neutro, essa classificação é considerada correta. Entretanto, se ele fosse classificado como positivo, essa classificação seria incorreta, pois ele teria que ter notas entre (3.5,5.0] para ser da classe positiva.  Além disso, você deve reportar a acurácia obtida pelo modelo, onde:
número de amostras classificadas corretamente

 ![image](https://user-images.githubusercontent.com/32073893/115159117-f9ebfc00-a067-11eb-8955-928cfadfb802.png)
 
A figura exemplifica o relatório que deve ser gerado:

 ![image](https://user-images.githubusercontent.com/32073893/115159125-0708eb00-a068-11eb-9962-8f706f6317ad.png)
