using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKS_BankApp
{
    class Konto
    {
        public void skapaKonto(string kontoNummer,string kod, decimal saldo)
        {
            String AccountInfo = kontoNummer + ',' + kod + ',' + saldo;
            System.IO.File.WriteAllText(@"C:\Users\Public\CSHARP\" + kontoNummer + ".txt", AccountInfo);
        }
        public String LoggaInKonto()
        {
            Console.WriteLine("\nVar god logga in! Avbryt genom att endast trycka Enter.");
            Console.Write("Konto Nummer: ");
            string tempKonto = Console.ReadLine();

            char[] Splitter = { ',' };
            String KontoText;

            if (tempKonto == "") return "avbryt";

           while(!File.Exists(@"C:\Users\Public\CSHARP\" + tempKonto + ".txt"))
            {


                Console.WriteLine("Du har anget fel kontonummer. Avbryt genom att endast trycka Enter.");
                tempKonto = Console.ReadLine();
                if (tempKonto == "") return "avbryt";


            }

            KontoText = System.IO.File.ReadAllText(@"C:\Users\Public\CSHARP\" + tempKonto + ".txt");
            String[] AccountInfo = KontoText.Split(Splitter, 3, StringSplitOptions.RemoveEmptyEntries);

            while (tempKonto != AccountInfo[0]) { Console.WriteLine("Kan inte hitta ditt kontonummer, kontrollera din inmatning."); tempKonto = Console.ReadLine(); };

            return AccountInfo[0];
        }
        public String LoggaInKod(String Konto)
        {

            char[] Splitter = { ',' };
            Konto = System.IO.File.ReadAllText(@"C:\Users\Public\CSHARP\" + Konto + ".txt");
            String[] AccountInfo = Konto.Split(Splitter, 3, StringSplitOptions.RemoveEmptyEntries);
            Console.Write("Konto Kod: ");

            string tempKod = Console.ReadLine();
            if (tempKod == "") return "avbryt";

            while (tempKod != AccountInfo[1])

            {
                Console.WriteLine("Du har angett fel kod. Avbryt genom att endast trycka Enter.");
                tempKod = Console.ReadLine();
                if (tempKod == "") return "avbryt";
            };

            return AccountInfo[1];
        }


        public String HämtaBankInfo(string kontoNummer)
        {
            String kontoText;
            kontoText = System.IO.File.ReadAllText(@"C:\Users\Public\CSHARP\" + kontoNummer + ".txt");
            return kontoText;
        }

        public void PrintBankInfo(string KontoNummer)
        {
            char[] Splitter = { ',' };
            var AccountText = System.IO.File.ReadAllText(@"C:\Users\Public\CSHARP\" + KontoNummer + ".txt");
            var AccountInfo = AccountText.Split(Splitter, 3, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("Konto Nummer: "+AccountInfo[0]);
            Console.WriteLine("Kod: " + AccountInfo[1]);
            Console.WriteLine("Saldo: " + AccountInfo[2]);

        }

        public void MenyVal()
        {

            Console.WriteLine("\nVad vill du göra?");
            Console.WriteLine("1. Insättning av Pengar");
            Console.WriteLine("2. Uttag av Pengar");
            Console.WriteLine("3. Logga ut");
            Console.WriteLine("4. Avsluta");

        }

        public void Insattning (String Konto,decimal belopp)
        {

            if (belopp < 0) { Console.WriteLine("Fel typ av inmatning. Avbryter."); return; }
            char[] Splitter = { ',' };
            var TempKonto = System.IO.File.ReadAllText(@"C:\Users\Public\CSHARP\" + Konto + ".txt");
            File.Delete(@"C:\Users\Public\CSHARP\" + Konto + ".txt");

            String[] AccountInfo = TempKonto.Split(Splitter, 3, StringSplitOptions.RemoveEmptyEntries);
            var Saldo = decimal.Parse(AccountInfo[2]);
            decimal newSaldo = Saldo + belopp;
            AccountInfo[2] = newSaldo.ToString();
            

            String NewInfo = AccountInfo[0] + ',' + AccountInfo[1] + ',' + AccountInfo[2];


            System.IO.File.WriteAllText(@"C:\Users\Public\CSHARP\" + AccountInfo[0] + ".txt", NewInfo);
            Console.WriteLine("Du har satt in {0} kr på ditt konto.\nNuvarande Saldo: {1}", belopp, newSaldo);
                return;
        }

        public void Uttag(String Konto, decimal belopp)
        {

            while (true)
            {

            char[] Splitter = { ',' };
            var TempKonto = System.IO.File.ReadAllText(@"C:\Users\Public\CSHARP\" + Konto + ".txt");
            String[] AccountInfo = TempKonto.Split(Splitter, 3, StringSplitOptions.RemoveEmptyEntries);

            var Saldo = decimal.Parse(AccountInfo[2]);
                if (belopp < 0) { Console.WriteLine("Fel typ av inmatning. Avbryter."); return; }

                if (Saldo > belopp)
                {

                    Saldo -= belopp;
                    String newSaldo = Saldo.ToString();

                    AccountInfo[2] = newSaldo;
                    String NewInfo = AccountInfo[0] + ',' + AccountInfo[1] + ',' + AccountInfo[2];
                    var Placement = @"C:\Users\Public\CSHARP\" + Konto + ".txt";
                    File.Delete(Placement);
                    System.IO.File.WriteAllText(@"C:\Users\Public\CSHARP\" + AccountInfo[0] + ".txt", NewInfo);

                    Console.WriteLine("Du har tagit ut {0} kr. Ditt nuvarande saldo {1}", belopp, newSaldo);
                    return;
                }

                else
                {
                    Console.WriteLine("Du har inte tillräckligt med Saldo. Ditt saldo: {0}", Saldo);
                    return;

                }
                

            }

        }



    }
}
