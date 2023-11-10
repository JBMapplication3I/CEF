/* http://prismjs.com/download.html?themes=prism-coy&languages=markup+css+clike+javascript+aspnet+csharp+json+less+sql+typescript&plugins=line-highlight+line-numbers+jsonp-highlight+autoloader */
var _self = (typeof window !== 'undefined')
    ? window   // if in browser
    : (
        (typeof WorkerGlobalScope !== 'undefined' && self instanceof WorkerGlobalScope)
        ? self // if in worker
        : {}   // if in node js
    );

/**
 * Prism: Lightweight, robust, elegant syntax highlighting
 * MIT license http://www.opensource.org/licenses/mit-license.php/
 * @author Lea Verou http://lea.verou.me
 */

var Prism = (function(){

// Private helper vars
var lang = /\blang(?:uage)?-(\w+)\b/i;
var uniqueId = 0;

var _ = _self.Prism = {
    util: {
        encode: function (tokens) {
            if (tokens instanceof Token) {
                return new Token(tokens.type, _.util.encode(tokens.content), tokens.alias);
            } else if (_.util.type(tokens) === 'Array') {
                return tokens.map(_.util.encode);
            } else {
                return tokens.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/\u00a0/g, ' ');
            }
        },

        type: function (o) {
            return Object.prototype.toString.call(o).match(/\[object (\w+)\]/)[1];
        },

        objId: function (obj) {
            if (!obj['__id']) {
                Object.defineProperty(obj, '__id', { value: ++uniqueId });
            }
            return obj['__id'];
        },

        // Deep clone a language definition (e.g. to extend it)
        clone: function (o) {
            var type = _.util.type(o);

            switch (type) {
                case 'Object':
                    var clone = {};

                    for (var key in o) {
                        if (o.hasOwnProperty(key)) {
                            clone[key] = _.util.clone(o[key]);
                        }
                    }

                    return clone;

                case 'Array':
                    // Check for existence for IE8
                    return o.map && o.map(function(v) { return _.util.clone(v); });
            }

            return o;
        }
    },

    languages: {
        extend: function (id, redef) {
            var lang = _.util.clone(_.languages[id]);

            for (var key in redef) {
                lang[key] = redef[key];
            }

            return lang;
        },

        /**
         * Insert a token before another token in a language literal
         * As this needs to recreate the object (we cannot actually insert before keys in object literals),
         * we cannot just provide an object, we need anobject and a key.
         * @param inside The key (or language id) of the parent
         * @param before The key to insert before. If not provided, the function appends instead.
         * @param insert Object with the key/value pairs to insert
         * @param root The object that contains `inside`. If equal to Prism.languages, it can be omitted.
         */
        insertBefore: function (inside, before, insert, root) {
            root = root || _.languages;
            var grammar = root[inside];

            if (arguments.length == 2) {
                insert = arguments[1];

                for (var newToken in insert) {
                    if (insert.hasOwnProperty(newToken)) {
                        grammar[newToken] = insert[newToken];
                    }
                }

                return grammar;
            }

            var ret = {};

            for (var token in grammar) {

                if (grammar.hasOwnProperty(token)) {

                    if (token == before) {

                        for (var newToken in insert) {

                            if (insert.hasOwnProperty(newToken)) {
                                ret[newToken] = insert[newToken];
                            }
                        }
                    }

                    ret[token] = grammar[token];
                }
            }

            // Update references in other language definitions
            _.languages.DFS(_.languages, function(key, value) {
                if (value === root[inside] && key != inside) {
                    this[key] = ret;
                }
            });

            return root[inside] = ret;
        },

        // Traverse a language definition with Depth First Search
        DFS: function(o, callback, type, visited) {
            visited = visited || {};
            for (var i in o) {
                if (o.hasOwnProperty(i)) {
                    callback.call(o, i, o[i], type || i);

                    if (_.util.type(o[i]) === 'Object' && !visited[_.util.objId(o[i])]) {
                        visited[_.util.objId(o[i])] = true;
                        _.languages.DFS(o[i], callback, null, visited);
                    }
                    else if (_.util.type(o[i]) === 'Array' && !visited[_.util.objId(o[i])]) {
                        visited[_.util.objId(o[i])] = true;
                        _.languages.DFS(o[i], callback, i, visited);
                    }
                }
            }
        }
    },
    plugins: {},

    highlightAll: function(async, callback) {
        var env = {
            callback: callback,
            selector: 'code[class*="language-"], [class*="language-"] code, code[class*="lang-"], [class*="lang-"] code'
        };

        _.hooks.run("before-highlightall", env);

        var elements = env.elements || document.querySelectorAll(env.selector);

        for (var i=0, element; element = elements[i++];) {
            _.highlightElement(element, async === true, env.callback);
        }
    },

    highlightElement: function(element, async, callback) {
        // Find language
        var language, grammar, parent = element;

        while (parent && !lang.test(parent.className)) {
            parent = parent.parentNode;
        }

        if (parent) {
            language = (parent.className.match(lang) || [,''])[1];
            grammar = _.languages[language];
        }

        // Set language on the element, if not present
        element.className = element.className.replace(lang, '').replace(/\s+/g, ' ') + ' language-' + language;

        // Set language on the parent, for styling
        parent = element.parentNode;

        if (/pre/i.test(parent.nodeName)) {
            parent.className = parent.className.replace(lang, '').replace(/\s+/g, ' ') + ' language-' + language;
        }

        var code = element.textContent;

        var env = {
            element: element,
            language: language,
            grammar: grammar,
            code: code
        };

        if (!code || !grammar) {
            _.hooks.run('complete', env);
            return;
        }

        _.hooks.run('before-highlight', env);

        if (async && _self.Worker) {
            var worker = new Worker(_.filename);

            worker.onmessage = function(evt) {
                env.highlightedCode = evt.data;

                _.hooks.run('before-insert', env);

                env.element.innerHTML = env.highlightedCode;

                callback && callback.call(env.element);
                _.hooks.run('after-highlight', env);
                _.hooks.run('complete', env);
            };

            worker.postMessage(JSON.stringify({
                language: env.language,
                code: env.code,
                immediateClose: true
            }));
        }
        else {
            env.highlightedCode = _.highlight(env.code, env.grammar, env.language);

            _.hooks.run('before-insert', env);

            env.element.innerHTML = env.highlightedCode;

            callback && callback.call(element);

            _.hooks.run('after-highlight', env);
            _.hooks.run('complete', env);
        }
    },

    highlight: function (text, grammar, language) {
        var tokens = _.tokenize(text, grammar);
        return Token.stringify(_.util.encode(tokens), language);
    },

    tokenize: function(text, grammar, language) {
        var Token = _.Token;

        var strarr = [text];

        var rest = grammar.rest;

        if (rest) {
            for (var token in rest) {
                grammar[token] = rest[token];
            }

            delete grammar.rest;
        }

        tokenloop: for (var token in grammar) {
            if(!grammar.hasOwnProperty(token) || !grammar[token]) {
                continue;
            }

            var patterns = grammar[token];
            patterns = (_.util.type(patterns) === "Array") ? patterns : [patterns];

            for (var j = 0; j < patterns.length; ++j) {
                var pattern = patterns[j],
                    inside = pattern.inside,
                    lookbehind = !!pattern.lookbehind,
                    greedy = !!pattern.greedy,
                    lookbehindLength = 0,
                    alias = pattern.alias;

                pattern = pattern.pattern || pattern;

                for (var i=0; i<strarr.length; i++) { // Don’t cache length as it changes during the loop

                    var str = strarr[i];

                    if (strarr.length > text.length) {
                        // Something went terribly wrong, ABORT, ABORT!
                        break tokenloop;
                    }

                    if (str instanceof Token) {
                        continue;
                    }

                    pattern.lastIndex = 0;

                    var match = pattern.exec(str),
                        delNum = 1;

                    // Greedy patterns can override/remove up to two previously matched tokens
                    if (!match && greedy && i != strarr.length - 1) {
                        // Reconstruct the original text using the next two tokens
                        var nextToken = strarr[i + 1].matchedStr || strarr[i + 1],
                            combStr = str + nextToken;

                        if (i < strarr.length - 2) {
                            combStr += strarr[i + 2].matchedStr || strarr[i + 2];
                        }

                        // Try the pattern again on the reconstructed text
                        pattern.lastIndex = 0;
                        match = pattern.exec(combStr);
                        if (!match) {
                            continue;
                        }

                        var from = match.index + (lookbehind ? match[1].length : 0);
                        // To be a valid candidate, the new match has to start inside of str
                        if (from >= str.length) {
                            continue;
                        }
                        var to = match.index + match[0].length,
                            len = str.length + nextToken.length;

                        // Number of tokens to delete and replace with the new match
                        delNum = 3;

                        if (to <= len) {
                            if (strarr[i + 1].greedy) {
                                continue;
                            }
                            delNum = 2;
                            combStr = combStr.slice(0, len);
                        }
                        str = combStr;
                    }

                    if (!match) {
                        continue;
                    }

                    if(lookbehind) {
                        lookbehindLength = match[1].length;
                    }

                    var from = match.index + lookbehindLength,
                        match = match[0].slice(lookbehindLength),
                        to = from + match.length,
                        before = str.slice(0, from),
                        after = str.slice(to);

                    var args = [i, delNum];

                    if (before) {
                        args.push(before);
                    }

                    var wrapped = new Token(token, inside? _.tokenize(match, inside) : match, alias, match, greedy);

                    args.push(wrapped);

                    if (after) {
                        args.push(after);
                    }

                    Array.prototype.splice.apply(strarr, args);
                }
            }
        }

        return strarr;
    },

    hooks: {
        all: {},

        add: function (name, callback) {
            var hooks = _.hooks.all;

            hooks[name] = hooks[name] || [];

            hooks[name].push(callback);
        },

        run: function (name, env) {
            var callbacks = _.hooks.all[name];

            if (!callbacks || !callbacks.length) {
                return;
            }

            for (var i=0, callback; callback = callbacks[i++];) {
                callback(env);
            }
        }
    }
};

var Token = _.Token = function(type, content, alias, matchedStr, greedy) {
    this.type = type;
    this.content = content;
    this.alias = alias;
    // Copy of the full string this token was created from
    this.matchedStr = matchedStr || null;
    this.greedy = !!greedy;
};

Token.stringify = function(o, language, parent) {
    if (typeof o == 'string') {
        return o;
    }

    if (_.util.type(o) === 'Array') {
        return o.map(function(element) {
            return Token.stringify(element, language, o);
        }).join('');
    }

    var env = {
        type: o.type,
        content: Token.stringify(o.content, language, parent),
        tag: 'span',
        classes: ['token', o.type],
        attributes: {},
        language: language,
        parent: parent
    };

    if (env.type == 'comment') {
        env.attributes['spellcheck'] = 'true';
    }

    if (o.alias) {
        var aliases = _.util.type(o.alias) === 'Array' ? o.alias : [o.alias];
        Array.prototype.push.apply(env.classes, aliases);
    }

    _.hooks.run('wrap', env);

    var attributes = '';

    for (var name in env.attributes) {
        attributes += (attributes ? ' ' : '') + name + '="' + (env.attributes[name] || '') + '"';
    }

    return '<' + env.tag + ' class="' + env.classes.join(' ') + '" ' + attributes + '>' + env.content + '</' + env.tag + '>';

};

if (!_self.document) {
    if (!_self.addEventListener) {
        // in Node.js
        return _self.Prism;
    }
    // In worker
    _self.addEventListener('message', function(evt) {
        var message = JSON.parse(evt.data),
            lang = message.language,
            code = message.code,
            immediateClose = message.immediateClose;

        _self.postMessage(_.highlight(code, _.languages[lang], lang));
        if (immediateClose) {
            _self.close();
        }
    }, false);

    return _self.Prism;
}

//Get current script and highlight
var script = document.currentScript || [].slice.call(document.getElementsByTagName("script")).pop();

if (script) {
    _.filename = script.src;

    if (document.addEventListener && !script.hasAttribute('data-manual')) {
        document.addEventListener('DOMContentLoaded', _.highlightAll);
    }
}

return _self.Prism;

})();

if (typeof module !== 'undefined' && module.exports) {
    module.exports = Prism;
}

// hack for components to work correctly in node.js
if (typeof global !== 'undefined') {
    global.Prism = Prism;
}
;
Prism.languages.markup = {
    'comment': /<!--[\w\W]*?-->/,
    'prolog': /<\?[\w\W]+?\?>/,
    'doctype': /<!DOCTYPE[\w\W]+?>/,
    'cdata': /<!\[CDATA\[[\w\W]*?]]>/i,
    'tag': {
        pattern: /<\/?(?!\d)[^\s>\/=.$<]+(?:\s+[^\s>\/=]+(?:=(?:("|')(?:\\\1|\\?(?!\1)[\w\W])*\1|[^\s'">=]+))?)*\s*\/?>/i,
        inside: {
            'tag': {
                pattern: /^<\/?[^\s>\/]+/i,
                inside: {
                    'punctuation': /^<\/?/,
                    'namespace': /^[^\s>\/:]+:/
                }
            },
            'attr-value': {
                pattern: /=(?:('|")[\w\W]*?(\1)|[^\s>]+)/i,
                inside: {
                    'punctuation': /[=>"']/
                }
            },
            'punctuation': /\/?>/,
            'attr-name': {
                pattern: /[^\s>\/]+/,
                inside: {
                    'namespace': /^[^\s>\/:]+:/
                }
            }

        }
    },
    'entity': /&#?[\da-z]{1,8};/i
};

// Plugin to make entity title show the real entity, idea by Roman Komarov
Prism.hooks.add('wrap', function(env) {

    if (env.type === 'entity') {
        env.attributes['title'] = env.content.replace(/&amp;/, '&');
    }
});

Prism.languages.xml = Prism.languages.markup;
Prism.languages.html = Prism.languages.markup;
Prism.languages.mathml = Prism.languages.markup;
Prism.languages.svg = Prism.languages.markup;

Prism.languages.css = {
    'comment': /\/\*[\w\W]*?\*\//,
    'atrule': {
        pattern: /@[\w-]+?.*?(;|(?=\s*\{))/i,
        inside: {
            'rule': /@[\w-]+/
            // See rest below
        }
    },
    'url': /url\((?:(["'])(\\(?:\r\n|[\w\W])|(?!\1)[^\\\r\n])*\1|.*?)\)/i,
    'selector': /[^\{\}\s][^\{\};]*?(?=\s*\{)/,
    'string': /("|')(\\(?:\r\n|[\w\W])|(?!\1)[^\\\r\n])*\1/,
    'property': /(\b|\B)[\w-]+(?=\s*:)/i,
    'important': /\B!important\b/i,
    'function': /[-a-z0-9]+(?=\()/i,
    'punctuation': /[(){};:]/
};

Prism.languages.css['atrule'].inside.rest = Prism.util.clone(Prism.languages.css);

if (Prism.languages.markup) {
    Prism.languages.insertBefore('markup', 'tag', {
        'style': {
            pattern: /(<style[\w\W]*?>)[\w\W]*?(?=<\/style>)/i,
            lookbehind: true,
            inside: Prism.languages.css,
            alias: 'language-css'
        }
    });

    Prism.languages.insertBefore('inside', 'attr-value', {
        'style-attr': {
            pattern: /\s*style=("|').*?\1/i,
            inside: {
                'attr-name': {
                    pattern: /^\s*style/i,
                    inside: Prism.languages.markup.tag.inside
                },
                'punctuation': /^\s*=\s*['"]|['"]\s*$/,
                'attr-value': {
                    pattern: /.+/i,
                    inside: Prism.languages.css
                }
            },
            alias: 'language-css'
        }
    }, Prism.languages.markup.tag);
};
Prism.languages.clike = {
    'comment': [
        {
            pattern: /(^|[^\\])\/\*[\w\W]*?\*\//,
            lookbehind: true
        },
        {
            pattern: /(^|[^\\:])\/\/.*/,
            lookbehind: true
        }
    ],
    'string': {
        pattern: /(["'])(\\(?:\r\n|[\s\S])|(?!\1)[^\\\r\n])*\1/,
        greedy: true
    },
    'class-name': {
        pattern: /((?:\b(?:class|interface|extends|implements|trait|instanceof|new)\s+)|(?:catch\s+\())[a-z0-9_\.\\]+/i,
        lookbehind: true,
        inside: {
            punctuation: /(\.|\\)/
        }
    },
    'keyword': /\b(if|else|while|do|for|return|in|instanceof|function|new|try|throw|catch|finally|null|break|continue)\b/,
    'boolean': /\b(true|false)\b/,
    'function': /[a-z0-9_]+(?=\()/i,
    'number': /\b-?(?:0x[\da-f]+|\d*\.?\d+(?:e[+-]?\d+)?)\b/i,
    'operator': /--?|\+\+?|!=?=?|<=?|>=?|==?=?|&&?|\|\|?|\?|\*|\/|~|\^|%/,
    'punctuation': /[{}[\];(),.:]/
};

Prism.languages.javascript = Prism.languages.extend('clike', {
    'keyword': /\b(as|async|await|break|case|catch|class|const|continue|debugger|default|delete|do|else|enum|export|extends|finally|for|from|function|get|if|implements|import|in|instanceof|interface|let|new|null|of|package|private|protected|public|return|set|static|super|switch|this|throw|try|typeof|var|void|while|with|yield)\b/,
    'number': /\b-?(0x[\dA-Fa-f]+|0b[01]+|0o[0-7]+|\d*\.?\d+([Ee][+-]?\d+)?|NaN|Infinity)\b/,
    // Allow for all non-ASCII characters (See http://stackoverflow.com/a/2008444)
    'function': /[_$a-zA-Z\xA0-\uFFFF][_$a-zA-Z0-9\xA0-\uFFFF]*(?=\()/i
});

Prism.languages.insertBefore('javascript', 'keyword', {
    'regex': {
        pattern: /(^|[^/])\/(?!\/)(\[.+?]|\\.|[^/\\\r\n])+\/[gimyu]{0,5}(?=\s*($|[\r\n,.;})]))/,
        lookbehind: true,
        greedy: true
    }
});

Prism.languages.insertBefore('javascript', 'class-name', {
    'template-string': {
        pattern: /`(?:\\\\|\\?[^\\])*?`/,
        greedy: true,
        inside: {
            'interpolation': {
                pattern: /\$\{[^}]+\}/,
                inside: {
                    'interpolation-punctuation': {
                        pattern: /^\$\{|\}$/,
                        alias: 'punctuation'
                    },
                    rest: Prism.languages.javascript
                }
            },
            'string': /[\s\S]+/
        }
    }
});

if (Prism.languages.markup) {
    Prism.languages.insertBefore('markup', 'tag', {
        'script': {
            pattern: /(<script[\w\W]*?>)[\w\W]*?(?=<\/script>)/i,
            lookbehind: true,
            inside: Prism.languages.javascript,
            alias: 'language-javascript'
        }
    });
}

Prism.languages.js = Prism.languages.javascript;
Prism.languages.aspnet = Prism.languages.extend('markup', {
    'page-directive tag': {
        pattern: /<%\s*@.*%>/i,
        inside: {
            'page-directive tag': /<%\s*@\s*(?:Assembly|Control|Implements|Import|Master(?:Type)?|OutputCache|Page|PreviousPageType|Reference|Register)?|%>/i,
            rest: Prism.languages.markup.tag.inside
        }
    },
    'directive tag': {
        pattern: /<%.*%>/i,
        inside: {
            'directive tag': /<%\s*?[$=%#:]{0,2}|%>/i,
            rest: Prism.languages.csharp
        }
    }
});
// Regexp copied from prism-markup, with a negative look-ahead added
Prism.languages.aspnet.tag.pattern = /<(?!%)\/?[^\s>\/]+(?:\s+[^\s>\/=]+(?:=(?:("|')(?:\\\1|\\?(?!\1)[\w\W])*\1|[^\s'">=]+))?)*\s*\/?>/i;

// match directives of attribute value foo="<% Bar %>"
Prism.languages.insertBefore('inside', 'punctuation', {
    'directive tag': Prism.languages.aspnet['directive tag']
}, Prism.languages.aspnet.tag.inside["attr-value"]);

Prism.languages.insertBefore('aspnet', 'comment', {
    'asp comment': /<%--[\w\W]*?--%>/
});

// script runat="server" contains csharp, not javascript
Prism.languages.insertBefore('aspnet', Prism.languages.javascript ? 'script' : 'tag', {
    'asp script': {
        pattern: /(<script(?=.*runat=['"]?server['"]?)[\w\W]*?>)[\w\W]*?(?=<\/script>)/i,
        lookbehind: true,
        inside: Prism.languages.csharp || {}
    }
});
Prism.languages.csharp = Prism.languages.extend('clike', {
    'keyword': /\b(abstract|as|async|await|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|async|await|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b/,
    'string': [
        /@("|')(\1\1|\\\1|\\?(?!\1)[\s\S])*\1/,
        /("|')(\\?.)*?\1/
    ],
    'number': /\b-?(0x[\da-f]+|\d*\.?\d+f?)\b/i
});

Prism.languages.insertBefore('csharp', 'keyword', {
    'preprocessor': {
        pattern: /(^\s*)#.*/m,
        lookbehind: true,
        alias: 'property',
        inside: {
            // highlight preprocessor directives as keywords
            'directive': {
                pattern: /(\s*#)\b(define|elif|else|endif|endregion|error|if|line|pragma|region|undef|warning)\b/,
                lookbehind: true,
                alias: 'keyword'
            }
        }
    }
});

Prism.languages.json = {
    'property': /".*?"(?=\s*:)/ig,
    'string': /"(?!:)(\\?[^"])*?"(?!:)/g,
    'number': /\b-?(0x[\dA-Fa-f]+|\d*\.?\d+([Ee]-?\d+)?)\b/g,
    'punctuation': /[{}[\]);,]/g,
    'operator': /:/g,
    'boolean': /\b(true|false)\b/gi,
    'null': /\bnull\b/gi,
};

Prism.languages.jsonp = Prism.languages.json;

/* FIXME :
 :extend() is not handled specifically : its highlighting is buggy.
 Mixin usage must be inside a ruleset to be highlighted.
 At-rules (e.g. import) containing interpolations are buggy.
 Detached rulesets are highlighted as at-rules.
 A comment before a mixin usage prevents the latter to be properly highlighted.
 */

Prism.languages.less = Prism.languages.extend('css', {
    'comment': [
        /\/\*[\w\W]*?\*\//,
        {
            pattern: /(^|[^\\])\/\/.*/,
            lookbehind: true
        }
    ],
    'atrule': {
        pattern: /@[\w-]+?(?:\([^{}]+\)|[^(){};])*?(?=\s*\{)/i,
        inside: {
            'punctuation': /[:()]/
        }
    },
    // selectors and mixins are considered the same
    'selector': {
        pattern: /(?:@\{[\w-]+\}|[^{};\s@])(?:@\{[\w-]+\}|\([^{}]*\)|[^{};@])*?(?=\s*\{)/,
        inside: {
            // mixin parameters
            'variable': /@+[\w-]+/
        }
    },

    'property': /(?:@\{[\w-]+\}|[\w-])+(?:\+_?)?(?=\s*:)/i,
    'punctuation': /[{}();:,]/,
    'operator': /[+\-*\/]/
});

// Invert function and punctuation positions
Prism.languages.insertBefore('less', 'punctuation', {
    'function': Prism.languages.less.function
});

Prism.languages.insertBefore('less', 'property', {
    'variable': [
        // Variable declaration (the colon must be consumed!)
        {
            pattern: /@[\w-]+\s*:/,
            inside: {
                "punctuation": /:/
            }
        },

        // Variable usage
        /@@?[\w-]+/
    ],
    'mixin-usage': {
        pattern: /([{;]\s*)[.#](?!\d)[\w-]+.*?(?=[(;])/,
        lookbehind: true,
        alias: 'function'
    }
});

Prism.languages.sql= {
    'comment': {
        pattern: /(^|[^\\])(?:\/\*[\w\W]*?\*\/|(?:--|\/\/|#).*)/,
        lookbehind: true
    },
    'string' : {
        pattern: /(^|[^@\\])("|')(?:\\?[\s\S])*?\2/,
        lookbehind: true
    },
    'variable': /@[\w.$]+|@("|'|`)(?:\\?[\s\S])+?\1/,
    'function': /\b(?:COUNT|SUM|AVG|MIN|MAX|FIRST|LAST|UCASE|LCASE|MID|LEN|ROUND|NOW|FORMAT)(?=\s*\()/i, // Should we highlight user defined functions too?
    'keyword': /\b(?:ACTION|ADD|AFTER|ALGORITHM|ALL|ALTER|ANALYZE|ANY|APPLY|AS|ASC|AUTHORIZATION|BACKUP|BDB|BEGIN|BERKELEYDB|BIGINT|BINARY|BIT|BLOB|BOOL|BOOLEAN|BREAK|BROWSE|BTREE|BULK|BY|CALL|CASCADED?|CASE|CHAIN|CHAR VARYING|CHARACTER (?:SET|VARYING)|CHARSET|CHECK|CHECKPOINT|CLOSE|CLUSTERED|COALESCE|COLLATE|COLUMN|COLUMNS|COMMENT|COMMIT|COMMITTED|COMPUTE|CONNECT|CONSISTENT|CONSTRAINT|CONTAINS|CONTAINSTABLE|CONTINUE|CONVERT|CREATE|CROSS|CURRENT(?:_DATE|_TIME|_TIMESTAMP|_USER)?|CURSOR|DATA(?:BASES?)?|DATETIME|DBCC|DEALLOCATE|DEC|DECIMAL|DECLARE|DEFAULT|DEFINER|DELAYED|DELETE|DENY|DESC|DESCRIBE|DETERMINISTIC|DISABLE|DISCARD|DISK|DISTINCT|DISTINCTROW|DISTRIBUTED|DO|DOUBLE(?: PRECISION)?|DROP|DUMMY|DUMP(?:FILE)?|DUPLICATE KEY|ELSE|ENABLE|ENCLOSED BY|END|ENGINE|ENUM|ERRLVL|ERRORS|ESCAPE(?:D BY)?|EXCEPT|EXEC(?:UTE)?|EXISTS|EXIT|EXPLAIN|EXTENDED|FETCH|FIELDS|FILE|FILLFACTOR|FIRST|FIXED|FLOAT|FOLLOWING|FOR(?: EACH ROW)?|FORCE|FOREIGN|FREETEXT(?:TABLE)?|FROM|FULL|FUNCTION|GEOMETRY(?:COLLECTION)?|GLOBAL|GOTO|GRANT|GROUP|HANDLER|HASH|HAVING|HOLDLOCK|IDENTITY(?:_INSERT|COL)?|IF|IGNORE|IMPORT|INDEX|INFILE|INNER|INNODB|INOUT|INSERT|INT|INTEGER|INTERSECT|INTO|INVOKER|ISOLATION LEVEL|JOIN|KEYS?|KILL|LANGUAGE SQL|LAST|LEFT|LIMIT|LINENO|LINES|LINESTRING|LOAD|LOCAL|LOCK|LONG(?:BLOB|TEXT)|MATCH(?:ED)?|MEDIUM(?:BLOB|INT|TEXT)|MERGE|MIDDLEINT|MODIFIES SQL DATA|MODIFY|MULTI(?:LINESTRING|POINT|POLYGON)|NATIONAL(?: CHAR VARYING| CHARACTER(?: VARYING)?| VARCHAR)?|NATURAL|NCHAR(?: VARCHAR)?|NEXT|NO(?: SQL|CHECK|CYCLE)?|NONCLUSTERED|NULLIF|NUMERIC|OFF?|OFFSETS?|ON|OPEN(?:DATASOURCE|QUERY|ROWSET)?|OPTIMIZE|OPTION(?:ALLY)?|ORDER|OUT(?:ER|FILE)?|OVER|PARTIAL|PARTITION|PERCENT|PIVOT|PLAN|POINT|POLYGON|PRECEDING|PRECISION|PREV|PRIMARY|PRINT|PRIVILEGES|PROC(?:EDURE)?|PUBLIC|PURGE|QUICK|RAISERROR|READ(?:S SQL DATA|TEXT)?|REAL|RECONFIGURE|REFERENCES|RELEASE|RENAME|REPEATABLE|REPLICATION|REQUIRE|RESTORE|RESTRICT|RETURNS?|REVOKE|RIGHT|ROLLBACK|ROUTINE|ROW(?:COUNT|GUIDCOL|S)?|RTREE|RULE|SAVE(?:POINT)?|SCHEMA|SELECT|SERIAL(?:IZABLE)?|SESSION(?:_USER)?|SET(?:USER)?|SHARE MODE|SHOW|SHUTDOWN|SIMPLE|SMALLINT|SNAPSHOT|SOME|SONAME|START(?:ING BY)?|STATISTICS|STATUS|STRIPED|SYSTEM_USER|TABLES?|TABLESPACE|TEMP(?:ORARY|TABLE)?|TERMINATED BY|TEXT(?:SIZE)?|THEN|TIMESTAMP|TINY(?:BLOB|INT|TEXT)|TOP?|TRAN(?:SACTIONS?)?|TRIGGER|TRUNCATE|TSEQUAL|TYPES?|UNBOUNDED|UNCOMMITTED|UNDEFINED|UNION|UNIQUE|UNPIVOT|UPDATE(?:TEXT)?|USAGE|USE|USER|USING|VALUES?|VAR(?:BINARY|CHAR|CHARACTER|YING)|VIEW|WAITFOR|WARNINGS|WHEN|WHERE|WHILE|WITH(?: ROLLUP|IN)?|WORK|WRITE(?:TEXT)?)\b/i,
    'boolean': /\b(?:TRUE|FALSE|NULL)\b/i,
    'number': /\b-?(?:0x)?\d*\.?[\da-f]+\b/,
    'operator': /[-+*\/=%^~]|&&?|\|?\||!=?|<(?:=>?|<|>)?|>[>=]?|\b(?:AND|BETWEEN|IN|LIKE|NOT|OR|IS|DIV|REGEXP|RLIKE|SOUNDS LIKE|XOR)\b/i,
    'punctuation': /[;[\]()`,.]/
};
Prism.languages.typescript = Prism.languages.extend('javascript', {
    'keyword': /\b(break|case|catch|class|const|continue|debugger|default|delete|do|else|enum|export|extends|false|finally|for|function|get|if|implements|import|in|instanceof|interface|let|new|null|package|private|protected|public|return|set|static|super|switch|this|throw|true|try|typeof|var|void|while|with|yield|module|declare|constructor|string|Function|any|number|boolean|Array|enum)\b/
});

(function(){

if (typeof self === 'undefined' || !self.Prism || !self.document || !document.querySelector) {
    return;
}

function $$(expr, con) {
    return Array.prototype.slice.call((con || document).querySelectorAll(expr));
}

function hasClass(element, className) {
  className = " " + className + " ";
  return (" " + element.className + " ").replace(/[\n\t]/g, " ").indexOf(className) > -1
}

// Some browsers round the line-height, others don't.
// We need to test for it to position the elements properly.
var isLineHeightRounded = (function() {
    var res;
    return function() {
        if(typeof res === 'undefined') {
            var d = document.createElement('div');
            d.style.fontSize = '13px';
            d.style.lineHeight = '1.5';
            d.style.padding = 0;
            d.style.border = 0;
            d.innerHTML = '&nbsp;<br />&nbsp;';
            document.body.appendChild(d);
            // Browsers that round the line-height should have offsetHeight === 38
            // The others should have 39.
            res = d.offsetHeight === 38;
            document.body.removeChild(d);
        }
        return res;
    }
}());

function highlightLines(pre, lines, classes) {
    var ranges = lines.replace(/\s+/g, '').split(','),
        offset = +pre.getAttribute('data-line-offset') || 0;

    var parseMethod = isLineHeightRounded() ? parseInt : parseFloat;
    var lineHeight = parseMethod(getComputedStyle(pre).lineHeight);

    for (var i=0, range; range = ranges[i++];) {
        range = range.split('-');

        var start = +range[0],
            end = +range[1] || start;

        var line = document.createElement('div');

        line.textContent = Array(end - start + 2).join(' \n');
        line.className = (classes || '') + ' line-highlight';

    //if the line-numbers plugin is enabled, then there is no reason for this plugin to display the line numbers
    if(!hasClass(pre, 'line-numbers')) {
      line.setAttribute('data-start', start);

      if(end > start) {
        line.setAttribute('data-end', end);
      }
    }

        line.style.top = (start - offset - 1) * lineHeight + 'px';

    //allow this to play nicely with the line-numbers plugin
    if(hasClass(pre, 'line-numbers')) {
      //need to attack to pre as when line-numbers is enabled, the code tag is relatively which screws up the positioning
      pre.appendChild(line);
    } else {
      (pre.querySelector('code') || pre).appendChild(line);
    }
    }
}

function applyHash() {
    var hash = location.hash.slice(1);

    // Remove pre-existing temporary lines
    $$('.temporary.line-highlight').forEach(function (line) {
        line.parentNode.removeChild(line);
    });

    var range = (hash.match(/\.([\d,-]+)$/) || [,''])[1];

    if (!range || document.getElementById(hash)) {
        return;
    }

    var id = hash.slice(0, hash.lastIndexOf('.')),
        pre = document.getElementById(id);

    if (!pre) {
        return;
    }

    if (!pre.hasAttribute('data-line')) {
        pre.setAttribute('data-line', '');
    }

    highlightLines(pre, range, 'temporary ');

    document.querySelector('.temporary.line-highlight').scrollIntoView();
}

var fakeTimer = 0; // Hack to limit the number of times applyHash() runs

Prism.hooks.add('complete', function(env) {
    var pre = env.element.parentNode;
    var lines = pre && pre.getAttribute('data-line');

    if (!pre || !lines || !/pre/i.test(pre.nodeName)) {
        return;
    }

    clearTimeout(fakeTimer);

    $$('.line-highlight', pre).forEach(function (line) {
        line.parentNode.removeChild(line);
    });

    highlightLines(pre, lines);

    fakeTimer = setTimeout(applyHash, 1);
});

if(window.addEventListener) {
    window.addEventListener('hashchange', applyHash);
}

})();

(function() {

if (typeof self === 'undefined' || !self.Prism || !self.document) {
    return;
}

Prism.hooks.add('complete', function (env) {
    if (!env.code) {
        return;
    }

    // works only for <code> wrapped inside <pre> (not inline)
    var pre = env.element.parentNode;
    var clsReg = /\s*\bline-numbers\b\s*/;
    if (
        !pre || !/pre/i.test(pre.nodeName) ||
            // Abort only if nor the <pre> nor the <code> have the class
        (!clsReg.test(pre.className) && !clsReg.test(env.element.className))
    ) {
        return;
    }

    if (env.element.querySelector(".line-numbers-rows")) {
        // Abort if line numbers already exists
        return;
    }

    if (clsReg.test(env.element.className)) {
        // Remove the class "line-numbers" from the <code>
        env.element.className = env.element.className.replace(clsReg, '');
    }
    if (!clsReg.test(pre.className)) {
        // Add the class "line-numbers" to the <pre>
        pre.className += ' line-numbers';
    }

    var match = env.code.match(/\n(?!$)/g);
    var linesNum = match ? match.length + 1 : 1;
    var lineNumbersWrapper;

    var lines = new Array(linesNum + 1);
    lines = lines.join('<span></span>');

    lineNumbersWrapper = document.createElement('span');
    lineNumbersWrapper.className = 'line-numbers-rows';
    lineNumbersWrapper.innerHTML = lines;

    if (pre.hasAttribute('data-start')) {
        pre.style.counterReset = 'linenumber ' + (parseInt(pre.getAttribute('data-start'), 10) - 1);
    }

    env.element.appendChild(lineNumbersWrapper);

});

}());
(function() {
    if ( !self.Prism || !self.document || !document.querySelectorAll || ![].filter) return;

    var adapters = [];
    function registerAdapter(adapter) {
        if (typeof adapter === "function" && !getAdapter(adapter)) {
            adapters.push(adapter);
        }
    }
    function getAdapter(adapter) {
        if (typeof adapter === "function") {
            return adapters.filter(function(fn) { return fn.valueOf() === adapter.valueOf()})[0];
        }
        else if (typeof adapter === "string" && adapter.length > 0) {
            return adapters.filter(function(fn) { return fn.name === adapter})[0];
        }
        return null;
    }
    function removeAdapter(adapter) {
        if (typeof adapter === "string")
            adapter = getAdapter(adapter);
        if (typeof adapter === "function") {
            var index = adapters.indexOf(adapter);
            if (index >=0) {
                adapters.splice(index,1);
            }
        }
    }

    Prism.plugins.jsonphighlight = {
        registerAdapter: registerAdapter,
        removeAdapter: removeAdapter,
        highlight: highlight
    };
    registerAdapter(function github(rsp, el) {
        if ( rsp && rsp.meta && rsp.data ) {
            if ( rsp.meta.status && rsp.meta.status >= 400 ) {
                return "Error: " + ( rsp.data.message || rsp.meta.status );
            }
            else if ( typeof(rsp.data.content) === "string" ) {
                return typeof(atob) === "function"
                    ? atob(rsp.data.content.replace(/\s/g, ""))
                    : "Your browser cannot decode base64";
            }
        }
        return null;
    });
    registerAdapter(function gist(rsp, el) {
        if ( rsp && rsp.meta && rsp.data && rsp.data.files ) {
            if ( rsp.meta.status && rsp.meta.status >= 400 ) {
                return "Error: " + ( rsp.data.message || rsp.meta.status );
            }
            else {
                var filename = el.getAttribute("data-filename");
                if (filename == null) {
                    // Maybe in the future we can somehow render all files
                    // But the standard <script> include for gists does that nicely already,
                    // so that might be getting beyond the scope of this plugin
                    for (var key in rsp.data.files) {
                        if (rsp.data.files.hasOwnProperty(key)) {
                            filename = key;
                            break;
                        }
                    }
                }
                if (rsp.data.files[filename] !== undefined) {
                    return rsp.data.files[filename].content;
                }
                else {
                    return "Error: unknown or missing gist file " + filename;
                }
            }
        }
        return null;
    });
    registerAdapter(function bitbucket(rsp, el) {
        return rsp && rsp.node && typeof(rsp.data) === "string"
            ? rsp.data
            : null;
    });

    var jsonpcb = 0,
        loadstr = "Loading…";

    function highlight() {
        Array.prototype.slice.call(document.querySelectorAll("pre[data-jsonp]")).forEach(function(pre) {
            pre.textContent = "";

            var code = document.createElement("code");
            code.textContent = loadstr;
            pre.appendChild(code);

            var adapterfn = pre.getAttribute("data-adapter");
            var adapter = null;
            if ( adapterfn ) {
                if ( typeof(window[adapterfn]) === "function" ) {
                    adapter = window[adapterfn];
                }
                else {
                    code.textContent = "JSONP adapter function '" + adapterfn + "' doesn't exist";
                    return;
                }
            }

            var cb = "prismjsonp" + ( jsonpcb++ );

            var uri = document.createElement("a");
            var src = uri.href = pre.getAttribute("data-jsonp");
            uri.href += ( uri.search ? "&" : "?" ) + ( pre.getAttribute("data-callback") || "callback" ) + "=" + cb;

            var timeout = setTimeout(function() {
                // we could clean up window[cb], but if the request finally succeeds, keeping it around is a good thing
                if ( code.textContent === loadstr )
                    code.textContent = "Timeout loading '" + src + "'";
            }, 5000);

            var script = document.createElement("script");
            script.src = uri.href;

            window[cb] = function(rsp) {
                document.head.removeChild(script);
                clearTimeout(timeout);
                delete window[cb];

                var data = "";

                if ( adapter ) {
                    data = adapter(rsp, pre);
                }
                else {
                    for ( var p in adapters ) {
                        data = adapters[p](rsp, pre);
                        if ( data !== null ) break;
                    }
                }

                if (data === null) {
                    code.textContent = "Cannot parse response (perhaps you need an adapter function?)";
                }
                else {
                    code.textContent = data;
                    Prism.highlightElement(code);
                }
            };

            document.head.appendChild(script);
        });
    }

    highlight();
})();
(function () {
    if (typeof self === 'undefined' || !self.Prism || !self.document || !document.createElement) {
        return;
    }

    // The dependencies map is built automatically with gulp
    var lang_dependencies = /*languages_placeholder[*/{"javascript":"clike","actionscript":"javascript","aspnet":"markup","bison":"c","c":"clike","csharp":"clike","cpp":"c","coffeescript":"javascript","crystal":"ruby","css-extras":"css","d":"clike","dart":"clike","fsharp":"clike","glsl":"clike","go":"clike","groovy":"clike","haml":"ruby","handlebars":"markup","haxe":"clike","jade":"javascript","java":"clike","kotlin":"clike","less":"css","markdown":"markup","nginx":"clike","objectivec":"c","parser":"markup","php":"clike","php-extras":"php","processing":"clike","qore":"clike","jsx":["markup","javascript"],"ruby":"clike","sass":"css","scss":"css","scala":"java","smarty":"markup","swift":"clike","textile":"markup","twig":"markup","typescript":"javascript","wiki":"markup"}/*]*/;

    var lang_data = {};

    var config = Prism.plugins.autoloader = {
        languages_path: 'components/',
        use_minified: true
    };

    /**
     * Lazy loads an external script
     * @param {string} src
     * @param {function=} success
     * @param {function=} error
     */
    var script = function (src, success, error) {
        var s = document.createElement('script');
        s.src = src;
        s.async = true;
        s.onload = function() {
            document.body.removeChild(s);
            success && success();
        };
        s.onerror = function() {
            document.body.removeChild(s);
            error && error();
        };
        document.body.appendChild(s);
    };

    /**
     * Returns the path to a grammar, using the language_path and use_minified config keys.
     * @param {string} lang
     * @returns {string}
     */
    var getLanguagePath = function (lang) {
        return config.languages_path +
            'prism-' + lang
            + (config.use_minified ? '.min' : '') + '.js'
    };

    /**
     * Tries to load a grammar and
     * highlight again the given element once loaded.
     * @param {string} lang
     * @param {HTMLElement} elt
     */
    var registerElement = function (lang, elt) {
        var data = lang_data[lang];
        if (!data) {
            data = lang_data[lang] = {};
        }

        // Look for additional dependencies defined on the <code> or <pre> tags
        var deps = elt.getAttribute('data-dependencies');
        if (!deps && elt.parentNode && elt.parentNode.tagName.toLowerCase() === 'pre') {
            deps = elt.parentNode.getAttribute('data-dependencies');
        }

        if (deps) {
            deps = deps.split(/\s*,\s*/g);
        } else {
            deps = [];
        }

        loadLanguages(deps, function () {
            loadLanguage(lang, function () {
                Prism.highlightElement(elt);
            });
        });
    };

    /**
     * Sequentially loads an array of grammars.
     * @param {string[]|string} langs
     * @param {function=} success
     * @param {function=} error
     */
    var loadLanguages = function (langs, success, error) {
        if (typeof langs === 'string') {
            langs = [langs];
        }
        var i = 0;
        var l = langs.length;
        var f = function () {
            if (i < l) {
                loadLanguage(langs[i], function () {
                    i++;
                    f();
                }, function () {
                    error && error(langs[i]);
                });
            } else if (i === l) {
                success && success(langs);
            }
        };
        f();
    };

    /**
     * Load a grammar with its dependencies
     * @param {string} lang
     * @param {function=} success
     * @param {function=} error
     */
    var loadLanguage = function (lang, success, error) {
        var load = function () {
            var force = false;
            // Do we want to force reload the grammar?
            if (lang.indexOf('!') >= 0) {
                force = true;
                lang = lang.replace('!', '');
            }

            var data = lang_data[lang];
            if (!data) {
                data = lang_data[lang] = {};
            }
            if (success) {
                if (!data.success_callbacks) {
                    data.success_callbacks = [];
                }
                data.success_callbacks.push(success);
            }
            if (error) {
                if (!data.error_callbacks) {
                    data.error_callbacks = [];
                }
                data.error_callbacks.push(error);
            }

            if (!force && Prism.languages[lang]) {
                languageSuccess(lang);
            } else if (!force && data.error) {
                languageError(lang);
            } else if (force || !data.loading) {
                data.loading = true;
                var src = getLanguagePath(lang);
                script(src, function () {
                    data.loading = false;
                    languageSuccess(lang);

                }, function () {
                    data.loading = false;
                    data.error = true;
                    languageError(lang);
                });
            }
        };
        var dependencies = lang_dependencies[lang];
        if(dependencies && dependencies.length) {
            loadLanguages(dependencies, load);
        } else {
            load();
        }
    };

    /**
     * Runs all success callbacks for this language.
     * @param {string} lang
     */
    var languageSuccess = function (lang) {
        if (lang_data[lang] && lang_data[lang].success_callbacks && lang_data[lang].success_callbacks.length) {
            lang_data[lang].success_callbacks.forEach(function (f) {
                f(lang);
            });
        }
    };

    /**
     * Runs all error callbacks for this language.
     * @param {string} lang
     */
    var languageError = function (lang) {
        if (lang_data[lang] && lang_data[lang].error_callbacks && lang_data[lang].error_callbacks.length) {
            lang_data[lang].error_callbacks.forEach(function (f) {
                f(lang);
            });
        }
    };

    Prism.hooks.add('complete', function (env) {
        if (env.element && env.language && !env.grammar) {
            registerElement(env.language, env.element);
        }
    });

}());
