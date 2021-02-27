using System;
using System.Collections.Generic;
using System.Text;
using PluginFramework;

namespace PluginOne
{
    public class RegisterService : IRegisterService
    {
        public string ServiceName { get => "PluginOne"; set => throw new NotImplementedException(); }
    }
}
