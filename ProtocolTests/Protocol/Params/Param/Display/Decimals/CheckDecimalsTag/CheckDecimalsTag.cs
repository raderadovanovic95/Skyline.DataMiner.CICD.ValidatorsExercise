namespace ProtocolTests.Protocol.Params.Param.Display.Decimals.CheckDecimalsTag
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    using Skyline.DataMiner.CICD.Validators.Common.Interfaces;
    using Skyline.DataMiner.CICD.Validators.Common.Model;
    using Skyline.DataMiner.CICD.Validators.Protocol.Common;
    using Skyline.DataMiner.CICD.Validators.Protocol.Interfaces;
    using Skyline.DataMiner.CICD.Validators.Protocol.Tests.Protocol.Params.Param.Display.Decimals.CheckDecimalsTag;

    [TestClass]
    public class Validate
    {
        private readonly IValidate check = new CheckDecimalsTag();

        #region Valid Checks

        [TestMethod]
        public void Param_CheckDecimalsTag_Valid()
        {
            Generic.ValidateData data = new Generic.ValidateData
            {
                TestType = Generic.TestType.Valid,
                FileName = "Valid",
                ExpectedResults = new List<IValidationResult>()
            };

            Generic.Validate(check, data);
        }

        #endregion

        #region Invalid Checks

        [TestMethod]
        public void Param_CheckDecimalsTag_InvalidTagForDateTime()
        {
            Generic.ValidateData data = new Generic.ValidateData
            {
                TestType = Generic.TestType.Invalid,
                FileName = "InvalidTagForDateTime",
                ExpectedResults = new List<IValidationResult>
                {
                    Error.InvalidTagForDateTime(null, null, null, "10"),
                }
            };

            Generic.Validate(check, data);
        }

        #endregion
    }

    [TestClass]
    public class ErrorMessages
    {
        [TestMethod]
        public void Param_CheckDecimalsTag_InvalidTagForDateTime()
        {
            // Create ErrorMessage
            var message = Error.InvalidTagForDateTime(null, null, null, "paramId");
                        
            var expected = new ValidationResult
            {
                Severity = Severity.Major,
                Certainty = Certainty.Certain,
                FixImpact = FixImpact.NonBreaking,
                GroupDescription = "",
                Description = "Missing tag 'Display/Decimals' with expected value '8' for Param 'paramId'.",
                Details = "By default, only 6 decimals are saved in memory. Parameters holding datetime values need at least 8 decimals to be accurate." + Environment.NewLine + "Otherwise, there might be rounding issues when retrieving the parameter from an external source like an Automation script.",
                HasCodeFix = false,
            };

            // Assert
            message.Should().BeEquivalentTo(expected, Generic.ExcludePropertiesForErrorMessages);
        }
    }

    [TestClass]
    public class Attribute
    {
        private readonly IRoot check = new CheckDecimalsTag();

        [TestMethod]
        public void Param_CheckDecimalsTag_CheckCategory() => Generic.CheckCategory(check, Category.Param);

        [TestMethod]
        public void Param_CheckDecimalsTag_CheckId() => Generic.CheckId(check, CheckId.CheckDecimalsTag);
    }
}