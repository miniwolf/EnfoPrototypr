
// Could be a base for background piece (war drums)
play{Limiter.ar(GVerb.ar(sin(Ringz.ar(Impulse.ar(2**LFNoise1.ar(1!1).range(1,2).round/4),[20,80,110,240,330,150],0.7).sum),5,3,0.7)/4)}



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