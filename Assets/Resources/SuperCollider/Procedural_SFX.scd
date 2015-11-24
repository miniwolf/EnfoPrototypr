// WORKING
(
OSCdef.new(
	\toggle,
	{
		arg msg, time, addr, port;
		msg.postln;
		//x.set(\amp, msg[1].linexp(0,1,0.001,1));
		x = Synth.new(\sineTest);
	},
	'/main/toggle1'
);

OSCdef.new(
	\wind,
	{
		arg msg, time, addr, port;
		msg.postln;
		y = Synth.new(\windSFX);
	},
	'/main/wind1'
);
)

(
SynthDef.new(\sineTest, {
	arg noiseHz=8;
	var freq,amp,sig;
	freq = LFNoise0.kr(noiseHz).exprange(200,1000);
	amp = LFNoise1.kr(12).exprange(0.02,1);
	sig = SinOsc.ar(freq) * amp;
	Out.ar([0,1], sig);
}).add
)


(
SynthDef.new(\windSFX, {
	var sig, freq;
	freq = 700;
	sig = LPF.ar(PinkNoise.ar, freq) * LFNoise1.kr(1);
	sig = FreeVerb.ar(sig, 0.7, 0.8, 0.25);
	Out.ar([0,1], sig);
}).add
)



// TODO:
// SFX: Player wounded, enemy wounded, player swing sword, enemy swing sword...
// Target loses life (done)

// (Important!) Freshness of sound!:
// Random amplitude levels, use envelopes