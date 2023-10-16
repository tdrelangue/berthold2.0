using System.ComponentModel;
public static class B 
{
    public static void Present()
    {
        Console.WriteLine("\n\n========================================================================================================================\n\n");
    }

    public static List<int> FindPrimes(int max)
    {
        List<int> primes = new List<int>();

        for(int i=2; i <= max +1;i++)
        {
            bool prime = true;
            for(int j=0; j < primes.Count();j++)
            {
                if(i%primes[j]==0)
                {
                    prime = false;
                }
            }
            if(prime)
            {
                primes.Add(i);
            }
        }

        return primes;
    }
}