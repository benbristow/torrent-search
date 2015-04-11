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

        private async Task doSearch(string query)
        {
            //Create search engine instance
            var engine = new Engine.SearchEngine();

            //Perform search with task
            var queryTask =  engine.queryResults(query);

            //Disable search bar
            queryTextBox.Enabled = false;

            //Get results from task and add to binding list view (so we can sort)
            var sortableResults = new BindingListView<TorrentResult>(await queryTask);            

            //Set DGV datasource so user can see
            dataGridView1.DataSource = sortableResults;

            //We found no torrents, show an error.
            if (sortableResults.Count == 0)
            {
                MessageBox.Show("No torrents found!");
            }

            //Renable search bar
            queryTextBox.Enabled = true;
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
                    //Do search!
                    doSearch(queryTextBox.Text);
                    //Disable annoying 'ding' sound.
                    e.Handled = true;
                }
            }
        }

        /* Data Grid View */

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //Double click
            downloadSelectedResult();
        }

        private async void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                //don't do anything if no row selected
                return;
            }

            //Clear current file listing.
            fileListingBox.Items.Clear();
            fileListingBox.Items.Add("Loading file listing...");   

            //Update file listing on result section
            try
            {
                //Get selected torrent
                var result = getSelectedResult();

                //Create task to get file listing
                var fileListingTask = result.getFileListing();
                
                //Await
                List<string> fileListing = await fileListingTask;

                //Clear loading message
                fileListingBox.Items.Clear();

                //Add to list
                foreach (string f in fileListing)
                {
                    fileListingBox.Items.Add(f);
                }
            }
            catch(NullReferenceException) { }
        }


        /* Context Menu */
        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Download
            downloadSelectedResult();
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

        private void downloadSelectedResult()
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

        }

        private TorrentResult getSelectedResult()
        {
            //No torrent selected.
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("No torrent selected!");
                return null;
            }
            
            //Get torrent result from objectview.
            ObjectView<TorrentResult> sortedResult = (ObjectView<TorrentResult>)dataGridView1.SelectedRows[0].DataBoundItem;
            return sortedResult.Object;
        }

        private void openLink(string url)
        {
            //Open link in shell
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = url;
            p.StartInfo = psi;
            p.Start();            
        }

    }
}
