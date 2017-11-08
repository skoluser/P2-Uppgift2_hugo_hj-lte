using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calc
{
    public partial class Form1 : Form
    {
        //Hela uträkningen, med operatorer(?) och tal å sånt
        List<string> equation = new List<string>();

        //Kollar om användaren skriver inom en parentes
        //Används i btnParentheses_Click
        bool parentheses = false;

        //Användarens input
        string baseNumber = "";

        public Form1()
        {
            InitializeComponent();
        }

        //***************************************************************
        //siffror
        //***************************************************************

        //För knappar 1-9
        public void GeneralButtonClick (string siffra)
        {
            //Om baseNumber är en operator:
            //skicka operatorn till equation och sätt baseNumber till siffra
            if (baseNumber == "+" || baseNumber == "-" || baseNumber == "/" || baseNumber == "*" || baseNumber == "(")
            {
                equation.Add(baseNumber);
                baseNumber = siffra;
            }

            //Om baseNumber är en slutparentes
            //Lägg till slutparentesen och ett multiplikationstecken i equation
            //Sätt baseNumber till siffra
            else if (baseNumber == ")")
            {
                equation.Add(baseNumber);
                equation.Add("*");
                baseNumber = siffra;
            }

            //Annars:
            //Lägg till siffra
            else
                baseNumber += siffra;

            //Textrutan skriver ut baseNumber
            txtOutput.Text = baseNumber;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            GeneralButtonClick("1");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            GeneralButtonClick("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            GeneralButtonClick("3");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            GeneralButtonClick("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            GeneralButtonClick("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            GeneralButtonClick("6");
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            GeneralButtonClick("7");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            GeneralButtonClick("8");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            GeneralButtonClick("9");
        }

        //Siffran 0
        private void btn0_Click(object sender, EventArgs e)
        {
            //Skicka operatorn till equation och sätt baseNumber till siffra
            if (baseNumber == "+" || baseNumber == "-" || baseNumber == "*" || baseNumber == "/" || baseNumber == "(")
            {
                equation.Add(baseNumber);
                baseNumber = "0";
            }

            //Om baseNumber inte är en enkel nolla
            //Adda noll till baseNumber
            else if (baseNumber != "0")
            {
                baseNumber += "0";
            }

            txtOutput.Text = baseNumber;
        }

        //Decimaltecken
        private void btnDecimal_Click(object sender, EventArgs e)
        {
            //Om baseNumber är tom eller är en negativ "signifier"
            //Lägg till "0," till baseNumber
            if (baseNumber == "" || baseNumber == "(-")
                baseNumber += "0,";

            //Om baseNumber är en operator:
            //Skicka operatorn till equation och sätt baseNumber till "0,"
            else if (baseNumber == "+" || baseNumber == "-" || baseNumber == "/" || baseNumber == "*" || baseNumber == "(")
            {
                equation.Add(baseNumber);
                baseNumber = "0,";
            }

            //Om basenumber är en slutparentes
            //Adda baseNumber och ett multiplikationstecken till equation
            //Sätt baseNumber till "0,"
            else if (baseNumber == ")")
            {
                equation.Add(baseNumber);
                equation.Add("*");
                baseNumber = "0,";
            }

            //Förhindrar att användaren har flera decimaltecken i ett tal
            else if (!baseNumber.Contains(","))
                baseNumber += ",";

            //Om baseNumber är 0 eller "(-0," så händer ingenting

            txtOutput.Text = baseNumber;
        }

        //************************************************************************
        //operators
        //************************************************************************

        //För +, -, /, och *
        public void GeneralOperatorClick(string op)
        {
            //Om baseNumber redan är en operator:
            //Skriv över baseNumber
            if (baseNumber == "+" || baseNumber == "-" || baseNumber == "/" || baseNumber == "*")
                baseNumber = op;

            //Om baseNumber är ett positivt tal:
            //Skicka baseNumber till equation och skriv över baseNumber med operator
            else if (baseNumber != "" && baseNumber != "0," && !baseNumber.Contains("(-") && baseNumber != "(-0," && baseNumber != "(")
            {
                equation.Add(baseNumber);
                baseNumber = op;
            }

            //Om baseNumber är ett negativt tal:
            //Ta bort parantesen och lägg till baseNumber i equation
            //Sätt baseNumber till operator
            if (baseNumber.Contains("(-") && baseNumber != "(-")
            {
                baseNumber = baseNumber.Remove(0, 1);
                equation.Add(baseNumber);
                baseNumber = op;
            }

            txtOutput.Text = baseNumber;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GeneralOperatorClick("+");
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            GeneralOperatorClick("-");
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            GeneralOperatorClick("*");
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            GeneralOperatorClick("/");
        }

        //För negativa tal
        private void btnNegative_Click(object sender, EventArgs e)
        {
            //Om baseNumber är en operator:
            //Skicka operatorn till equation och sätt baseNumber till "(-"
            if (baseNumber == "+" || baseNumber == "-" || baseNumber == "/" || baseNumber == "*" || baseNumber == "(")
            {
                equation.Add(baseNumber);
                baseNumber = "(-";                
            }
            
            //Om baseNumber är tom
            //Sätt baseNumber till "(-"
            else if (baseNumber == "" || baseNumber == "0,")
            {
                baseNumber = "(-";
            }

            else if (baseNumber == ")")
            {
                equation.Add(baseNumber);
                equation.Add("*");
                baseNumber = "(-";
            }

            //Om baseNumber är ett tal eller en negativ "signifier" gör knappen ingenting

            txtOutput.Text = baseNumber;
        }

        //Parenteser
        private void btnParentheses_Click(object sender, EventArgs e)
        {
            //Om användaren inte skriver i en parentes
            if (!parentheses)
            {
                parentheses = true;

                //Om baseNumber är en operator
                //Lägg till operatorn i equation     
                if (baseNumber == "+" || baseNumber == "-" || baseNumber == "*" || baseNumber == "/")
                {
                    equation.Add(baseNumber);
                }

                //Om baseNumber är tom gör ingenting särskilt
                else if (baseNumber == "")
                {
                }

                //Om baseNumber är ett tal eller en slutparentes
                //Lägg till talet och ett multiplikationstecken i equation
                else if (baseNumber != "(-" || baseNumber != "0," || baseNumber != "(-0,")
                {
                    equation.Add(baseNumber);
                    equation.Add("*");
                }

                //Sätt alltid baseNumber till "("
                baseNumber = "(";

                txtOutput.Text = baseNumber;
            }

            //Om användaren skriver i en parentes
            else
            {
                //Om baseNumber är ett tal
                //lägg till talet i equation och sätt baseNumber till ")"
                //sätt parentheses till false;
                if (baseNumber != "+" && baseNumber != "-" && baseNumber != "*" && baseNumber != "/" && baseNumber != "(-" && baseNumber != "(-0," && baseNumber != "0," && baseNumber != "(")
                {
                    equation.Add(baseNumber);
                    baseNumber = ")";
                    parentheses = false;
                }

                //Annars gör ingenting
                else
                {
                }

                txtOutput.Text = baseNumber;
            }
        }

        //Equals
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //Om baseNumber inte är ett tal eller om equation är tom, gör ingenting
            if (baseNumber == "(-" || baseNumber == "+" || baseNumber == "-" || baseNumber == "/" || baseNumber == "*" || baseNumber == "(" || equation.Count == 0)
            {
            }

            else
            {
                //Om baseNumber är ett negativt tal:
                //Ta bort parentesen och skicka baseNumber till equation
                if (baseNumber.Contains("(-"))
                {
                    equation.Add(baseNumber.Remove(0, 1));
                }

                //Annars: Lägg till baseNumber i equation och räkna ut
                else
                {
                    //Lägg till baseNumber i equation
                    equation.Add(baseNumber);
                }

                //Om användaren skriver i en parentes
                //lägg till en slutparentes
                if (parentheses)
                {
                    equation.Add(")");
                }

                //Skapa "Calculate"-objekt
                Calculate calc = new Calculate();

                //Calculate-objketet returnerar resultatet i en string
                string msg = calc.Kalkylera(equation);

                //Displayar resultatet i textrutan
                txtOutput.Text = msg;

                //Om resultatet inte är error:
                //Sätt baseNumber till resultatet, så att man kan fortsätta sin uträkning
                if (msg != "Error: dividera inte med 0")
                {
                    baseNumber = msg;
                }

                //Force-kallar på txtOutput_TextChanged eftersom rutan inte ändrar på sig vid t.ex. 1 * 1 eller x * 0
                txtOutput_TextChanged(sender, e);
                //Rensar equation
                equation.Clear();
                //Användaren skriver inte i en parentes
                parentheses = false;                
            }
        }

        //*******************************************************************
        //övriga funktioner
        //*******************************************************************

        //"C-knappen" (står för cancel, kanske?)
        //"Återställer" miniräknaren
        private void btnC_Click(object sender, EventArgs e)
        {
            equation.Clear();
            baseNumber = "";
            parentheses = false;
            txtOutput.Clear();
        }

        //Backspace
        //Tar bort sista indexen i equation och
        //sätter baseNumber till nya sista indexen (och tar bort DEN indexen från equation)
        private void btnBackspace_Click(object sender, EventArgs e)
        {
            //Om equation inte är tom
            if (equation.Count != 0)
            {
                //Om användaren vill ta bort en slutparentes
                if (baseNumber == ")")
                {
                    //Användaren skriver i en parentes
                    parentheses = true;
                }

                //Om användaren vill ta bort en "börjanparentes"
                if (baseNumber == "(")
                {
                    //Användaren skriver inte i en parentes
                    parentheses = false;
                }

                //Sätter baseNumber till sista stringen i equation
                baseNumber = equation.Last();
                //Tar bort sista stringen i equation
                //(Eftersom den är i baseNumber)
                //(Annars skulle den hamna två gånger i equation)
                equation.Remove(equation.Last());
            }

            //Annars ta bort baseNumber
            else
                baseNumber = "";

            txtOutput.Text = baseNumber;
        }

        //Visar användarens uträkning, eller vad man vill kalla det
        //Uppdateras när stora textrutan ändras
        private void txtOutput_TextChanged(object sender, EventArgs e)
        {
            txtHelp.Clear();

            //För varje item i equation
            foreach (string item in equation)
            {
                //Skriv ut item + space
                txtHelp.Text += $"{item} ";
            }
        }
    }
}
