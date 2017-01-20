using System;
using System.Collections.Generic;

namespace ExpressMapperTutorial.Models.Test2
{
    public class SourceTest : IRandomCreateable, IHandWrittenMapperable<DestTest>
    {
        public Dictionary<int, List<SourceA>> Dictionary { get; set; }

        public void FillWithRandomValues()
        {
            Random rand = new Random();
            Dictionary = new Dictionary<int, List<SourceA>>();
            for (int i = 0; i < 10; i++)

            {
                var sourceAs = new List<SourceA>();
                for (int j = 0; j < 10; j++)
                {
                    var bs = new SourceB[j];
                    for (int k = 0; k < j; k++)
                    {
                        bs[k] = new SourceB
                        {
                            Str = k.ToString()
                        };
                    }
                    sourceAs.Add(new SourceA
                    {
                        Bs = bs,
                        C = new SourceC {Float = (float) rand.NextDouble()},
                        Char = 'a',
                        Long = rand.Next()
                    });
                }
                Dictionary.Add(i, sourceAs);
            }
        }

        public DestTest Map()
        {
            DestTest dest = new DestTest
            {
                Dictionary = new Dictionary<int, List<DestA>>()
            };
            foreach (var pair in this.Dictionary)
            {
                var destAs = new List<DestA>();
                foreach (SourceA sourceA in pair.Value)
                {
                    DestB[] destBs = new DestB[sourceA.Bs.Length];
                    for (int i = 0; i < sourceA.Bs.Length; i++)
                    {
                        destBs[i] = new DestB
                        {
                            Str = sourceA.Bs[i].Str
                        };
                    }
                    DestA destA = new DestA
                    {
                        Bs = destBs,
                        Long = sourceA.Long,
                        Char = sourceA.Char,
                        C = new DestC {Float = sourceA.C.Float}
                    };
                }
                dest.Dictionary.Add(pair.Key, destAs);
            }

            return dest;
        }
    }
}