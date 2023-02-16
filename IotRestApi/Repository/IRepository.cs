using IotRestApi.Model;

namespace IotRestApi.Repository
{
    public interface IRepository
    {
        IEnumerable<IotAlert> GetIotAlerts(int pageNumber, int pageSize, string orderBy, bool asc);
    }
}
