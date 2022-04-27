using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using TripArc.Common.Base.Model;

namespace TripArc.Case.Shared.Case.InputModels
{
    [ExcludeFromCodeCoverage]
    [ModelBinder(BinderType = typeof(RequestModelBinder<FollowUpSearchByProfileIdInputModel>))]    
    public class FollowUpSearchByProfileIdInputModel
    {
        [JsonPropertyName("id")]
        public int ProfileId { get; set; }
        public DateTime DueDate { get; set; }
        //public string CaseStatus { get; set; }
        public string ClientName { get; set; }
        public DateTime TravelDate { get; set; }
        public string FollowUpType { get; set; }
        public bool QuoteExpired { get; set; }
        public bool FlaggedFollowUps { get; set; }
        public string Channel { get; set; }
        public string CaseName { get; set; }
        public bool CompletedFollowUps { get; set; }
        public string SearchByKeyWord { get; set; }
        public string SortColumns { get; set; }
    }
}