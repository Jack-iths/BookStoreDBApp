using BookStoreDbFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreDbFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool kör = true;

            while (kör)
            {
                Console.Clear();
                Console.WriteLine("=== BOKHANDELNS LAGERSYSTEM ===");
                Console.WriteLine();
                Console.WriteLine("1. Visa lagersaldo för en butik");
                Console.WriteLine("2. Lägg till bok i butik");
                Console.WriteLine("3. Ta bort bok från butik");
                Console.WriteLine("4. Ändra antal böcker");
                Console.WriteLine("5. Visa alla böcker");
                Console.WriteLine("0. Avsluta");
                Console.WriteLine();
                Console.Write("Välj: ");

                string val = Console.ReadLine();

                if (val == "1")
                {
                    VisaLagersaldo();
                }
                else if (val == "2")
                {
                    LäggTillBok();
                }
                else if (val == "3")
                {
                    TaBortBok();
                }
                else if (val == "4")
                {
                    ÄndraAntal();
                }
                else if (val == "5")
                {
                    VisaAllaBöcker();
                }
                else if (val == "0")
                {
                    kör = false;
                    Console.WriteLine("Hejdå!");
                }
                else
                {
                    Console.WriteLine("Ogiltigt val!");
                    Console.ReadKey();
                }
            }
        }

        static void VisaLagersaldo()
        {
            using (var context = new CompaniesContext())
            {
                Console.Clear();
                Console.WriteLine("=== VISA LAGERSALDO ===");
                Console.WriteLine();

                // Hämta alla butiker
                var butiker = context.Butikers.ToList();

                // Visa butikerna
                Console.WriteLine("Välj butik:");
                for (int i = 0; i < butiker.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + butiker[i].Namn);
                }

                Console.Write("Ange nummer: ");
                int butikNummer = int.Parse(Console.ReadLine());

                if (butikNummer < 1 || butikNummer > butiker.Count)
                {
                    Console.WriteLine("Ogiltigt val!");
                    Console.ReadKey();
                    return;
                }

                var valdButik = butiker[butikNummer - 1];

                // Hämta lagersaldo för butiken
                var lagerSaldo = context.LagerSaldos
                    .Include(l => l.Bok)
                    .Where(l => l.ButikId == valdButik.ButikId)
                    .ToList();

                Console.WriteLine();
                Console.WriteLine("Lagersaldo för " + valdButik.Namn + ":");
                Console.WriteLine("----------------------------------------");

                if (lagerSaldo.Count == 0)
                {
                    Console.WriteLine("Inga böcker i lager.");
                }
                else
                {
                    foreach (var item in lagerSaldo)
                    {
                        Console.WriteLine(item.Bok.Titel + " - Antal: " + item.Antal);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Tryck på en tangent...");
                Console.ReadKey();
            }
        }

        static void LäggTillBok()
        {
            using (var context = new CompaniesContext())
            {
                Console.Clear();
                Console.WriteLine("=== LÄGG TILL BOK I BUTIK ===");
                Console.WriteLine();

                // Välj butik
                var butiker = context.Butikers.ToList();

                Console.WriteLine("Välj butik:");
                for (int i = 0; i < butiker.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + butiker[i].Namn);
                }

                Console.Write("Ange nummer: ");
                int butikNummer = int.Parse(Console.ReadLine());

                if (butikNummer < 1 || butikNummer > butiker.Count)
                {
                    Console.WriteLine("Ogiltigt val!");
                    Console.ReadKey();
                    return;
                }

                var valdButik = butiker[butikNummer - 1];

                // Visa alla böcker
                var böcker = context.Böckers.ToList();

                Console.WriteLine();
                Console.WriteLine("Välj bok:");
                for (int i = 0; i < böcker.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + böcker[i].Titel + " (ISBN: " + böcker[i].Isbn13 + ")");
                }

                Console.Write("Ange nummer: ");
                int bokNummer = int.Parse(Console.ReadLine());

                if (bokNummer < 1 || bokNummer > böcker.Count)
                {
                    Console.WriteLine("Ogiltigt val!");
                    Console.ReadKey();
                    return;
                }

                var valdBok = böcker[bokNummer - 1];

                Console.Write("Ange antal: ");
                int antal = int.Parse(Console.ReadLine());

                // Skapa ny lagersaldo-post
                var nyLagerSaldo = new LagerSaldo();
                nyLagerSaldo.ButikId = valdButik.ButikId;
                nyLagerSaldo.Isbn13 = valdBok.Isbn13;
                nyLagerSaldo.Antal = antal;

                context.LagerSaldos.Add(nyLagerSaldo);
                context.SaveChanges();

                Console.WriteLine();
                Console.WriteLine("Boken har lagts till i butiken!");
                Console.ReadKey();
            }
        }

        static void TaBortBok()
        {
            using (var context = new CompaniesContext())
            {
                Console.Clear();
                Console.WriteLine("=== TA BORT BOK FRÅN BUTIK ===");
                Console.WriteLine();

                // Välj butik
                var butiker = context.Butikers.ToList();

                Console.WriteLine("Välj butik:");
                for (int i = 0; i < butiker.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + butiker[i].Namn);
                }

                Console.Write("Ange nummer: ");
                int butikNummer = int.Parse(Console.ReadLine());

                if (butikNummer < 1 || butikNummer > butiker.Count)
                {
                    Console.WriteLine("Ogiltigt val!");
                    Console.ReadKey();
                    return;
                }

                var valdButik = butiker[butikNummer - 1];

                // Visa böcker i butiken
                var lagerSaldo = context.LagerSaldos
                    .Include(l => l.Bok)
                    .Where(l => l.ButikId == valdButik.ButikId)
                    .ToList();

                if (lagerSaldo.Count == 0)
                {
                    Console.WriteLine("Inga böcker i butiken.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("Böcker i " + valdButik.Namn + ":");
                for (int i = 0; i < lagerSaldo.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + lagerSaldo[i].Bok.Titel + " - Antal: " + lagerSaldo[i].Antal);
                }

                Console.Write("Vilken bok vill du ta bort? Ange nummer: ");
                int bokNummer = int.Parse(Console.ReadLine());

                if (bokNummer < 1 || bokNummer > lagerSaldo.Count)
                {
                    Console.WriteLine("Ogiltigt val!");
                    Console.ReadKey();
                    return;
                }

                var valdLagerSaldo = lagerSaldo[bokNummer - 1];

                Console.Write("Är du säker? (j/n): ");
                string svar = Console.ReadLine();

                if (svar == "j")
                {
                    context.LagerSaldos.Remove(valdLagerSaldo);
                    context.SaveChanges();
                    Console.WriteLine("Boken har tagits bort!");
                }
                else
                {
                    Console.WriteLine("Avbrutet.");
                }

                Console.ReadKey();
            }
        }

        static void ÄndraAntal()
        {
            using (var context = new CompaniesContext())
            {
                Console.Clear();
                Console.WriteLine("=== ÄNDRA ANTAL BÖCKER ===");
                Console.WriteLine();

                // Välj butik
                var butiker = context.Butikers.ToList();

                Console.WriteLine("Välj butik:");
                for (int i = 0; i < butiker.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + butiker[i].Namn);
                }

                Console.Write("Ange nummer: ");
                int butikNummer = int.Parse(Console.ReadLine());

                if (butikNummer < 1 || butikNummer > butiker.Count)
                {
                    Console.WriteLine("Ogiltigt val!");
                    Console.ReadKey();
                    return;
                }

                var valdButik = butiker[butikNummer - 1];

                // Visa böcker i butiken
                var lagerSaldo = context.LagerSaldos
                    .Include(l => l.Bok)
                    .Where(l => l.ButikId == valdButik.ButikId)
                    .ToList();

                if (lagerSaldo.Count == 0)
                {
                    Console.WriteLine("Inga böcker i butiken.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("Böcker i " + valdButik.Namn + ":");
                for (int i = 0; i < lagerSaldo.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + lagerSaldo[i].Bok.Titel + " - Antal: " + lagerSaldo[i].Antal);
                }

                Console.Write("Vilken bok vill du ändra? Ange nummer: ");
                int bokNummer = int.Parse(Console.ReadLine());

                if (bokNummer < 1 || bokNummer > lagerSaldo.Count)
                {
                    Console.WriteLine("Ogiltigt val!");
                    Console.ReadKey();
                    return;
                }

                var valdLagerSaldo = lagerSaldo[bokNummer - 1];

                Console.Write("Ange nytt antal: ");
                int nyttAntal = int.Parse(Console.ReadLine());

                valdLagerSaldo.Antal = nyttAntal;
                context.SaveChanges();

                Console.WriteLine("Antalet har uppdaterats!");
                Console.ReadKey();
            }
        }

        static void VisaAllaBöcker()
        {
            using (var context = new CompaniesContext())
            {
                Console.Clear();
                Console.WriteLine("=== ALLA BÖCKER ===");
                Console.WriteLine();

                var böcker = context.Böckers
                    .Include(b => b.Författar)
                    .ToList();

                foreach (var bok in böcker)
                {
                    Console.WriteLine("Titel: " + bok.Titel);
                    Console.WriteLine("ISBN: " + bok.Isbn13);

                    if (bok.Författar != null)
                    {
                        Console.WriteLine("Författare: " + bok.Författar.Förnamn + " " + bok.Författar.Efternamn);
                    }
                    else
                    {
                        Console.WriteLine("Författare: Okänd");
                    }

                    Console.WriteLine("----------------------------------------");
                }

                Console.WriteLine();
                Console.WriteLine("Totalt: " + böcker.Count + " böcker");
                Console.WriteLine();
                Console.WriteLine("Tryck på en tangent...");
                Console.ReadKey();
            }
        }
    }
}
