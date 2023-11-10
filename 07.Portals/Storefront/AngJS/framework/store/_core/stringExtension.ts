var proto: any = String.prototype;

proto.startsWith = function (prefix: string) {
    return this.slice(0, prefix.length) === prefix;
};

proto.endsWith = function (suffix: string) {
    return this.slice(-suffix.length) === suffix;
};
