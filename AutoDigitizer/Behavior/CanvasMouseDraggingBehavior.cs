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
            defaultMetadata: new PropertyMetadata(null, MouseEventCallback)
        );
        public static Point GetStartPos(DependencyObject target) => (Point)target.GetValue(StartPosProperty);
        public static void SetStartPos(DependencyObject target, Point? value) => target.SetValue(StartPosProperty, value);

        public static readonly DependencyProperty EndPosProperty = DependencyProperty.RegisterAttached(
            name: "EndPos",
            propertyType: typeof(Point?),
            ownerType: typeof(CanvasMouseDraggingBehavior),
            defaultMetadata: new FrameworkPropertyMetadata(null, MouseEventCallback)
        );
        public static Point GetEndPos(DependencyObject target) => (Point)target.GetValue(EndPosProperty);
        public static void SetEndPos(DependencyObject target, Point? value) => target.SetValue(EndPosProperty, value);

        public static readonly DependencyProperty CursorPosProperty = DependencyProperty.RegisterAttached(
            name: "CursorPos",
            propertyType: typeof(Point?),
            ownerType: typeof(CanvasMouseDraggingBehavior),
            defaultMetadata: new FrameworkPropertyMetadata(null, MouseEventCallback)
        );
        public static Point GetCursorPos(DependencyObject target) => (Point)target.GetValue(CursorPosProperty);
        public static void SetCursorPos(DependencyObject target, Point? value) => target.SetValue(CursorPosProperty, value);


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

        private static void MouseEventCallback(DependencyObject? sender, DependencyPropertyChangedEventArgs e) {
            if (sender is not Canvas) return;
            Canvas canvas = (Canvas)sender;

            canvas.MouseDown += MouseClicked;
            canvas.MouseLeave += MouseLeft;
            canvas.MouseUp += MouseLeft;
            canvas.MouseMove += GetMousePosition;
        }
    }
}
