module cef.store.locations {
    cefApp.directive("locationStoreHours", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/location/locationStoreHours.html", "ui"),
        scope: {
            store: "=",
            shortFormat: "=",
            smallText: "="
        },
        controller: function ($scope, $translate) {
            const weekDays = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
            let transWeekDays = {};
            this.templateHours = {};

            $scope.$watch(() => this.store, (newVal) => {
                if (angular.isObject(newVal)) {
                    $translate.onReady(() => {
                        transWeekDays = translateWeekDays();
                        const storeHours = parseHours(newVal);
                        this.templateHours = this.shortFormat
                            ? simplifyHours(groupHours(storeHours), storeHours)
                            : this.templateHours = translateHours(storeHours);
                    });
                }
            });

            function parseHours(store) {
                return weekDays.reduce((acc, day) => {
                    const open = store[`OperatingHours${day}Start`];
                    const close = store[`OperatingHours${day}End`];
                    if (open && close) {
                        acc[day] = {
                            open: open,
                            close: close
                        };
                    } else {
                        acc[day] = null;
                    }
                    return acc;
                }, {});
            }

            function sameHours(day1, day2) {
                return (day1 && day2) && ((day1.open === day2.open) && (day1.close === day2.close));
            }

            function groupHours(hoursList) {
                const usedDays: Array<string> = [];
                let rdays = weekDays.slice();
                return weekDays.reduce((hacc, day) => {
                    let prevDay = -1;
                    const matching = rdays.reduce((acc, cday, idx) => {
                        if ((idx === (prevDay + 1)) && sameHours(hoursList[day], hoursList[cday])) {
                            acc.push(cday);
                            usedDays.push(cday);
                            prevDay = idx;
                        }
                        return acc;
                    }, []);
                    rdays = rdays.filter(rday => !(usedDays.indexOf(rday) >= 0));
                    if (matching && matching.length > 0) {
                        hacc[day] = matching;
                    }
                    return hacc;
                }, {});
            }

            function simplifyHours(groupedHours, hoursList) {
                let usedDays = [];
                return weekDays.reduce((acc, cday) => {
                    const matchDays = groupedHours[cday];
                    if (matchDays && matchDays.length > 1) {
                        acc[`${transWeekDays[cday]} - ${transWeekDays[matchDays[matchDays.length - 1]]}`] = hoursList[cday];
                        usedDays = usedDays.concat(matchDays);
                    } else if (!(usedDays.indexOf(cday) >= 0)) {
                        acc[cday] = hoursList[cday];
                    }
                    return acc;
                }, {});
            }

            function translateHours(hoursList) {
                return Object.keys(hoursList).reduce((acc, cday) => {
                    acc[transWeekDays[cday]] = hoursList[cday];
                    return acc;
                }, {});
            }

            function translateWeekDays() {
                const transWeekDays = $translate.instant(
                    weekDays.map(day => `ui.storefront.location.shipTo.shipToDetails.${day}`));
                return weekDays.reduce((acc, oday) => {
                    acc[oday] = transWeekDays[(`ui.storefront.location.shipTo.shipToDetails.${oday}`)];
                    return acc;
                }, {});
            }
        },
        controllerAs: "locationHoursCtrl",
        bindToController: true
    }));
}
