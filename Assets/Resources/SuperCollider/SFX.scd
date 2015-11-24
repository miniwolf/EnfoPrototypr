(
~musicVar = Buffer.read(Server.default, "C:/Users/Sindri/gamecourse/EnfoPrototypr/Assets/Resources/SuperCollider/Sound effects/castlevania.wav");
~windVar = Buffer.read(Server.default, "C:/Users/Sindri/gamecourse/EnfoPrototypr/Assets/Resources/SuperCollider/Sound effects/wind.wav");
)

(
SynthDef(\sampler, {
	arg rate = 1, amp = 0.5, buffer = 0;
	var signal;

	signal =
	PlayBuf.ar(1, buffer,
		BufRateScale.kr(buffer)*rate,
		doneAction: 2
	);

	Out.ar([0,1], signal * amp);
}).add;
)

(
// MUSIC OSC
OSCdef.new(
\music,
	{arg msg;
		[msg].postln;

		Synth(\sampler, [\buffer, ~musicVar]);
	},
	'/music',
	nil, 57120
);

// WIND OSC
OSCdef.new(
\wind,
	{arg msg;
		[msg].postln;

		Synth(\sampler, [\buffer, ~windVar]);
	},
	'/wind',
	nil, 57120
);
)





OSCdef('sineTest',
	{arg msg;
		[msg].postln;

		Out.ar([0,1], LFNoise1.kr(12).exprange(0.02,1));
    },
	'main/sineTest1',
    nil, 57120
);

// WORKING
(
OSCdef.new(
	\toggle,
	{
		arg msg, time, addr, port;
		//msg.postln;
		//x.set(\amp, msg[1].linexp(0,1,0.001,1));
		x = Synth.new(\sineTest);
	},
	'/main/toggle1'
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






// TODO:
// Ambient: Wind + light rain
// SFX: Player wounded, enemy wounded, player swing sword, enemy swing sword...

// Background music: make medieval score
// Voice acting? modify voice when player is clicked, Warcraft 3 style! :)

// (Important!) Freshness of sound!:
// Make 2-3 versions of every sound
// Random amplitude levels