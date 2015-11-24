// WORKING
(
OSCdef.new(
	\music,
	{
		arg msg, time, addr, port;
		msg.postln;
		//x.set(\amp, msg[1].linexp(0,1,0.001,1));
		x = Synth.new(\soundtrack);
	},
	'/main/music1'
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

OSCdef.new(
	\bubbles,
	{
		arg msg, time, addr, port;
		msg.postln;

		x = Synth.new(\bpfsaw,
			[

				\amp, 1,
				\cfmin, 50*2,
				\cfmax, 50*50,
				\rqmin, 0.02,
				\rqmax, 0.08,
				\cfhzmin, 10,
				\cfhzmax, 100,
			],
		);


		//x = Synth.new(\bpfsaw);

	},
	'/main/bubbles1'
);
)



/*
(
SynthDef.new(\soundtrack, {
	arg noiseHz=8;
	var freq,amp,sig;
	freq = LFNoise0.kr(noiseHz).exprange(200,1000);
	amp = LFNoise1.kr(12).exprange(0.02,1);
	sig = SinOsc.ar(freq) * amp;
	Out.ar([0,1], sig);
}).add
)
*/

(
SynthDef.new(\soundtrack, {
	var sig, freq, amp;
	freq = 0.5;


	//sig = Pulse.ar(freq);
	sig = SinOsc.ar(440, 0, 0.1, 0);
	sig =
	Out.ar([0,1], sig);

}).add
)

{SinOsc.ar(440, 0, 0.1, 0)}.play

{Dust2.ar(10, 1)}.play


(
SynthDef.new(\windSFX, {
	var sig, freq, room, damp, mul;
	freq = 700;
	room = 0.8;
	damp = 0.25;
	mul = 2;
	sig = LPF.ar(PinkNoise.ar, freq) * LFNoise1.kr(1);
	sig = FreeVerb.ar(sig, 0.7, room, 0.25, mul);
	Out.ar([0,1], sig);
}).add
)



(
SynthDef(\bpfsaw, {
	arg atk=2, sus=0, rel=3, c1=1, c2=(-1),
	freq=500, detune=0.2, pan=0, cfhzmin=0.1, cfhzmax=0.3,
	cfmin=500, cfmax=2000, rqmin=0.1, rqmax=0.2,
	lsf=200, ldb=0, amp=1, out=0;
	var sig, env;
	env = EnvGen.kr(Env([0,1,1,0],[atk,sus,rel],[c1,0,c2]),doneAction:2);
	sig = Saw.ar(freq * {LFNoise1.kr(0.5,detune).midiratio}!2);
	sig = BPF.ar(
		sig,
		{LFNoise1.kr(
			LFNoise1.kr(4).exprange(cfhzmin, cfhzmax)
		).exprange(cfmin,cfmax)}!2,
		{LFNoise1.kr(0,1).exprange(rqmin,rqmax)}!2
	);
	sig = BLowShelf.ar(sig,lsf,0.5,ldb);
	sig = Balance2.ar(sig[0], sig[1], pan);
	sig = sig * env * amp;
	Out.ar(out, sig);
}).add;
)

(
3.do{
	Synth(
		\bpfsaw,
		[
			\freq, 50,
			\amp, 1,
			\cfmin, 50*2,
			\cfmax, 50*50,
			\rqmin, 0.02,
			\rqmax, 0.08,
			\cfhzmin, 10,
			\cfhzmax, 100,
		],
	);
};
)

(
Pbind(
	\instrument, \bpfsaw,
	\dur, 2,
	\midinote, Pseq([54,61,56],1),
	\detune, 0.08,
	\cfmin, 100,
    \cfmax, 1500,
	\atk, 1,
	\ldb, 6,
	\amp, 0.2,
	\out, 0,
).play
)




{SinOsc.ar(LFNoise0.kr().exprange(240,380), 0, 0.2!2)}.play



























// TODO:
// SFX: Player wounded, enemy wounded, player swing sword, enemy swing sword...
// Target loses life (done)

// (Important!) Freshness of sound!:
// Random amplitude levels, use envelopes