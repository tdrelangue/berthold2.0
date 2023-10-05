using System.Resources;
public class Berthold
{
    private bool Solve;

    public Berthold()
    {

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
        List<decimal> coefs = new List<decimal>(); 

        do
        {
            string answer = Console.ReadLine()!;
            if (decimal.TryParse(answer, out decimal coef))
            {
                coefs.Add(coef);
            }
            else
            {
                continu = false;
            }

        }while (continu);

        if(coefs.Count() == 0 )
        {
            coefs.Add(0);
        }
    }
}