/**
 * @file framework/store/_services/cvFormValidationService.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Form validation service class
 */
module cef.store.services {
    export interface IFormValidationService {
        validate(paramsArray: Array<ng.IFormController>): ng.IPromise<boolean>;
    }

    export class FormValidationService implements IFormValidationService {
        // Functions
        validate(paramsArray: Array<ng.IFormController>): ng.IPromise<boolean> {
            paramsArray.forEach(x => x.$setSubmitted());
            const validate = _.filter(paramsArray, x => x.$invalid);
            return this.$q.resolve(validate.length <= 0);
        }
        // Constructor
        constructor(private readonly $q: ng.IQService) { }
    }
}
