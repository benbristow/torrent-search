using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Equin.ApplicationFramework;
using Torrent_Search.Engine;

namespace Torrent_Search
{
    public partial class MainForm : Form
    {
        /* Main Form Stuff */
        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private void queryTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Check for key press enter
            if (e.KeyChar == (char)Keys.Enter)
            {
                //Is there any search content?
                if (queryTextBox.Text.Trim().Length == 0)
                {
                    //Nope, show error
                    MessageBox.Show("No search query entered");                
                }
                else
                {
                    //Do search
                    var engine = new Engine.SearchEngine();
                    var results = engine.getResults(queryTextBox.Text);
                    var sortableResults = new BindingListView<Engine.TorrentResult>(results);
                    dataGridView1.DataSource = sortableResults;
                }
            }
        }


        /* Context Menu */
        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Download

            //Favour magnet over torrents
            var result = getSelectedResult();
            if (result != null)
            {
                if (result.magnet != null)
                {
                    openLink(result.magnet);
                    return;
                }

                if (result.torrentFile != null)
                {
                    openLink(result.torrentFile);
                    return;
                }

                MessageBox.Show("Couldn't find torrent link.");
            }
            else
            {
                MessageBox.Show("No torrent selected!");
            }
        }

        private void openURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open URL
            var result = getSelectedResult();
            if (result != null)
            {
                openLink(result.url);
            }
        }

        /* Top Toolbar */

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //File -> Exit
            Environment.Exit(0);
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Help -> About
            var aboutFrm = new AboutBox();
            aboutFrm.Show();
            aboutFrm.BringToFront();
        }



        /* Misc Functions */

        private Engine.TorrentResult getSelectedResult()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return null;
            }
            
            ObjectView<TorrentResult> sortedResult = (ObjectView<TorrentResult>)dataGridView1.SelectedRows[0].DataBoundItem;
            return sortedResult.Object;
        }

        private void openLink(string url)
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = url;
            p.StartInfo = psi;
            p.Start();            
        }
    }
}
