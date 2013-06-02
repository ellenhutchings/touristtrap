using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tourism {
    [Serializable()]
    public class State {
        public string Name {
            get;
            set;
        }
        public List<Region> Regions {
            get;
            set;
        }
        public State() {
            Name = null;
            Regions = new List<Region>();
        }
    }
}
