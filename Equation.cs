public class Equation
{
    public List<decimal> Coefs{get; private set;}   


    public Equation()
    {
        Coefs = new List<decimal>();
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


    private List<decimal> FindObviousDivisors()
    {
        //divisors of the coefficient of smallest exponent
        List<decimal> listA0 = new List<decimal>();
        int max = Coefs.Count()-1;
        int a0 = Convert.ToInt32(Coefs[max]);
        if(Math.Ceiling(Coefs[max]) == Math.Floor(Coefs[max]))
        {
            for(int i = 1 ; i <= a0; i++)
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


    public List<decimal> FindDivisors()
    {
        List<decimal> solutions = new List<decimal>();
        
        List<decimal> obviousDividers = FindObviousDivisors();

        //we do the sythetic division for each obvious divider
        List<decimal> syntheticDivision;
        for(int i = 0; i < obviousDividers.Count(); i++ )
        {
            syntheticDivision = SyntheticDivision(obviousDividers[i]);
            if(syntheticDivision[syntheticDivision.Count() - 1] == 0)
            {
                solutions.Add(obviousDividers[i]);
            }
        }
        return solutions;
    }



    public List<decimal> SyntheticDivision( decimal divisor)
    {
        List<decimal> syntheticDivision;
        syntheticDivision = new List<decimal>();
        //Let's go with sythetic division
        //First we bring down the coefficient with the highest exponent
        syntheticDivision.Add(Coefs[0]);
        //Then we do last number down * divisor + coef
        for(int k = 1; k < Coefs.Count(); k++)
        {
            syntheticDivision.Add(syntheticDivision[syntheticDivision.Count()-1] * divisor + Coefs[k]);
        }

        return syntheticDivision;
    }
    

    public Equation Duplicate()
    {
        Equation eq = new Equation();
        foreach(decimal i in Coefs)
        {
            eq.AddCoef(i);
        }
        return eq;
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
}