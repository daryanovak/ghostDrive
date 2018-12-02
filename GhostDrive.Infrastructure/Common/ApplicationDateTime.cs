using System;
using GhostDrive.Common;

namespace GhostDrive.Infrastructure.Common
{
    public class ApplicationDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
