/**
 * This file is run via Task Runner Explorer (https://visualstudiogallery.msdn.microsoft.com/8e1b4368-4afb-467a-bc13-9650572db708)
 * It controls what happens to our cef after Visual Studio's internal compiler and typescript compiler are finished
 * To install any missing dependencies, right click the file 'package.json' and choose 'NPM install packages'
 */

/* ==================================================================
 * Loading in of Gulp and its plugins, plus the build options.
 * ================================================================== */
var gulp = require("gulp");
var loadPlugins = require("gulp-load-plugins");
var plugins = loadPlugins(); // Automatically loads any plugin that starts with "gulp-" and attaches it to this namespace.
var extend = require("util")._extend;
var globby = require("globby");
var Promise = require("promise");
var sass = require("gulp-sass");
var autoprefixer = require("gulp-autoprefixer");

/**
 * Simple object check.
 * @param item
 * @returns {boolean}
 */
function isObject(item) {
    return item && typeof item === 'object' && !Array.isArray(item);
}

/**
 * Deep merge two objects.
 * @param target
 * @param ...sources
 */
function mergeDeep(target, ...sources) {
    if (!sources.length) { return target; }
    var source = sources.shift();
    if (isObject(target) && isObject(source)) {
        for (var key in source) {
            if (isObject(source[key])) {
                if (!target[key]) {
                    Object.assign(target, { [key]: {} });
                }
                mergeDeep(target[key], source[key]);
                continue;
            }
            Object.assign(target, { [key]: source[key] });
        }
    }
    return mergeDeep(target, ...sources);
}

var defaultOptions = {
    "debugTemplatesOverriding": false,
    "disableTemplateCache": false,
    "disableStoreBuilds": false,
    "minifyAfterRebuild": false,
    "inputFonts": {
      "overridesLocationRoot": "../../Storefront/Skins/Clarity/Ecommerce/",
      "additions": []
    },
    "outputFonts": {
        "root": "lib/cef/fonts",
        "rootMin": "lib/cef/fonts"
    },
    "inputCSS": {
        "overridesLocationRoot": "../../Storefront/Skins/Clarity/Ecommerce/",
        "additions": []
    },
    "outputCSS": {
        "root": "lib/cef/css",
        "rootMin": "lib/cef/css",
        "coreFileName": "cef.css"
    },
    "inputJS": {
        "overridesLocationRoot": "../../Storefront/Skins/Clarity/Ecommerce/",
        "additions": {
            "angular": [],
            "kendo": [],
            "store": []
        }
    },
    "outputJS": {
        "templatePrefixes": {
            "store": "/DesktopModules/ClarityEcommerce/UI-Storefront/framework/store/"
        },
        "root": "lib/cef/js",
        "rootMin": "lib/cef/js",
        "jQueryFiles": [
            "node_modules/jquery/dist/jquery.js",
            "node_modules/jquery/dist/jquery.min.js",
            "node_modules/jquery/dist/jquery.min.map"
        ],
        "jQueryNamePrefix": "0-",
        "angularFileName": "1-angular.js",
        "kendoFileName": "2-kendo.js",
        "storeFileName": "4-cef-store-main.js",
        "storeTemplatesCacheFileName": "5-cef-store-templates.js",
        "initStoreFileName": "6-cef-store-init.js"
    }
};

var options;
try {
    options = require("../../../Solution Items/cef_gulp_config.json");
    const merged = mergeDeep(defaultOptions, options);
    options = merged;
} catch (e) {
    console.log("No cef_gulp_config.json file found. Using defaults.");
    options = defaultOptions;
}

/* ==================================================================
 * Options for various gulp plugins
 * ================================================================== */
/**
 * ngAnnotate makes sure that Angular injections are converted to the
 * uglify-safe syntax so you don't have to.
 */
var ngAnnotateOptions = {
    add: true,
    single_quotes: true
};

/** Typescript compiler options */
var baseTsOptions = {
    module: "amd",
    target: "ES5",
    typescript: require("typescript"),
    declarationFiles: false,
    sortOutput: true,
    allowJs: false,
    noEmit: false,
    noImplicitAny: false // Makes the compiler forgiving on type checking
};

/* LESS CSS compiler */
var lessOptions = {
    minify: {
        keepSpecialComments: false
    },
    prefixer: {
        browsers: ["last 3 versions"],
        cascade: true
    },
    less: {
        paths: ["."]
    }
};

function taskGetFonts(callback) {
    var fontFiles = [
        "node_modules/bootstrap/fonts/**"
    ];
    if (options.inputFonts.additions && options.inputFonts.additions.length) {
        fontFiles = fontFiles.concat(options.inputFonts.additions);
    }
    conventionCompare(fontFiles, options.inputFonts.overridesLocationRoot).then(function (response) {
        gulp.src(response)
            .pipe(gulp.dest(options.outputFonts.root))
            .on("end", function () { callback(); })
            .on("error", function (errorResponse) { callback(new Error(errorResponse)); });
    });
}

/**
 * Do a merge of two directory file lists for a convention-based override scheme. The first glob array is the
 * base. The second glob array is the override.
 * @param {string[]} filesArr   Array of files.
 * @param {string} overrideRoot The override root.
 * @return {Promise<string[]>} A promise with a string array result of the consolidated files
 */
function conventionCompare(filesArr, overrideRoot) {
    if (options.debugTemplatesOverriding) {
        console.log("==== conventionCompare(...) =====================");
        console.log("== filesArr ==");
        console.log(filesArr);
        console.log("== overrideRoot: " + overrideRoot + " ==");
        console.log("== conventionCompare return Promise(...) ==");
    }
    // Expects ([files array], [root of the override folder])
    return new Promise(function (resolve, reject) {
        if (options.debugTemplatesOverriding) {
            console.log("== conventionCompare return Promise(...): START ==");
        }
        var g1 = globby(filesArr);
        var g2 = globby(filesArr, { cwd: overrideRoot });
        Promise.all([g1, g2]).then(function (responseArr) {
            if (options.debugTemplatesOverriding) {
                console.log("== conventionCompare return Promise.all([g1,g2]): START ==");
                console.log("== conventionCompare return Promise.all([g1,g2]) g1 array has " + responseArr[0].length + " files ==");
                console.log("== conventionCompare return Promise.all([g1,g2]) g2 array contents: START [" + responseArr[1].length + " files] ==");
                for(var i = 0; i < responseArr[1].length; i++) {
                    console.log(responseArr[1][i]);
                }
                console.log("== conventionCompare return Promise.all([g1,g2]) g2 array contents: END [" + responseArr[1].length + " files] ==");
            }
            var tc = treeComparison(responseArr[0], responseArr[1], overrideRoot);
            resolve(tc);
            if (options.debugTemplatesOverriding) {
                console.log("== conventionCompare return Promise.all([g1,g2]): END ==");
            }
        }, function () {
            reject("CEF BUILD: Directory comparison failed.");
        });

        function treeComparison(arr1, arr2, overrideRoot) {
            return arr1.map(function (item) {
                var inThere = arr2.filter(function (item2) {
                    return item.toLowerCase() === item2.toLowerCase();
                });
                if (inThere[0]) {
                    if (options.debugTemplatesOverriding) {
                        console.log("== yes override: " + overrideRoot + inThere[0]);
                    }
                    return overrideRoot + inThere[0];
                }
                if (options.debugTemplatesOverriding) {
                    console.log("== no  override: " + item);
                }
                return item;
            });
        }
    });
}

var minifyConfig = {
    ext: {
        source: ".source.js",
        min: ".min.js"
    },
    max_line_len: false,
    mangle: false
};

function taskGetJQuery(callback) {
    gulp.src(options.outputJS.jQueryFiles)
        .pipe(plugins.rename({ prefix: options.outputJS.jQueryNamePrefix }))
        .pipe(gulp.dest(options.outputJS.root))
        .on("end", function () { callback(); })
        .on("error", function (response) { callback(new Error(response)); });
}

function taskGenAngularFile(callback) {
    var angularFiles = [
        "node_modules/angular-polyfills/dist/all.js",
        "node_modules/angular/angular.js",
        "node_modules/angular-animate/angular-animate.js",
        "node_modules/angular-sanitize/angular-sanitize.js",
        "node_modules/angular-cookies/angular-cookies.js",
        "node_modules/angular-messages/angular-messages.js",
        "node_modules/angular-scroll/angular-scroll.js",
        "node_modules/angular-ui-router/release/angular-ui-router.js",
        "Scripts/thirdparty/angular/angular-bootstrap-toggle/src/angular-bootstrap-toggle.js",
        "Scripts/thirdparty/angular/ui-bootstrap4/dist/ui-bootstrap-tpls.js",
        "Scripts/thirdparty/angular/angular-credit-cards/release/angular-credit-cards.js",
        "node_modules/angular-bind-html-compile/angular-bind-html-compile.js",
        "node_modules/angular-ui-select/select.js",
        "node_modules/angular-ui-mask/dist/mask.js",
        "node_modules/angular-css/angular-css.js",
        "node_modules/angular-payments/lib/angular-payments.js",
        "node_modules/angular-translate/dist/angular-translate.js",
        "node_modules/angular-wizard/dist/angular-wizard.min.js",
        "node_modules/angular-simple-logger/dist/angular-simple-logger.js",
        "node_modules/angular-google-maps/dist/angular-google-maps.js",
        "node_modules/angular-validation-match/dist/angular-validation-match.js",
        "node_modules/angular-sticky-plugin/dist/angular-sticky.js",
        "node_modules/angular-intro.js/build/angular-intro.js",
        "node_modules/moment/moment.js",
        "node_modules/angular-moment/angular-moment.js",
        "node_modules/checklist-model/checklist-model.js",
        "Scripts/thirdparty/angular-usersnap-logging/usersnap-logging.js", // Captures angular stuff to usersnap
        "Scripts/thirdparty/lodash/lodash.js",
        "node_modules/djv/djv.js",
    ];
    if (options.inputJS.additions.angular && options.inputJS.additions.angular.length) {
        angularFiles = angularFiles.concat(options.inputJS.additions.angular);
    }
    conventionCompare(angularFiles, options.inputJS.overridesLocationRoot).then(function (r) {
        gulp.src(r)
            .pipe(plugins.concat(options.outputJS.angularFileName))
            .pipe(plugins.ngAnnotate(ngAnnotateOptions))
            .pipe(gulp.dest(options.outputJS.root))
            .on("end", function () { callback(); })
            .on("error", function (response) { callback(new Error(response)); });
    });
}

function taskGenKendoFile(callback) {
    var kendoFiles = [
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.core.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.angular.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.data.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.grid.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.pager.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.groupable.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.columnsorter.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.userevents.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.draganddrop.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.window.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.popup.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.list.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.dropdownlist.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.menu.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.calendar.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.tabstrip.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.editor.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.upload.js",
        "Scripts/thirdparty/telerik-kendo-ui/src/js/kendo.dom.js",
    ];
    if (options.inputJS.additions.kendo && options.inputJS.additions.kendo.length) {
        kendoFiles = kendoFiles.concat(options.inputJS.additions.kendo);
    }
    conventionCompare(kendoFiles, options.inputJS.overridesLocationRoot).then(function (r) {
        gulp.src(r)
            .pipe(plugins.concat(options.outputJS.kendoFileName))
            .pipe(plugins.ngAnnotate(ngAnnotateOptions))
            .pipe(gulp.dest(options.outputJS.root))
            .on("end", function () { callback(); })
            .on("error", function (response) { callback(new Error(response)); });
    });
}

function taskGenMainStore(callback) {
    var project = plugins.typescript.createProject(
        "./tsconfig.json",
        extend({ out: options.outputJS.storeFileName }, baseTsOptions));
    var tsFiles = [
        "framework/store/**/*.ts"
    ];
    if (options.inputJS.additions.store && options.inputJS.additions.store.length) {
        tsFiles = tsFiles.concat(options.inputJS.additions.store);
    }
    conventionCompare(tsFiles, options.inputJS.overridesLocationRoot).then(function (r) {
        var tsResult = gulp.src(r).pipe(plugins.typescript(project));
        tsResult.dts.pipe(gulp.dest(options.outputJS.root));
        tsResult.js
            .pipe(plugins.concat(options.outputJS.storeFileName))
            .pipe(plugins.ngAnnotate(ngAnnotateOptions))
            .pipe(gulp.dest(options.outputJS.root))
            .on("end", function () { callback(); })
            .on("error", function (response) { callback(new Error(response)); });
    });
}

function taskGenTemplatesCacheStore(callback) {
    var partials = [
        "framework/store/**/*.html",
        "!framework/store/jquery-plugins/**"
    ];
    if (options.disableTemplateCache) {
        partials = [
            "framework/store/emptyCefStoreTemplates.html"
        ];
    }
    conventionCompare(partials, options.inputJS.overridesLocationRoot).then(function (r) {
        var stripBase = "framework/store/";
        var renameRgx = new RegExp("((\\.\\./)*" + options.inputJS.overridesLocationRoot.replace(/\/Storefront/, "") + stripBase + ")");
        gulp.src(r, { base: stripBase })
            .pipe(plugins.htmlmin({
                collapseWhitespace: true,
                html5: true,
                minifyCss: true,
                minifyJS: true,
                removeComments: true,
                removeScriptTypeAttributes: true,
                removeStyleLinkTypeAttributes: true
            }))
            .pipe(plugins.ngHtml2js({
                prefix: options.outputJS.templatePrefixes.store || "/UI-Storefront/framework/store/",
                moduleName: "cefStoreTemplates",
                template: "  $templateCache.put('<%= template.url %>', '<%= template.prettyEscapedContent %>');",
                rename: function (templateUrl/*, templateFile*/) {
                    return templateUrl.replace(renameRgx, "");
                }
            }))
            .pipe(plugins.concat(options.outputJS.storeTemplatesCacheFileName))
            .pipe(plugins.ngAnnotate(ngAnnotateOptions))
            .pipe(plugins.header("// Templates Cache for the Storefront\r\n(function(module){\r\ntry{\r\nmodule=angular.module(\"cefStoreTemplates\")\r\n}catch(e){\r\nmodule=angular.module(\"cefStoreTemplates\",[])\r\n}\r\n})();\r\nangular.module(\"cefStoreTemplates\").run([\"$templateCache\", function($templateCache) {\r\n"))
            .pipe(plugins.footer("\r\n}]);\r\n"))
            .pipe(gulp.dest(options.outputJS.root))
            .on("end", function () { callback(); })
            .on("error", function (response) { callback(new Error(response)); });
    });
}

function taskGenInitCallStore(callback) {
    gulp.src("framework/store/emptyCefStoreTemplates.html")
        .pipe(plugins.concat(options.outputJS.initStoreFileName))
        .pipe(plugins.footer("console.log(\"Bootstrap the Clarity eCommerce Framework Storefront\");\r\nangular.bootstrap(document, [\"cef.store\"]);\r\n"))
        .pipe(plugins.ngAnnotate(ngAnnotateOptions))
        .pipe(gulp.dest(options.outputJS.root))
        .on("end", function () { callback(); })
        .on("error", function (response) { callback(new Error(response)); });
}

function taskStoreSASS(callback) {
    var root = options.inputCSS.overridesLocationRoot + "../css/";
    root = root.replace("/Ecommerce/../", "/");
    console.log("SASS Skin CSS Root: " + root);
    gulp.src(root + "*.scss")
        .pipe(sass({
            outputStyle: "compressed",
            sourceMap: true
        })
        .on("error", function(response) {
            sass.logError(response);
            callback(new Error(response));
        }))
        .pipe(autoprefixer({
            browsers: ["last 2 versions"],
            cascade: false
        }))
        .pipe(gulp.dest(root))
        .on("end", function () { callback(); })
        .on("error", function (response) { callback(new Error(response)); });
}

/* ==================================================================
 * The Tasks (they just call the functions above in the order they need)
 * ================================================================== */
gulp.task("build:fonts",                  [], function (callback) { taskGetFonts(callback); });
gulp.task("build:store:sass",             [], function (callback) { taskStoreSASS(callback); });
gulp.task("build:getjquery",              [], function (callback) { taskGetJQuery(callback); });
gulp.task("build:angular",                [], function (callback) { taskGenAngularFile(callback); });
gulp.task("build:kendo",                  [], function (callback) { taskGenKendoFile(callback); });
gulp.task("build:store:init",             [], function (callback) { taskGenInitCallStore(callback); });
gulp.task("build:store:templates",        [], function (callback) { taskGenTemplatesCacheStore(callback); });
gulp.task("build:store:main",             [], function (callback) { taskGenMainStore(callback); });

gulp.task("build:static", [
    "build:getjquery",
    "build:angular",
    "build:kendo",
], function (callback) { callback(); });

gulp.task("build:store:short", options.disableStoreBuilds ? [] : [
    "build:store:templates",
    "build:store:main",
    "build:store:sass"
], function (callback) { callback(); });

gulp.task("build:store", options.disableStoreBuilds ? [] : [
    "build:static",
    "build:store:init",
    "build:store:short"
], function (callback) { callback(); });

var arr = ["build:static"];
if (!options.disableStoreBuilds) {
    arr.push("build:store");
    arr.push("build:store:sass");
}
arr.push("build:fonts");
gulp.task("build", arr, function (callback) { callback(); });

function minifySplitFiles(callback) {
    gulp.src([
        options.outputJS.root + "/" + options.outputJS.angularFileName,
        options.outputJS.root + "/" + options.outputJS.kendoFileName,
        options.outputJS.root + "/" + options.outputJS.storeFileName,
        options.outputJS.root + "/" + options.outputJS.storeTemplatesCacheFileName,
        options.outputJS.root + "/" + options.outputJS.initStoreFileName,
    ])
    //.pipe(plugins.sourcemaps.init({ largeFile: true }))
    .pipe(plugins.minify(minifyConfig))
    //.pipe(plugins.sourcemaps.write("."))
    .pipe(gulp.dest(options.outputJS.rootMin))
    .on("end", function () { callback(); });
}

function minifySingle(origFileName, callback) {
    gulp.src([
        options.outputJS.root + "/" + origFileName,
    ])
    //.pipe(plugins.sourcemaps.init({ largeFile: true }))
    .pipe(plugins.minify(minifyConfig))
    //.pipe(plugins.sourcemaps.write("."))
    .pipe(gulp.dest(options.outputJS.rootMin))
    .on("end", function () { callback(); });
}

gulp.task("build:minify:angular",               ["build:angular"              ], function (callback) { minifySingle(options.outputJS.angularFileName,                     callback); });
gulp.task("build:minify:kendo",                 ["build:kendo"                ], function (callback) { minifySingle(options.outputJS.kendoFileName,                       callback); });
gulp.task("build:minify:store:main",            ["build:store:main"           ], function (callback) { minifySingle(options.outputJS.storeFileName,                       callback); });
gulp.task("build:minify:store:templates",       ["build:store:templates"      ], function (callback) { minifySingle(options.outputJS.storeTemplatesCacheFileName,         callback); });
gulp.task("build:minify:store:init",            ["build:store:init"           ], function (callback) { minifySingle(options.outputJS.initStoreFileName,                   callback); });

gulp.task("build:minify", [/*"build"*/], function (callback) { minifySplitFiles(callback); });

/** Watch-- It watches for file changes and fires off a build task automatically. */
function setupBuildStoreTemplatesWatch() {
    if (!options.disableStoreBuilds) {
        gulp.watch([
            "framework/store/**/*.html",
            options.inputJS.overridesLocationRoot + "framework/store/**/*.html"
        ], options.minifyAfterRebuild ? ["build:minify:store:templates"] : ["build:store:templates"]);
    }
}

function setupBuildStoreSASSWatch() {
    if (!options.disableStoreBuilds) {
        var root = options.inputCSS.overridesLocationRoot + "../css/";
        root = root.replace("/Ecommerce/../", "/");
        console.log("SASS Skin CSS Root: " + root);
        gulp.watch([
            root + "*.scss",
            root + "clarityHelpers/*.scss",
            root + "components/**/*.scss",
            root + "../bootstrap/stylesheets/*.scss",
            root + "../bootstrap/stylesheets/**/*.scss",
            root + "node_modules/angular-bootstrap-toggle/src/angular-bootstrap-toggle.scss"
        ], ["build:store:sass"]);
    }
}

function setupBuildStoreMainWatch() {
    if (!options.disableStoreBuilds) {
        gulp.watch([
            "../../Admin/AngJS/framework/admin/controls/types/types.searchControllerStore.ts",
            "framework/store/**/*.ts",
            options.inputJS.overridesLocationRoot + "framework/store/**/*.ts",
            options.inputJS.overridesLocationRoot + "framework/store/**/*.js"
        ], options.minifyAfterRebuild ? ["build:minify:store:main"] : ["build:store:main"]);
    }
}

gulp.task("watch", function () {
    setupBuildStoreTemplatesWatch();
    setupBuildStoreMainWatch();
    setupBuildStoreSASSWatch();
});

gulp.task("watch:store", function () {
    setupBuildStoreTemplatesWatch();
    setupBuildStoreMainWatch();
    setupBuildStoreSASSWatch();
});
