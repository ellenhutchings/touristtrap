using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tourism {
    [Serializable()]
    public class Area {
        public string Name {
            get;
            set;
        }
        public int Rooms {
            get;
            set;
        }
        public int Beds {
            get;
            set;
        }
        public int Employees {
            get;
            set;
        }
        public double[] Occupancy {
            get;
            set;
        }
        public double[] Stay {
            get;
            set;
        }

        public Area() {
            this.Name = null;
            this.Rooms = 0;
            this.Beds = 0;
            this.Employees = 0;
            Occupancy = new double[12];
            Stay = new double[12];
        }

        public Area(string name, int q, int rooms, int beds, int emp, double octOcc, double NovOcc, double DecOcc, double octStay, double NovStay, double DecStay) {
            this.Name = name;
            this.Rooms = rooms;
            this.Beds = beds;
            this.Employees = emp;
            Occupancy = new double[12];
            Stay = new double[12];
            quarterOccupancyStay(q, octOcc, NovOcc, DecOcc, octStay, NovStay, DecOcc);
        }

        public void quarterOccupancyStay(int q, double firstOcc, double secondOcc, double thirdOcc, double firstStay, double secondStay, double thirdStay) {
            if(q == 1) {
                Occupancy[0] = firstOcc;
                Occupancy[1] = secondOcc;
                Occupancy[2] = thirdOcc;
                Stay[0] = firstStay;
                Stay[1] = secondStay;
                Stay[2] = thirdStay;
            } else if(q ==2) {
                Occupancy[3] = firstOcc;
                Occupancy[4] = secondOcc;
                Occupancy[5] = thirdOcc;
                Stay[3] = firstStay;
                Stay[4] = secondStay;
                Stay[5] = thirdStay;
            } else if(q ==3) {
                Occupancy[6] = firstOcc;
                Occupancy[7] = secondOcc;
                Occupancy[8] = thirdOcc;
                Stay[6] = firstStay;
                Stay[7] = secondStay;
                Stay[8] = thirdStay;
            } else if(q == 4) {
                Occupancy[9] = firstOcc;
                Occupancy[10] = secondOcc;
                Occupancy[11] = thirdOcc;
                Stay[9] = firstStay;
                Stay[10] = secondStay;
                Stay[11] = thirdStay;
            }
        }

        public void addNewestQuarter(int q, int room, int bed, int employee, double firstOcc, double secondOcc, double thirdOcc, double firstStay, double secondStay, double thirdStay) {
            Rooms = room;
            Beds = bed;
            Employees = employee;
            quarterOccupancyStay(q, firstOcc, secondOcc, thirdOcc, firstStay, secondStay, thirdStay);
        }
    }
}
