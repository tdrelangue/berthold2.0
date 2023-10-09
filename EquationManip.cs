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
}