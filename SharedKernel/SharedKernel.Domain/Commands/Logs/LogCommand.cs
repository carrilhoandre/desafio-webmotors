using SharedKernel.Domain.Enums;
using System;

namespace SharedKernel.Domain.Commands.Logs
{
    public class LogCommand : Command, ICommand
    {
        public dynamic data { get; set; }
        public DateTime Date { get; set; }
        public ELogType Type { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
