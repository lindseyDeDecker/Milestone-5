using static Milestone_2._1.Buisness.Inventory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.ConstrainedExecution;
using static Milestone_2._1.inventoryForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

/*
 * Lindsey DeDecker
 * CST - 150
 * Milestone 2
 * July 13, 2024
 * Gaddis, T. (2019). Starting out with Visual C# (5th ed.). Pearson. ISBN-13: 9780135183519
 * 
 */

namespace Milestone_2._1
{
    public partial class inventoryForm : Form
    {
        //declaring a type inventory that can be used to store an instance of inventory class.
        private Buisness.Inventory inventory;
        public inventoryForm()
        {
            InitializeComponent();
            //Initializing the instance of inventory
            inventory = new Buisness.Inventory();
            //Make the label for inventory not visible
            dataInfo.Visible = false;
        }

        private void lblInventory_Click(object sender, EventArgs e)
        {
            //The label for the inventory will be displayed
            dataInfo.Visible = true;

            //Call method that gets the inventory and put it into the array
            List<Buisness.Inventory.Book> inventoryBooks = inventory.GetInventory();

            //Display the inventory in the grid
            inventory.DisplayInventory(dataInfo);

        }
        private void addInventory_Click(object sender, EventArgs e)
        {
            string text = "Click on the quantity of the book you would like to add one to.";
            MessageBox.Show(text);
        }

        private void dataInfo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //This is used to test and track that the cell that is being clicked is showing correctly in the code. 
            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            messageBoxCS.AppendFormat("{0} = {1}", "ColumnIndex", e.ColumnIndex);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("{0} = {1}", "RowIndex", e.RowIndex);
            messageBoxCS.AppendLine();

            //This stops and prompts the user if they do not click on the cell that has the inventory in it.
            if (e.ColumnIndex != 3)
            {
                string message = "Please click on the quanitity of the book you would like to add 1 to.";
                MessageBox.Show(message);
            }

            //setting the row clicked as selected row
            int selectedRow = e.RowIndex;

            //Calling method to icrease the invetory of the selected cell
            inventory.IncreaseInventory(dataInfo, selectedRow);
        }

        //This button will replace the file with the updated books. 
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            //Calling getbooks to get the updated array of books
            List<Book> books = inventory.GetBooks();
            //Use the stream writer to send the new array of books back into the document so that it is updated.
            using StreamWriter sw = new StreamWriter("C:\\Users\\kydec\\Desktop\\Lindsey School\\CSR 150\\Activity 2\\Milestone 2.1\\bin\\Debug\\net8.0-windows\\Inventory.txt");
            {
                foreach (var b in books)
                {
                    //Make sure the books are being broken up with | so that it can be read correctly the next time.
                    string line = $"{b.Title}|{b.Author}|{b.Price}|{b.Quantity}|{b.Isbn}|{b.Genre}";
                    sw.WriteLine(line);
                }
            }
            //Notidy user that the updates have been saved to the file
            string confirmation = "Your changes have been saved!";
            MessageBox.Show(confirmation);
        }
    }
}
