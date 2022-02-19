using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNetV3Template.Handlers
{
    internal interface IHandler
    {
        public Task InitializeAsync();
    }
}