using System.Resources;
public class Berthold
{
    private bool Solve;
    private EquationManip Polynome;

    public Berthold()
    {
        Polynome = new EquationManip();

        Present("Hello dear user ! I am Berthold, a polynomial equation solver.\nLets start solving");
        // if(Console.ReadLine()!.ToLower() == "yes") { Solve = true; }
        // else { Solve = false; }
        
        do {
            Solve = false;

            CreateEquation();
            Present("Here is your polynome");
            System.Console.WriteLine(Polynome);

            Polynome.MakeEquInt();


            Present("Here is your an equivalent with all the numbers as integers");
            System.Console.WriteLine(Polynome);
            
            Present("We will start by finding for rational solutions");
            SolveRatio();
            System.Console.WriteLine(Polynome);

            if (Polynome.Coefs.Count()>1)
                Present("I am not yet capable to find any other solution, but I know there are. Wait for future updates!");

            Present("Shall we solve another one?");
            if(Console.ReadLine()!.ToLower() == "yes") { Solve = true; }
        }while(Solve);

        Present("Have a wonderful day then user, and may you be successful in your mathematic venture !");
    }


    

    private void Present(string sentence)
    {
        B.Present();
        Console.WriteLine(sentence);
    }


    private void CreateEquation()
    {
        Present("We will now define the Coefficients ! ");
        Console.WriteLine("Type the coefficients from the one with the highest exponent to the one with the lowest.");
        Console.WriteLine("When you are done enter anything other than a decimal number");
        bool continu = true;

        do
        {
            string answer = Console.ReadLine()!;
            if (decimal.TryParse(answer, out decimal coef))
            {
                Polynome.Coefs.Add(coef);
            }
            else
            {
                continu = false;
            }

        }while (continu);

        if(Polynome.Coefs.Count() == 0 )
        {
            Polynome.Coefs.Add(0);
        }

        Polynome.SaveEqu();
    }

    private void SolveRatio()
    {
        Polynome.FindDivisors();
    }
}