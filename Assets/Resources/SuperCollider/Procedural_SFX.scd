(
// START MENU MUSIC OSC
OSCdef.new(
	\music2,
	{
		arg msg, time, addr, port;
		msg.postln;

		// Pattern to play the music
		(
			Pbind(
				\instrument, \bpfsaw,
				\dur, 1.5,
				\midinote, Pseq([54,61,56,63,58,61],inf),
				\detune, 0,
				\cfmin, 100,
				\cfmax, 1500,
				\atk, 1,
				\ldb, 6,
				\amp, 0.5,
				\out, 0,
			).play
		)

	},
	'/main/music2'
);

// BEAT OSC
OSCdef.new(
	\beat1,
	{
		arg msg, time, addr, port;
		msg.postln;

		// Pattern to play the beat
		(
			Pbind(
				\instrument, \smooth,
				//\midinote, Pseq([60, 72, 71, 67, 69, 71, 72, 60, 69, 67], inf),

				\midinote, Pseq([20, 20, 20, 20, 20, 20, 20], inf),
				//\dur, 0.3,
				\dur, Pseq([0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5], inf),
				\amp, 2,
				\out, 0,
			).play
		)
	},
	'/main/beat1'
);


// WIND OSC
OSCdef.new(
	\wind,
	{
		arg msg, time, addr, port;
		msg.postln;
		x = Synth.new(\windSynth,
			[
				\amp, 1,
			],
		);
	},
	'/main/wind1'
);


// ENEMIES DYING OSC
OSCdef.new(
	\bubbles,
	{
		arg msg, time, addr, port;
		msg.postln;

		x = Synth.new(\bpfsaw,
			[

				\amp, 2,
				\cfmin, 50*2,
				\cfmax, 50*50,
				\rqmin, 0.02,
				\rqmax, 0.08,
				\cfhzmin, 10,
				\cfhzmax, 50,
			],
		);
	},
	'/main/bubbles1'
);


// TARGET HIT OSC
OSCdef.new(
	\targetHit,
	{
		arg msg, time, addr, port;
		msg.postln;
		x = Synth.new(\targetSynth);
	},
	'/main/targetHit1'
);


// CLICK OSC
// TODO: Add click OSC to everything clickable...
// Tweak the click sound  - More coconut like
OSCdef.new(
	\click,
	{

		arg msg, time, addr, port;
		msg.postln;

		x = Synth.new(\ClickSynth);
	},
	'/main/click1'
);

)

(
SynthDef.new(\targetSynth, {
	var sig, freq, env;
	env = XLine.kr(1, 0.01, 1, doneAction:2);
	freq = XLine.kr(LinRand(470,250), 105, 1.0, doneAction:2);
	sig = Pulse.ar(freq) * env;
	Out.ar([0,1], sig);
}).add
)

(
SynthDef.new(\windSynth, {
	var sig, freq, room, damp, mul;
	freq = 700;
	room = 0.8;
	damp = 0.25;
	mul = 0.5;
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
SynthDef(\smooth, { |freq = 440, sustain = 1, amp = 0.5|
    var sig;
    sig = SinOsc.ar(freq, 0, amp) * EnvGen.kr(Env.linen(0.05, sustain, 0.1), doneAction: 2);
    Out.ar(0, sig ! 2)
}).add;
)


(
SynthDef.new(\StartMenuMusicSynth, {
	var a, sig;

	a = Pbind(
		\instrument, \bpfsaw,
		\dur, 1.5,
		\midinote, Pseq([54,61,56,63,58,61],inf),
		\detune, 0,
		\cfmin, 100,
		\cfmax, 1500,
		\atk, 1,
		\ldb, 6,
		\amp, 0.2,
		\out, 0,
	);

	sig = a.asStream;
}).add
)


(
SynthDef.new(\BackgroundBeat, {
	var level, sig, out;
	level = 1; // Out amplitude
	sig = Limiter.ar(GVerb.ar(sin(Ringz.ar(Impulse.ar(2*LFNoise1.ar(1).range(1,1).round/4),[20,80,110,240,330,150],1.0).sum),5, 3 , 0.8)/4, level);
	Out.ar([0,1], sig);
});
)


(
SynthDef.new(\ClickSynth, {
	var sig, env, dur, freq,
			start, end;
	dur = 0.07;
	freq = 65;
	start = 0.4;
	end = 1;

	env = XLine.kr(start, end, dur, doneAction:2);
	sig = Pulse.ar(freq) * env;

	Out.ar([0,1], sig);
}).add
)



//
// TEST for clickable sound
//
(
x = {
	var sig, env, dur, freq,
			start, end;
	dur = 0.04;
	freq = 85;
	start = 1;
	end = 0.2;

	env = XLine.kr(start, end, dur, doneAction:2);
	sig = Pulse.ar(freq) * env;
}.play;
)



(
SynthDef(\beep, {
    arg freq = 440;
    var sig;
    sig = SinOsc.ar(freq, mul:0.2);
    sig = sig * EnvGen.kr(Env.perc(0.1, 0.5), doneAction:2);
	Out.ar([0,1], sig);
}).send(s);
)




a = {};
c = TempoClock(4);

c.schedAbs(
    0, //evaluate this immediately
    {
        arg ...args;
        args[0].postln;    // arg[0] is the beat number
        a.value(args[0]); // pass the beat number to our function
        1.0               // do it all again on the next beat
    }
);

a = {Synth(\beep)};


a = {Synth(\beep, [\freq, 580])};

a = {
    arg beat;
    // if beat number is even play a high note
    if(beat % 2 == 0, {Synth(\beep, [\freq, 440])});

    // play a low note every beat
    Synth(\beep, [\freq, 219]);
}



//~notes = [56, 58, 64, 70, 40, 40, 40, 32, 32];
~notes = [60, 70];
a = {Synth(\beep, [\freq, ~notes.choose.midicps])};


a = {
	arg beat;

	// Búa til takt hérna
	Synth(\beep, [\freq, 440]);
	Synth(\beep, [\freq, 440]);
	Synth(\beep, [\freq, 440]);
	Synth(\beep, [\freq, 219]);
}