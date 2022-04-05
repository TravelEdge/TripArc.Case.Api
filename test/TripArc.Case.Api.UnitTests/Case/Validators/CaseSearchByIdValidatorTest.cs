using FluentValidation.TestHelper;
using TripArc.Case.Api.Case.Validators;
using TripArc.Case.Shared.Case.InputModels;
using Xunit;

namespace TripArc.Case.Api.UnitTests.Case.Validators
{
    public class CaseSearchByIdValidatorTest
    {
        private readonly CaseSearchByIdValidator _validator;
        private readonly CaseSearchByIdInputModel _model;

        public CaseSearchByIdValidatorTest()
        {
            _validator = new CaseSearchByIdValidator();
            _model = new CaseSearchByIdInputModel();
        }
        
        [Fact]
        public void Should_Not_Have_Any_Errors_For_Valid_InputModel()
        {
            _model.Id = 1;
            var result = _validator.TestValidate(_model);

            result.ShouldNotHaveAnyValidationErrors();
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Should_Have_Id_Error_For_Invalid_Id(int value)
        {
            _model.Id = value;
            var result = _validator.TestValidate(_model);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }        
    }
}