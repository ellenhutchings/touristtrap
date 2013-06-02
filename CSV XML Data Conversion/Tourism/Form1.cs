using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Tourism {
    public partial class Form1 : Form {
        public Country country = new Country();
        public State selectedState;

        public List<Region> Regions = new List<Region>();
        public Region selectedRegion;

        //
        public List<int> quarters = new List<int>() { 1, 2, 3, 4 };
        public Form1() {
            InitializeComponent();
            loadData();
        }

        private void browseButton_Click(object sender, EventArgs e) {
            OpenFileDialog open = new OpenFileDialog();
            open.ShowDialog();

            fileBox.Text = open.FileName;
        }

        private void createButton_Click(object sender, EventArgs e) {
            State s = new State();
            if(File.Exists(fileBox.Text)) {

                using(CsvReader csv = new CsvReader(new StreamReader(fileBox.Text), true)) {
                    //extract field count and headers from csv
                    int fieldCount = csv.FieldCount;
                    string[] headers = csv.GetFieldHeaders();
                    bool area = false;

                    #region Header
                    csv.ReadNextRecord();

                    headers = csv[0].Split(',');
                    s.Name = headers[2].Trim();

                    //quarter based off of headers[3] in format Jun, 20XX
                    int quarter = 20;

                    headers = headers[3].Split(' ');

                    switch(headers[1]) {
                        case "Mar":
                            quarter = 1;
                            break;
                        case "Jun":
                            quarter = 2;
                            break;
                        case "Sep":
                            quarter = 3;
                            break;
                        case "Dec":
                            quarter = 4;
                            break;
                    }

                    quarters.Remove(quarter);
                    #endregion

                    for(int i=0; i<3; i++) {
                        csv.ReadNextRecord();
                    }
                    Region r = null;
                    while(csv.ReadNextRecord()) {
                        #region Region Name

                        int room;
                        int bed;
                        int employee;
                        double octOcc;
                        double novOcc;
                        double decOcc;
                        double octStay;
                        double novStay;
                        double decStay;
                        if(csv[0].Contains("(TR)")) {
                            if(csv[0].Contains("Total")) {
                                area = false;

                                //Region Data
                                string n = csv[0].Trim();

                                string rString = csv[2].Trim().Replace(",", "");
                                if(rString.Equals("") != true) {
                                    room = int.Parse(rString);
                                } else {
                                    room = 0;
                                }
                                string bString = csv[3].Trim().Replace(",", "");
                                if(bString.Equals("") != true) {
                                    bed = int.Parse(bString);
                                } else {
                                    bed = 0;
                                }

                                string eString = csv[4].Trim().Replace(",", "");
                                if(eString.Equals("") != true) {
                                    employee = int.Parse(eString);
                                } else {
                                    employee = 0;
                                }

                                try {
                                    // CultureInfo.InvariantCulture
                                    Double.TryParse(csv[9].Trim(), out octOcc);
                                } catch(Exception ex) {
                                    octOcc =0.0;
                                }
                                try {
                                    Double.TryParse(csv[10].Trim(), out novOcc);
                                } catch(Exception ex) {
                                    novOcc =0.0;
                                }
                                try {
                                    Double.TryParse(csv[11].Trim(), out decOcc);
                                } catch(Exception ex) {
                                    decOcc =0.0;
                                }
                                try {
                                    Double.TryParse(csv[24].Trim(), out octStay);
                                } catch(Exception ex) {
                                    octStay =0.0;
                                }
                                try {
                                    Double.TryParse(csv[25].Trim(), out novStay);
                                } catch(Exception ex) {
                                    novStay =0.0;
                                }
                                try {
                                    Double.TryParse(csv[26].Trim(), out decStay);
                                } catch(Exception ex) {
                                    decStay =0.0;
                                }
                                r.Rooms = room;
                                r.Beds = bed;
                                r.Employees = employee;

                                r.Occupancy[9] = octOcc;
                                r.Occupancy[10] = novOcc;
                                r.Occupancy[11] = decOcc;
                                r.Stay[9] = octStay;
                                r.Stay[10] = novStay;
                                r.Stay[11] = decStay;

                                Regions.Add(r);

                            } else {
                                string n = csv[0].Replace(" (TR)", "");
                                r= new Region(n);
                                area = true;
                            }
                        #endregion
                        } else if(area) {
                            #region RegionArea
                            string n = csv[0].Trim();
                            //
                            string rString = csv[2].Trim().Replace(",", "");
                            if(rString.Equals("") != true) {
                                room = int.Parse(rString);
                            } else {
                                room = 0;
                            }
                            string bString = csv[3].Trim().Replace(",", "");
                            if(bString.Equals("") != true) {
                                bed = int.Parse(bString);
                            } else {
                                bed = 0;
                            }

                            string eString = csv[4].Trim().Replace(",", "");
                            if(eString.Equals("") != true) {
                                employee = int.Parse(eString);
                            } else {
                                employee = 0;
                            }

                            try {
                                // CultureInfo.InvariantCulture
                                Double.TryParse(csv[9].Trim(), out octOcc);
                            } catch(Exception ex) {
                                octOcc =0.0;
                            }
                            try {
                                Double.TryParse(csv[10].Trim(), out novOcc);
                            } catch(Exception ex) {
                                novOcc =0.0;
                            }
                            try {
                                Double.TryParse(csv[11].Trim(), out decOcc);
                            } catch(Exception ex) {
                                decOcc =0.0;
                            }
                            try {
                                Double.TryParse(csv[24].Trim(), out octStay);
                            } catch(Exception ex) {
                                octStay =0.0;
                            }
                            try {
                                Double.TryParse(csv[25].Trim(), out novStay);
                            } catch(Exception ex) {
                                novStay =0.0;
                            }
                            try {
                                Double.TryParse(csv[26].Trim(), out decStay);
                            } catch(Exception ex) {
                                decStay =0.0;
                            }

                            r.addArea(new Area(n, quarter, room, bed, employee, octOcc, novOcc, decOcc, octStay, novStay, decStay));
                        }
                            #endregion
                    }
                }
                createButton.Visible = false;
                updateButton.Visible = true;
            }
            s.Regions = Regions;
            country.addState(s);
        }

        public void readNewMonths() {
            int quarter;
            using(CsvReader csv = new CsvReader(new StreamReader(fileBox.Text), true)) {
                //extract field count and headers from csv
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();

                csv.ReadNextRecord();

                headers = csv[0].Split(',');

                string name = headers[2].Trim();
                if(selectState(name)) {
                    //quarter based off of headers[3] in format Jun, 20XX
                    quarter = 20;

                    headers = headers[3].Split(' ');

                    switch(headers[1]) {
                        case "Mar":
                            quarter = 1;
                            break;
                        case "Jun":
                            quarter = 2;
                            break;
                        case "Sep":
                            quarter = 3;
                            break;
                        case "Dec":
                            quarter = 4;
                            break;
                    }

                    quarters.Remove(quarter);
                    bool area = false;

                    for(int i=0; i<3; i++) {
                        csv.ReadNextRecord();
                    }
                    while(csv.ReadNextRecord()) {
                        double octOcc;
                        double novOcc;
                        double decOcc;
                        double octStay;
                        double novStay;
                        double decStay;

                        if(csv[0].Contains("(TR)")) {
                            if(csv[0].Contains("Total")) {
                                area = false;
                                //Region Data
                                try {
                                    // CultureInfo.InvariantCulture
                                    Double.TryParse(csv[9].Trim(), out octOcc);
                                } catch(Exception ex) {
                                    octOcc =0.0;
                                }
                                try {
                                    Double.TryParse(csv[10].Trim(), out novOcc);
                                } catch(Exception ex) {
                                    novOcc =0.0;
                                }
                                try {
                                    Double.TryParse(csv[11].Trim(), out decOcc);
                                } catch(Exception ex) {
                                    decOcc =0.0;
                                }
                                try {
                                    Double.TryParse(csv[24].Trim(), out octStay);
                                } catch(Exception ex) {
                                    octStay =0.0;
                                }
                                try {
                                    Double.TryParse(csv[25].Trim(), out novStay);
                                } catch(Exception ex) {
                                    novStay =0.0;
                                }
                                try {
                                    Double.TryParse(csv[26].Trim(), out decStay);
                                } catch(Exception ex) {
                                    decStay =0.0;
                                }
                                selectedRegion.quarterOccupancyStay(quarter, octOcc, novOcc, decOcc, octStay, novOcc, decOcc);
                            } else {
                                string n = csv[0].Replace(" (TR)", "");
                                selectRegion(n);
                                area = true;
                            }
                        } else if(area) {
                            string n = csv[0].Trim();
                            //
                            try {
                                // CultureInfo.InvariantCulture
                                Double.TryParse(csv[9].Trim(), out octOcc);
                            } catch(Exception ex) {
                                octOcc =0.0;
                            }
                            try {
                                Double.TryParse(csv[10].Trim(), out novOcc);
                            } catch(Exception ex) {
                                novOcc =0.0;
                            }
                            try {
                                Double.TryParse(csv[11].Trim(), out decOcc);
                            } catch(Exception ex) {
                                decOcc =0.0;
                            }
                            try {
                                Double.TryParse(csv[24].Trim(), out octStay);
                            } catch(Exception ex) {
                                octStay =0.0;
                            }
                            try {
                                Double.TryParse(csv[25].Trim(), out novStay);
                            } catch(Exception ex) {
                                novStay =0.0;
                            }
                            try {
                                Double.TryParse(csv[26].Trim(), out decStay);
                            } catch(Exception ex) {
                                decStay =0.0;
                            }

                            selectedRegion.addQuarters(n, quarter, octOcc, novOcc, decOcc, octStay, novStay, decStay);
                        }
                    }
                } else {
                    MessageBox.Show("To Convert Data for a New State, please select the option from the Import Menu", "Error adding data", MessageBoxButtons.OK);
                }
            }
        }

        private void updateButton_Click(object sender, EventArgs e) {
            if(File.Exists(fileBox.Text)) {
                readNewMonths();
            }
            if(quarters.Count == 0) {
                updateButton.Visible = false;
                newQuarterButton.Visible = true;
            }
        }

        public void selectRegion(string name) {
            for(int i=0; i<selectedState.Regions.Count; i++) {
                if(selectedState.Regions[i].Name.Equals(name)) {
                    selectedRegion = selectedState.Regions[i];
                }
            }
        }

        public void addNewMonths() {
            int quarter;
            using(CsvReader csv = new CsvReader(new StreamReader(fileBox.Text), true)) {
                //extract field count and headers from csv
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();

                csv.ReadNextRecord();

                    headers = csv[0].Split(',');

                    string name = headers[2].Trim();
                    if(selectState(name)) {
                    //quarter based off of headers[3] in format Jun, 20XX
                    quarter = 20;

                    headers = headers[3].Split(' ');

                    switch(headers[1]) {
                        case "Mar":
                            quarter = 1;
                            break;
                        case "Jun":
                            quarter = 2;
                            break;
                        case "Sep":
                            quarter = 3;
                            break;
                        case "Dec":
                            quarter = 4;
                            break;
                    }

                    quarters.Remove(quarter);
                    bool area = false;

                    for(int i=0; i<3; i++) {
                        csv.ReadNextRecord();
                    }
                    while(csv.ReadNextRecord()) {
                        int room;
                        int bed;
                        int employee;
                        double octOcc;
                        double novOcc;
                        double decOcc;
                        double octStay;
                        double novStay;
                        double decStay;

                        if(csv[0].Contains("(TR)")) {
                            if(csv[0].Contains("Total")) {
                                area = false;
                                string n = csv[0].Trim();
                                //
                                string rString = csv[2].Trim().Replace(",", "");
                                if(rString.Equals("") != true) {
                                    room = int.Parse(rString);
                                } else {
                                    room = 0;
                                }
                                string bString = csv[3].Trim().Replace(",", "");
                                if(bString.Equals("") != true) {
                                    bed = int.Parse(bString);
                                } else {
                                    bed = 0;
                                }

                                string eString = csv[4].Trim().Replace(",", "");
                                if(eString.Equals("") != true) {
                                    employee = int.Parse(eString);
                                } else {
                                    employee = 0;
                                }

                                try {
                                    // CultureInfo.InvariantCulture
                                    Double.TryParse(csv[9].Trim(), out octOcc);
                                } catch(Exception ex) {
                                    octOcc =0.0;
                                }
                                try {
                                    Double.TryParse(csv[10].Trim(), out novOcc);
                                } catch(Exception ex) {
                                    novOcc =0.0;
                                }
                                try {
                                    Double.TryParse(csv[11].Trim(), out decOcc);
                                } catch(Exception ex) {
                                    decOcc =0.0;
                                }
                                try {
                                    Double.TryParse(csv[24].Trim(), out octStay);
                                } catch(Exception ex) {
                                    octStay =0.0;
                                }
                                try {
                                    Double.TryParse(csv[25].Trim(), out novStay);
                                } catch(Exception ex) {
                                    novStay =0.0;
                                }
                                try {
                                    Double.TryParse(csv[26].Trim(), out decStay);
                                } catch(Exception ex) {
                                    decStay =0.0;
                                }
                                selectedRegion.addNewestQuarter(quarter, room, bed, employee, octOcc, novOcc, decOcc, octStay, novOcc, decOcc);
                            } else {
                                string n = csv[0].Replace(" (TR)", "");
                                selectRegion(n);
                                area = true;
                            }
                        } else if(area) {
                            string n = csv[0].Trim();
                            //
                            string rString = csv[2].Trim().Replace(",", "");
                            if(rString.Equals("") != true) {
                                room = int.Parse(rString);
                            } else {
                                room = 0;
                            }
                            string bString = csv[3].Trim().Replace(",", "");
                            if(bString.Equals("") != true) {
                                bed = int.Parse(bString);
                            } else {
                                bed = 0;
                            }

                            string eString = csv[4].Trim().Replace(",", "");
                            if(eString.Equals("") != true) {
                                employee = int.Parse(eString);
                            } else {
                                employee = 0;
                            }

                            try {
                                // CultureInfo.InvariantCulture
                                Double.TryParse(csv[9].Trim(), out octOcc);
                            } catch(Exception ex) {
                                octOcc =0.0;
                            }
                            try {
                                Double.TryParse(csv[10].Trim(), out novOcc);
                            } catch(Exception ex) {
                                novOcc =0.0;
                            }
                            try {
                                Double.TryParse(csv[11].Trim(), out decOcc);
                            } catch(Exception ex) {
                                decOcc =0.0;
                            }
                            try {
                                Double.TryParse(csv[24].Trim(), out octStay);
                            } catch(Exception ex) {
                                octStay =0.0;
                            }
                            try {
                                Double.TryParse(csv[25].Trim(), out novStay);
                            } catch(Exception ex) {
                                novStay =0.0;
                            }
                            try {
                                Double.TryParse(csv[26].Trim(), out decStay);
                            } catch(Exception ex) {
                                decStay =0.0;
                            }

                            selectedRegion.addNewestQuarters(n, quarter, bed, room, employee, octOcc, novOcc, decOcc, octStay, novStay, decStay);
                        }
                    }
                } else {
                    MessageBox.Show("To Convert Data for a New State, please select the option from the Import Menu", "Error adding data", MessageBoxButtons.OK);
                }
            }
        }

        private void newQuarterButton_Click(object sender, EventArgs e) {
            if(File.Exists(fileBox.Text)) {
                addNewMonths();
            }
        }

        private void cSVToolStripMenuItem_Click(object sender, EventArgs e) {
            browseButton_Click(sender, e);
        }

        public void loadData() {
            if(File.Exists("Accomodation.dat")) {
                Stream fileStream = File.OpenRead("Accomodation.dat");
                BinaryFormatter deserializer = new BinaryFormatter();
                quarters = (List<int>)deserializer.Deserialize(fileStream);
                int count = (int)deserializer.Deserialize(fileStream);
                // Read in each card.
                country = new Country();
                for(int i = 0; i < count; i++) {
                    this.country.addState((State)deserializer.Deserialize(fileStream));
                }
                fileStream.Close();
                createButton.Visible = false;
                updateButton.Visible = true;
            }
            if(quarters.Count == 0) {
                updateButton.Visible = false;
                newQuarterButton.Visible = true;
            }
        }

        public void saveData() {
            Stream fileStream = File.Create("Accomodation.dat");
            BinaryFormatter serializer = new BinaryFormatter();

            serializer.Serialize(fileStream, this.quarters);
            // Store the number of regions. 
            serializer.Serialize(fileStream, this.country.States.Count);

            //Then try and write each region
            try {
                foreach(State state in this.country.States) {
                    serializer.Serialize(fileStream, state);
                }
            } catch(ArgumentNullException ex) {
                MessageBox.Show("Error: Could not save the data");
            } catch(SerializationException ex) {
                MessageBox.Show("Error: Could not save the data");
            } finally {
                fileStream.Close();
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            saveData();
        }

        public void exportXML() {

            XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Country));
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"accomodation.xml");


            writer.Serialize(file, country);

            file.Close();

        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e) {
            exportXML();
        }

        public bool selectState(string name) {
            for(int i=0; i<country.States.Count; i++) {
                if(country.States[i].Name.Equals(name)) {
                    selectedState = country.States[i];
                    return true;
                }
            }
            return false;
        }

        private void newStateToolStripMenuItem_Click(object sender, EventArgs e) {
            createButton.Visible = true;
            updateButton.Visible = false;
            newQuarterButton.Visible = false;
        }
    }
}
