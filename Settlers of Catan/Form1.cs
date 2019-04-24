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
        }

        private void picGrid_Click(object sender, EventArgs e)
        {

        }



        // Checks if the hexagons have been generated.
        private bool generated = false;

        // Random number generator.
        Random rdm = new Random();

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
        private Generation gen = new Generation();

        public void Generate()
        {
            Gameboard.Standard(Grid);

            // This loop places pictureboxes on the gameboard.
            for (int i = 0; i < Grid.Length; i++)
            {
                Hexagon grid = Grid[i];
                var picture = new PictureBox
                {
                    Name = "pictureBox",
                    Size = new Size(hexSize, hexSize),
                    Location = new Point(grid.LocX, grid.LocY),
                    Image = Image.FromFile(Grid[i].Path[Grid[i].Type]),
                    BackColor = Color.Transparent,
                };
                background.Add(picture);
            }

            // This loop fixes transparancy issues for the most part.
            for (int i = 0; i < Grid.Length; i++)
            {
                Hexagon thisHex = Grid[i];
                if (thisHex.Layer > 0)
                {
                    int reference = i - thisHex.FirstTouchingCnt;
                    Hexagon referenceHex = Grid[reference];
                    PictureBox thisPic = background[i];
                    PictureBox referencePic = background[reference];

                    // Find out which pictureboxes need to be changed.
                    if (thisHex.Touching == "both")
                    {
                        //If current hex is touching on both, two pic boxes need to be changed.
                        PictureBox leftPic = new PictureBox()
                        {
                            Name = "pictureBox",
                            Size = new Size(hexSize, hexSize),
                            Location = new Point((hexSize / 2), (hexSize - (hexSize / 4))),
                            Image = thisPic.Image,
                            BackColor = Color.Transparent,
                        };
                        PictureBox rightPic = new PictureBox()
                        {
                            Name = "pictureBox",
                            Size = new Size(hexSize, hexSize),
                            Location = new Point(-(hexSize / 2), (hexSize - (hexSize / 4))),
                            Image = thisPic.Image,
                            BackColor = Color.Transparent,
                        };
                        PictureBox preRefPic = background[reference - 1];
                        this.Controls.Add(preRefPic);
                        this.Controls.Add(referencePic);
                        this.Controls.Add(thisPic);
                        preRefPic.Controls.Add(leftPic);
                        referencePic.Controls.Add(rightPic);
                    }
                    if (thisHex.Touching == "left")
                    {
                        // Just change the left one.
                        PictureBox leftPic = new PictureBox()
                        {
                            Name = "pictureBox",
                            Size = new Size(hexSize, hexSize),
                            Location = new Point((hexSize / 2), (hexSize - (hexSize / 4))),
                            Image = thisPic.Image,
                            BackColor = Color.Transparent,
                        };
                        this.Controls.Add(referencePic);
                        this.Controls.Add(thisPic);
                        referencePic.Controls.Add(leftPic);
                    }
                    if (thisHex.Touching == "right")
                    {
                        // Just change the right one.
                        PictureBox rightPic = new PictureBox()
                        {
                            Name = "pictureBox",
                            Size = new Size(hexSize, hexSize),
                            Location = new Point(-(hexSize / 2), (hexSize - (hexSize / 4))),
                            Image = thisPic.Image,
                            BackColor = Color.Transparent,
                        };
                        this.Controls.Add(referencePic);
                        this.Controls.Add(thisPic);
                        referencePic.Controls.Add(rightPic);
                    }
                }

                // Troubleshooting with transparancy...
                // It's a headache!
                PictureBox left = new PictureBox()
                {
                    Name = "pictureBox",
                    Size = new Size(hexSize, hexSize),
                    Location = new Point((hexSize / 2), (hexSize - (hexSize / 4))),
                    Image = background[6].Image,
                    BackColor = Color.Transparent,
                };
                background[2].Controls.Add(left);
                PictureBox right = new PictureBox()
                {
                    Name = "pictureBox",
                    Size = new Size(hexSize, hexSize),
                    Location = new Point(-(hexSize / 2), (hexSize - (hexSize / 4))),
                    Image = background[16].Image,
                    BackColor = Color.Transparent,
                };
                background[13].Controls.Add(right);
                PictureBox right2 = new PictureBox()
                {
                    Name = "pictureBox",
                    Size = new Size(hexSize, hexSize),
                    Location = new Point(-(hexSize / 2), (hexSize - (hexSize / 4))),
                    Image = background[17].Image,
                    BackColor = Color.Transparent,
                };
                background[14].Controls.Add(right2);
            }
            /*
            PictureBox clone = new PictureBox()
            {
                Name = "pictureBox",
                Size = new Size(hexSize, hexSize),
                Location = new Point(-150, 225),
                Image = background[3].Image,
                BackColor = Color.Transparent,
            };
            this.Controls.Add(background[0]);
            this.Controls.Add(background[3]);
            this.Controls.Add(background[4]);
            background[0].Controls.Add(clone);
            */
            /*
            var pictur = new PictureBox
            {
                Name = "pictureBox",
                Size = new Size(300, 300),
                Location = new Point(150, 0),
                Image = Image.FromFile("Media\\bordersTest.png"),
                BackColor = Color.Transparent,
            };
            this.Controls.Add(pictur);
            var picturee = new PictureBox
            {
                Name = "pictureBox",
                Size = new Size(300, 300),
                Location = new Point(0, 225),
                Image = Image.FromFile("Media\\bordersTest.png"),
                BackColor = Color.Transparent,
            };
            this.Controls.Add(picturee);
            var pictureee = new PictureBox
            {
                Name = "pictureBox",
                Size = new Size(300, 225),
                Location = new Point(0, 225),
                Image = Image.FromFile("Media\\bordersTest.png"),
                BackColor = Color.Transparent,
            };
            //this.Controls.Add(picturee);
            pictur.Controls.Add(pictureee);
            */
        }
        
        private void btnRdm_Click(object sender, EventArgs e)
        {
            Generate();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Generate();
        }

        private void btnGenerate_Click_1(object sender, EventArgs e)
        {
            Generate();
        }
    }
}
