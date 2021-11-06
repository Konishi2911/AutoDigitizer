using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;

# nullable enable
namespace AutoDigitizer.FileOpening {
    public class OpenFileDialogCommand: ICommand {
        public event EventHandler? CanExecuteChanged;
        /// <summary>
        /// Occurs when a file is selected by Open File Doalog.
        /// </summary>
        public event EventHandler? FileSelected;

        /// <summary>
        /// The URI of the selected file.
        /// If no files are selected, null will be set.
        /// </summary>
        public Uri? Uri { get; private set; }

        public bool CanExecute(object? parameter) {
            return true;
        }

        public void Execute(object? paramter) {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.Cancel) {
                this.Uri = new Uri(ofd.FileName);
                if (this.FileSelected != null) this.FileSelected(this, new EventArgs());
            }
        }
    }
}
