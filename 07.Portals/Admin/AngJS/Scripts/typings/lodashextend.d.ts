declare namespace _ {
    //_.flatMapDeep
    interface LoDashStatic {
        /**
         * Creates an array of flattened values by running each element in collection through iteratee
         * and concating its result to the other mapped values. The iteratee is invoked with three arguments:
         * (value, index|key, collection).
         *
         * @param collection The collection to iterate over.
         * @param iteratee The function invoked per iteration.
         * @return Returns the new flattened array.
         */
        flatMapDeep<T, TResult>(
            collection: List<T>,
            iteratee?: ListIterator<T, TResult | TResult[]>
        ): TResult[];

        /**
         * @see _.flatMapDeep
         */
        flatMapDeep<TResult>(
            collection: List<any>,
            iteratee?: ListIterator<any, TResult | TResult[]>
        ): TResult[];

        /**
         * @see _.flatMapDeep
         */
        flatMapDeep<T, TResult>(
            collection: Dictionary<T>,
            iteratee?: DictionaryIterator<T, TResult | TResult[]>
        ): TResult[];

        /**
         * @see _.flatMapDeep
         */
        flatMapDeep<TResult>(
            collection: Dictionary<any>,
            iteratee?: DictionaryIterator<any, TResult | TResult[]>
        ): TResult[];

        /**
         * @see _.flatMapDeep
         */
        flatMapDeep<T, TResult>(
            collection: NumericDictionary<T>,
            iteratee?: NumericDictionaryIterator<T, TResult | TResult[]>
        ): TResult[];

        /**
         * @see _.flatMapDeep
         */
        flatMapDeep<TResult>(
            collection: NumericDictionary<any>,
            iteratee?: NumericDictionaryIterator<any, TResult | TResult[]>
        ): TResult[];

        /**
         * @see _.flatMapDeep
         */
        flatMapDeep<TObject extends Object, TResult>(
            collection: TObject,
            iteratee?: ObjectIterator<any, TResult | TResult[]>
        ): TResult[];

        /**
         * @see _.flatMapDeep
         */
        flatMapDeep<TResult>(
            collection: Object,
            iteratee?: ObjectIterator<any, TResult | TResult[]>
        ): TResult[];

        /**
         * @see _.flatMapDeep
         */
        flatMapDeep<TWhere extends Object, TObject extends Object>(
            collection: TObject,
            iteratee: TWhere
        ): boolean[];

        /**
         * @see _.flatMapDeep
         */
        flatMapDeep<TObject extends Object, TResult>(
            collection: TObject,
            iteratee: Object | string
        ): TResult[];

        /**
         * @see _.flatMapDeep
         */
        flatMapDeep<TObject extends Object>(
            collection: TObject,
            iteratee: [string, any]
        ): boolean[];

        /**
         * @see _.flatMapDeep
         */
        flatMapDeep<TResult>(
            collection: string
        ): string[];

        /**
         * @see _.flatMapDeep
         */
        flatMapDeep<TResult>(
            collection: Object,
            iteratee?: Object | string
        ): TResult[];
    }

}



declare module "lodash/flatMapDeep" {
   const flatMapDeep: typeof _.flatMapDeep;
   export = flatMapDeep;
}
