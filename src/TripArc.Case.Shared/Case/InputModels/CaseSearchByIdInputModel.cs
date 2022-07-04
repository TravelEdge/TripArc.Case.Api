namespace TripArc.Case.Shared.Case.InputModels;

[ExcludeFromCodeCoverage]
[ModelBinder(BinderType = typeof(RequestModelBinder<CaseSearchByIdInputModel>))]
public class CaseSearchByIdInputModel : SearchByIdBaseInputModel
{
}