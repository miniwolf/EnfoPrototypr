(
~musicVar = Buffer.read(Server.default, "C:/Users/Sindri/gamecourse/EnfoPrototypr/Assets/Resources/SuperCollider/Sound effects/castlevania.wav");
~windVar = Buffer.read(Server.default, "C:/Users/Sindri/gamecourse/EnfoPrototypr/Assets/Resources/SuperCollider/Sound effects/wind.wav");
)

(
SynthDef(\sampler, {
	arg rate = 1, amp = 0.5, buffer = 0; // Test out = 0
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



// TODO:
// Ambient: Wind, rain
// SFX: Player wounded, enemy wounded, player swing sword, enemy swing sword
// Voice acting?
// Background music: make medieval score