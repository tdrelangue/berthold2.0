public class EquationManip : Equation
{
    public EquastionStart BaseEquation;


    public EquationManip(): base ()
    {
        BaseEquation = new EquastionStart();
    }


    public void SaveEqu()
    {
        for(int i = 0 ; i < Coefs.Count() ; i++)
        {
            BaseEquation.AddCoef(Coefs[i]);
        }
    }

    public override string ToString()
    {
        string sentence = "";
        bool needParanthesis = false;

        // if there is no equation left un factorized, we need to multiply by an
        if (Coefs.Count() == 1)
        {
            sentence += $"{BaseEquation.Coefs[0]}";
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
}