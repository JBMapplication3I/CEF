// <copyright file="GeographicDistanceTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the geographic distance tests class</summary>
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local
#pragma warning disable format
namespace Clarity.Ecommerce.Testing
{
    using System;
    using Enums;
    using Utilities;
    using Xunit;

    [Trait("Category", "Workflows.Geography.GeographicDistance")]
    public class GeographicDistanceTests : XUnitLogHelper
    {
        public GeographicDistanceTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public void Verify_GeographicDistance_BetweenLocations_ReturnsProperResults()
        {
            Verify_GeographicDistance_BetweenLocations_ReturnsProperResultsInner(30.266667d, -97.75d, 30.2666670d, -97.7500000d,      "miles",   0.00000000000000d,                 "".Length); // Austin -> Austin in miles = 0
            Verify_GeographicDistance_BetweenLocations_ReturnsProperResultsInner(30.266667d, -97.75d, 35.4823090d, -97.5349940d,      "miles", 360.63095634647561d,   "63095634647561".Length); // Austin -> Oklahoma City in miles = 360.63095634647561
            Verify_GeographicDistance_BetweenLocations_ReturnsProperResultsInner(30.266667d, -97.75d, 30.3903669d, -97.7487976d,      "miles",   8.54944070404918d,   "54944070404918".Length); // Austin -> Clarity Ventures in miles = 8.549440704049184
            Verify_GeographicDistance_BetweenLocations_ReturnsProperResultsInner(30.266667d, -97.75d, 30.2666670d, -97.7500000d,     "meters",   0.00000000000000d,                 "".Length); // Austin -> Austin in meters = 0
            Verify_GeographicDistance_BetweenLocations_ReturnsProperResultsInner(30.266667d, -97.75d, 35.4823090d, -97.5349940d,     "meters", 580379.44536593370d,       "4453659337".Length); // Austin -> Oklahoma City in meters = 580379.4453659337
            Verify_GeographicDistance_BetweenLocations_ReturnsProperResultsInner(30.266667d, -97.75d, 30.3903669d, -97.7487976d,     "meters", 13758.995357120277d,     "995357120277".Length); // Austin -> Clarity Ventures in meters = 13758.995357120277
            Verify_GeographicDistance_BetweenLocations_ReturnsProperResultsInner(30.266667d, -97.75d, 30.2666670d, -97.7500000d, "kilometers",   0.00000000000000d,                 "".Length); // Austin -> Austin in kilometers = 0
            Verify_GeographicDistance_BetweenLocations_ReturnsProperResultsInner(30.266667d, -97.75d, 35.4823090d, -97.5349940d, "kilometers", 580.37944536593370d,    "3794453659337".Length); // Austin -> Oklahoma City in kilometers = 580.3794453659337
            Verify_GeographicDistance_BetweenLocations_ReturnsProperResultsInner(30.266667d, -97.75d, 30.3903669d, -97.7487976d, "kilometers",  13.758995357120277d, "758995357120277".Length); // Austin -> Clarity Ventures in kilometers = 13.758995357120277
        }

        private static void Verify_GeographicDistance_BetweenLocations_ReturnsProperResultsInner(
            double lat1,
            double lng1,
            double lat2,
            double lng2,
            string unitsStr,
            double distExpected,
            int precision)
        {
            // Arrange
            Assert.True(Enum.TryParse(unitsStr, true, out LocatorUnits units));
            // Act
            var dist = GeographicDistance.BetweenLocations(lat1, lng1, lat2, lng2, units);
            // Assert
            Assert.Equal(distExpected, dist, precision);
        }
    }
}
