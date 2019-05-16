

using Model;

namespace Service
{
    public interface IServiceEvent:IservicePattern<UserEvent>
    {
        void participate(int idu, int ide);
        void like(int idu, int ide);
    }
}
