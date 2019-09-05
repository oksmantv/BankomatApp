using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKS_BankApp
{
    class Program
    {

        public static String RandomKonto()
        {
            String KontoText;
            Random random = new Random();
            int KontoNumber = random.Next(10000, 90000);
            while (File.Exists(@"C:\Users\Public\CSHARP\" + KontoNumber + ".txt")) { KontoNumber = random.Next(10000, 90000); break; }
            KontoText = KontoNumber.ToString();
            Console.WriteLine("Ditt konto nummer är " + KontoNumber);
            return KontoText;

        }
        public static String RandomKod()
        {
            Random random = new Random();
            String KodText;
            int NumberKod = random.Next(1000, 4000);
            KodText = NumberKod.ToString();
            Console.WriteLine("Din konto kod är " + NumberKod);
            return KodText;
        }

        static void Main(string[] args)
        {

            Konto TempKonto;
            bool bankApp = true;
            bool startApp;
            bool menu;
            String KontoText = "";
            String KodText = "";
            decimal finalBelopp;
            char sepdec = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            while (bankApp)
            {
                startApp = true;
                TempKonto = new Konto();

                while (startApp)
                {
                    
                Console.WriteLine("Vill du registrera ett nytt konto? (J/N) - Tryck endast Enter för att avsluta.");
                String Svar = Console.ReadLine().ToLower();

                if (Svar.Equals("j"))
                {
                    KontoText = RandomKonto();
                    KodText = RandomKod();
                        //
                        TempKonto.skapaKonto(KontoText, KodText, 0);
                        break;
                }
                else if (Svar.Equals("n"))
                {
                    break;
                }
                else if (Svar == "") { return; }
                else Console.WriteLine("Fel typ av inmatning, försök igen");


            }
                
                

                String Konto = TempKonto.LoggaInKonto();
                if (Konto == "avbryt") continue;
                String Kod = TempKonto.LoggaInKod(Konto);
                if (Kod == "avbryt") continue;
                Console.WriteLine("\nDu är nu inloggad.");
                TempKonto.PrintBankInfo(Konto);
                menu = true;


                while (menu)
                {
                    TempKonto.MenyVal();
                    String UserInput = Console.ReadLine();
                    switch (UserInput)
                    {
                        case "1":
                            {
                                Console.WriteLine("Du har valt insättning. Ange det belopp du vill föra in.");
                                while (!decimal.TryParse(Console.ReadLine().Replace(",", sepdec.ToString()).Replace(".", sepdec.ToString()), out finalBelopp)) { Console.WriteLine("Fel typ av inmatning, försök igen"); }
                                finalBelopp = Convert.ToDecimal(finalBelopp, new CultureInfo("en-US"));
                                if (finalBelopp == 0) { Console.WriteLine("Du kan inte föra in 0 kronor."); break; }
                                TempKonto.Insattning(Konto, finalBelopp);
                                break;

                            }

                        case "2":
                            {
                                Console.WriteLine("Du har valt uttagning. Ange det belopp du vill ta ut.");
                                while (!decimal.TryParse(Console.ReadLine().Replace(",", sepdec.ToString()).Replace(".", sepdec.ToString()), out finalBelopp)) { Console.WriteLine("Fel typ av inmatning, försök igen"); }
                                if (finalBelopp == 0) { Console.WriteLine("Du kan inte ta ut 0 kronor."); break; }
                                TempKonto.Uttag(Konto, finalBelopp);
                                break;
                            };

                        case "3":
                            {
                                startApp = false;
                                menu = false;
                                break;
                            };

                        case "4":
                            {
                                return;
                            };

                        default:
                            {
                                Console.WriteLine("Fel typ av inmatning, försök igen");
                                break;
                            }

                    }

                }
            }

            Console.ReadLine();
        }
    }
}
