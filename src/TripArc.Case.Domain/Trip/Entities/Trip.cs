using System;

namespace TripArc.Case.Domain.Trip.Entities
{
    public class Trip
    {
        public int TripId { get; set; }
        public int CompanyId { get; set; }
        public string TripReference { get; set; }
        public string TripName { get; set; }
        public int QuoteAgentId { get; set; }
        public int? MasterTripId { get; set; }
        public int? TourAgentId { get; set; }
        public DateTime BookedDate { get; set; }
        public DateTime TripStartDate { get; set; }
        public bool Cancelled { get; set; }
        public DateTime? DateCancelled { get; set; }
        public int MainContactClientId { get; set; }
        public string SystemOfRecordId { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfPeople { get; set; }
        public int CompanyBrandId { get; set; }
        public int SalesCoaAllocationId { get; set; }
        public bool IsArchived { get; set; }
        public string GroupName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public int? TripLockId { get; set; }
        public int? ApprovalRequestId { get; set; }
        public int? TramsresCardNum { get; set; }
        public bool PassportAlertDismiss { get; set; }        
    }
}