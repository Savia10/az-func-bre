﻿using Az.Serverless.Bre.Func01.Repositories.Implementations;
using Az.Serverless.Bre.Func01.Repositories.Interfaces;
using Az.Serverless.Bre.Tests.Utilities;
using FluentAssertions;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;

namespace Az.Serverless.Bre.Tests
{
    public class BlobRulesStoreRepositoryTest
    {
        public BlobRulesStoreRepositoryTest()
        {

        }

        [Fact]
        public async Task Blob_Rules_Store_GetConfig_Should_Return_Rules_Config_Object()
        {
            //Arrange

            string rulesConfigFileName = "FDInterestRates.json";

            string RuleConfigPath = Path.GetFullPath($"..\\..\\..\\TestData\\RuleConfigs\\{rulesConfigFileName}");

            IRulesStoreRepository _rulesStoreRepository = new BlobRulesStoreRepository(
                BlobUtils.MockBlobContainerClient(RuleConfigPath));

            string expectedWorflowName = "FDInterestRates";

            //Act
            dynamic result = await _rulesStoreRepository.GetConfigAsObjectAsync(rulesConfigFileName);
            string actualWorkflowName = result[0].WorkflowName.Value;


            //Assert
            actualWorkflowName.Should().Be(expectedWorflowName);


        }

        [Fact]
        public async Task Blob_Rules_Store_Repository_GetConfig_Should_Return_Rules_Config_As_String()
        {
            //Arrange
            string rulesConfigFileName = "FDInterestRates.json";

            string RuleConfigPath = Path.GetFullPath($"..\\..\\..\\TestData\\RuleConfigs\\{rulesConfigFileName}");

            IRulesStoreRepository _rulesStoreRepository = new BlobRulesStoreRepository(
                BlobUtils.MockBlobContainerClient(RuleConfigPath));

            string expectedWorflowName = "FDInterestRates";

            //Act
            var configString = await _rulesStoreRepository.GetConfigAsStringAsync(rulesConfigFileName);

            Action action = () =>
            {

                JToken.Parse(configString);
            };

            //Assert
            action.Should().NotThrow();

            configString.Should().Contain(expectedWorflowName);
        }

        [Fact]
        public void Blob_Rules_Store_Constructor_Should_Throw_Argument_NullException_When_BlobContainerClient_Is_Null()
        {
            //Arrange
            var expectedErrorMessage = "Value cannot be null. (Parameter 'blobContainerClient')";

            //Act
            try
            {
                IRulesStoreRepository rulesStoreRepository = new BlobRulesStoreRepository(null);
            }
            catch (Exception ex)
            {
                //Assert
                ex.Should().BeOfType<ArgumentNullException>();
                ex.Message.Should().BeEquivalentTo(expectedErrorMessage);

            }


        }

        [Fact]
        public async void Blob_Rules_Store_Must_Return_Null_When_Blob_Not_Found()
        {
            //Arrange

            IRulesStoreRepository rulesStoreRepository = new BlobRulesStoreRepository(
                BlobUtils.MockBlobContainerClient(null));

            string rulesConfigFileName = "FDInterestRates.json";

            // Act
            var result = await rulesStoreRepository.GetConfigAsObjectAsync(rulesConfigFileName);

            //Assert
            result.Should().BeNull();
        }
    }
}
