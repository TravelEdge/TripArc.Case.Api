﻿using TripArc.Common.Abstractions.Entity;

namespace TripArc.Case.Domain.Case.Entities;

public class Case : ISoftDeleteEntity
{
    // public Case()
    // {
    //     CaseActions = new HashSet<CaseActions>();
    // }
    
    public int CaseId { get; set; }
    public int? ParentCaseId { get; set; }
    public string CaseReference { get; set; }
    public string Name { get; set; }
    public int CaseType { get; set; }
    public int CaseStatus { get; set; }
    public int RequestedLocale { get; set; }
    public DateTimeOffset DepartureDate { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public string TravelerPlaceholder { get; set; }
    public int? TripId { get; set; }
    public bool Deleted { get; set; }
    
    public virtual Trip.Entities.Trip Trip { get; set; }
    public virtual ICollection<CaseActions> CaseActions { get; set; }
}