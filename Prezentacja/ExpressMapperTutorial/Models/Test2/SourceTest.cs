using System;
using System.Collections.Generic;

namespace ExpressMapperTutorial.Models.Test2
{
    public class SourceTest : IRandomCreateable, IHandWrittenMapperable<DestTest>
    {
        public List<List<SourceA>> Lists { get; set; }

        public void FillWithRandomValues()
        {
            Random rand = new Random();
            Lists = new List<List<SourceA>>();
            for (int i = 0; i < 5; i++)

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
                Lists.Add(sourceAs);
            }
        }

        public DestTest Map()
        {
            DestTest dest = new DestTest
            {
                Lists = new List<List<DestA>>()
            };
            foreach (var list in Lists)
            {
                var destAs = new List<DestA>();
                foreach (SourceA sourceA in list)
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
                dest.Lists.Add(destAs);
            }

            return dest;
        }
    }
}