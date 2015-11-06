// Wind ambient
{LPF.ar(PinkNoise.ar, 700) * LFNoise2.kr(1)}.play


SynthDef.new(\wind, {
	var sig;
	var freq=700;

	sig = LPF.ar(PinkNoise.ar, freq) * LFNoise1.kr(1);
	sig = FreeVerb.ar(sig, 0.7, 0.8, 0.25);
	Out.ar([0,1], sig);
}).add

~x = Synth.new(\wind);

// TODO: Changeable variables
//x.set(\freq, 300);
