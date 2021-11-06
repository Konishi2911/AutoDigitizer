using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutoDigitizer {
    public class MainWindowViewModel: ViewModelBase {
        public Uri? FigurePath { get; private set; }
        public ImageSource? ImageSource {
            get {
                if (this.FigurePath != null)
                    return new BitmapImage(this.FigurePath);
                else
                    return null;
            }
        }

        public FileOpening.OpenFileDialogCommand OFDCommand { get; private set; }
        
        public MainWindowViewModel() {
            this.OFDCommand = new FileOpening.OpenFileDialogCommand();

            this.OFDCommand.FileSelected += this.GetFigurePath;
        }

        public void GetFigurePath(object? sender, EventArgs e) {
            this.FigurePath = this.OFDCommand.Uri;

            this.RaisePropertyChanged(nameof(FigurePath));
            this.RaisePropertyChanged(nameof(ImageSource));
        }
    }
}
