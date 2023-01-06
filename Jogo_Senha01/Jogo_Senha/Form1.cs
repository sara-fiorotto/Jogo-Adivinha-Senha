using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jogo_Senha
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        int tentativas = 0;
        public bool txtBox1_OK, txtBox2_OK, txtBox3_OK, txtBox4_OK, boolauxiliar = false; 
        string gerado = "";


        //Função que libera botão jogar apenas se houver Texto nas quatro MaskaraTextBox.
        public bool Libera_Jogar()
        {
            bool libera=false;
            if (txtBox1_OK && txtBox2_OK && txtBox3_OK && txtBox4_OK)//Verifica se todas as Maskaras estão preenchidas
            {
                libera = true;
            }
            return btnJogar.Enabled = libera;
        }

        //Função que verifica se há Texto no parametro informado, se sim retorna  True
        public bool Libera_Parametro(string parametroTexto)
        {

            if (parametroTexto.ToString() != "")
            {
                return boolauxiliar = true;
            }
            else
            {
                return boolauxiliar = false;
            }
            
        }



        //Gerador de números aleatórios, de 1 a 10, sem repetição e armazenados na string Num
        public string Gera_Numero()
        {
            string Numeros = "", num = "";

            Random randNum = new Random();
            Numeros += randNum.Next(1, 10);

            for (int i = 1; i <= 4; i++)
            {
                if (!num.Contains(Numeros.ToString()))
                {
                    num += Numeros;
                }

                else
                {
                    i--;
                }
                Numeros = "";
                Numeros += randNum.Next(1, 10);
            }
            return num;
        }

        //Abrindo fomulário de instruções e botão de fechar formulário do jogo.
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Form2 f = (Form2)Application.OpenForms["Form2"];
            if (f != null)
                f.BringToFront();
            else
            {
                Form2 form = new Form2();
                form.Show();
            }

        }
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }



        //Tratamento na TextChange em todas as mascáras 
        private void mskTxt1_TextChanged(object sender, EventArgs e)
        {
            Libera_Parametro(mskTxt1.Text);
            txtBox1_OK = boolauxiliar;
            Libera_Jogar();
        }
        private void mskTxt2_TextChanged(object sender, EventArgs e)
        {
            Libera_Parametro(mskTxt2.Text);
            txtBox2_OK = boolauxiliar;
            Libera_Jogar();
        }

        private void mskTxt3_TextChanged(object sender, EventArgs e)
        {
            Libera_Parametro(mskTxt3.Text);
            txtBox3_OK = boolauxiliar;
            Libera_Jogar();
        }
        private void mskTxt4_TextChanged(object sender, EventArgs e)
        {
            Libera_Parametro(mskTxt4.Text);
            txtBox4_OK = boolauxiliar;
            Libera_Jogar();
        }



        //Funcionamento do Jogo, comparação da tentativa do jogador com a resposta gerada pelo computador
        private void btnJogar_Click(object sender, EventArgs e)
        {
            tentativas++;
            label1.Text = tentativas.ToString();

            if (gerado == "") { gerado = Gera_Numero(); }//Gerando string de 4 digitos aleatória

            //teste.Text = gerado.ToString();//Teste para verificar a resposta e a funcionalidade do jogo

            string tentativa = //Adicionando o 4 digitos informados pelo jogador na string tentativa
                mskTxt1.Text.ToString() + 
                mskTxt2.Text.ToString() +
                mskTxt3.Text.ToString() +
                mskTxt4.Text.ToString();

            listBox1.Items.Add(tentativa);

            int otimo = 0, bom = 0, ruim = 0;

            for (int i = 0; i <= 3; i++)
            {           
                string tent = tentativa[i].ToString();

                if (tentativa[i] == gerado[i]){otimo++;} //Se o número/tentativa na posição de x for igual ao numero/gerado na mesma posição, soma um ótimo!
                
                else if (gerado.Contains(tent)){bom++;} //Se o numero/tentativa conter na string gerado, soma um bom!

                else{ ruim++;} //Caso o número não tenha na string gerado, soma um ruim!
            }

            listBox3.Items.Add("Ótimo " + otimo + " ,Bom " + bom + " ,Ruim" + ruim).ToString();

            if (tentativa == gerado) //Caso a ordem e os números estejam iguais, o jogador ganha!
            { 
                MessageBox.Show("Parabéns, você ganhouu o número é: "+gerado);
                gerado = "";
                tentativas = 0;
                listBox1.Items.Clear();
                listBox3.Items.Clear();
            }
            mskTxt1.Clear();
            mskTxt2.Clear();
            mskTxt3.Clear();
            mskTxt4.Clear();
            
            mskTxt1.Focus();

        }
    }
}
