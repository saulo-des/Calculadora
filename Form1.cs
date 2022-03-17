using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculo
{
    public partial class Form1 : Form
    {
        double primeiroNum = 0;
        double segundoNum = 0;
        string operacoes;
        int fase = 1;
        int limiteCampo = 20;
        bool selectReais = false;
        NumberFormatInfo nfi;
        bool numeroComVirgual = false;
        bool resultadoComVirgula = false;
        string telaInferior = "";
        int qtdeNumPosVirgula = 0;

        public Form1()
        {
            InitializeComponent();
        }

        #region Abrindo tela e limpando valores
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Text = "0";

            nfi = new NumberFormatInfo() //Instanciando uma classe NumberFormatInfo para inserir separador "."
            {
                NumberGroupSeparator = "."
            };

        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "0";

            primeiroNum = 0;
            segundoNum = 0;
            fase = 1;
            numeroComVirgual = false;
        }
        #endregion

        #region Imprimindo valor 0 na tela
        private void button1_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
         
            if (textBox2.Text != "0")
                if (textBox2.Text.Length < limiteCampo)
                    ImprimirValor("0");

        }
        #endregion

        #region Imprimindo valores na tela 1 ao 9
        private void button8_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor("1");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor("2");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor("3");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor("4");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor("5");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor("6");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor("7");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor("8");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();
            ImprimirValor("9");
        }
        #endregion

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

        public void RecebeOperacaoNumUM(string op)
        {

            if (primeiroNum == 0)
            {
                primeiroNum = Convert.ToDouble(textBox2.Text.ToString());
            }

            operacoes = op;

            textBox1.Text = primeiroNum + " " + op + " ";

            numeroComVirgual = false;

            fase = 2;
        }

        #region Imprime Resultado
        private void button15_Click(object sender, EventArgs e)
        {
            string N = "N0";
            double result = 0;
            string[] valor = new string[5];
                        
            if (operacoes != null)
            {
               
                if (textBox1.Text.Contains("="))
                {
                    primeiroNum = Convert.ToDouble(textBox2.Text.ToString());
                }
                else
                {
                    segundoNum = Convert.ToDouble(textBox2.Text.ToString());
                }

                textBox1.Text = primeiroNum + " " + operacoes+ " " + segundoNum + " = ";

                switch (operacoes)
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

                if (Convert.ToString(result).Contains(","))
                {
                    resultadoComVirgula = true;
                }

                if (!selectReais)
                {

                    if (resultadoComVirgula)
                    {
                        qtdeNumPosVirgula = 0;

                        ContaVirgula(result);

                        for (int i = 0; i <= qtdeNumPosVirgula; i++)
                        {
                            N = "N" + i;
                        }
                        textBox2.Text = result.ToString(N);
                    }
                    else
                    {
                        textBox2.Text = result.ToString("N0", nfi);
                    }
                }
                else
                {
                    textBox2.Text = result.ToString("C2");
                }

                primeiroNum = 0;

                if (!textBox1.Text.Contains("="))
                {
                    segundoNum = 0;
                }
                
                fase = 3;
            }
            else
            {
                textBox1.Text = "Insira operação";
                LetTheGameStart_Load(sender, e);
            }
        }
        #endregion

        #region Talvez eu use para contar numero de virgulas
        public void ContaVirgula(double valor)
        {
            string numero = valor.ToString();
            int verifica = 0;

            char[] ch = new char[numero.Length];

            for (int i = 0; i < numero.Length; i++)
            {
                ch[i] = numero[i];
            }

            foreach (char c in ch)
            {
                if (c == ',')
                {
                    verifica = 1;
                }

                if (verifica == 1)
                {
                    qtdeNumPosVirgula++;
                }
            }

            qtdeNumPosVirgula--;
        }
        #endregion

        private async void LetTheGameStart_Load(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            textBox1.Text = "";
        }

        public void ImprimirValor(string valorClicado)
        {
            float valorRelativo = Convert.ToSingle(valorClicado);

            if (textBox2.Text == Convert.ToString(0))
            {
                textBox2.Text = ""; //NOTE: Limpa campo para adicionar novo valor!
            }

            if (textBox2.Text.Length < limiteCampo)
            {
                textBox2.Text += valorRelativo.ToString();

                decimal numeroTela = Convert.ToDecimal(textBox2.Text);

                if (Convert.ToString(numeroTela).Contains(","))
                {
                    numeroComVirgual = true;
                }

                if (!numeroComVirgual)
                {
                    textBox2.Text = numeroTela.ToString("N0", nfi);
                }

            }
        }

        public void RecebeOrdemFase()
        {
            if (fase == 2) //NOTE: Verifica se esta na segunda fase da conta
            {
                textBox2.Text = Convert.ToString(0); //NOTE: Se estiver segunda fase da conta ele recebe o zero e inicia o fase da conta como 1
                fase = 1;
            }

            if (fase == 3)
            {
                textBox1.Text = "";
                fase = 1;
                textBox2.Text = "";
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            RecebeOrdemFase();

            if (!textBox2.Text.Contains(","))
            {
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    textBox2.Text += "0,";
                }
                else
                {
                    textBox2.Text += ",";
                }
            }

            else
            {
                textBox1.Text = "impossivel duas virgula";
                LetTheGameStart_Load(sender, e);
            }


        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (fase != 3)
            {
                if (textBox2.Text.Length != 1)
                {
                    string novoValor = textBox2.Text.Remove(textBox2.Text.Length - 1);
                    textBox2.Text = novoValor;
                }
                else
                {
                    textBox2.Text = Convert.ToString(0);
                }
            }
            else
            {
                textBox1.Text = "";   
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (selectReais)
            {
                button19.BackColor = Color.White;
                selectReais = false;
            }
            else
            {
                button19.BackColor = Color.Yellow;
                selectReais = true;

                textBox1.Text = "Use virgula para centavos";
                LetTheGameStart_Load(sender, e);
            }
        }
    }
}
