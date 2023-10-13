using System.Xml.Linq;

public abstract class Equation
{
    public List<decimal> Coefs{get; protected set;}
    protected int Multiplicator=1;
    protected List<int> Primes;
    public List<decimal> ObviousRatioDividers {get; set;}


    public Equation()
    {
        Coefs = new List<decimal>();
        Primes = new List<int>();
        ObviousRatioDividers = new List<decimal>();
    }


    public override string ToString()
    {
        string sentence = "";
        if (Coefs != null)
        {
            for(int i = 0; i < Coefs.Count()- 1; i++)
            {
                sentence += $"{Coefs[i]} * x^{Coefs.Count() - 1 - i} + ";
            }
            sentence += $"{Coefs[Coefs.Count()- 1]}";
        }
        else
        {
            sentence += "0";
        }
        return sentence;
    }


    public void AddCoef(decimal coef)
    {
        Coefs.Add(coef);
    }


protected int FindLowestWholeMultiplicator(decimal nb)
    {
        int multi = 0;
        
        do{
            multi++;
        }
        while((nb*multi)%1 != 0);

        return multi;
    }


    protected int FindLowestCommonDenominator(int current, int next)
    {
        int highest;
        if (current >= next) {highest = current;}
        else {highest = next;}
        Primes = B.FindPrimes(highest);

        // find Prime factors of Current
        int newCurrent = current;
        List<int> factorsCurrent = new List<int>();
        for(int i = 0 ; i < Primes.Count() ; i++)
        {
             bool divided;
            do{
                divided = false;
                if(newCurrent % Primes[i]==0)
                {
                    newCurrent /= Primes[i];
                    factorsCurrent.Add(Primes[i]);
                    divided = true;
                }
            }while(divided);
        }

        // find prime factors of next
        int newNext= next;
        List<int> factorsNext = new List<int>();
        for(int i = 0 ; i < Primes.Count() ; i++)
        {
            bool divided;
            do{
                divided = false;
                if(newNext % Primes[i]==0)
                {
                    newNext /= Primes[i];
                    factorsNext.Add(Primes[i]);
                    divided = true;
                }
            }while(divided);
        }
        
        int lowestCommonFactor = 1;
        // reduce to the smallest common number
        foreach(int prime in Primes)
        {
            // count the amount of each prime in current
            int countCurrent=0;
            foreach(int factor in factorsCurrent)
            {
                if(factor == prime)
                {
                    countCurrent++;
                }
            }
            
            // count the amount of each prime in current
            int countNext=0;
            foreach(int factor in factorsNext)
            {
                if(factor == prime)
                {
                    countNext++;
                }
            }

            if(countNext < countCurrent)
            {
                for(int i=0 ; i < countCurrent; i++)
                {
                    lowestCommonFactor *= prime;
                }
            }
            else
            {
                for(int i=0 ; i < countNext; i++)
                {
                    lowestCommonFactor *= prime;
                }
            }
        }


        return lowestCommonFactor;
    }

    
    public bool TestNbInt()
    {
        bool equCoefsInt = true;

        for (int i = 0; i < Coefs.Count(); i++)
        {
            if (Coefs[i] != Math.Round(Coefs[i]))
            {
                equCoefsInt = false;
            }
        }

        return equCoefsInt;
    }


    public void MakeEquInt()
    {
        for(int i=0; i< Coefs.Count();i++)
        {
            Multiplicator = FindLowestCommonDenominator(Multiplicator,FindLowestWholeMultiplicator(Coefs[i])); 
            //à optimiser par réduction au plus bas multiplicateur commun
        }

        for(int i=0; i<Coefs.Count();i++)
        {
            Coefs[i] *= Multiplicator;
        }   
    }


    private void FindDivisors(List<decimal> list, int coefIndex)
    {
        list.Add(1);
        int a = Convert.ToInt32(Coefs[coefIndex]);
        if (Math.Ceiling(Coefs[coefIndex]) == Math.Floor(Coefs[coefIndex]))
        {
            for (int i = 1; i <= Math.Abs(a); i++)
            {
                if (a % i == 0)
                {
                    list.Add(i);
                }
            }
        }
        else
        {
            list.Add(a);
        }
    }

    
    private static List<decimal> MakeObviousDivisors(List<decimal> listA0, List<decimal> listAn)
    {
        List<decimal> divisors = new List<decimal>();
        for (int i = 0; i < listA0.Count(); i++)
        {
            for (int j = 0; j < listAn.Count(); j++)
            {
                divisors.Add(listA0[i] / listAn[j]);
                divisors.Add(-listA0[i] / listAn[j]);
            }
        }
        return divisors;
    }


    public List<decimal> FindObviousRatioDivisors()
    {
        //divisors of the coefficient of smallest exponent
        List<decimal> listA0 = new List<decimal> { 1 };
        FindDivisors(listA0, Coefs.Count() - 1);

        //divisors of the coefficient of highest exponent
        List<decimal> listAn = new List<decimal>();
        FindDivisors(listAn, 0);

        //Let's now make the obvious divisors of the equation
        return MakeObviousDivisors(listA0, listAn);;
    }


    
}