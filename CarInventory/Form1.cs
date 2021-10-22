using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CarInventory
{
    public partial class Form1 : Form
    {
        List<Car> inventory = new List<Car>();

        public Form1()
        {
            InitializeComponent();
            loadInventory();
            outputLabel.Text = "";
            foreach (Car car in inventory)
            {
                outputLabel.Text += car.year + " " + car.make + " " + car.colour + " " + car.mileage + "\n";
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Car newCar = new Car(yearInput.Text, makeInput.Text, colourInput.Text, mileageInput.Text);
            inventory.Add(newCar);

            outputLabel.Text = "";
            foreach (Car car in inventory)
            {
                outputLabel.Text += car.year + " " + car.make + " " + car.colour + " " + car.mileage + "\n";
            }

            yearInput.Text = "";
            makeInput.Text = "";
            colourInput.Text = "";
            mileageInput.Text = "";
        }

        private void listButton_Click(object sender, EventArgs e)
        {
            outputLabel.Text = "";
            foreach (Car car in inventory)
            {
                outputLabel.Text += car.year + " " + car.make + " " + car.colour + " " + car.mileage + "\n";
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            int index = inventory.FindIndex(x => x.make == makeInput.Text);
            if(index >= 0)
            {
                outputLabel.Text = $"Removed car of make {inventory[index].make}";
                inventory.RemoveAt(index);
            }
            else
            {
                outputLabel.Text = $"Could not remove car of make {makeInput.Text}";
            }
            makeInput.Text = "";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            XmlWriter writer = XmlWriter.Create("inventoryData.xml", null);

            writer.WriteStartElement("Inventory");

            foreach(Car c in inventory)
            {

                writer.WriteStartElement("Car");

                writer.WriteElementString("year", c.year);
                writer.WriteElementString("make", c.make);
                writer.WriteElementString("colour", c.colour);
                writer.WriteElementString("mileage", c.mileage);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.Close();
        }

        private void loadInventory()
        {
            XmlReader reader = XmlReader.Create("inventoryData.xml", null);

            string year, make, colour, mileage;

            while (reader.Read())
            {

                if (reader.NodeType == XmlNodeType.Text)
                {
                    year = reader.ReadString();

                    reader.ReadToNextSibling("make");
                    make = reader.ReadString();

                    reader.ReadToNextSibling("colour");
                    colour = reader.ReadString();

                    reader.ReadToNextSibling("mileage");
                    mileage = reader.ReadString();


                    Car newCar = new Car(year, make, colour, mileage);
                    inventory.Add(newCar);
                }
                
            }

            reader.Close();
        }

    }
}
