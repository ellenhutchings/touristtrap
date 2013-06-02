using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tourism {
    [Serializable()]
    public class Country {
        public List<State> States {
            get;
            set;
        }
        public Country(){
            States = new List<State>();
        }

        public bool addState(State s){
            for(int i=0; i<States.Count; i++){
                if(s.Name == States[i].Name){
                    return false;
                }
            }
            States.Add(s);
            return true;
        }
    }
}
