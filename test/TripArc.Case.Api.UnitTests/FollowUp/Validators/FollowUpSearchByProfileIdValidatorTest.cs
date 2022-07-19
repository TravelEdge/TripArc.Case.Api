using System;
using FluentValidation.TestHelper;
using TripArc.Case.Api.FollowUp.Validators;
using TripArc.Case.Shared.FollowUp.InputModels;
using Xunit;

namespace TripArc.Case.Api.UnitTests.FollowUp.Validators;

public class FollowUpSearchByProfileIdValidatorTest
{
    private readonly FollowUpSearchByProfileIdValidator _validator;
    private readonly FollowUpSearchByProfileIdInputModel _model;

    private const int ProfileId = 1;
    private readonly DateTime _dateTimeFrom = new(2022,2,1);
    private readonly DateTime _dateTimeTo = new(2022,1,31);

    public FollowUpSearchByProfileIdValidatorTest()
    {
        _validator = new FollowUpSearchByProfileIdValidator();
        _model = new FollowUpSearchByProfileIdInputModel();
    }
    
    [Fact]
    public void Should_Not_Have_Any_Errors_For_Valid_InputModel()
    {
        _model.ProfileId = ProfileId;
        _model.DueDateFrom = DateTime.Now;
        _model.DueDateTo = DateTime.Now.AddDays(1);
        var result = _validator.TestValidate(_model);

        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Have_ProfileId_Error_For_Invalid_ProfileId(int value)
    {
        _model.ProfileId = value;
        var result = _validator.TestValidate(_model);

        result.ShouldHaveValidationErrorFor(x => x.ProfileId);
    }

    [Fact]
    public void Should_Have_An_Error_For_Null_DueDateFrom()
    {
        _model.ProfileId = ProfileId;
        _model.DueDateTo = _dateTimeTo;
        
        var result = _validator.TestValidate(_model);
        result.ShouldHaveValidationErrorFor(x => x.DueDateFrom);
    }
    
    [Fact]
    public void Should_Have_An_Error_For_Invalid_DueDateTo()
    {
        _model.ProfileId = ProfileId;
        _model.DueDateFrom = _dateTimeFrom;
        _model.DueDateTo = _dateTimeTo;
        
        var result = _validator.TestValidate(_model);
        result.ShouldHaveValidationErrorFor(x => x.DueDateTo);
    }
    
    [Fact]
    public void Should_Have_An_Error_For_Invalid_DateCreateFrom()
    {
        _model.ProfileId = ProfileId;
        _model.DateCreatedTo = _dateTimeTo;
        
        var result = _validator.TestValidate(_model);
        result.ShouldHaveValidationErrorFor(x => x.DateCreatedFrom);
    }
    
    [Fact]
    public void Should_Have_An_Error_For_Invalid_DateCreateTo()
    {
        _model.ProfileId = ProfileId;
        _model.DateCreatedFrom = _dateTimeFrom;
        _model.DateCreatedTo = _dateTimeTo;
        
        var result = _validator.TestValidate(_model);
        result.ShouldHaveValidationErrorFor(x => x.DateCreatedTo);
    }
    
    [Fact]
    public void Should_Have_An_Error_For_Invalid_StartDateFrom()
    {
        _model.ProfileId = ProfileId;
        _model.StartDateTo = _dateTimeTo;
        
        var result = _validator.TestValidate(_model);
        result.ShouldHaveValidationErrorFor(x => x.StartDateFrom);
    }

    [Fact]
    public void Should_Have_An_Error_For_Invalid_StartDateTo()
    {
        _model.ProfileId = ProfileId;
        _model.StartDateFrom = _dateTimeFrom;
        _model.StartDateTo = _dateTimeTo;
        
        var result = _validator.TestValidate(_model);
        result.ShouldHaveValidationErrorFor(x => x.StartDateTo);
    }    
}