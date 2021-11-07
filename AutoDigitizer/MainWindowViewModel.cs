using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#nullable enable
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

        /// <summary>
        /// The width of axis of the chart in display dimension.
        /// </summary>
        public Double AxisWidth { get; private set; } = 100;
        /// <summary>
        /// The height of axis of the chart in display dimension.
        /// </summary>
        public Double AxisHeight { get; private set; } = 100;
        /// <summary>
        /// The Left position of axis of the chart in display dimension.
        /// </summary>
        public Double AxisLeft { get; private set; } = 10;
        /// <summary>
        /// The Top position of axis of the chart in display dimension.
        /// </summary>
        public Double AxisTop { get; private set; } = 10;

        private Point axisStartPos_;
        public Point AxisStartPos {
            get => this.axisStartPos_;
            set {
                this.axisStartPos_ = value;
                this.calcAxisRect();
            }
        }

        public Point axisEndPos_;
        public Point AxisEndPos {
            get => this.axisEndPos_;
            set {
                this.axisEndPos_ = value;
                this.calcAxisRect();
            }
        }

        
        public MainWindowViewModel() {
            this.OFDCommand = new FileOpening.OpenFileDialogCommand();

            this.OFDCommand.FileSelected += this.GetFigurePath;
        }

        public void GetFigurePath(object? sender, EventArgs e) {
            this.FigurePath = this.OFDCommand.Uri;

            this.RaisePropertyChanged(nameof(FigurePath));
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        private void calcAxisRect() {
            this.AxisTop = Math.Min(AxisStartPos.Y, AxisEndPos.Y);
            this.AxisLeft = Math.Min(AxisStartPos.X, AxisEndPos.X);
            this.AxisWidth = Math.Abs(AxisStartPos.X - AxisEndPos.X);
            this.AxisHeight = Math.Abs(AxisStartPos.Y - AxisEndPos.Y);

            RaisePropertyChanged(nameof(AxisTop));
            RaisePropertyChanged(nameof(AxisLeft));
            RaisePropertyChanged(nameof(AxisWidth));
            RaisePropertyChanged(nameof(AxisHeight));
        }

    }
}
