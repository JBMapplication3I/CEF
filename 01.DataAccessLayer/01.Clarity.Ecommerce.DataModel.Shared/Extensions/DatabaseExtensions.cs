// <copyright file="DatabaseExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the database extensions class</summary>
namespace Clarity.Ecommerce.Utilities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    /// <summary>A database extensions.</summary>
    public static class DatabaseExtensions
    {
        /// <summary>The <see cref="IClarityEcommerceEntities"/> extension method that saves a unit of work.</summary>
        /// <param name="context">       The context to act on.</param>
        /// <param name="changeRequired">True if change required.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool SaveUnitOfWork(this IClarityEcommerceEntities context, bool changeRequired = false)
        {
            try
            {
                var changes = context.SaveChanges();
                // Change Required means that SaveChanges must return something greater than 0
                // (EF would be saying that one or more records changed)
                // No changes required means we allow nothing to have changed, like an Update with the same information
                return changeRequired ? changes > 0 : changes >= 0;
            }
            catch (InvalidOperationException ex1)
            {
                Debug.WriteLine(ex1.Message);
                SaveErrorFromSaving(context.ContextProfileName, ex1);
                throw;
            }
            catch (DbEntityValidationException ex2)
            {
                var msg = BuildDbEntityValidationExceptionMessage(ex2);
                Debug.WriteLine(msg);
                SaveErrorFromSaving(context.ContextProfileName, ex2, msg);
                throw new DbEntityValidationException(msg, ex2);
            }
            catch (DbUpdateException ex3)
            {
                var msg = BuildDbUpdateExceptionMessage(ex3);
                SaveErrorFromSaving(context.ContextProfileName, ex3, msg);
                throw;
            }
            catch (EntityCommandExecutionException ex4)
            {
                var msg = ex4.ToString();
                if (ex4.InnerException != null)
                {
                    msg += ex4.InnerException.ToString();
                }
                Debug.WriteLine(msg);
                SaveErrorFromSaving(context.ContextProfileName, ex4, msg);
                throw;
            }
            catch (EntityException ex5)
            {
                var msg = ex5.ToString();
                if (ex5.InnerException != null)
                {
                    msg += ex5.InnerException.ToString();
                }
                Debug.WriteLine(msg);
                SaveErrorFromSaving(context.ContextProfileName, ex5, msg);
                throw;
            }
            catch (Exception ex6)
            {
                var msg = ex6.ToString();
                Debug.WriteLine(msg);
                SaveErrorFromSaving(context.ContextProfileName, ex6, msg);
                throw;
            }
        }

        /// <summary>The <see cref="IClarityEcommerceEntities"/> extension method that saves a unit of work.</summary>
        /// <param name="context">       The context to act on.</param>
        /// <param name="changeRequired">True if change required.</param>
        /// <returns>A Task{bool}.</returns>
        public static async Task<bool> SaveUnitOfWorkAsync(this IClarityEcommerceEntities context, bool changeRequired = false)
        {
            try
            {
                var changes = await context.SaveChangesAsync().ConfigureAwait(false);
                // Change Required means that SaveChanges must return something greater than 0
                // (EF would be saying that one or more records changed)
                // No changes required means we allow nothing to have changed, like an Update with the same information
                return changeRequired ? changes > 0 : changes >= 0;
            }
            catch (InvalidOperationException ex1)
            {
                Debug.WriteLine(ex1.Message);
                await SaveErrorFromSavingAsync(context.ContextProfileName, ex1).ConfigureAwait(false);
                throw;
            }
            catch (DbEntityValidationException ex2)
            {
                var msg = BuildDbEntityValidationExceptionMessage(ex2);
                Debug.WriteLine(msg);
                await SaveErrorFromSavingAsync(context.ContextProfileName, ex2, msg).ConfigureAwait(false);
                throw new DbEntityValidationException(msg, ex2);
            }
            catch (DbUpdateException ex3)
            {
                var msg = BuildDbUpdateExceptionMessage(ex3);
                await SaveErrorFromSavingAsync(context.ContextProfileName, ex3, msg).ConfigureAwait(false);
                throw;
            }
            catch (EntityCommandExecutionException ex4)
            {
                var msg = ex4.ToString();
                if (ex4.InnerException != null)
                {
                    msg += ex4.InnerException.ToString();
                }
                Debug.WriteLine(msg);
                await SaveErrorFromSavingAsync(context.ContextProfileName, ex4, msg).ConfigureAwait(false);
                throw;
            }
            catch (EntityException ex5)
            {
                var msg = ex5.ToString();
                if (ex5.InnerException != null)
                {
                    msg += ex5.InnerException.ToString();
                }
                Debug.WriteLine(msg);
                await SaveErrorFromSavingAsync(context.ContextProfileName, ex5, msg).ConfigureAwait(false);
                throw;
            }
            catch (Exception ex6)
            {
                var msg = ex6.ToString();
                Debug.WriteLine(msg);
                await SaveErrorFromSavingAsync(context.ContextProfileName, ex6, msg).ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>Builds database entity validation exception message.</summary>
        /// <param name="ex">Details of the exception.</param>
        /// <returns>A string.</returns>
        private static string BuildDbEntityValidationExceptionMessage(DbEntityValidationException ex)
        {
            return ex.EntityValidationErrors
                .SelectMany(x => x.ValidationErrors)
                .Select(x => $"{x.PropertyName}: {x.ErrorMessage}")
                .Distinct()
                .DefaultIfEmpty("None")
                .Aggregate((c, n) => c + "\r\n" + n);
        }

        /// <summary>Builds database update exception message.</summary>
        /// <param name="ex">Details of the exception.</param>
        /// <returns>A string.</returns>
        private static string BuildDbUpdateExceptionMessage(DbUpdateException ex)
        {
            var msg = "==================================================";
            msg += "\r\nDbUpdateException START\r\n";
            msg += ex.ToString();
            msg += "\r\nDbUpdateException Validation Errors\r\n";
            msg = ex.Entries
                .Select(e => e.GetValidationResult())
                .Where(e => e != null)
                .Select(e => new
                {
                    e!.IsValid,
                    Entry = Newtonsoft.Json.JsonConvert.SerializeObject(e.Entry?.Entity),
                    OriginalValues = e.Entry != null && e.Entry.State != EntityState.Added
                        ? e.Entry.OriginalValues.PropertyNames
                            .Select(propName => new
                            {
                                propName,
                                value = e.Entry.OriginalValues.GetValue<object>(propName),
                            })
                            .ToList()
                        : null,
                    CurrentValues = e.Entry != null && e.Entry.State != EntityState.Deleted
                        ? e.Entry.CurrentValues.PropertyNames
                            .Select(propName => new
                            {
                                propName,
                                value = e.Entry.CurrentValues.GetValue<object>(propName),
                            })
                            .ToList()
                        : null,
                    ValidationErrors = e.ValidationErrors
                        .Select(x => $"{x.PropertyName}: {x.ErrorMessage}")
                        .Distinct()
                        .DefaultIfEmpty("None")
                        .Aggregate((c, n) => c + "\r\n" + n),
                })
                .Select(x => Newtonsoft.Json.JsonConvert.SerializeObject(x, SerializableAttributesDictionaryExtensions.JsonSettings))
                .DefaultIfEmpty("None")
                .Aggregate(msg, (c, n) => c + "\r\n" + n);
            msg += "\r\nDbUpdateException END\r\n";
            msg += "\r\n==================================================";
            return msg;
        }

        /// <summary>Saves an error from saving.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="ex">                The exception.</param>
        /// <param name="description">       The description.</param>
        private static void SaveErrorFromSaving(string? contextProfileName, Exception ex, string? description = null)
        {
            try
            {
                // TODO: Get this to read contextProfileName in RegistryLoaderWrapper.GetContext(...); Cannot reference project directly
                if (Contract.CheckValidKey(contextProfileName))
                {
                    return;
                }
                using var errorContext = new ClarityEcommerceEntities();
                var entity = errorContext.EventLogs.Create();
                entity.Active = true;
                entity.CreatedDate = DateExtensions.GenDateTime;
                entity.CustomKey = $"CEF:{Guid.NewGuid()}";
                entity.Name = $"{ex.GetType().Name}: {ex.Message}";
                entity.Description = description ?? ex.ToString();
                entity.LogLevel = 10; // Logger.LogLevels.Error
                errorContext.EventLogs.Add(entity);
                errorContext.SaveUnitOfWork(true);
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <summary>Saves an error from saving.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="ex">                The exception.</param>
        /// <param name="description">       The description.</param>
        /// <returns>A Task.</returns>
        private static async Task SaveErrorFromSavingAsync(string? contextProfileName, Exception ex, string? description = null)
        {
            try
            {
                // TODO: Get this to read contextProfileName in RegistryLoaderWrapper.GetContext(...); Cannot reference project directly
                if (Contract.CheckValidKey(contextProfileName))
                {
                    return;
                }
                using var errorContext = new ClarityEcommerceEntities();
                var entity = errorContext.EventLogs.Create();
                entity.Active = true;
                entity.CreatedDate = DateExtensions.GenDateTime;
                entity.CustomKey = $"CEF:{Guid.NewGuid()}";
                entity.Name = $"{ex.GetType().Name}: {ex.Message}";
                entity.Description = description ?? ex.ToString();
                entity.LogLevel = 10; // Logger.LogLevels.Error
                await errorContext.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            catch
            {
                // Do Nothing
            }
        }
    }
}
