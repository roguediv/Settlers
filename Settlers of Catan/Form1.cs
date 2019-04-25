﻿using SOCForm.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphics = SOCForm.Classes.Graphics;

namespace SOCForm
{
    public partial class Form1 : Form
    {
        public Form1(int players)
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Gameboard.HexSize = hexSize;
            Gameboard.StartHex = startingHex;
            Gen.hexSize = hexSize;
            btnManage.hexSize = hexSize;
            place = new PiecePlacement(hexSize);
        }

        // Checks if the hexagons have been generated.
        private bool generated = false;

        // Random number generator.
        Random rdm = new Random();

        // Stores information about the map tiles
        private Hexagon[,] tiles;

        // Store the height and width for the hexagons.
        private int hexSize = 300;

        // Store the starting location of the first land tile.
        private int startingHex = 300;
        
        // Store the info for the tile pieces.
        public Hexagon[] Grid = new Hexagon[19];

        // For generating teh picture boxes
        private List<PictureBox> background = new List<PictureBox>();

        // Stores the info for the last generated hexagon.
        private Hexagon lastHex;

        // Stores the maps that can be used.
        private Maps Gameboard = new Maps();

        // A class used to make png backgrounds transparent.
        private Generation Gen = new Generation();

        // A class for managing the buttons.
        private ButtonManager btnManage = new ButtonManager();

        // A new instance of generation for pieces.
        private PiecePlacement place;

        public void Generate()
        {
            Point newLoc = new Point(5, 5); // Set whatever you want for initial location

                Button b = new Button();
                b.Size = new Size(10, 50);
                b.Location = newLoc;
                newLoc.Offset(0, b.Height + 5);
                b.BackColor = Color.Red;
                Controls.Add(b);


            // Fills all the information needed to generate a grid.
            Gameboard.Standard(Grid);

            // Generates a grid background.
            Gen.Standard(this, background, Grid);

            // Places pieces.
            place.Towns(this, Grid, 0, 5);

            btnManage.genAllBtn(this, Grid);
            
        }

        private void btnGenerate_Click_1(object sender, EventArgs e)
        {
            Generate();
        }
    }
}
