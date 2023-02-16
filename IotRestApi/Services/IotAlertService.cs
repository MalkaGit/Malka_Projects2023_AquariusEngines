using IotRestApi.Model;
using IotRestApi.Repository;

namespace IotRestApi.Services
{
    public class IotAlertService
    {
        private readonly IRepository                _repository;
        private readonly ILogger<IotAlertService>   _logger;


        public IotAlertService(IRepository repository, ILogger<IotAlertService> logger)
        {
            _repository = repository;
            _logger     = logger;
        }

        public IEnumerable<IotAlert> GetIotAlerts(int pageNumber, int pageSize, string orderBy, bool asc)
        {
            return _repository.GetIotAlerts(pageNumber, pageSize, orderBy, asc);
        }

    }
}
