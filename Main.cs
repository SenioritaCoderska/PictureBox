using PictureBoxApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureBoxApp
{
    public partial class Main : Form
    {
        private FileHelper<PathDetails> _fileHelper = new FileHelper<PathDetails>(Program.applicationRecordFilePath);
        public Main()
        {
            //initialize form components
            InitializeComponent();
            //read path from file
            ReadFromFile();
            //hide/unhide remove button
            IsEmptyOrNull(pbPic);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //Write selected path to file
            WriteToFile();
            //Read path from file
            ReadFromFile();
            //hide/unhide remove button
            IsEmptyOrNull(pbPic);
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            //Removeimage from memory alocation
            pbPic.Image.Dispose();
            //assigne null to image location
            pbPic.ImageLocation = null;
            // write null path in application file
            var serializationData = new PathDetails() { PathDetailsForFile = null };
            _fileHelper.SerializeToFile(serializationData);
            //hide/unhide remove button
            IsEmptyOrNull(pbPic);
        }
        private void IsEmptyOrNull(PictureBox pictureBox)
        {
            //hide/unhide remove button
            if (pictureBox.ImageLocation == null)
            {
                btnRemove.Visible = false;
            }
            else
            {
                btnRemove.Visible = true;
            }
        }
        private void ReadFromFile()
        {
            //Read path from file
            try
            {
                var deserializedData = _fileHelper.DeserializedFromFile();
                var path = deserializedData.PathDetailsForFile;
                pbPic.ImageLocation = path;
            }
            catch
            {
                MessageBox.Show("No Picture available. Please choose new one");
            }
        }
        private void WriteToFile()
        {
            //write path to file
            try
            {
                var fileDialog = new OpenFileDialog();
                fileDialog.ShowDialog();
                var serializationData = new PathDetails() { PathDetailsForFile = @"" + fileDialog.FileName };
                _fileHelper.SerializeToFile(serializationData);
            }
            catch
            {
                MessageBox.Show("No path selected. Please select new pic!");
            }
        }
    }
}