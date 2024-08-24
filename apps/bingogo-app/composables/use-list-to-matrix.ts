export function useListToMatrix<T>(list: T[], elementsPerSubArray: number) {
  let matrix = [],
    i: number,
    k: number;

  for (i = 0, k = -1; i < list.length; i++) {
    if (i % elementsPerSubArray === 0) {
      k++;
      matrix[k] = [];
    }

    matrix[k].push(list[i]);
  }

  return matrix;
}
