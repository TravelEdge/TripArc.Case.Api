using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using TripArc.Common.Base.Model;
using TripArc.Common.InputModels;

namespace TripArc.Case.Shared.Case.InputModels;

[ExcludeFromCodeCoverage]
[ModelBinder(BinderType = typeof(RequestModelBinder<CaseSearchByIdInputModel>))]
public class CaseSearchByIdInputModel : SearchByIdBaseInputModel
{
}