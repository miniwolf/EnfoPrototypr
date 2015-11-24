
// Could be a base for background piece (war drums)
play{Limiter.ar(GVerb.ar(sin(Ringz.ar(Impulse.ar(2**LFNoise1.ar(1!1).range(1,2).round/4),[20,80,110,240,330,150],0.7).sum),5,3,0.7)/4)}



