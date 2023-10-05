using System.Resources;
public class Berthold
{
    private bool Solve;
    private Equation Polynome;

    public Berthold()
    {
        Polynome = new Equation();

        Present("Hello dear user ! I am Berthold, a polynomial equation solver.\nShall I solve one for you ?");
        if((Console.ReadLine()!.ToLower() == "yes"))
        {
            Solve = true;
        }
        else
        {
            Solve = false;
        }
        
        while(Solve)
        {
            CreateEquation();
            System.Console.WriteLine(Polynome);

            System.Console.WriteLine(Polynome.TestNbInt());

            Solve = false; //temporary
        }
        Present("Have a wonderful day then user and may you be successful in your mathematic venture !");
        B.Present();
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
    }
}