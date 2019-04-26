

using Model;

namespace Service
{
    public interface IServiceUserEvent:IservicePattern<UserEvent>
    {
        void participate(int idu, int ide);
    }
}
