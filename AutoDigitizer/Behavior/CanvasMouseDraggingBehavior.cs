using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#nullable enable
namespace AutoDigitizer.Behavior {
    internal class CanvasMouseDraggingBehavior {
        public static readonly DependencyProperty StartPosProperty = DependencyProperty.RegisterAttached(
            name: "StartPos",
            propertyType: typeof(Point?),
            ownerType: typeof(CanvasMouseDraggingBehavior),
            defaultMetadata: new PropertyMetadata(null, RegisterMouseEventCallback)
        );
        public static Point GetStartPos(DependencyObject target) => (Point)target.GetValue(StartPosProperty);
        public static void SetStartPos(DependencyObject target, Point? value) => target.SetValue(StartPosProperty, value);

        public static readonly DependencyProperty EndPosProperty = DependencyProperty.RegisterAttached(
            name: "EndPos",
            propertyType: typeof(Point?),
            ownerType: typeof(CanvasMouseDraggingBehavior),
            defaultMetadata: new FrameworkPropertyMetadata(null, RegisterMouseEventCallback)
        );
        public static Point GetEndPos(DependencyObject target) => (Point)target.GetValue(EndPosProperty);
        public static void SetEndPos(DependencyObject target, Point? value) => target.SetValue(EndPosProperty, value);

        public static readonly DependencyProperty CursorPosProperty = DependencyProperty.RegisterAttached(
            name: "CursorPos",
            propertyType: typeof(Point?),
            ownerType: typeof(CanvasMouseDraggingBehavior),
            defaultMetadata: new FrameworkPropertyMetadata(null, RegisterMouseEventCallback)
        );
        public static Point GetCursorPos(DependencyObject target) => (Point)target.GetValue(CursorPosProperty);
        public static void SetCursorPos(DependencyObject target, Point? value) => target.SetValue(CursorPosProperty, value);

        public static readonly DependencyProperty EnableDraggingProperty = DependencyProperty.RegisterAttached(
            name: "EnableDragging",
            propertyType: typeof(bool),
            ownerType: typeof(CanvasMouseDraggingBehavior),
            defaultMetadata: new FrameworkPropertyMetadata(false, RegisterMouseEventCallback)
        );
        public static bool GetEnableDragging(DependencyObject target) => (bool)target.GetValue(EnableDraggingProperty);
        public static void SetEnableDragging(DependencyObject target, bool value) => target.SetValue(EnableDraggingProperty, value);


        private static void MouseClicked(object? sender, MouseEventArgs e) {
            if (sender is not Canvas) return;
            Canvas canvas = (Canvas)sender;

            SetStartPos(canvas, e.GetPosition(canvas));
        }

        private static void MouseLeft(object? sender, MouseEventArgs e) {
            if (sender is not Canvas) return;
            Canvas canvas = (Canvas)sender;

            SetEndPos(canvas, e.GetPosition(canvas));
        }

        private static void GetMousePosition(object? sender, MouseEventArgs e) {
            if (sender is not Canvas) return;
            Canvas canvas = (Canvas)sender;

            if (e.LeftButton == MouseButtonState.Pressed) {
                var pos = e.GetPosition(canvas);
                SetCursorPos(canvas, e.GetPosition(canvas));
            }
        }

        private static void RegisterMouseEventCallback(DependencyObject? sender, DependencyPropertyChangedEventArgs e) {
            if (sender is not Canvas) return;
            Canvas canvas = (Canvas)sender;

            if (e.Property.Name == "EnableDragging") {
                if ((bool)e.NewValue) {
                    canvas.MouseDown += MouseClicked;
                    canvas.MouseLeave += MouseLeft;
                    canvas.MouseUp += MouseLeft;
                    canvas.MouseMove += GetMousePosition;
                } else {
                    canvas.MouseDown -= MouseClicked;
                    canvas.MouseLeave -= MouseLeft;
                    canvas.MouseUp -= MouseLeft;
                    canvas.MouseMove -= GetMousePosition;
                }
            }
        }
    }
}
