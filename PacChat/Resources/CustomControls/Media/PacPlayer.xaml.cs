using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PacChat.Resources.CustomControls.Media
{
    /// <summary>
    /// Interaction logic for PacPlayer.xaml
    /// </summary>
    public partial class PacPlayer : UserControl
    {
        private static List<String> SupportedExtensions = new List<string>()
        {
            ".mp4", ".wmv"
        };

        private bool canReplay = false;

        public PacPlayer()
        {
            InitializeComponent();
            VideoFull.LoadedBehavior = MediaState.Manual;

            VideoFull.ScrubbingEnabled = true;
        }

        #region Open Play Pause Loop
        private void PlayBtnClick(object sender, RoutedEventArgs e)
        {
            if (VideoFull.Clock == null) return;

            if (VideoFull.Clock.IsPaused)
            {
                Play();
            } else
            {
                Pause();
            }
            if (canReplay)
            {
                Replay();
            }
        }

        private void Clock_Completed(object sender, EventArgs e)
        {
            canReplay = true;
            PlayPauseIcon.Kind = PackIconKind.ArrowRotateRight;
            if (Loop) 
                Replay();
            else
                dockPanel.Opacity = 1;
        }

        private void OnMediaOpened(object sender, RoutedEventArgs e)
        {
            canReplay = false;

            if (VideoFull.Clock.NaturalDuration.HasTimeSpan)
            {
                TimeSpan ts = VideoFull.Clock.NaturalDuration.TimeSpan;
                SeekBar.Maximum = ts.TotalSeconds;
                SeekBar.SmallChange = 3;
                SeekBar.LargeChange = Math.Min(10, ts.Seconds / 10);
            }
            dockPanel.Opacity = 1;
            PlayPauseIcon.Kind = PackIconKind.Pause;
        }

        private void Play1Click(object sender, RoutedEventArgs e)
        {
            Loop = !Loop;
            if (Loop)
            {
                Play1Icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF"));
            } else
            {
                Play1Icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC5C5C5"));
            }
        }
        #endregion

        #region SeekBar

        String timePattern = "{0}:{1:D2}";
        TimeSpan timeSpan;
        bool isJumping = false;
        bool isMediaChange = false;
        bool isDragging = false;

        private void Clock_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            if (VideoFull.Clock == null) return;
            if (isJumping || isDragging) return;

            isMediaChange = true;
            SeekBar.Value = VideoFull.Clock.CurrentTime.Value.TotalSeconds;
            lbTime.Content = String.Format(timePattern, VideoFull.Clock.CurrentTime.Value.Minutes, VideoFull.Clock.CurrentTime.Value.Seconds);
            isMediaChange = false;
        }

        private void OnSeeking(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (VideoFull.Clock == null) return;
            timeSpan = TimeSpan.FromSeconds(SeekBar.Value);
            lbTime.Content = String.Format(timePattern, timeSpan.Minutes, timeSpan.Seconds);

            if (isMediaChange || isDragging) return;

            isJumping = true;
            canReplay = false;
            VideoFull.Clock.Controller.Seek(timeSpan, TimeSeekOrigin.BeginTime);
            isJumping = false;
        }

        private void SeekBar_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            if (isJumping || isMediaChange) return;
            isDragging = true;
        }

        private void SeekBar_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (VideoFull.Clock == null) return;
            canReplay = false;
            VideoFull.Clock.Controller.Seek(TimeSpan.FromSeconds(SeekBar.Value), TimeSeekOrigin.BeginTime);
            isDragging = false;
        }
        #endregion

        public void Close()
        {            
            if (VideoFull.Clock == null) return;

            VideoFull.Clock.Controller.Remove();
            VideoFull.Clock = null;
        }

        public void Play()
        {
            if (VideoFull.Clock == null) return;
            VideoFull.Clock.Controller.Resume();
            canReplay = false;
            PlayPauseIcon.Kind = PackIconKind.Pause;
        }

        public void Pause()
        {
            if (VideoFull.Clock == null) return;
            VideoFull.Clock.Controller.Pause();
            PlayPauseIcon.Kind = PackIconKind.Play;
        }

        public void Stop()
        {
            if (VideoFull.Clock == null) return;
            VideoFull.Clock.Controller.Stop();
            lbTime.Content = String.Format(timePattern, 0, 0);
            SeekBar.Value = 0;
            PlayPauseIcon.Kind = PackIconKind.Play;
        }

        public void Replay()
        {
            if (VideoFull.Clock == null) return;
            VideoFull.Clock.Controller.Seek(TimeSpan.FromSeconds(0), TimeSeekOrigin.BeginTime);
            Play();
        }

        #region Property: Source
        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(Uri), typeof(PacPlayer), new PropertyMetadata(default(Uri)));
        #endregion

        #region Property: Loop

        public bool Loop
        {
            get { return (bool)GetValue(LoopProperty); }
            set { SetValue(LoopProperty, value); }
        }

        public static readonly DependencyProperty LoopProperty =
            DependencyProperty.Register("Loop", typeof(bool), typeof(PacPlayer), new PropertyMetadata(false));

        #endregion

        #region Property: Volume

        public int Volume
        {
            get { return (int)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Volume.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VolumeProperty =
            DependencyProperty.Register("Volume", typeof(int), typeof(PacPlayer), new PropertyMetadata(50));

        #endregion

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == SourceProperty && !Source.Equals(default(Uri)))
            {
                if (VideoFull.Clock != null)
                {
                    VideoFull.Clock.Controller.Remove();
                }

                var tl = new MediaTimeline(Source);
                var clock = tl.CreateClock(true) as MediaClock;
                clock.CurrentTimeInvalidated += Clock_CurrentTimeInvalidated;
                clock.Completed += Clock_Completed;
                VideoFull.Clock = clock;
            }

            if (e.Property == VolumeProperty)
            {
                VideoFull.Volume = (double)Volume / 100;
                if (Volume >= 70)
                    VolumeIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeHigh;

                if (Volume < 70 && Volume > 20)
                    VolumeIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeMedium;

                if (Volume <= 20 && Volume != 0)
                    VolumeIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeLow;

                if (Volume == 0)
                {
                    VolumeIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.VolumeMute;
                }
            }
            base.OnPropertyChanged(e);
        }

        private void ShowControl(object sender, MouseEventArgs e)
        {
            dockPanel.Opacity = 1;
        }

        private void HideControl(object sender, MouseEventArgs e)
        {
            if (!canReplay)
                dockPanel.Opacity = 0.15;
        }

        public static bool IsSupport(string file)
        {
            return SupportedExtensions.Contains(System.IO.Path.GetExtension(file).ToLower());
        }
    }
}
