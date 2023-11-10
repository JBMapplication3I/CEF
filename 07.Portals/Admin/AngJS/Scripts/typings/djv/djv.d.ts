/* ========================================================================== */
/**
 * @module utils
 * @desc
 * Utilities to check and normalize uri
 */
declare module djsv.utils {
    /**
     * @name keys
     * @type {object}
     * @desc
     * Keys to apply schema attributes & values
     */
    export const keys: { id: string, };

    /**
     * @name head
     * @type {function}
     * @desc
     * Clean an id from its fragment
     * @example
     * head('http://domain.domain:2020/test/a#test')
     * // returns 'http://domain.domain:2020/test/a'
     * @param {string} id
     * @returns {string} cleaned
     */
    export function head(uri: string): string;

    export function isFullUri(uri: string): boolean;

    /**
     * @name path
     * @type {function}
     * @desc
     * Gets a scheme, domain and a path part from the uri
     * @example
     * path('http://domain.domain:2020/test/a?test')
     * // returns 'http://domain.domain:2020/test/'
     * @param {string} uri
     * @returns {string} path
     */
    export function path(uri: string): string;

    /**
     * @desc
     * Get the fragment (#...) part of the uri
     * @see https://tools.ietf.org/html/rfc3986#section-3
     * @param {string} uri
     * @returns {string} fragment
     */
    export function fragment(uri: string): string;

    /**
     * @name makePath
     * @type function
     * @desc
     * Concat parts into single uri
     * @see https://tools.ietf.org/html/rfc3986#section-3
     * @param {array<string>} parts
     * @returns {string} uri
     */
    export function makePath(parts): string;

    /**
     * @name normalize
     * @type {function}
     * @desc
     * Replace json-pointer special symbols in a given uri.
     * @param {string} uri
     * @returns {string} normalizedUri
     */
    export function normalize(uri: string): string;
}
/* ========================================================================== */

/* ========================================================================== */
/**
 * @module template
 * @desc
 * Defines a small templater functionality for creating functions body.
 */
declare module djsv.template {
    /**
     * @name template
     * @type function
     * @desc
     * Provides a templater function, which adds a line of code into generated function body.
     *
     * @param {object} state - used in visit and reference method to iterate and find schemas.
     * @param {DjvConfig} options
     * @return {function} tpl
     */
    export function template(state: object, options: DjvConfig): (expression, ...args) => object;

    /**
     * @name restore
     * @type function
     * @desc
     * Generate a function by given body with a schema in a closure
     *
     * @param {string} source - function inner & outer body
     * @param {object} schema - passed as argument to meta function
     * @param {DjvConfig} config
     * @return {function} tpl
     */
    export function restore(source: string, schema: object, config: DjvConfig): Function;

    /**
     * @name body
     * @type function
     * @desc
     * Generate a function body, containing internal variables and helpers
     *
     * @param {object} tpl - template instance, containing all analyzed schema related data
     * @param {object} state - state of schema generation
     * @param {DjvConfig} config
     * @return {string} body
     */
    export function body(tpl: object, state: object, DjvConfig): string;
}
/* ========================================================================== */

/* ========================================================================== */
/**
 * @module state
 * @desc
 * State module is responsible for scope schemas resolution.
 * It also exports a main `generate` function.
 */
declare module djsv.state {
    /**
     * @name generate
     * @type {function}
     * @desc
     * The main schema process function.
     * Available and used both in external and internal generation.
     * Saves the state for internal recursive calls.
     * @param {object} env - djv environment
     * @param {object} schema - to process
     * @param {State} state - saved state
     * @param {Environment} options
     * @returns {function} restoredFunction
     */
    export function generate(env: object, schema: object, state: State, options: Environment): Function;

    export interface State {
        (schema: object, env: Environment): void;
        /**
         * @name addEntry
         * @type {function}
         * @desc
         * Generates an internal function.
         * Usually necessary for `allOf` types of validators.
         * Caches generated functions by schema object key.
         * Checks for an existing schema in a context stack to avoid double parsing and generation.
         * @param {string} url
         * @param {object} schema
         * @returns {number/boolean} index
         */
        addEntry(url: string, schema: object): number | boolean;
        /**
         * @name revealReference
         * @type {function}
         * @desc
         * If a schema was added during the add entry phase
         * Then it should be revealed in this step
         * @param {object} schema
         * @returns {void}
         */
        revealReference(schema: object): void;
        /**
         * @name link
         * @type {function}
         * @desc
         * Returns an entry's index in a context stack.
         * @param {string} url
         * @returns {string} entry
         */
        link(url: string): string;
        /**
         * @name resolveReference
         * @type {function}
         * @desc
         * Resolve reference against the stack.
         * @param {string} reference
         * @returns {string} resolvedReference
         */
        resolveReference(reference: string): string;
        /**
         * @name ascend
         * @private
         * @type {function}
         * @desc
         * Search for a parent schema by reference.
         * Iterates over the chain of schemas.
         * @param {string} reference
         * @returns {object} parentSchema
         */
        ascend(reference: string): object;
        /**
         * @name descend
         * @private
         * @type {function}
         * @desc
         * Search for a child schema by reference.
         * Iterates over the chain of schemas.
         * @param {string} reference
         * @returns {object} currentSchema
         */
        descend(reference: string, parentSchema: object): object;
        /**
         * @name resolve
         * @type {function}
         * @desc
         * Resolves schema by given reference and current registered context stack.
         * @param {string} url
         * @returns {object} schema
         */
        resolve(reference: string): object;
        /**
         * @name visit
         * @type {function}
         * @desc
         * Calls each registered validator with given schema and template instance.
         * Validator may or may not add code to generated validator function.
         * @param {object} pseudoSchema
         * @param {object} tpl
         * @returns {void}
         */
        visit(pseudoSchema: object, tpl: object): void;
    }
}
/* ========================================================================== */

/* ========================================================================== */
/**
 * @module environment
 * @desc
 * Update the given environment
 */
declare module djsv.environment {
    export function add(version: string, config: object): void;
    export function use(version: string): void;
}
/* ========================================================================== */

/* ========================================================================== */
/**
 * @module formats
 * @desc
 * Validators as string for format keyword rules.
 * A validator is a string, which when executed returns `false` if test is failed, `true` otherwise.
 */
declare module djsv {
    export interface formats {
        alpha: string;
        alphanumeric: string;
        identifier: string;
        hexadecimal: string;
        numeric: string;
        'date-time': string;
        uppercase: string;
        lowercase: string;
        hostname: string;
        uri: string;
        email: string;
        ipv4: string;
        ipv6: string;
        regex: string;
        'json-pointer': string;
        'uri-reference': string;
        'uri-template': string;
    }

    /**
     * Configuration for template
     * @typedef {object} DjvConfig
     * @property {string?} version - defines which version of json-schema draft to use,
     * draft-04 by default
     * @property {function?} versionConfigure - handler to apply for environment version
     * @property {boolean?} inner - a generating object should be considered as inner
     * Default value is false/undefined.
     * If true, then it avoid creating variables in a generated function body,
     * however without proper wrapper function approach will not work.
     * @see template/body, template/body
     * @property {object?} formats - an object containing list of formatters to add for environment
     * @property {function?} errorHandler - a handler to use for generating custom error messages
     */
    export interface DjvConfig {
        /**
         * A generating object should be considered as inner.
         * If true, then it avoids creating variables in a generated function body, however
         * without proper wrapper function approach will not work.
         * @default false|undefined.
         */
        inner?: boolean;

        /**
         * Defines which version of json-schema draft to use,
         * @default "draft-04"
         * */
        version?: string;

        /** Handler to apply for environment version */
        versionConfigure?: Function;

        /** An object containing list of formatters to add for environment */
        formats?: object;

        /** A handler to use for generating custom error messages */
        errorHandler?: Function;
    }

    export interface resolved {
        schema: object;
        fn(_this: Environment, schema: object, undefined: undefined, options): object;
    }

    export interface Environment {
        /**
         * @name Environment
         * @desc
         * Key constructor used for creating enivornment instance
         * @type {function} constructor
         * @param {DjvConfig} options passed to templater and utilities
         *
         * Usage
         *
         * ```javascript
         * const env = djv();
         * const env = new djv();
         * const env = new djv({ errorHandler: () => ';' });
         * ```
         */
        (options?: DjvConfig): void;

        /**
         * check if object correspond to schema
         *
         * Usage
         *
         * ```javascript
         * env.validate('test#/common', { type: 'common' });
         * // => undefined
         *
         * env.validate('test#/common', { type: 'custom' });
         * // => 'required: data'
         *
         * @param {string} name
         * @param {object} object
         * @returns {string} error - undefined if it is valid
         */
        validate(name: string, object: object): string | undefined;

        /**
         * add schema to djv environment
         *
         * Usage
         *
         * ```javascript
         * env.addSchema('test', jsonSchema);
         * ```
         *
         * @param {string?} name
         * @param {object} schema
         * @returns {resolved}
         */
        addSchema(name: string, schema: object): resolved;

        /**
         * removes a schema or the whole structure from djv environment
         *
         * Usage
         *
         * ```javascript
         * env.removeSchema('test');
         * ```
         *
         * @param {string} name
         */
        removeSchema(name: string): void;

        /**
         * resolves name by existing environment
         *
         * Usage
         *
         * ```javascript
         * env.resolve('test');
         * // => { name: 'test', schema: {} }, fn: ... }
         * ```
         *
         * @param {string} name
         * @returns {resolved}
         */
        resolve(name: string): resolved;

        /**
         * exports the whole structure object from environment or by resolved name
         *
         * Usage
         *
         * ```javascript
         * env.export();
         * // => { test: { name: 'test', schema: {}, ... } }
         * ```
         *
         * @param {string} name
         * @returns {serializedInternalState}
         */
        export(name: string): string;

        /**
         * imports all found structure objects to internal environment structure
         * Usage
         *
         * ```javascript
         * env.import(config);
         * ```
         *
         * @param {object} config - internal structure or only resolved schema object
         */
        import(config: object): void;

        /**
         * @name addFormat
         * @type function
         * @desc
         * Add formatter to djv environment.
         * When a string is passed it is interpreted as an expression which
         * when returns `true` goes with an error, when returns `false` then a property is valid.
         * When a function is passed it will be executed during schema compilation
         * with a current schema and template helper arguments.
         * @see utils/formats
         *
         * Usage
         *
         * ```javascript
         * env.addFormat('UpperCase', '%s !== %s.toUpperCase()');
         * // or
         * env.addFormat('isOk', function(schema, tpl){
         *   return `!${schema.isOk} || %s !== %s.toUpperCase()`;
         * });
         * ```
         *
         * @param {string/object?} name
         * @param {string/function} formatter
         */
        addFormat(name: string | object, formatter: string | Function): void;

        /**
         * @name setErrorHandler
         * @type function
         * @desc
         * Specify custom error handler which will be used in generated functions when problem found.
         * The function should return a string expression, which will be executed when generated
         * validator function is executed. The simpliest use case is the default one
         * @see template/defaultErrorHandler
         * ```javascript
         *  function defaultErrorHandler(errorType) {
         *    return `return "${errorType}: ${tpl.data}";`;
         *  }
         * ```
         * It returns an expression 'return ...', so the output is an error string.
         * Usage
         * ```javascript
         * djv({ errorHandler: () => 'return { error: true };' }) // => returns an object
         * djv({
         *  errorHandler: function customErrorHandler(errorType, property) {
         *    return `errors.push({
         *      type: '${type}',
         *      schema: '${this.schema[this.schema.length - 1]}',
         *      data: '${this.data[this.data.length - 1]}'
         *    });
         *  }
         * })`;
         * ```
         * When a custom error handler is used, the template body function adds a `error` variable inside
         * a generated validator, which can be used to put error information. `errorType` is always
         * passed to error handler function. Some validate utilities put extra argument, like f.e.
         * currently processed property value. Inside the handler context is a templater instance,
         * which contains `this.schema`, `this.data` paths arrays to identify validator position.
         * @see test/index/setErrorHandler for more examples
         * @param {function} errorHandler - a function called each time compiler creates an error branch
         * @returns void
         */
        setErrorHandler(errorHandler: Function): void;

        /**
         * @name useVersion
         * @type {function}
         * @desc
         * Add a specification version for environment
         * A configure function is called with exposed environments, like keys, formats, etc.
         * Updates internals utilities and configurations to fix versions implementation conflicts
         * @param {string} version of json-schema specification to use
         * @param {function} configure
         * @returns void
         */
        useVersion(version: string, configure: Function): void;
    }
}

// Ambient declarations
declare var djv: djsv.Environment;
/* ========================================================================== */
