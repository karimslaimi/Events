using Model;


namespace Service
{
    public interface IserviceUniv:IservicePattern<University>
    {
        dynamic Univstat();
    }
}
