using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculo
{
    public partial class Form1 : Form
    {
        #region Variaveis
        double primeiroNum = 0; //NOTE: Essa variavel primeiroNum recebe primeiro numero digitado na tela para o calculo com Segundo número
        double segundoNum = 0; //NOTE: Essa variavel segundoNum recebe segundo numero digitado na tela para o calculo com Primeiro número
        string operacoes = ""; //NOTE: Essa variavel operacoes recebe a operação digitado na tela para o cálculo entre Primeiro número e Segundo numero
        int fase = 1; //NOTE: Essa variavel recebe as fases durante o cálculo para poder definir em que fase está para prosseguir com cálculo
        int limiteCampo = 20; //NOTE: Essa variavel recebe limite de campo no caso 20 .. contando com virgula e pontos
        int limiteVirgula = 5; //NOTE: Colocando limite de virgula
        bool selectReais = false; //NOTE: Essa variavel recebe modo de cálculo ao usuário
        NumberFormatInfo nfi; //Variavel com objeto da classe NumberFormatInfo
        bool numeroComVirgula = false; //Variavel recebendo se numero tem virgula
        bool resultadoComVirgula = false; //Variavel recebendo se resultado tem virgula
        int qtdeNumPosVirgula = 0; //variavel que recebe quantidade de numero após virgula
        string numeroUm = ""; // variavel numeroUm que recebe valor do textbox1 para mostrar os avisos 
        
        #endregion

        #region Inicia Componente
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region Abrindo tela inicial
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Text = "0"; //NOTE: Tela inicia com zero

            nfi = new NumberFormatInfo() //Instanciando uma classe NumberFormatInfo para inserir separador "."
            {
                NumberGroupSeparator = "." //NOTE: Aqui ele vai separar o valor digitado em . a cada 3 carateres tipo > 3.500.369.001
            };

        }
        #endregion

        #region Limpando tela zerando valores das variaveis
        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "0";

            primeiroNum = 0;
            segundoNum = 0;
            fase = 1;
            numeroComVirgula = false;
            operacoes = "";
        }
        #endregion

        #region Imprimindo valor 0 na tela
        private void button1_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase(); //Recebe fase

            if (textBox2.Text != "0") //Caso seja diferente de zero
                if (textBox2.Text.Length < limiteCampo) //Caso seja menor que o limite estimado
                    ImprimirValor(sender, e, "0"); //imprimi o valor zero na tela

        }
        #endregion

        #region Imprimindo valores na tela 1 ao 9
        private void button8_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor(sender, e, "1");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor(sender, e, "2");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor(sender, e, "3");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor(sender, e, "4");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor(sender, e, "5");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor(sender, e, "6");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor(sender, e, "7");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor(sender, e, "8");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor(sender, e, "9");
        }
        #endregion

        #region Recebe Operações
        private void button14_Click_1(object sender, EventArgs e)
        {
            RecebeOperacaoNumUM("÷");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            RecebeOperacaoNumUM("x");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            RecebeOperacaoNumUM("+");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            RecebeOperacaoNumUM("-");
        }
        #endregion

        #region Recebendo número um para cálculo
        public void RecebeOperacaoNumUM(string op)
        {
            if (primeiroNum == 0) //Se primeiro numero for zero
            {
                primeiroNum = Convert.ToDouble(textBox2.Text.ToString().Replace("R$","").Replace(" ", "")); //Caso tenha R$ ou espaço ele remove, passando valor para primeiro numero
            }

            operacoes = op; //Recebendo operação escolhida pelo usuário

            textBox1.Text = primeiroNum + " " + op + " "; //Mostrando valor digitado e operação na tela superior da calculadora

            numeroComVirgula = false; //Número da virgula recebendo valor de falso para fase 2

            fase = 2;//ALterando para fase dois dos cálculos
        }
        #endregion

        #region Imprime Resultado 
        private void button15_Click(object sender, EventArgs e)
        {            
            string N = "N0"; //Usando N para imprimir resultado, sendo N número inteiro (10.000) e N1 numero decimal (10.000,0) e assim sucessivamente
            double result = 0; //Recebendo resultado como 0
            bool textoGrande = false; //Recebdendo texto grande como falso

            if (operacoes != "") //Se existe uma operação
            {
                if (textBox1.Text.Contains("=") || numeroUm.Contains("=")) //Se acaso estiver na segunda fase do cálculo ou texto superior contém =
                {
                    if (selectReais) //Se acaso estiver modo reais selecionado
                    {
                        primeiroNum = Convert.ToDouble(textBox2.Text.Replace("R$", "").Replace(" ", "")); //primeiro número recebe o valor do textbox2 removendo R$ e espaço para poder calcular apenas o valor
                    }
                    else
                    {
                        primeiroNum = Convert.ToDouble(textBox2.Text.ToString()); //Caso calculo esteja e modo normal, primeiro num recebe valor normal
                    }

                    numeroUm = ""; //numero um que tinha valor do txtbox1 é apagado nessa fase pois não terá mais necessidade aqui
                }
                else
                {
                    segundoNum = Convert.ToDouble(textBox2.Text.ToString()); //caso ainda esteja na primeira fase do cálculo ele recebe o segundo número ara cálcular
                }

                textBox1.Text = primeiroNum + " " + operacoes + " " + segundoNum + " = "; //imprime na tela primeiro número com segundo número 

                switch (operacoes) //Verifica operação para Cálculo
                {
                    case "÷":
                        result = primeiroNum / segundoNum;
                        break;
                    case "x":
                        result = primeiroNum * segundoNum;
                        break;
                    case "-":
                        result = primeiroNum - segundoNum;
                        break;
                    case "+":
                        result = primeiroNum + segundoNum;
                        break;
                }

                if (Convert.ToString(result).Contains(",")) //Caso resultado tenha virgula
                {
                    resultadoComVirgula = true; //resultadoComVirgula é verdadeiro
                }

                if (Convert.ToString(result).Length > limiteCampo) //Se resultado maior que limite de campo 
                {
                    textoGrande = true; //textoGrande é verdadeiro
                }

                if (!selectReais) //Se modo de resultado não for reais 
                {
                    if (resultadoComVirgula) //se for resultado com Virgula
                    {
                        qtdeNumPosVirgula = 0;

                        ContaVirgula(result); //Função que conta numero após virgula

                        for (int i = 0; i <= qtdeNumPosVirgula; i++) //Loop para receber o número de virgulas e imprimir como n numeros
                        {
                            N = "N" + i;

                            if (i == limiteVirgula) //Colocando limite para imprimir resultado com virgula
                            {
                                break;
                            }
                        }

                        if (textoGrande) // || qtdeNumPosVirgula > limiteCampo) //Caso resultado for maior que limite campo e quantidade de virgula for amior que limite ele força
                        {
                            textBox2.Text = result.ToString(N).Substring(0, limiteCampo); //Forçando imprimir com limite exigido
                        }
                        else
                        {
                            textBox2.Text = result.ToString(N); //Imprime valor normal que resultou
                        }
                    }
                    else
                    {
                        if (textoGrande) //Se resultado não contém virgula e texto for grande
                        {
                            textBox2.Text = result.ToString("N0", nfi).Substring(0, limiteCampo); //Retorna valor do resultado com limite
                        }
                        else
                        {
                            textBox2.Text = result.ToString("N0", nfi); //Retorna sem limite, pois o mesmo esta aqui dentro do limite
                        }
                    }
                }
                else
                {
                    textBox2.Text = result.ToString("C2"); //Retorna impressão com valor de Reais
                }

                primeiroNum = 0; //Primeiro numero recebe valor de zero

                if (!textBox1.Text.Contains("=")) //Caso texto superior não contenha = .. ou esteja na fase
                {
                    segundoNum = 0;
                }

                fase = 3;
            }
            else //Se não existe operação ele mostra mensagem para inserir
            {
                numeroUm = textBox1.Text; //Reserva o textbox1 no numero um para retorna o valor apos mensagem 
                textBox1.Text = "Insira operação"; //Mensagem de Aviso ao usuário
                LetTheGameStart_Load(sender, e, numeroUm); //Função de timer do aviso, passando numeroUm para após msg
                numeroUm = ""; //Retorna numero vazio
            }
        }
        #endregion

        #region Função para contar virgula
        public void ContaVirgula(double valor)
        {
            string numero = valor.ToString(); //Converte o resultado para string
            int verifica = 0;

            char[] ch = new char[numero.Length]; //Um array para percorrer o numero em caracteres

            for (int i = 0; i < numero.Length; i++) //Faz um loop no numero para contar virgula
            {
                ch[i] = numero[i]; //Adiciona caracter no array ch
            }

            foreach (char c in ch) // Loop no array ch
            {
                if (c == ',') //Quando chegar na virgula a verificação recebe 1
                {
                    verifica = 1;
                }

                if (verifica == 1) //Com a verificação sendo 1 ele começa a contar
                {
                    qtdeNumPosVirgula++;
                }
            }

            qtdeNumPosVirgula--; //Tirando um valor, pois ele contoua virgula junto
        }
        #endregion

        #region Função para timer de avisos
        private async void LetTheGameStart_Load(object sender, EventArgs e, string numeroUm)
        {
            await Task.Delay(3000);
            textBox1.Text = numeroUm;
        }
        #endregion

        #region Imprime valor do número digitado
        public void ImprimirValor(object sender, EventArgs e, string valorClicado)
        {
            
            if (textBox2.Text == Convert.ToString(0)) //Se textBox2 for igual a zero
            {
                textBox2.Text = ""; //NOTE: Limpa campo para adicionar novo valor!
            }

            if (textBox2.Text.Length < limiteCampo) //Verifica se textBox esta dentro do limite para poder digitar
            {
                textBox2.Text += valorClicado.ToString(); //Imprime valor na tela inferior (txtBox2)

                decimal numeroTela = Convert.ToDecimal(textBox2.Text); //É preciso colocar em double para poder imprimir como N

                if (Convert.ToString(numeroTela).Contains(",")) //Caso tenha virgula no número (txtBox2) ele recebe true
                {
                    numeroComVirgula = true; 
                }

                if (!numeroComVirgula) //Caso não tenha virgula ele recebe milhares adicionando . entre milhares 1.256.236.256
                {
                    textBox2.Text = numeroTela.ToString("N0", nfi); //Caso tenha virgula ele vai imprimir 1.225,55654525854566 tirando o ponto
                }

            }
            else //Se limite foi alcançado ele mostra aviso na tela
            {
                numeroUm = textBox1.Text; 
                textBox1.Text = "limite alcançado";
                LetTheGameStart_Load(sender, e, numeroUm);
                numeroUm = "";
            }
        }
        #endregion

        #region Recebendo fase do cálculo
        public void RecebeOrdemFase()
        {
            if (fase == 2) //NOTE: Verifica se esta na segunda fase da conta
            {
                textBox2.Text = Convert.ToString(0); //NOTE: Se estiver segunda fase da conta ele recebe o zero e inicia o fase da conta como 1
                fase = 1; //Voltando para fase 1
            }

            if (fase == 3) // se estiver na última fase que é 3 ele limpa tela e volta na fase 1
            {
                textBox1.Text = "";
                fase = 1;
                textBox2.Text = "";
            }
        }
        #endregion

        #region Botão da virgula
        private void button17_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase(); // Verifica fase e atualiza se preciso

            if (textBox2.Text.Length < limiteCampo) //Se texto estiver dentro do limite de campo para digitar
            {

                if (!textBox2.Text.Contains(",")) //Caso não contenha virgula
                {
                    if (string.IsNullOrEmpty(textBox2.Text)) //Se textobox2 for vazio ou nulo ele recebe valor de 0,
                    {
                        textBox2.Text += "0,";
                    }
                    else //Se não ele recebe apenas valor de ,
                    {
                        textBox2.Text += ",";
                    }
                }

                else //Caso texto já tenha virgula
                {
                    numeroUm = textBox1.Text;
                    textBox1.Text = "impossivel duas virgula";
                    LetTheGameStart_Load(sender, e, numeroUm);
                    numeroUm = "";
                }
            }
            else //Caso campo esteja no limite ele imprime aviso
            {
                numeroUm = textBox1.Text;
                textBox1.Text = "limite alcançado";
                LetTheGameStart_Load(sender, e, numeroUm);
                numeroUm = "";
            }

        }
        #endregion

        #region Botão de remover caracteres
        private void button18_Click(object sender, EventArgs e)
        {
            if (fase != 3) //Caso esteja na fase 1 ou 2 
            {
                if (textBox2.Text.Length != 1) //Caso tenha mais de um caracter no txtBox2 ele irá remover 1 caracter
                {
                    string novoValor = textBox2.Text.Remove(textBox2.Text.Length - 1);
                    textBox2.Text = novoValor;
                }
                else //Caso tenha apenas um caracter na tela, ele irá receber 0
                {
                    textBox2.Text = Convert.ToString(0);
                }
            }
            else //Caso esteja na fase 3 ele limpa textBox um e guarda valor dele em número um
            {
                numeroUm = textBox1.Text;
                textBox1.Text = "";
            }
        }
        #endregion

        #region Alterando modo de cálculo
        private void button19_Click(object sender, EventArgs e)
        {
            string[] textos = new string[1]; //Array que o tipo de moeda para imprimir conforme a escolha de modo de cálculo

            if (!selectReais) //Se modo moeda for falso ele recebe moeda no texto de aviso
            {
                textos[0] = "moeda";
            }
            else
            {
                textos[0] = "normal"; //Caso já esteja modo moeda ele mostra normal no texto de aviso a seguir
            }

            if (MessageBox.Show("Deseja alterar modo de resultado para modo " + textos[0] + " ?", "Atenção", MessageBoxButtons.YesNo) == DialogResult.Yes) //Se for sim reposta
            {
                double numeroTela = 0;

                if (textBox2.Text.Contains("R$")) //Caso tenha R$ no texto ele remove para colocar apenas número, porém mantendo a virgula
                {
                    numeroTela = Convert.ToDouble(textBox2.Text.Replace("R$", "").Replace(" ", ""));
                }
                else //Caso não tenha R$ ele imprime valor normal 
                {
                    numeroTela = Convert.ToDouble(textBox2.Text);
                }

                if (selectReais) //Se for reais 
                {
                    if (!String.IsNullOrEmpty(textBox1.Text)) //Se texto da tela superior for diferente de vazio, ele recebe numeroTela que vem do tipo double para converter modo N.
                    {
                        textBox2.Text = numeroTela.ToString("N"); //Mudando modo moeda para modo normal
                    }

                    button19.BackColor = Color.White;
                    selectReais = false; //Reais torna valor falso
                }
                else
                {
                    if (!String.IsNullOrEmpty(textBox1.Text))  //Se texto for diferente de vazio, ele recebe numeroTela que vem do tipo double para converter modo moeda
                    {
                        textBox2.Text = numeroTela.ToString("C2"); //Recebendo modo moeda na tela
                    }

                    button19.BackColor = Color.Yellow; //Deixa botão amarelo
                    selectReais = true;


                    MessageBox.Show("Use virgula para centavos");

                }
            }
        }
        #endregion

        #region Alterando número para positivo/negativo 
        private void button20_Click(object sender, EventArgs e)
        {
            double numeroDigitado = 0;

            if (selectReais) //Se modo de cálculo for reais ele irá remover valores não númericos
            {
                numeroDigitado = Convert.ToDouble(textBox2.Text.Replace("R$", "").Replace("-", "")); 
            }
            else //Se modo for normal ele mantém valores digitado
            {
                numeroDigitado = Convert.ToDouble(textBox2.Text.Replace("-", ""));
            }


            if (!textBox2.Text.Contains("-")) //Caso seja positivo, ele torna valor negativo
            {
                double contaResultado = numeroDigitado - (numeroDigitado + numeroDigitado);

                if (selectReais)
                {
                    textBox2.Text = Convert.ToString(contaResultado.ToString("C2"));
                }
                else
                {
                    textBox2.Text = Convert.ToString(contaResultado);
                }
            }
            else //Caso seja negativo ele torna positivo
            {
                if (selectReais)
                {
                    textBox2.Text = Convert.ToString(numeroDigitado.ToString("C2"));
                }
                else
                {
                    textBox2.Text = Convert.ToString(numeroDigitado);
                }
            }
        }
        #endregion

        #region Trazendo apenas número da string
        public void ApenaNumero()
        {
            string textoAlphanumeric = "Saulo Regex 12378 - + testando = 15, 80";
            double apenasNumeros = Convert.ToDouble(String.Join("", System.Text.RegularExpressions.Regex.Split(textoAlphanumeric, @"[^\d]")));
        }
        #endregion
    }
}
