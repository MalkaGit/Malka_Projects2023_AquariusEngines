using IotAlertator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotAlertator.Repository
{
    internal interface IRepository
    {
        int CreateIotAlert(IotAlert iotAlert);
    }
}
