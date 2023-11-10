// <copyright file="ServerSidePaging.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the server side paging class</summary>
namespace Clarity.Ecommerce.UI.XPortal.SharedLibrary.Shared.Paged
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MVC.Api.Models;
    using MVC.Core;
    using MVC.Utilities;

    /// <summary>A server side paging.</summary>
    /// <typeparam name="TRecord">     Type of the record.</typeparam>
    /// <typeparam name="TPagedRecord">Type of the paged record collection wrapper.</typeparam>
    /// <typeparam name="TEndpoint">   Type of the endpoint.</typeparam>
    public class ServerSidePaging<TRecord, TPagedRecord, TEndpoint>
        where TRecord : BaseModel
        where TPagedRecord : PagedResultsBase<TRecord>
        where TEndpoint : BaseSearchModel, new()
    {
        // Data
        private int totalCount;
        private int? filteredCount;
        private string? quickFilter = string.Empty;
        private int pageSize;
        private Dictionary<int/*page*/, List<TRecord>> data = new();
        private List<TRecord> dataUnpaged = new();
        private int currentPage;
        private Dictionary<int/*page*/, List<TRecord>>? cachedFilteredData;
        private int[]? cachedAllPages;

        private readonly Func<TEndpoint, Task<IHttpPromiseCallbackArg<TPagedRecord>>> searchCall;
        private readonly string? searchParameterName;
        private readonly Func<TEndpoint?>? searchParamsToMerge;
        private readonly Func<bool>? waitCondition;
        private readonly Action? callback;

        /// <summary>The pages loaded.</summary>
        public List<int> PagesLoaded = new();

        /// <summary>(Immutable) The name.</summary>
        public readonly string Name;

        /// <summary>Initializes a new instance of the <see cref="ServerSidePaging{TRecord, TPagedRecord, TEndpoint}"/>
        /// class.</summary>
        /// <param name="searchCall">         The search call.</param>
        /// <param name="size">               The size.</param>
        /// <param name="name">               The name.</param>
        /// <param name="searchParameterName">Name of the search parameter.</param>
        /// <param name="searchParamsToMerge">The search parameters to merge.</param>
        /// <param name="waitCondition">      The wait condition.</param>
        public ServerSidePaging(
            Func<TEndpoint, Task<IHttpPromiseCallbackArg<TPagedRecord>>> searchCall,
            int size,
            string name,
            string? searchParameterName = "IDOrCustomKeyOrName",
            Func<TEndpoint>? searchParamsToMerge = null,
            Func<bool>? waitCondition = null,
            Action? callback = null)
        {
            searchParamsToMerge ??= () => new TEndpoint { Active = true, AsListing = true };
            pageSize = size;
            currentPage = 0;
            this.searchParamsToMerge = searchParamsToMerge;
            Name = name;
            this.searchParameterName = searchParameterName;
            this.waitCondition = waitCondition;
            this.searchCall = searchCall;
            this.callback = callback;
            Search();
            /* TODO: Convert this to C#
            const unbind1 = $scope.$watch(() => angular.toJson(searchParamsToMerge()), () => this.search());
            $scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
            */
        }

        /// <summary>Gets the number of totals.</summary>
        /// <value>The total number of count.</value>
        public int TotalCount => totalCount;

        /// <summary>Gets the number of filtered.</summary>
        /// <value>The number of filtered.</value>
        public int FilteredCount
        {
            get
            {
                if (filteredCount is null)
                {
                    _ = FilteredData; // Fills the value
                }
                return filteredCount!.Value;
            }
        }

        /// <summary>Gets or sets the size of the page.</summary>
        /// <value>The size of the page.</value>
        public int PageSize
        {
            get => pageSize;
            set
            {
                pageSize = value;
                PagesLoaded.Clear();
                ResetCaches();
                ResetAll();
                Search();
            }
        }

        /// <summary>Gets or sets the quick filter.</summary>
        /// <value>The quick filter.</value>
        public string? QuickFilter
        {
            get => quickFilter;
            set
            {
                quickFilter = value;
                PagesLoaded.Clear();
                ResetCaches();
                Search();
            }
        }

        /// <summary>Gets the data unpaged.</summary>
        /// <value>The data unpaged.</value>
        public List<TRecord> DataUnpaged => dataUnpaged;

        /// <summary>Gets or sets the data.</summary>
        /// <value>The data.</value>
        public Dictionary<int, List<TRecord>> Data
        {
            get => data;
            set
            {
                if (value != null!)
                {
                    var ttlCount = 0;
                    List<TRecord> unpaged = new();
                    PagesLoaded.Clear();
                    foreach (var page in value.Keys)
                    {
                        ttlCount += value[page].Count;
                        foreach (var i in value[page])
                        {
                            // TODO: i["__page"] = page;
                            unpaged.Add(i);
                        }
                        PagesLoaded.Add(page);
                    }
                    data = value;
                    dataUnpaged = unpaged;
                    totalCount = ttlCount;
                }
                else
                {
                    data = new();
                    dataUnpaged.Clear();
                    PagesLoaded.Clear();
                    totalCount = 0;
                }
                ResetCaches();
            }
        }

        /// <summary>Gets the current page from.</summary>
        /// <value>The current page from.</value>
        public int CurrentPageFrom => Skip() + 1;

        /// <summary>Gets the current page to.</summary>
        /// <value>The current page to.</value>
        public int CurrentPageTo
        {
            get
            {
                var unlimitedTo = Skip() + PageSize;
                return FilteredCount < unlimitedTo
                    ? FilteredCount
                    : unlimitedTo;
            }
        }

        /// <summary>Gets the showing label content.</summary>
        /// <value>The showing label content.</value>
        public string ShowingLabelContent => FilteredCount <= 0
            ? "No Results"
            : $@"Showing {CurrentPageFrom:N0} to {CurrentPageTo:N0} of {FilteredCount:N0}";

        /// <summary>Gets or sets the current page.</summary>
        /// <value>The current page.</value>
        public int CurrentPage
        {
            get
            {
                if (currentPage <= 0)
                {
                    currentPage = 0;
                }
                return currentPage;
            }
            set
            {
                var newValueChecked = Math.Max(0, Math.Min(value, MaxPage() - 1)); // -1 to make it zero based
                if (newValueChecked != 0 && currentPage == newValueChecked)
                {
                    return;
                }
                currentPage = newValueChecked;
                Search();
            }
        }

        /// <summary>Resets the cached pages.</summary>
        public void ResetCachedPages()
        {
            cachedAllPages = null;
        }

        /// <summary>Resets the cached filtered data.</summary>
        public void ResetCachedFilteredData()
        {
            cachedFilteredData = null;
            filteredCount = 0;
        }

        /// <summary>Resets the caches.</summary>
        public void ResetCaches()
        {
            ResetCachedFilteredData();
            ResetCachedPages();
            currentPage = CurrentPage;
        }

        /// <summary>Resets all.</summary>
        public void ResetAll()
        {
            Data = new();
        }

        /// <summary>Sets current page if different.</summary>
        /// <param name="newValue">The new value.</param>
        public void SetCurrentPageIfDifferent(int newValue)
        {
            if (newValue < 0)
            {
                newValue = 0;
            }
            if (CurrentPage == newValue)
            {
                return;
            }
            CurrentPage = newValue;
        }

        /// <summary>Gets the skip.</summary>
        /// <returns>An int.</returns>
        public int Skip()
        {
            return CurrentPage * PageSize;
        }

        /// <summary>Maximum page.</summary>
        /// <returns>An int.</returns>
        public int MaxPage()
        {
            return (int)Math.Ceiling(FilteredCount / (decimal)PageSize);
        }

        /**
        * @deprecated Use `pages()` instead. Here for compatibility with
        * the WithSets version of this class
        */
        public int[] AllPages()
        {
            if (cachedAllPages is not null)
            {
                return cachedAllPages;
            }
            Pages(); // Populates the data
            return cachedAllPages!;
        }

        /// <summary>Gets the pages.</summary>
        /// <returns>An int[].</returns>
        public int[] Pages()
        {
            if (cachedAllPages is not null)
            {
                return cachedAllPages;
            }
            var all = new List<int>();
            for (var i = 0; i < MaxPage(); i++)
            {
                all.Add(i);
            }
            cachedAllPages = all.ToArray();
            return cachedAllPages;
        }

        /// <summary>Gets the filtered data set.</summary>
        /// <value>The filtered data set.</value>
        public Dictionary<int, List<TRecord>> FilteredData
        {
            get
            {
                if (cachedFilteredData is not null)
                {
                    return cachedFilteredData;
                }
                if (Contract.CheckEmpty(Data))
                {
                    filteredCount = 0;
                    cachedFilteredData = new()
                    {
                        [0] = new(),
                    };
                    return cachedFilteredData;
                }
                if (!Contract.CheckValidKey(QuickFilter))
                {
                    cachedFilteredData = data;
                    filteredCount = totalCount;
                    return cachedFilteredData;
                }
                // TODO: Use json serialize instead of ToString
                var rawFiltered = dataUnpaged.Where(x => x.ToString()!.Contains(QuickFilter!)).ToList();
                if (Contract.CheckEmpty(rawFiltered))
                {
                    filteredCount = 0;
                    cachedFilteredData = new()
                    {
                        [0] = new(),
                    };
                    return cachedFilteredData;
                }
                Dictionary<int, List<TRecord>> paged = new();
                var pageIndex = 0;
                foreach (var i in rawFiltered)
                {
                    if (!paged.ContainsKey(pageIndex))
                    {
                        paged[pageIndex] = new();
                    }
                    if (paged[pageIndex].Count >= pageSize)
                    {
                        pageIndex++;
                    }
                    if (!paged.ContainsKey(pageIndex))
                    {
                        paged[pageIndex] = new();
                    }
                    // TODO: i["__filteredPage"] = pageIndex;
                    paged[pageIndex].Add(i);
                }
                cachedFilteredData = paged;
                filteredCount = totalCount = rawFiltered.Count;
                return cachedFilteredData;
            }
        }

        /// <summary>Go to first page.</summary>
        public void GoToFirstPage()
        {
            SetCurrentPageIfDifferent(0);
        }

        /// <summary>Go to previous page.</summary>
        public void GoToPreviousPage()
        {
            SetCurrentPageIfDifferent(Math.Max(0, CurrentPage - 1));
        }

        /// <summary>Go to next page.</summary>
        public void GoToNextPage()
        {
            SetCurrentPageIfDifferent(Math.Min(MaxPage(), CurrentPage + 1));
        }

        /// <summary>Go to last page.</summary>
        public void GoToLastPage()
        {
            SetCurrentPageIfDifferent(MaxPage() - 1); // -1 to make it zero based
        }

        /// <summary>Calls for data from the server with any search parameters if set.</summary>
        /// <returns>The results.</returns>
        public void Search()
        {
            if (waitCondition?.Invoke() == true || PagesLoaded.Contains(currentPage))
            {
                return;
            }
            var toMerge = searchParamsToMerge?.Invoke();
            var dto = toMerge ?? new();
            dto.Paging = new()
            {
                StartIndex = currentPage + 1, // Skip
                Size = PageSize, // Take
            };
            if (Contract.CheckValidKey(QuickFilter))
            {
                // TODO: dto[searchParameterName ?? "IDOrCustomKeyOrName"] = QuickFilter;
            }
            var awaiter = searchCall(dto).GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                Console.WriteLine("=============================================");
                var r = awaiter.GetResult();
                if (data == null!)
                {
                    data = new();
                }
                if (!Data.ContainsKey(currentPage))
                {
                    data[currentPage] = new();
                }
                foreach (var i in r.data!.Results!)
                {
                    // TODO: i["__page"] = currentPage;
                    data[currentPage].Add(i);
                    DataUnpaged.Add(i);
                }
                data[currentPage] = data[currentPage].DistinctBy(x => x.ID).OrderBy(x => x.ID).ToList();
                dataUnpaged = dataUnpaged.DistinctBy(x => x.ID).OrderBy(x => x.ID).ToList();
                PagesLoaded.Add(currentPage);
                totalCount = r.data.TotalCount;
                ResetCaches();
                _ = FilteredData;
                callback?.Invoke();
            });
        }
    }
}
