using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;


namespace KinectPhotobooth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //declare the sensor
        KinectSensor sensor;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);

        }

        protected void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if(KinectSensor.KinectSensors.Count > 0)
            {
                this.sensor = KinectSensor.KinectSensors[0];
                this.StartSensor();
                this.sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                this.sensor.DepthStream.Enable();
                this.sensor.SkeletonStream.Enable();
                this.sensor.ColorFrameReady += sensor_ColorFrameReady;
                this.sensor.SkeletonFrameReady += sensor_SkeletonFrameReady;
            }
            else
            {
                MessageBox.Show("ya fucked up");
                this.Close();
            }
        }

        private void StartSensor()
        {
            if(this.sensor != null && !this.sensor.IsRunning)
            {
                this.sensor.Start();
            }
        }
        void sensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            // Get the current image frame from sensor.
            using (ColorImageFrame imageFrame = e.OpenColorImageFrame())
            {
                // Check if there is any frame drop
                if (imageFrame == null)
                {
                    return;
                }
                else
                {
                    // get the frame pixel data length
                    byte[] pixelData = new byte[imageFrame.PixelDataLength];

                    //copy the pixel data
                    imageFrame.CopyPixelDataTo(pixelData);

                    //calculate the stride
                    int stride = imageFrame.Width * imageFrame.BytesPerPixel;

                    //assign the bitmap image source into image control
                    cameraControl.Source = BitmapSource.Create(
                        imageFrame.Width,
                        imageFrame.Height,
                        96,
                        96,
                        PixelFormats.Bgr32,
                        null,
                        pixelData,
                        stride);
                
                    
                }
            }
        }

        void sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {

                Skeleton[] totalSkeleton = new Skeleton[6];

                if(skeletonFrame == null)
                {
                    return;
                }
                skeletonFrame.CopySkeletonDataTo(totalSkeleton);
                Skeleton firstSkeleton = (from trackskeleton in totalSkeleton where trackskeleton.TrackingState == SkeletonTrackingState.Tracked select trackskeleton).FirstOrDefault();
                if(firstSkeleton == null)
                {
                    return;
                }
                if(firstSkeleton.Joints[JointType.HandRight].TrackingState == JointTrackingState.Tracked)
                {
                    this.MapJointsWithUIElement(firstSkeleton);
                }
            }
        }

        private void MapJointsWithUIElement(Skeleton skeleton)
        {
            Point mappedPoint = this.ScalePosition(skeleton.Joints[JointType.HandRight].Position);
            Canvas.SetLeft(righthand, mappedPoint.X-20);
            Canvas.SetTop(righthand, mappedPoint.Y-80);
        }

        private Point ScalePosition(SkeletonPoint skeletonPoint)
        {
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skeletonPoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }

        private void SetSensorAngle(int angleValue)
        {
            if(angleValue > sensor.MinElevationAngle || angleValue < sensor.MaxElevationAngle)
            {
                this.sensor.ElevationAngle = angleValue;
            }
        }

        private void buttonUp_Click(object sensor, RoutedEventArgs e)
        {
            SetSensorAngle(this.sensor.MaxElevationAngle - 1);
        }

        private void buttonDown_Click(object sensor, RoutedEventArgs e)
        {
            SetSensorAngle(this.sensor.MinElevationAngle + 1);
        }


    }
}
