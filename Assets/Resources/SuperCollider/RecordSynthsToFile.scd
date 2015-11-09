// RECORD WIND
~durationSeconds = 10.0;
~frequency = 440;

(
SynthDef.new(\wind, { |freq, duration|
	var sig;

	sig = LPF.ar(PinkNoise.ar, freq) * LFNoise1.kr(1);
	sig = FreeVerb.ar(sig, 0.7, 0.8, 0.25);
	Out.ar([0,1], sig);
}).add
)


(
SynthDef(\recordTone, { |buffer|
    RecordBuf.ar(In.ar(0,2), buffer, loop: 0, doneAction: 2);
}).add;
)

(
Routine({
    var recordfn = { |freq, duration|
        var server = Server.local;
        var buffer = Buffer.alloc(server, server.sampleRate * duration, 2);

        server.sync;

        server.makeBundle(func: {
            var player = Synth(\wind, [\freq, freq, \duration, duration]);
            var recorder = Synth.after(player, \recordTone, [\buffer, buffer]);
        });

        duration.wait;

        buffer.write(
            "C:/Users/Sindri/gamecourse/EnfoPrototypr/Assets/Resources/SuperCollider/Sound effects/wind.wav",
            "WAVE",
            "int16",
            completionMessage: ["/b_free", buffer]
        );
    };

    recordfn.value(~frequency, ~durationSeconds);
}).next
)

// RECORD ...