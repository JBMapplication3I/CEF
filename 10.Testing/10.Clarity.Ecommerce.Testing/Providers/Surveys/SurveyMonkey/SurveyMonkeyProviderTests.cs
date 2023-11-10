// <copyright file="SurveyMonkeyProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the survey monkey provider tests class</summary>
namespace Clarity.Ecommerce.Providers.Surveys.SurveyMonkey.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;
    using Xunit;

    [Trait("Category", "Providers.Surveys.SurveyMonkey")]
    public class SurveyMonkeyProviderTests
    {
        [Fact(Skip = "Don't run automatically")]
        public Task Verify_CreateEventSurvey_Works()
        {
            return new SurveyMonkeyProvider().CreateEventSurveyAsync(CreateDummyEvent(), null);
        }

        private static ICalendarEventModel CreateDummyEvent()
        {
            return new CalendarEventModel
            {
                ID = 12,
                Name = "Clarity Test -- The event of the year!",
                UserEventAttendances = new List<UserEventAttendanceModel>
                {
                    new UserEventAttendanceModel
                    {
                        Active = true,
                        Slave = new UserModel
                        {
                            Active = true,
                            ContactEmail = "kevin.rodzinski@clarityMIS.com",
                            ContactFirstName = "Kevin",
                            ContactLastName = "Rodzinski"
                        }
                    },
                    new UserEventAttendanceModel
                    {
                        Active = true,
                        Slave = new UserModel
                        {
                            Active = true,
                            ContactEmail = "tyler.morales@clarityMIS.com",
                            ContactFirstName = "Tyler",
                            ContactLastName = "Morales"
                        }
                    },
                    new UserEventAttendanceModel
                    {
                        Active = true,
                        Slave = new UserModel
                        {
                            Active = true,
                            ContactEmail = "johnmccannon@clarityMIS.com",
                            ContactFirstName = "John",
                            ContactLastName = "McCannon",
                            Contact = new ContactModel
                            {
                                MiddleName = "Sir."
                            }
                        },
                        TypeKey = "ORGANIZER"
                    },
                    new UserEventAttendanceModel
                    {
                        Active = true,
                        Slave = new UserModel
                        {
                            Active = true,
                            ContactEmail = "enriquesalvador@clarityMIS.com",
                            ContactFirstName = "Enrique",
                            ContactLastName = "Salvador",
                            Contact = new ContactModel
                            {
                                MiddleName = "Doc."
                            }
                        },
                        TypeKey = "SPIRITUAL"
                    }
                }
            };
        }
    }
}
