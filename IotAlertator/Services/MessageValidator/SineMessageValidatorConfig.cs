using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotAlertator.Services.MessageValidator
{
    internal class SineMessageValidatorConfig
    {
        public int Frequency { get; set; }
        public int MinAmplitude { get; set; }
        public int MaxAmplitude { get; set; }

    }
}
