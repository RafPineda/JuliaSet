using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {

        double x, y, z, c1, c2, iterCnt, xCount, yCount; // x, y, z and c components for Julia function



        const double scale = (.004);            // for a 1000 by 1000 pixel screen, scale is .004
        int  px, py;
        bool divergent;
        

        public Form1()
        {
            InitializeComponent();
        }

        //accept user input upon button press
        //make sure to error check for numbers and return error if user input is incorrect

        private void Enter_Click(object sender, EventArgs e) //upon button press, take in user values into c1 and c2, then calculate/display julia set
        {
            Enter.Enabled = false; //disable button click until calculations are done
            if (Double.TryParse((textBox1.Text), out c1) == true && Double.TryParse((textBox2.Text), out c2) == true && Double.TryParse((Iterations.Text), out iterCnt)) //if user values are not valid, return error statement
            {
                if (iterCnt <= 0) //errmsg if user requests for "negative" iterations
                {
                    MessageBox.Show("Invalid Entry. \nPlease Try again using a postive iteration value.");
                }

                /*  from coordinates -2 to 2 for both x and y
                    for each point test if it diverges or converges using formulas                
                    xn = (x(n-1))^2 - (y(n-1))^2 + c1
                    yn = 2 * (x(n-1)) * (y(n-1)) + c2
                    Check if (xn)^2 + (yn)^2 > 20.
                    if it is > 20, mark bool divergent true, paint point red, and break loop
                    Else loop again.
                    If after iteration loops, divergent is still false, then we end the loop and mark the point blue
                    AFTER testing a point (for example (x,y) = (-2, -2)), increase x by a factor of (1/175) and y by a factor of (1/88)
                    These values are obtained by dividing pixel density by 4.
                 */

                else
                {  //run through all points from (-2, -2) to (2, 2)
                    Bitmap bm = new Bitmap(Canvas.ClientSize.Width, Canvas.ClientSize.Height);
                    using (Graphics g = Graphics.FromImage(bm))
                    {
                        c2 = -c2;
                        for (int i = 0; i < 1000; i++)
                        {
                            yCount = -2 + i * scale;
                            px = 0;
                            xCount = -2;
                            for (int j = 0; j < 1000; j++)
                            {
                                divergent = false;
                                x = xCount;
                                y = yCount;
                                for (int k = 0; k < iterCnt; k++) // perform comparisons
                                {
                                    double xSquared = x * x;          //x squared
                                    double ySquared = y * y;          //y squared
                                    double twoXY = 2 * x * y;         //2*x*y
                                    x = xSquared - ySquared + c1;     //newx = x^2 - y^2 + c1
                                    y = twoXY + c2;                   //newy = x^2 - y^2 + c2
                                    xSquared = x * x;
                                    ySquared = y * y;
                                    z = xSquared + ySquared;            //check if z > 20 for divergence
                                    if (z > 20)
                                    {
                                        divergent = true;
                                    }

                                    if (divergent == true)
                                    {
                                        break;
                                    }
                                }

                                if (divergent == false) //point does not diverge
                                {
                                    bm.SetPixel(j, i, Color.Cyan);
                                }

                                else //point diverges
                                {
                                    bm.SetPixel(j, i, Color.LightSalmon);

                                }

                                px++;       // increment x pixel by one, and increase x count by .004
                                xCount = -2 + j * scale;

                            }
                            py++;       // increment y pixel by one, and increase y count by .004                        
                        }
                        Canvas.Image = bm;
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid Entry. \nPlease Try again using valid c1, c2 and iteration values.");
            }

            Enter.Enabled = true; // enable button click again.
        }
    }
}
