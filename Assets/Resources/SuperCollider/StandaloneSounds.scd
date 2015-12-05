
// Could be a base for background piece (war drums)
play{Limiter.ar(GVerb.ar(sin(Ringz.ar(Impulse.ar(2*LFNoise1.ar(1!1).range(1,1).round),[40,80,110,240,330,150],1.0).sum),5, 3 , 0.3)/4)}




var level = 1; // Out amplitude
var sig = Limiter.ar(GVerb.ar(sin(Ringz.ar(Impulse.ar(2*LFNoise1.ar(1).range(1,1).round/4),[20,80,110,240,330,150],1.0).sum),5, 3 , 0.8)/4, level);
{sig}.play

// Could be a base for background piece (war drums)
play{Limiter.ar(GVerb.ar(sin(Ringz.ar(Impulse.ar(2*LFNoise1.ar(1).range(1,1).round/4),[20,80,110,240,330,150],1.0).sum),5, 3 , 0.8)/4, level)}



// Useful components
{PinkNoise.ar * 0.5}.play


// TARGET LOSES LIFE
x = {
	var sig, freq, env;
	env = XLine.kr(1, 0.01, 1, doneAction:2);
	freq = XLine.kr(LinRand(470,250), 105, 1.0, doneAction:2);
	//sig = Pulse.ar(ExpRand(30, 200), 0.5) * env;
	sig = Pulse.ar(freq) * env;

	Out.ar([0,1], sig);

}.play


x = {
	var sig, freq, env;
	env = XLine.kr(1, 0.01, 1, doneAction:2);
	freq = XLine.kr(LinRand(470,250), 105, 1.0, doneAction:2);
	//sig = Pulse.ar(ExpRand(30, 200), 0.5) * env;
	sig = Pulse.ar(freq) * env;

	Out.ar([0,1], sig);

}.play


// a function to convert an array or ratios to a Scale
(
~makeScale = {|ratios| // supply an array of ratios
Scale( // make a Scale
(0..ratios.size-1), // list the steps in the scale from 0 to the number of ratios
ratios.size, // how many steps we have in our scale
Tuning( // make a Tuning
(
ratios.ratiomidi // first we convert out ratios to midi
% 12 // then we mod 12 to put everything
).sort // lastly we sort it from low to high
)
);
};
);

// three JI scales, each with different root
a = ~makeScale.value([1,9/8,6/5,4/3,3/2,8/5,7/4]); // our initial scale
b = ~makeScale.value((5/4)*[1,9/8,6/5,4/3,3/2,8/5,7/4]); // same scale transposed a 5/4
c = ~makeScale.value((3/2)*[1,9/8,6/5,4/3,3/2,8/5,7/4]); // same scale transposed a 3/2

// test it out
(
Pbind(
\root, 0, // the pitch class of the scale's root
\octave, 5, // the octave of the root with a scale degree of 0
\scale, Pstep([a,b,c],2,inf), // play each scale/tuning every 2 seconds
\degree, Pseq((0..a.size),inf), // walk up the scale
\dur, 0.125 // each note is a 1/16th
).play
)