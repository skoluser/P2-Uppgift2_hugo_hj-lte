using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc
{
    class Calculate
    {
        //Konstruktor
        public Calculate()
        {
        }        

        //Försöker räkna ut vad användaren har skrivit
        public string Kalkylera(List<string> equation)
        {
            //Om equation innehåller en parentes
            if (equation.Contains("("))
            {
                KalkyleraParentes(equation);
            }

            //Om equation innehåller ett divisionstecken
            if (equation.Contains("/"))
            {
                do //Gör detta så länge equation innehåller ett divisionstecken
                {
                    //Nämnaren är item med index 1 större än divisionstecknet
                    float nämnare = float.Parse(equation.ElementAt(equation.IndexOf("/") + 1));

                    //Om nämnaren inte är 0
                    if (nämnare != 0)
                    {
                        //Täljaren är item med index 1 mindre än divisionstecknet
                        float täljare = float.Parse(equation.ElementAt(equation.IndexOf("/") - 1));

                        //Ta bort täljare och nämnare från equation
                        equation.RemoveAt(equation.IndexOf("/") + 1);
                        equation.RemoveAt(equation.IndexOf("/") - 1);

                        //Skriv över divisionstecknet med kvoten av täljare och nämnare (i string)
                        equation.Insert(equation.IndexOf("/"), (täljare / nämnare).ToString());

                        //Ta bort operatorn
                        equation.Remove("/");
                    }

                    //Om nämnaren är 0 returnerar vi error
                    else
                    {
                        return "Error: dividera inte med 0";
                    }

                } while (equation.Contains("/")); //Gör ovanstående så länge equation innehåller divisionstecken
            }

            //Om equation innehåller ett multiplikationstecken
            if (equation.Contains("*"))
            {
                do //Gör detta så länge equation innehåller multiplikationstecken
                {
                    //Faktor1 & faktor2 är item med index 1 större resp. mindre än multiplikationstecknet
                    float faktor1 = float.Parse(equation.ElementAt(equation.IndexOf("*") - 1));
                    float faktor2 = float.Parse(equation.ElementAt(equation.IndexOf("*") + 1));

                    //Ta bort faktor 1 & 2 från equation
                    equation.RemoveAt(equation.IndexOf("*") - 1);
                    equation.RemoveAt(equation.IndexOf("*") + 1);

                    //Skriv över multiplikationstecknet med produkten av faktor 1 & 2 (i string)
                    equation.Insert(equation.IndexOf("*"), (faktor1 * faktor2).ToString());

                    //Ta bort operatorn
                    equation.Remove("*");

                } while (equation.Contains("*")); //Gör ovanstående så länge equation innehåller multiplikationstecken
            }

            //Om equation innehåller ett minustecken
            if (equation.Contains("-"))
            {
                do //Gör detta så länge equation innehåller minustecken
                {
                    //Term1 & term2 är item med index 1 större resp. mindre än minustecknet
                    float term1 = float.Parse(equation.ElementAt(equation.IndexOf("-") - 1));
                    float term2 = float.Parse(equation.ElementAt(equation.IndexOf("-") + 1));
                    
                    //Ta bort term 1 & 2 från equation
                    equation.RemoveAt(equation.IndexOf("-") - 1);
                    equation.RemoveAt(equation.IndexOf("-") + 1);

                    //Skriv över minustecknet med differensen av term 1 & 2 (i string)
                    equation.Insert(equation.IndexOf("-"), (term1 - term2).ToString());

                    //Ta bort operatorn
                    equation.Remove("-");

                } while (equation.Contains("-")); //Gör ovanstående så länge equation innehåller minustecken
            }

            //Om equation innehåller ett plusstecken
            if (equation.Contains("+"))
            {
                do //Gör detta så länge equation innehåller plustecken
                {
                    //Faktor1 & faktor2 är item med index 1 större resp. mindre än plustecknet
                    float term1 = float.Parse(equation.ElementAt(equation.IndexOf("+") - 1));
                    float term2 = float.Parse(equation.ElementAt(equation.IndexOf("+") + 1));

                    //Ta bort term 1 & 2 från equation
                    equation.RemoveAt(equation.IndexOf("+") - 1);
                    equation.RemoveAt(equation.IndexOf("+") + 1);

                    //Skriv över plustecknet med summan av term 1 & 2 (i string)
                    equation.Insert(equation.IndexOf("+"), (term1 + term2).ToString());

                    //Ta bort operatorn
                    equation.Remove("+");                    

                } while (equation.Contains("+")); //Gör ovanstående så länge equation innehåller plustecken
            }

            //Denna ska vi returnera
            float result = 0;
            //För varje summa/differens/produkt/kvot i equation
            foreach (string item in equation)
            {
                //Lägg till >>summan<< i result
                result += float.Parse(item);
            }

            //Returnera vårt slutgiltiga resultat i en sträng
            return result.ToString();
        }

        public void KalkyleraParentes(List<string> equation)
        {
            //"Kopiera" equation till en ny list som kallas "peq"
            List<string> peq = new List<string>(equation);

            //Tar bort allt upp till och med första vänsterparentesen(?)
            //Tar bort allt från och med första slutparentesen
            peq.RemoveRange(0, (peq.IndexOf("(") + 1));
            int thisBig = peq.Count();
            thisBig -= peq.IndexOf(")");
            peq.RemoveRange(peq.IndexOf(")"), thisBig);

            //Kallar på sig själv, skickar med peq
            string result = Kalkylera(peq);

            //Om parentesen innehöll en division med 0, kalla på Kalkylera och
            //skicka med parentesen igen eftersom denna metod inte returnerar något
            if (result == "Error: dividera inte med 0")
            {
                Kalkylera(peq);
            }

            //Annars, ta bort parenteserna och dess innehåll från equation
            //Ersätt det med resultatet
            //Kalla sedan på Kalkylera() igen, 
            //skicka med nya equation som har ett par parenteser mindre 
            else
            {
                equation.Insert(equation.IndexOf("("), result);
                int range = equation.IndexOf(")") - equation.IndexOf("(") + 1;
                equation.RemoveRange(equation.IndexOf("("), range);
                Kalkylera(equation);
            }
        }

    }//Klass
}
