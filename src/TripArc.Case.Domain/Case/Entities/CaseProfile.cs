using System;
using TripArc.Common.Abstractions.Entity;

namespace TripArc.Case.Domain.Case.Entities
{
    public class CaseProfile : ISoftDeleteEntity
    {
        public int CaseProfileId { get; set; }
        public int CaseId { get; set; }
        public int ProfileId { get; set; }
        public int CaseRole { get; set; }
        public bool IsPrimary { get; set; }
        public bool Contact { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public bool IsValid => true;
        public bool Deleted { get; set; }
    }
}