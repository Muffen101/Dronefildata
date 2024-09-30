namespace Bente_s_drone_fil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = //Stien til CS-filen, som skal læses (Inputfil)
      @"C:\Users\Steffan\OneDrive - Grindsted Gymnasie- & Erhvervsskole\3.g HTX\Programmering\04. Manupulation af tekststrenge c#\flyvning1.csv";
            StreamReader reader = null;

            string OutputFile = //Stien til outputfilen, som skal skrives til
                @"C:\Users\Steffan\OneDrive - Grindsted Gymnasie- & Erhvervsskole\3.g HTX\Programmering\04. Manupulation af tekststrenge c#\rettetdata.csv";
            StreamWriter writer = null;
            if (File.Exists(filePath)) //Tjekker om inputfilen faktisk eksisterer
            {
                reader = new StreamReader(File.OpenRead(filePath)); // åbner CSV-filen så den læses
                writer = new StreamWriter(OutputFile); // opretter en fil til at skrive outputdata
                List<string> listA = new List<string>(); // opretter en liste som gemmer data fra filen
                int index = 0; // variabel der bruges til at gemme index for hvor punktum findes i teksten
                int linecounter = 0; // for at holde styr på antal linjer
                while (!reader.EndOfStream) // læser linje for linje
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';'); // linjen splittes ved hver semikolon for at få individuelle værdier

                    for (int i = 0; i < values.Length; i++) 
                    {
                        if (linecounter != 0) //hvis det ikke er første linje så;
                        {
                             
                            if (i == 2 || i == 3) // hvis det er kolonne 2 eller 3 så fjernes punktummerne
                            {
                                index = values[i].IndexOf("."); // finder første forekomst af "." i strengen
                                while (index != -1) // fjerner alle punktummer i strengen
                                {
                                    values[i] = values[i].Remove(index, 1); 
                                    index = values[i].IndexOf(".");
                                
                                }
                                if (i == 2)
                                {
                                    values[i] = values[i].Insert(1, ".");
                                }
                                if (i == 3)
                                {
                                    values[i] = values[i].Insert(2, ".");
                                }
                        
                            }
                        }
                        listA.Add(values[i]); // tilføjer den ændrede værdi til listen
                        Console.WriteLine(values[i]); // udskriver den ændrede værdi til konsollen
                    }
                    if(linecounter > 0) // hvis det ikke er første linje, bliver gps-koordinaterne sat sammen (Values[2] og values[3])
                    {
                        string ExtraValue = values[2] + "," + values[3];
                        listA.Add(ExtraValue);
                    }
                    else
                    {
                        listA.Add("GPS-Koord"); // ændre 'values[2]' og 'values[3]' som er slået sammen, til 'GPS-Kord'
                    }

                    string OutputLine = string.Join(";", listA.ToArray()); // slutter linjerne sammen i et enkelt output med semikolon imellem
                    writer.WriteLine(OutputLine); // skriver den sammensatte linje til outputfilen
                    listA.Clear(); // rydder listen
                    linecounter++; // øger linjetælleren
                }

                reader.Close(); // lukker læseren
                writer.Close(); // lukker skriveren

            }
            else
            {
                Console.WriteLine("File doesn't exist"); // hvis inputfilen ikke findes, udskrives en fejlmeddelelse til konsollen
            }
            Console.ReadLine(); // Venter på at brugeren trykker på en tast før programmet lukker
        }
    }
}