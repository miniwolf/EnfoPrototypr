(

~moveVar = Buffer.read(Server.default, "C:/Users/Sindri/gamecourse/EnfoPrototypr/Assets/Resources/SuperCollider/Sound effects/scavengers_footstep1.aif");
~musicVar = Buffer.read(Server.default, "C:/Users/Sindri/gamecourse/EnfoPrototypr/Assets/Resources/SuperCollider/Sound effects/castlevania.wav");

SynthDef(\sampler, {
	arg out = [0,1], rate = 1, amp = 0.5, buffer = 0; // Test out = 0
	var signal;

	signal =
	PlayBuf.ar(1, buffer,
		BufRateScale.kr(buffer)*rate,
		doneAction: 2
	);

	Out.ar(out, signal * amp);
}).add;

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

// MOVE OSC
OSCdef.new(
\move,
	{arg msg;
		[msg].postln;

		Synth(\sampler, [\buffer, ~moveVar]);
	},
	'/move',
	nil, 57120
);

)

// TODO:
// Sterio sound
// Various synths / buffers for mixing each audio file
// Ambients (From scratch: Wind + maybe rain)
// Custom SFX (from scratch)
// Background music (sample and mix??? make from scratch?)