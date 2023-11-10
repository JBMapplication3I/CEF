/*
 * angular-ui-bootstrap
 * http://angular-ui.github.io/bootstrap/

 * Version: 0.10.0 - 2014-01-16
 * License: MIT
 */
angular.module("ui.bootstrapTabs", ["ui.bootstrap.tpls", "ui.bootstrap.tabs"]);
angular.module("ui.bootstrap.tpls", ["template/tabs/tab.html", "template/tabs/tabset.html", "template/tabs/notab.html"]);

angular.module('ui.bootstrap.tabs', [])

.controller('TabsetController', ['$scope', function TabsetCtrl($scope) {
    var ctrl = this,
        tabs = ctrl.tabs = $scope.tabs = [];

    ctrl.select = function (tab) {
        angular.forEach(tabs, function (tab) {
            tab.active = false;
        });
        tab.active = true;
    };

    ctrl.addTab = function addTab(tab) {
        tabs.push(tab);
        if (tabs.length === 1 || tab.active) {
            ctrl.select(tab);
        }
    };

    ctrl.removeTab = function removeTab(tab) {
        var index = tabs.indexOf(tab);
        //Select a new tab if the tab to be removed is selected
        if (tab.active && tabs.length > 1) {
            //If this is the last tab, select the previous tab. else, the next tab.
            var newActiveIndex = index == tabs.length - 1 ? index - 1 : index + 1;
            ctrl.select(tabs[newActiveIndex]);
        }
        tabs.splice(index, 1);
    };
}])

.directive('tabset', function () {

          var em= $('#angularTabEditMode').val();
          if (em && em==='true') return {}


    return {
        restrict: 'EA',
        transclude: true,
        replace: true,
        scope: {},
        controller: 'TabsetController',
        templateUrl: 'template/tabs/tabset.html',
        link: function (scope, element, attrs) {
            scope.vertical = angular.isDefined(attrs.vertical) ? scope.$parent.$eval(attrs.vertical) : false;
            scope.justified = angular.isDefined(attrs.justified) ? scope.$parent.$eval(attrs.justified) : false;
            scope.type = angular.isDefined(attrs.type) ? scope.$parent.$eval(attrs.type) : 'tabs';
        }
    };
})

.directive('tab', ['$parse', function ($parse) {
    return {
        require: '^tabset',
        restrict: 'EA',
        replace: true,
        templateUrl: 'template/tabs/tab.html',
        transclude: true,
        scope: {
            heading: '@',
            onSelect: '&select', //This callback is called in contentHeadingTransclude
            //once it inserts the tab's content into the dom
            onDeselect: '&deselect'
        },
        controller: function () {
            //Empty controller so other directives can require being 'under' a tab
        },
        compile: function (elm, attrs, transclude) {
            return function postLink(scope, elm, attrs, tabsetCtrl) {
                var getActive, setActive;
                if (attrs.active) {
                    getActive = $parse(attrs.active);
                    setActive = getActive.assign;
                    scope.$parent.$watch(getActive, function updateActive(value, oldVal) {
                        // Avoid re-initializing scope.active as it is already initialized
                        // below. (watcher is called async during init with value ===
                        // oldVal)
                        if (value !== oldVal) {
                            scope.active = !!value;
                        }
                    });
                    scope.active = getActive(scope.$parent);
                } else {
                    setActive = getActive = angular.noop;
                }

                scope.$watch('active', function (active) {
                    // Note this watcher also initializes and assigns scope.active to the
                    // attrs.active expression.
                    setActive(scope.$parent, active);
                    if (active) {
                        tabsetCtrl.select(scope);
                        scope.onSelect();
                    } else {
                        scope.onDeselect();
                    }
                });

                scope.disabled = false;
                if (attrs.disabled) {
                    scope.$parent.$watch($parse(attrs.disabled), function (value) {
                        scope.disabled = !!value;
                    });
                }

                scope.select = function () {
                    if (!scope.disabled) {
                        scope.active = true;
                    }
                };

                tabsetCtrl.addTab(scope);

                scope.$on('$destroy', function () {
                    tabsetCtrl.removeTab(scope);
                });


                //We need to transclude later, once the content container is ready.
                //when this link happens, we're inside a tab heading.
                scope.$transcludeFn = transclude;
            };
        }
    };
}])

.directive('tabHeadingTransclude', [function () {
    return {
        restrict: 'A',
        require: '^tab',
        link: function (scope, elm, attrs, tabCtrl) {
            scope.$watch('headingElement', function updateHeadingElement(heading) {
                if (heading) {
                    elm.html('');
                    elm.append(heading);
                }
            });
        }
    };
}])

.directive('tabContentTransclude', function ($timeout) {
    return {
        restrict: 'A',
        require: '^tabset',
        link: function (scope, elm, attrs) {
            var tab = scope.$eval(attrs.tabContentTransclude);
            var editui= [];
            //Now our tab is ready to be transcluded: both the tab heading area
            //and the tab content area are loaded.  Transclude 'em both.
            
            tab.$transcludeFn(tab.$parent, function (contents) {
                angular.forEach(contents, function (node) {
                    if (isTabHeading(node)) {
                        //Let tabHeadingTransclude know.
                        tab.headingElement = node;
                    } else {
                      elm.append(node);
                      editui.push(node);
                    }
                });
            });

          // Clean up tho DOM after moving stuff around.
          $timeout(function() {
            for (x in editui) {
              var jqNode= $(editui[x]);
                  var re= /^[^0-9]+([0-9]+)[^0-9]+$/;
                     var m= re.exec(jqNode.attr('id'));
                     if (angular.isArray(m) && m.length>0) {
                      var jqBar= $('.DnnModule-'+m[1]);
                      var jqP= jqBar.parent();
                      var jqLi= jqBar.contents('li');
                          jqP.append(jqLi);   
                          jqLi.append(jqBar.hide());
                          }   
                        }        
          });

        }
    };
    function isTabHeading(node) {
        return node.tagName && (
          node.hasAttribute('tab-heading') ||
          node.hasAttribute('data-tab-heading') ||
          node.tagName.toLowerCase() === 'tab-heading' ||
          node.tagName.toLowerCase() === 'data-tab-heading'
        );
    }
});

angular.module("template/tabs/tab.html", []).run(["$templateCache", function ($templateCache) {
    $templateCache.put("template/tabs/tab.html",
      "<li ng-class=\"{active: active, disabled: disabled}\">\n" +
      "  <a ng-click=\"select()\" class=\"ui-tabs-anchor\" tab-heading-transclude>{{heading}}</a>\n" +
      "</li>\n" +
      "");
}]);

angular.module("template/tabs/pane.html", []).run(["$templateCache", function ($templateCache) {
    $templateCache.put("template/tabs/pane.html",
      "<div class=\"tab-pane\" ng-class=\"{active: selected}\" ng-show=\"selected\" ng-transclude></div>\n" +
      "");
}]);


angular.module("template/tabs/notab.html", []).run(["$templateCache", function ($templateCache) {
    $templateCache.put("template/tabs/notab.html",
      "" +
      "");
}]);

angular.module("template/tabs/tabs.html", []).run(["$templateCache", function ($templateCache) {
    $templateCache.put("template/tabs/tabs.html",
      "<div class=\"tabbable\">\n" +
      "  <ul class=\"nav nav-tabs\">\n" +
      "    <li ng-repeat=\"pane in panes\" ng-class=\"{active:pane.selected}\">\n" +
      "      <a ng-click=\"select(pane)\">{{pane.heading}}</a>\n" +
      "    </li>\n" +
      "  </ul>\n" +
      "  <div class=\"tab-content\" ng-transclude></div>\n" +
      "</div>\n" +
      "");
}]);

angular.module("template/tabs/tabset-titles.html", []).run(["$templateCache", function ($templateCache) {
    $templateCache.put("template/tabs/tabset-titles.html",
      "<ul class=\"nav {{type && 'nav-' + type}}\" ng-class=\"{'nav-stacked': vertical}\">\n" +
      "</ul>\n" +
      "");
}]);

angular.module("template/tabs/tabset.html", []).run(["$templateCache", function ($templateCache) {
    $templateCache.put("template/tabs/tabset.html",
      "\n" +
      "<div class=\"tabbable angular-tabs\">\n" +
      "  <ul class=\"nav {{type && 'nav-' + type}}\" ng-class=\"{'nav-stacked': vertical, 'nav-justified': justified}\" ng-transclude></ul>\n" +
      "  <div class=\"tab-content\">\n" +
      "    <div class=\"tab-pane\" \n" +
      "         ng-repeat=\"tab in tabs\" \n" +
      "         ng-class=\"{active: tab.active}\"\n" +
      "         tab-content-transclude=\"tab\">\n" +
      "    </div>\n" +
      "  </div>\n" +
      "</div>\n" +
      "");
}]);
