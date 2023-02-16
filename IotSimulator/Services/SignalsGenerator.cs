using  IotSimulator.Services.SignalGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotSimulator.Services
{
    internal class SignalsGenerator
    {
        private readonly IList<ISignalGenerator> _simulators;
        private readonly IList<Task>             _simlatorsTasks;

        internal SignalsGenerator(IList<ISignalGenerator> simulators)
        {
            _simulators     = simulators;
            _simlatorsTasks = new List<Task>();
        }

        internal void Start()
        {
            for (int i=0; i< _simulators.Count(); i++)
                _simlatorsTasks.Add (_simulators[i].Start(i));
        }

        internal void Stop()
        {
            //TODO
            //https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-cancel-a-task-and-its-children
        }
    }

}
