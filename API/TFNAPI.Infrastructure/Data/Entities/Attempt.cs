using System;

namespace TFNAPI.Infrastructure.Data.Entities
{
    public class Attempt
    {
        public int Id { get; set; }
        public DateTime AttemptTime { get; set; }
        public string Tfn { get; set; }
    }
}
