using System.ComponentModel;
using System.Data.SqlTypes;
using System.Collections.Generic;
public class Solve
{
    public Equation Equation{get; private set;}
    private Equation BaseEquation;
    private List<decimal> Solutions;
    private List<string> SolutionsComp;
    private List<string> SolutionsReal;
    private int simplifications=-1;
    private List<int> Prime;

    public Solve()
    {
        Equation = new Equation();
        Solutions = new List<decimal>();
        BaseEquation = new Equation();
        SolutionsComp = new List<string>();
        SolutionsReal = new List<string>();
        Prime = new List<int>();
    }


    public void CreateEquation(decimal[] tab)
    {
        Equation = new Equation();
        for (int i = 0; i < tab.Length; i++)
        {
            Equation.AddCoef(tab[i]);
        }

        simplifications	+= 1;

        if (simplifications == 0)
        {
            BaseEquation = Equation.Duplicate();
        }
    }


    public void CreateEquation(List<decimal> tab)
    {
        Equation = new Equation();
        for (int i = 0; i < tab.Count(); i++)
        {
            Equation.AddCoef(tab[i]);
        }

        simplifications	+= 1;

        if (simplifications == 0)
        {
            BaseEquation = Equation.Duplicate();
        }
    }


    public override string ToString()
    {
        string sentence = "";
        if(!IsSolved())
        {
            sentence += "The current form of th equation is : ";
            sentence += Equation.ToString();
            if (Solutions.Count()>=1)
            {
                sentence += $"\nThis equattion is by factoring {BaseEquation} by : \n";
                for(int i = 0; i < Solutions.Count(); i++)
                {
                    sentence += $"(x - {Solutions[i]}) ";
                }
            }
        }
        else
        {
            sentence += $"The equation is Solved. Here are the solutions to {BaseEquation} = 0 :\n";
            
            for(int i = 0; i < Solutions.Count(); i++)
            {
                sentence += $"{Solutions[i]} ; ";
            }
        }
        return sentence;
    }


    public void Simplify()
    {
        List<decimal> solutions = Equation.FindDivisors();

        for (int i = 0; i <=solutions.Count() - 1; i++)
        {
            while (Equation.SyntheticDivision(solutions[i])[Equation.SyntheticDivision(solutions[i]).Count()-1] == 0)
            {    
                List<decimal> equ = new List<decimal>();
                for (int k = 0; k < Equation.SyntheticDivision(solutions[i]).Count()-1; k++)
                {
                    equ.Add(Equation.SyntheticDivision(solutions[i])[k]);
                }
                CreateEquation(equ);
                Solutions.Add(solutions[i]);
            }
        }
    }

    public void SolveEq()
    {
        if(Equation.Coefs.Count() == 2)
        {
            SolveDeg1();
        }
        else
        {
            Console.WriteLine("Je ne suis pas assez compétent pour résoudre complètement cette équation pour l'instant");
        }
    }

    private void SolveDeg1()
    {
        List<decimal> equ = new List<decimal>();
        decimal divisor = - (Equation.Coefs[1] / Equation.Coefs[0]);
        equ.Add(Equation.Coefs[0] * divisor + Equation.Coefs[1]);
        CreateEquation(equ);
        Solutions.Add(divisor);
    }


    public bool IsSolved()
    {
        bool solved = true;

        for(int i = 0; i < Equation.Coefs.Count(); i++)
        {
            if(Equation.Coefs[i] != 0)
            {
                solved = false;
            }
        }

        return solved;
    }


    public List<int> FindPrimeNb(int max)
    {
        List<int> primeNbs = new List<int>();
        bool prime = true;

        for(int i = 2; i <= max; i++)
        {
            for(int k = 0; k <= primeNbs.Count()-1; k++)
            {
                if(i % primeNbs[k] == 0)
                {
                    prime = false;
                }
            }

            if(prime == true)
            {
                primeNbs.Add(i);
            }
            prime = true;
        }

        return primeNbs;
    }

    public void TurnToInt()
    {
        
    }

    public void ExtendPrime()
    {
        int length = Prime.Count();
        if (length == 0)
        {
            Prime.Add(2);
        } 
        bool extended = false;
        int i = Prime[length - 1];
        do
        {
            i++;
            bool prime = true;
            for(int j = 0; j < length ; j++)
            {
                if(i % j == 0)
                {
                    prime = false;
                }
            }
            if (prime)
            {
                extended = true;
                Prime.Add(i);
            }
        }
        while(!extended);
    }
}