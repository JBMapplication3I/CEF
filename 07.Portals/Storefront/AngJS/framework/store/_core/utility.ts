/**
 * @file framework/store/_core/utility.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Utility class with functions
 */
module cef.store.core {
    export interface Guid extends String { };

    export class Utility {
        // Returns an absolute path if and only if the first argument begins with '/'
        static joinPaths(...paths: string[]): string {
            if (paths.length === 0) {
                throw new Error("You must specify at least one path.");
            }
            // Remove '/' from the ends of each path and remove empty paths
            const joinedPath = _.filter(_.map(paths,(path) => path.replace(/^\/|\/$/g, "")),
                (path) => path.length > 0).join("/");
            // Add a leading '/' if the first argument had one
            return (paths[0].charAt(0) === "/") ? `/${joinedPath}` : joinedPath;
        }

        static convertJSONDate(input: string): Date {
            if (input && input.length > 0 && input.startsWith("/Date(")) {
                return new Date(parseInt(input.substr(6)));
            }
            // Some other format, try to return as a date
            return new Date(input);
        }

        static newGuid() {
            return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, c => {
                const r = Math.random() * 16 | 0, v = c === "x" ? r : (r & 0x3 | 0x8);
                return v.toString(16);
            });
        }
    }
}

interface Window {
    encodeURIComponent: Function;
}
