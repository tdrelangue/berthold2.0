using System.Runtime.CompilerServices;
using System.Xml.Linq;

public abstract class Equation
{
    public List<decimal> Coefs;
    protected int Multiplicator=1;
    protected List<int> Primes;
    protected List<decimal> ObviousRatioDividers;
    protected List<decimal> RatioSolutions;



    public Equation()
    {
        Coefs = new List<decimal>();
        Primes = new List<int>();
        ObviousRatioDividers = new List<decimal>();
        RatioSolutions = new List<decimal>();
    }


    public override string ToString()
    {
        string sentence = "";
        bool needParanthesis = false;

        // if there is no equation left un factorized, we need to multiply by an
        if (Coefs.Count() == 1)
        {
            sentence += $"{Coefs[0]}";
        }

        // factorize
        if (RatioSolutions.Count() != 0)
        {
            needParanthesis = true;
            foreach(decimal solution in RatioSolutions)
            {
                
                if (solution<0){
                    sentence += $"(x+{-solution})";
                }
                else{
                    sentence += $"(x-{solution})";
                }
                
            }
        }

        
        
        // Right what is left without factorisation if need be 
        if (Coefs.Count()>1)
        {
            if (needParanthesis)
            {
                sentence += "(";
            }       
            for(int i = 0; i < Coefs.Count()- 1; i++)
            {
                sentence += $"{Coefs[i]} * x^{Coefs.Count() - 1 - i} + ";
            }
            sentence += $"{Coefs[Coefs.Count()- 1]}";
            if (needParanthesis)
            {
                sentence += ")";
            }
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


    private void FindIntDivisors(List<decimal> list, int coefIndex)
    {
        list.Add(1);
        int a = Convert.ToInt32(Coefs[coefIndex]);
        if (Math.Ceiling(Coefs[coefIndex]) == Math.Floor(Coefs[coefIndex]))
        {
            for (int i = 2; i <= Math.Abs(a); i++)
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

    
    private List<decimal> MakeObviousRatioDivisors(List<decimal> listA0, List<decimal> listAn)
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


    private void FindObviousRatioDivisors()
    {
        //divisors of the coefficient of smallest exponent
        List<decimal> listA0 = new List<decimal>();
        FindIntDivisors(listA0, Coefs.Count() - 1);

        //divisors of the coefficient of highest exponent
        List<decimal> listAn = new List<decimal>();
        FindIntDivisors(listAn, 0);

        //Let's now make the obvious divisors of the equation
        ObviousRatioDividers = MakeObviousRatioDivisors(listA0, listAn);;
    }


    public void FindObviousDivisors()
    {
        FindObviousRatioDivisors();
    }


    // make it so that I dont have to change it when I add complex
    public List<decimal> MakeSyntheticDivision( decimal divisor)
    {
        //First we bring down the coefficient with the highest exponent
        List<decimal> syntheticDivision= new List<decimal>() {Coefs[0]};
        
        //Then we do last number down * divisor + coef
        for(int k = 1; k < Coefs.Count(); k++)
        {

            syntheticDivision.Add(syntheticDivision[syntheticDivision.Count()-1] * divisor + Coefs[k]);
        }

        return syntheticDivision;
    }


    private void FindRatioDivisors()
    {        
        FindObviousRatioDivisors();

        //we do the sythetic division for each obvious divider
        List<decimal> syntheticDivision;
        foreach(decimal obviousDivider in ObviousRatioDividers)
        {
            syntheticDivision = MakeSyntheticDivision(obviousDivider);
            if(syntheticDivision[syntheticDivision.Count() - 1] == 0)
            {
                RatioSolutions.Add(obviousDivider);
                Coefs = new List<decimal>();
                for(int i = 0; i < syntheticDivision.Count()-1;i++)
                {
                    Coefs.Add(syntheticDivision[i]);
                }
            }
        }
    }


    public void FindDivisors()
    {
        FindRatioDivisors();
    }

}