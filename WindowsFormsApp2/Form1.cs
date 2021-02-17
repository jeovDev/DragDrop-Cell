using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private DataGridViewCell drag_cell;
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;

        public Form1()
        {
            InitializeComponent();
           dataGridView1.AllowDrop = true;
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridView.HitTestInfo hti = dataGridView1.HitTest(e.X, e.Y);
                drag_cell = dataGridView1[hti.ColumnIndex, hti.RowIndex];
                // Proceed with the drag and drop, passing in the list item.                    
                DragDropEffects dropEffect = dataGridView1.DoDragDrop(
                drag_cell,
                DragDropEffects.Move);
            }
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            DataGridView.HitTestInfo hti = dataGridView1.HitTest(clientPoint.X, clientPoint.Y);
            DataGridViewCell targetCell = dataGridView1[hti.ColumnIndex, hti.RowIndex];


            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                targetCell.Value = drag_cell.Value;
                dataGridView1.Refresh();

                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    cell.Value = "";
           
                }
            }

        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {

                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = dataGridView1.DoDragDrop(
                    dataGridView1.Rows[rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }
    }
}
