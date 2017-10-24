using Dicom;
using Dicom.Imaging;
using Dicom.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xf_fo_dicom_sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("xf_fo_dicom_sample.jpeg-baseline.dcm");
            //Stream stream = assembly.GetManifestResourceStream("xf_fo_dicom_sample.CR_JPG_IR6a.dcm");

            var imageFile = DicomFile.Open(stream);
            var dicomImage = new DicomImage(imageFile.Dataset);

            byte[] byteArray = dicomImage.PixelData.GetFrame(0).Data;
            MemoryStream ms = new MemoryStream(byteArray);
            ImageSource imgSource = ImageSource.FromStream(() => ms);
            renderimage.Source = imgSource;

            var dump = imageFile.WriteToString();
            dicominfo.Text = dump;
        }
    }
}
