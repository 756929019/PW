using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace PW.Controls
{
    public class VirtualControl : HeaderedContentControl
    {
        private SplineDoubleKeyFrame sizeAnimationWidthKeyFrame;
		private SplineDoubleKeyFrame sizeAnimationHeightKeyFrame;
		private SplineDoubleKeyFrame positionAnimationXKeyFrame;
		private SplineDoubleKeyFrame positionAnimationYKeyFrame;
		private SplineDoubleKeyFrame heightAnimationKeyFrame;
		private Storyboard sizeAnimation;
		private Storyboard positionAnimation;
		private Storyboard heightOnlyAnimation;
		private bool sizeAnimating;
		private bool positionAnimating;
		private bool heightAnimating;
		private TimeSpan sizeAnimationTimespan = new TimeSpan(0, 0, 0, 0, 200);
		private TimeSpan positionAnimationTimespan = new TimeSpan(0, 0, 0, 0, 200);
		public event EventHandler MaximizedChanged;
		[Category("Animation Properties"), Description("The size animation duration.")]
		public TimeSpan SizeAnimationDuration
		{
			get
			{
				return this.sizeAnimationTimespan;
			}
			set
			{
				this.sizeAnimationTimespan = value;
				if (this.sizeAnimationWidthKeyFrame != null)
				{
					this.sizeAnimationWidthKeyFrame.KeyTime = KeyTime.FromTimeSpan(this.sizeAnimationTimespan);
				}
				if (this.sizeAnimationHeightKeyFrame != null)
				{
					this.sizeAnimationHeightKeyFrame.KeyTime = KeyTime.FromTimeSpan(this.sizeAnimationTimespan);
				}
			}
		}
		[Category("Animation Properties"), Description("The position animation duration.")]
		public TimeSpan PositionAnimationDuration
		{
			get
			{
				return this.positionAnimationTimespan;
			}
			set
			{
				this.positionAnimationTimespan = value;
				if (this.positionAnimationXKeyFrame != null)
				{
                    this.positionAnimationXKeyFrame.KeyTime = (KeyTime.FromTimeSpan(this.positionAnimationTimespan));
				}
				if (this.positionAnimationYKeyFrame != null)
				{
                    this.positionAnimationYKeyFrame.KeyTime = (KeyTime.FromTimeSpan(this.positionAnimationTimespan));
				}
			}
		}
		public VirtualControl()
		{
			this.sizeAnimation = new Storyboard();
			DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
			Storyboard.SetTarget(doubleAnimationUsingKeyFrames, this);
			Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, new PropertyPath("(FrameworkElement.Width)", new object[0]));
			this.sizeAnimationWidthKeyFrame = new SplineDoubleKeyFrame();
			SplineDoubleKeyFrame arg_B2_0 = this.sizeAnimationWidthKeyFrame;
			KeySpline keySpline = new KeySpline();
			keySpline.ControlPoint1 = (new Point(0.528, 0.0));
			keySpline.ControlPoint2 = (new Point(0.142, 0.847));
			arg_B2_0.KeySpline = (keySpline);
			this.sizeAnimationWidthKeyFrame.KeyTime = (KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500.0)));
			this.sizeAnimationWidthKeyFrame.Value = (0.0);
			doubleAnimationUsingKeyFrames.KeyFrames.Add(this.sizeAnimationWidthKeyFrame);
			DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames2 = new DoubleAnimationUsingKeyFrames();
			Storyboard.SetTarget(doubleAnimationUsingKeyFrames2, this);
			Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames2, new PropertyPath("(FrameworkElement.Height)", new object[0]));
			this.sizeAnimationHeightKeyFrame = new SplineDoubleKeyFrame();
			SplineDoubleKeyFrame arg_173_0 = this.sizeAnimationHeightKeyFrame;
			KeySpline keySpline2 = new KeySpline();
			keySpline2.ControlPoint1 = (new Point(0.528, 0.0));
            keySpline2.ControlPoint2 = (new Point(0.142, 0.847));
            arg_173_0.KeySpline = (keySpline2);
			this.sizeAnimationHeightKeyFrame.KeyTime=(KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500.0)));
			this.sizeAnimationHeightKeyFrame.Value=(0.0);
			doubleAnimationUsingKeyFrames2.KeyFrames.Add(this.sizeAnimationHeightKeyFrame);
			this.sizeAnimation.Children.Add(doubleAnimationUsingKeyFrames);
			this.sizeAnimation.Children.Add(doubleAnimationUsingKeyFrames2);
			this.sizeAnimation.Completed+=new EventHandler(this.SizeAnimation_Completed);
			this.positionAnimation = new Storyboard();
			DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames3 = new DoubleAnimationUsingKeyFrames();
			Storyboard.SetTarget(doubleAnimationUsingKeyFrames3, this);
			Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames3, new PropertyPath("(Canvas.Left)", new object[0]));
			this.positionAnimationXKeyFrame = new SplineDoubleKeyFrame();
			SplineDoubleKeyFrame arg_278_0 = this.positionAnimationXKeyFrame;
			KeySpline keySpline3 = new KeySpline();
			keySpline3.ControlPoint1=(new Point(0.528, 0.0));
			keySpline3.ControlPoint2=(new Point(0.142, 0.847));
			arg_278_0.KeySpline=(keySpline3);
			this.positionAnimationXKeyFrame.KeyTime=(KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500.0)));
			this.positionAnimationXKeyFrame.Value=(0.0);
			doubleAnimationUsingKeyFrames3.KeyFrames.Add(this.positionAnimationXKeyFrame);
			DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames4 = new DoubleAnimationUsingKeyFrames();
			Storyboard.SetTarget(doubleAnimationUsingKeyFrames4, this);
			Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames4, new PropertyPath("(Canvas.Top)", new object[0]));
			this.positionAnimationYKeyFrame = new SplineDoubleKeyFrame();
			SplineDoubleKeyFrame arg_339_0 = this.positionAnimationYKeyFrame;
			KeySpline keySpline4 = new KeySpline();
			keySpline4.ControlPoint1=(new Point(0.528, 0.0));
			keySpline4.ControlPoint2=(new Point(0.142, 0.847));
			arg_339_0.KeySpline=(keySpline4);
			this.positionAnimationYKeyFrame.KeyTime=(KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500.0)));
			this.positionAnimationYKeyFrame.Value=(0.0);
			doubleAnimationUsingKeyFrames4.KeyFrames.Add(this.positionAnimationYKeyFrame);
			this.positionAnimation.Children.Add(doubleAnimationUsingKeyFrames3);
			this.positionAnimation.Children.Add(doubleAnimationUsingKeyFrames4);
			this.positionAnimation.Completed+=(new EventHandler(this.PositionAnimation_Completed));
			this.heightOnlyAnimation = new Storyboard();
			this.heightOnlyAnimation.BeginTime=(new TimeSpan?(TimeSpan.FromMilliseconds(50.0)));
			DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames5 = new DoubleAnimationUsingKeyFrames();
			Storyboard.SetTarget(doubleAnimationUsingKeyFrames5, this);
			Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames5, new PropertyPath("(FrameworkElement.Height)", new object[0]));
			this.heightAnimationKeyFrame = new SplineDoubleKeyFrame();
			SplineDoubleKeyFrame arg_45F_0 = this.heightAnimationKeyFrame;
			KeySpline keySpline5 = new KeySpline();
			keySpline5.ControlPoint1=(new Point(0.528, 0.0));
			keySpline5.ControlPoint2=(new Point(0.142, 0.847));
			arg_45F_0.KeySpline=(keySpline5);
			this.heightAnimationKeyFrame.KeyTime=(KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(550.0)));
			this.heightAnimationKeyFrame.Value=(0.0);
			doubleAnimationUsingKeyFrames5.KeyFrames.Add(this.heightAnimationKeyFrame);
			this.heightOnlyAnimation.Children.Add(doubleAnimationUsingKeyFrames5);
			this.heightOnlyAnimation.Completed+=(new EventHandler(this.heightOnlyAnimation_Completed));
		}
		public void AnimateHeight(double height)
		{
			if (this.heightAnimating)
			{
				this.heightOnlyAnimation.Pause();
			}
			if (VisualTreeHelper.GetParent(this) != null)
			{
				base.Height=(base.ActualHeight);
				this.heightAnimating = true;
				this.heightAnimationKeyFrame.Value=(height);
				this.heightOnlyAnimation.Begin();
			}
		}
		public void AnimateSize(double width, double height)
		{
			if (this.sizeAnimating)
			{
				this.sizeAnimation.Pause();
			}
			if (VisualTreeHelper.GetParent(this) != null)
			{
				base.Width=(base.ActualWidth);
				base.Height=(base.ActualHeight);
				this.sizeAnimating = true;
				this.sizeAnimationWidthKeyFrame.Value=(width);
				this.sizeAnimationHeightKeyFrame.Value=(height);
				this.sizeAnimation.Begin();
			}
		}
		public void AnimatePosition(double x, double y)
		{
			if (this.positionAnimating)
			{
				this.positionAnimation.Pause();
			}
			if (VisualTreeHelper.GetParent(this) != null)
			{
				this.positionAnimating = true;
				this.positionAnimationXKeyFrame.Value=(x);
				this.positionAnimationYKeyFrame.Value=(y);
				this.positionAnimation.Begin();
			}
		}
		private void PositionAnimation_Completed(object sender, EventArgs e)
		{
			Canvas.SetLeft(this, this.positionAnimationXKeyFrame.Value);
			Canvas.SetTop(this, this.positionAnimationYKeyFrame.Value);
		}
		private void SizeAnimation_Completed(object sender, EventArgs e)
		{
			if (this.sizeAnimationWidthKeyFrame.Value < 0.0 || this.sizeAnimationHeightKeyFrame.Value < 0.0)
			{
				return;
			}
			base.Width=(this.sizeAnimationWidthKeyFrame.Value);
			base.Height=(this.sizeAnimationHeightKeyFrame.Value);
		}
		private void heightOnlyAnimation_Completed(object sender, EventArgs e)
		{
			if (this.heightAnimationKeyFrame.Value < 0.0)
			{
				return;
			}
			base.Height=(this.heightAnimationKeyFrame.Value);
		}
    }
}
