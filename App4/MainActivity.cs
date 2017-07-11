using Android.App;
using Android.Widget;
using Android.OS;
using Android.Media;
using System;

namespace App4
{
    [Activity(Label = "App4", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
       
        protected override void OnCreate(Bundle bundle)
        {
            int freqOfTone=500;
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            Button butt = FindViewById<Button>(Resource.Id.button1);
            TextView text = FindViewById<TextView>(Resource.Id.textView1);
            text.Text = string.Format("The value of the SeekBar is {0}", freqOfTone);
            SeekBar seek = FindViewById<SeekBar>(Resource.Id.seekBar1);
            seek.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
              {
                  freqOfTone = e.Progress+1;
                  text.Text= string.Format("The value of the SeekBar is {0}", freqOfTone);
              };
            butt.Click += (object sender, System.EventArgs e) =>
            {
               playSound(freqOfTone);
            };
            
            
        }
        public void playSound(int freqOfTone)
        {
            var duration = 1;
            var sampleRate = 8000;
            var numSamples = duration * sampleRate;
            var sample = new double[numSamples];
            //var freqOfTone = 1900;
            byte[] generatedSnd = new byte[2 * numSamples];

            for (int i = 0; i < numSamples; ++i)
            {
                sample[i] = Math.Sin(2 * Math.PI * i / (sampleRate / freqOfTone));
            }

            int idx = 0;
            foreach (double dVal in sample)
            {
                short val = (short)(dVal * 32767);
                generatedSnd[idx++] = (byte)(val & 0x00ff);
                generatedSnd[idx++] = (byte)((val & 0xff00) >> 8);
            }
            string str="";
            foreach(double dval in sample)
            {
                str += dval.ToString();
                str += "\n";
            }
            EditText edTxt = FindViewById<EditText>(Resource.Id.editText1);
            edTxt.Text = str;
            var track = new AudioTrack(global::Android.Media.Stream.Music, sampleRate, ChannelOut.Mono, Encoding.Pcm16bit, numSamples, AudioTrackMode.Static);
            track.Write(generatedSnd, 0, numSamples);
            track.Play();
        }



    }
}

