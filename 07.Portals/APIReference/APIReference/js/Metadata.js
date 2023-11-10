var docApp = angular.module("docApp", ["ngSanitize", "ui.bootstrap", "appSettingsExport"])
	.controller("DocumentationController", [
		"$scope", "$rootScope", "$templateRequest", "$sce", "$location", "$anchorScroll", "appSettingsExport",
		function ($scope, $rootScope, $templateRequest, $sce, $location, $anchorScroll, appSettingsExport) {
			$rootScope.appSettingsExport = appSettingsExport;
			$scope.currentArticle = "appSettings";
			$scope.highlightAll = Prism.highlightAll;
			$scope.highlightElement = Prism.highlightElement;
			$scope.setArticle = function(article) {
				$scope.currentArticle = article;
				$scope.currentArticleTemplatePath = "Metadata.Schemas." + article + ".html";
				//$scope.currentArticleContent = "<not loaded>";
				var templateUrl = $sce.getTrustedResourceUrl($scope.currentArticleTemplatePath);
				$templateRequest(templateUrl).then(function (/*template*/) {
					// template is the HTML template as a string
					// Let's put it into an HTML element and parse any directives and expressions
					// in the code. (Note: This is just an example, modifying the DOM from within
					// a controller is considered bad style.)
					//var linkFn = $compile(template);
					//$scope.currentArticleContent = () => { linkFn.html(); };
					setTimeout(1000, $scope.highlightAll);
				}, function () {
					// An error has occurred
				});
			};
			$scope.setArticle($scope.currentArticle);
			$scope.scrollTo = function (id) {
				console.log(id);
				$location.hash(id);
				$anchorScroll();
			};
			/**
			 * Loads a language, including all dependencies
			 * @param {string} lang the language to load
			 * @returns {Promise} the promise which resolves as soon as everything is loaded
			 */
			$scope.loadLanguage = function (lang) {
				// At first we need to fetch all dependencies for the main language
				// Note: we need to do this, even if the main language already is loaded (just to be sure...)
				// We load an array of all dependencies and call recursively this function on each entry
				// dependencies is now an (possibly empty) array of loading-promises
				var dependencies = $scope.getDependenciesOfLanguage(lang).map($scope.loadLanguage);
				// We create a promise, which will resolve, as soon as all dependencies are loaded.
				// They need to be fully loaded because the main language may extend them.
				return Promise.all(dependencies)
					.then(function () {
						// If the main language itself isn't already loaded, load it now
						// and return the newly created promise (we chain the promises).
						// If the language is already loaded, just do nothing - the next .then()
						// will immediately be called
						if (!Prism.languages[lang]) {
							return new Promise(function (resolve) {
								$u.script('components/prism-' + lang + '.js', resolve);
							});
						}
					});
			};
			/**
			 * Returns all dependencies (as identifiers) of a specific language
			 * @param {string} lang the language to load
			 * @returns {Array.<string>} the list of dependencies. Empty if the language has none.
			 */
			$scope.getDependenciesOfLanguage = function (lang) {
				if (!components.languages[lang] || !components.languages[lang].require) {
					return [];
				}
				return $u.type(components.languages[lang].require) === "array"
					? components.languages[lang].require
					: [components.languages[lang].require];
			};
			var /*form = $('form'), code = $('code', form),*/ languages = components.languages;
			$scope.loadLanguage("typescript").then(function () { $scope.highlightAll(true); });
		}
	]).directive("appSettingAccordionBody", function () {
		return {
			restrict: "EA",
			scope: {
				property: "="
			},
			templateUrl: "/DesktopModules/ClarityEcommerce/API-Reference/templates/appSettingAccBody.min.html",
			controller: function ($scope) {
			},
			controllerAs: "appSettingAccBodyCtrl",
			bindToController: true
		}
	}).directive("appSettingAccordion", function () {
		return {
			restrict: "EA",
			scope: {
				properties: "="
			},
			templateUrl: "/DesktopModules/ClarityEcommerce/API-Reference/templates/appSettingAcc.min.html",
			controller: function ($scope) {
				this.quickFilter = null;
			},
			controllerAs: "appSettingAccCtrl",
			bindToController: true
		}
	}).filter("zeroPadNumber", function () {
		function zeroPadNumberFn(num, len) {
			if (isNaN(num) || isNaN(len)) {
				return `${num}`;
			}
			if (`${num}`.indexOf(".") > -1) {
				const parts = `${num}`.split(".");
				let outDec = parts[0]; // Read the before decimal
				while (outDec.length < len) {
					outDec = `0${outDec}`;
				}
				return outDec + parts[1]; // Reappend the after decimal
			} else {
				let outInt = `${num}`;
				while (outInt.length < len) {
					outInt = `0${outInt}`;
				}
				return outInt;
			}
		}
		return zeroPadNumberFn;
	});
