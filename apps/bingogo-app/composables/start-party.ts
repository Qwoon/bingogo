import party from 'party-js';

export const useStartParty = () => {
  party.confetti(party.Rect.fromScreen(), {
    count: party.variation.range(100, 200),
  });
};
