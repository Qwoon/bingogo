export function useAnimate() {
  const { $anime } = useNuxtApp()

  let isAnimationPlaying = ref<boolean>(false)

  function animateFlipTile(elementId: string, duration: number = 200, delay: number = 0): void {
    if (isAnimationPlaying.value) return

    isAnimationPlaying.value = true

    // FIXME: It might be a better idea to flip +180deg and second time -180deg
    // so that we dont go above 360deg
    $anime({
      targets: [`#f-${elementId}`],
      scale: [{ value: 1 }, { value: 1 }, { value: 1 }],
      rotateY: { value: '+=180', delay: delay },
      easing: 'easeInOutSine',
      duration: duration
    })

    // this is a shit-hack, since I couldn't resolve it with CSS.
    // Anime.js resets the transformation and then rotates the element
    // Which causes the element to rotate 360 deg from the point of 180
    $anime({
      targets: [`#b-${elementId}`],
      scale: [{ value: 1 }, { value: 1 }, { value: 1 }],
      rotateY: { value: '+=360', delay: delay },
      easing: 'easeInOutSine',
      duration: duration
    })

    // timeout to be the same as the animation delay
    setTimeout(() => (isAnimationPlaying.value = false), duration + delay)
  }

  return { isAnimationPlaying, animateFlipTile }
}
