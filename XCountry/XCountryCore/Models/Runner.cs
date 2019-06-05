using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XCountryCore.Models
{
    public class Runner
    {
        public Runner()
        {
        }

        public string Name { get; set; }
        private string _split1;
        private string _split2;
        private string _finish;
        public string Split1 {
            get { return _split1; }
            set { _split1 = value; }
        }
        public string Split2 { get; set; }
        public string Finish { get; set; }
        public string Id { get; set; }

        public bool Split1Set { get; set; }
        public bool Split2Set { get; set; }
        public bool FinishSet { get; set; }

        public int UpdateTime (TimeSpan ts)
        {
            if (!Split1Set)
            {
                Split1 = ts.ToString(@"hh\:mm\:ss");
                Split1Set = true;
                return 0;
            }

            if (!Split2Set)
            {
                Split2 = ts.ToString(@"hh\:mm\:ss");
                Split2Set = true;
                return 1;
            }
            else
            {
                Finish = ts.ToString(@"hh\:mm\:ss");
                FinishSet = true;
                return 2;
            }
        }

    }
}
