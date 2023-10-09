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


    public List<decimal> FindObviousRatioDivisors()
    {
        //divisors of the coefficient of smallest exponent
        List<decimal> listA0 = new List<decimal>();
        int max = Coefs.Count()-1;
        int a0 = Convert.ToInt32(Coefs[max]);
        if(Math.Ceiling(Coefs[max]) == Math.Floor(Coefs[max]))
        {
            for(int i = 1 ; i <= Math.Abs(a0); i++)
            {
                if(a0 % i == 0)
                {
                    listA0.Add(i);
                }
            }
        }
        else
        {
            listA0.Add(a0);
            listA0.Add(1);
        }

        //divisors of the coefficient of highest exponent
        List<decimal> listAn = new List<decimal>();
        int an = Convert.ToInt32(Coefs[0]);
        if(Math.Ceiling(Coefs[0]) == Math.Floor(Coefs[0]))
        {
            for(int i = 1 ; i <= an; i++)
            {
                if(an % i == 0)
                {
                    listAn.Add(i);
                }
            }
        }
        else
        {
            listAn.Add(an);
            listAn.Add(1);
        }

        //Let's now make the obvious divisors of the equation
        List<decimal> divisors = new List<decimal>();
        for (int i = 0; i < listA0.Count(); i++)
        {
            for(int j = 0; j < listAn.Count(); j++)
            {
                divisors.Add(listA0[i] / listAn[j]);
                divisors.Add( - listA0[i] / listAn[j]);
            }
        }
        return divisors;
    }
}