/**
 * @file React/src/_shared/customHooks/useCustomCompareEffect.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc useCustomCompareEffect hook
 */
import { useRef, useEffect } from 'react';

function useCustomCompareMemo<T>(value: T, equalFn: (prev: T, current: T) => boolean): T {
  const ref = useRef<T>(value);

  if (!equalFn(value, ref.current)) {
    ref.current = value;
  }

  return ref.current;
}

function useCustomCompareEffect<T>(create: () => void | (() => void), input: T, equal: (prev: T, current: T) => boolean) {
  useEffect(create, [useCustomCompareMemo(input, equal)]);
}

export default useCustomCompareEffect;