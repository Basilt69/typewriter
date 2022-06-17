using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace typewriter
{
    public class InfoIteration
    {
        public int MisPrintCount { get; set; }
        public int IndexText { get; set; }
        public TimeSpan Time;
        private List<Tuple<int, TimeSpan>> Result = new List<Tuple<int, TimeSpan>>();

        public string GetTimeFormated()
        {
            return Time.ToString("mm':'ss':'fff");
        }

        public int GetCountResult()
        {
            return Results.Count;
        }

        public void AddResult(int lenght)
        {
            Results.Add(new Tuple<int, TimeSpan>(lenght, Time));
        }

        public string GetBestTime()
        {
            var best = Result.First();
            
            for (var i = 1; i < Results.Count; i++)
                if (Results[i].Item1 >= best.Item1 && Results[i].Item2 <= best.Item2)
                    best = Results[i];

            return $"{best.Item1} {Utils.Plural(best.Item1, "знак", "знака", "знаков")} за" +
                   $"{best.Item2:mm':'ss':'fff}";
        }
        
        public string GetWorseTime()
        {
            var worse = Result.First();
            
            for (var i = 1; i < Results.Count; i++)
                if (Results[i].Item1 >= worse.Item1 && Results[i].Item2 <= worse.Item2)
                    worse = Results[i];

            return $"{worse.Item1} {Utils.Plural(worse.Item1, "знак", "знака", "знаков")} за" +
                   $"{worse.Item2:mm':'ss':'fff}";
        }

        public string GetAverageTime()
        {
            var lenght = Results.First().Item1;
            var ticks = Results.First().Item2.Ticks;

            for (var i = 1; i < Results.Count; i++)
            {
                lenght += Results[i].Item1;
                ticks += Results[i].Item2.Ticks;
            }

            lenght /= Results.Count;
            ticks /= Results.Count;

            var time = new TimeSpan(ticks);
            
            return $"{lenght} {Utils.Plural(lenght, "знак", "знака", "знаков")} за " +
            $"{time: mm':'ss':'fff}";
        }
    }
}